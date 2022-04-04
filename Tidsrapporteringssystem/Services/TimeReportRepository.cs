using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidsrapporteringssystem.Model;

namespace Tidsrapporteringssystem.Services
{
    public class TimeReportRepository : ITimeReportRepository<TimeReport>
    {
        private AppDbContext _appContext;

        public TimeReportRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }
        public Task<TimeReport> Add(TimeReport newEntity)
        {
            throw new NotImplementedException();
        }

        public Task<TimeReport> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TimeReport>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<TimeReport> GetSingle(int id)
        {
            return await _appContext.TimReports.FirstOrDefaultAsync(t => t.TimeReportId == id);
        }

        public Task<TimeReport> Update(TimeReport Entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TimeReport> WorkedHours(int id, int week)
        {

          
            var result = _appContext.TimReports.Where(i => i.EmployeeId == id);
            if (result != null)
            {
                return await result.FirstOrDefaultAsync(w => w.Week == week);
            }
            return null;
            



        }
    }
}
