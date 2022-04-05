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
        public async Task<TimeReport> Add(TimeReport newEntity)
        {
            var result = await _appContext.TimReports.AddAsync(newEntity);
            await _appContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TimeReport> Delete(int id)
        {
            var result = await _appContext.TimReports.FirstOrDefaultAsync(t => t.TimeReportId == id);
            if (result != null)
            {
                _appContext.TimReports.Remove(result);
                await _appContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<TimeReport>> GetAll()
        {
            return await _appContext.TimReports.ToListAsync();
        }

        public async Task<TimeReport> GetSingle(int id)
        {
            return await _appContext.TimReports.FirstOrDefaultAsync(t => t.TimeReportId == id);
        }

        public async Task<TimeReport> Update(TimeReport Entity)
        {
            var result = await _appContext.TimReports.FirstOrDefaultAsync(t => t.TimeReportId == Entity.TimeReportId);
            if (result != null)
            {
                result.EmployeeId = Entity.EmployeeId;
                result.FillingDate = Entity.FillingDate;
                result.Week = Entity.Week;
                result.WorkedHours = Entity.WorkedHours;

                await _appContext.SaveChangesAsync();
                return result;
            }
            return null;

        }

        public async Task<TimeReport> WorkedHours(int id, int week)
        {
            //var result1 = (from TimeReport in _appContext.TimReports
            //               where TimeReport.TimeReportId == id && TimeReport.Week == week
            //               select TimeReport);
            var result = _appContext.TimReports.Where(i => i.EmployeeId == id).Where(w => w.Week == week);
            if (result != null)
            {
                return await result.FirstOrDefaultAsync();
            }
            return null;

        }
    }
}
