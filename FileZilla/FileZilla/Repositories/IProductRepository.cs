

using FileZilla.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetExpiredProductList();
    }
}
