

using FileZilla.Models;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public interface IExtraFeeRepository : IGenericRepository<ExtraFee>
    {
        Task<double> GetExtraDeliveryFeeByMinuteHour(int minute, int hour);
    }
}
