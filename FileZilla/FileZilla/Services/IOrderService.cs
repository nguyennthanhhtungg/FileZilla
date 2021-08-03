

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileZilla.Services
{
    public interface IOrderService
    {
        Task<string> ExportOrderSheetToCsvFileData();
    }
}
