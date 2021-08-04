
using FileZilla.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileZilla.Services
{
    public interface IProductService
    {
        Task<string> ExportExpiredProductListToCsvFileData();
    }
}
