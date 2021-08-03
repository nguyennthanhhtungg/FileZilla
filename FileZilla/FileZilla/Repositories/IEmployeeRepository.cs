
using FileZilla.Models;
using FileZilla.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<int>> GetEmployeeIdList();
    }
}
