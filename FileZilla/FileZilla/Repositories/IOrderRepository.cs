
using FileZilla.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrderListByOrderByEmployeeAndOrderByUpdatedDate();

        Task<List<Order>> GetOrderListByEmployeeIdAndMonthYear(int employeeId, int month, int year);
    }
}
