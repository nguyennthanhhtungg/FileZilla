
using CsvHelper;
using FileZilla.Models;
using FileZilla.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZilla.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IExtraFeeRepository _extraFeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IOrderRepository orderRepository, IExtraFeeRepository extraFeeRepository)
        {
            this._employeeRepository = employeeRepository;
            this._orderRepository = orderRepository;
            this._extraFeeRepository = extraFeeRepository;
        }

        public async Task<Employee> Add(Employee employee)
        {
            await _employeeRepository.Add(employee);
            return employee;
        }

        public async Task<List<Employee>> GetAll()
        {
            var employees = await _employeeRepository.GetAll();
            return employees.ToList();
        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            return employee;
        }

        public async Task<Employee> Update(Employee employee)
        {
            await _employeeRepository.Update(employee);
            return employee;
        }

        public async Task<string> ExportSalarySheetToCsvFileDataByMonthYear(int month, int year)
        {
            List<int> employeeIdList = await _employeeRepository.GetEmployeeIdList();


            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(memoryStream))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteField("STT");
                csvWriter.WriteField("EmployeeId");
                csvWriter.WriteField("Amount (VNĐ)");
                csvWriter.WriteField("Number of Order");
                csvWriter.NextRecord();

                double commissionPercentage = Convert.ToDouble(ConfigurationManager.AppSettings["CommissionPercentage"]);
                double standardDeliveryFee = Convert.ToDouble(ConfigurationManager.AppSettings["StandardDeliveryFee"]);

                for (int i = 0; i < employeeIdList.Count; i++)
                {
                    List<Order> orderList = await _orderRepository.GetOrderListByEmployeeIdAndMonthYear(employeeIdList[i], month, year);

                    if(orderList.Count == 0)
                    {
                        csvWriter.WriteField(i + 1);
                        csvWriter.WriteField(employeeIdList[i]);
                        csvWriter.WriteField(0);
                        csvWriter.WriteField(0);
                    }
                    else
                    {
                        double amount = 0.0;
                        
                        foreach(Order order in orderList)
                        {
                            if(order.DeliveryFee != 0)
                            {
                                amount += order.DeliveryFee * ((double)commissionPercentage / 100);
                            }
                            else
                            {
                                double extraFee = await _extraFeeRepository.GetExtraDeliveryFeeByMinuteHour(order.UpdatedDate.Minute, order.UpdatedDate.Hour);
                                amount += ((standardDeliveryFee + extraFee) * ((double)commissionPercentage / 100));
                            }
                        }

                        csvWriter.WriteField(i + 1);
                        csvWriter.WriteField(employeeIdList[i]);
                        csvWriter.WriteField(amount);
                        csvWriter.WriteField(orderList.Count);
                    }

                    csvWriter.NextRecord();
                }


                streamWriter.Flush();
                string result = Encoding.UTF8.GetString(memoryStream.ToArray());

                return result;
            }
        }
    }
}
