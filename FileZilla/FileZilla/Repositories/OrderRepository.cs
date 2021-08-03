

using FileZilla.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetOrderListByOrderByEmployeeAndOrderByUpdatedDate()
        {
            List<Order> orders = await _context.Set<Order>()
                .OrderBy(o => o.EmployeeId)
                .ThenBy(o => o.UpdatedDate)
                .ToListAsync();

            return orders;
        }


        public async Task<List<Order>> GetOrderListByEmployeeIdAndMonthYear(int employeeId, int month, int year)
        {
            return await _context.Set<Order>()
                .Where(o => o.EmployeeId == employeeId && o.UpdatedDate.Month == month && o.UpdatedDate.Year == year)
                .ToListAsync();
        }
    }
}
