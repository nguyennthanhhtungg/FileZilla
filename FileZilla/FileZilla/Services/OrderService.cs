
using CsvHelper;
using FileZilla.Models;
using FileZilla.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZilla.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;


        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }


        public async Task<string> ExportOrderSheetToCsvFileData()
        {
            List<Order> orderList = await _orderRepository.GetOrderListByOrderByEmployeeAndOrderByUpdatedDate();


            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(memoryStream))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteField("STT");
                csvWriter.WriteField("OrderId");
                csvWriter.WriteField("OrderNo");
                csvWriter.WriteField("EmployeeId");
                csvWriter.WriteField("CustomerId");
                csvWriter.WriteField("UpdatedDate");
                csvWriter.WriteField("TotalProduct");
                csvWriter.WriteField("TotalProductMoney");
                csvWriter.WriteField("Status");
                csvWriter.NextRecord();

                for (int i = 0; i < orderList.Count; i++)
                {
                    csvWriter.WriteField(i + 1);
                    csvWriter.WriteField(orderList[i].OrderId);
                    csvWriter.WriteField(orderList[i].OrderNo);
                    csvWriter.WriteField(orderList[i].EmployeeId);
                    csvWriter.WriteField(orderList[i].CustomerId);
                    csvWriter.WriteField(orderList[i].UpdatedDate);
                    csvWriter.WriteField(orderList[i].TotalProduct);
                    csvWriter.WriteField(orderList[i].TotalProductMoney);
                    csvWriter.WriteField(orderList[i].Status);
                    csvWriter.NextRecord();
                }


                streamWriter.Flush();
                string result = Encoding.UTF8.GetString(memoryStream.ToArray());

                return result;
            }
        }
    }
}
