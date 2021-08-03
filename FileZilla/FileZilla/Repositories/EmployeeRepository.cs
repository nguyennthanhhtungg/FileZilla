

using FileZilla.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<int>> GetEmployeeIdList()
        {
            return await _context.Set<Employee>()
                .Select(emp => emp.EmployeeId)
                .ToListAsync<int>();
        }
    }
}
