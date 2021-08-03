
using FileZilla.Models;
using FileZilla.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZilla.Repositories
{
    public class ExtraFeeRepository : GenericRepository<ExtraFee>, IExtraFeeRepository
    {
        public ExtraFeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<double> GetExtraDeliveryFeeByMinuteHour(int minute, int hour)
        {
            ExtraFee extraFee = await _context.Set<ExtraFee>()
                .Where(ef => ((ef.FromHour == hour && ef.FromMinute <= minute) || (ef.FromHour < hour))
                && ((ef.ToHour == hour && ef.ToMinute > minute) || (ef.ToHour > hour)))
                .FirstOrDefaultAsync();

            if (extraFee == null)
            {
                return 0.0;
            }
            else
            {
                return extraFee.ExtraDeliveryFee;
            }
        }
    }
}
