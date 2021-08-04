
using FileZilla.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetExpiredProductList()
        {
            return await _context.Set<Product>()
                .Where(p => p.ExpiryDate <= DateTime.Now.AddMonths(1))
                .ToListAsync();
        }
    }
}
