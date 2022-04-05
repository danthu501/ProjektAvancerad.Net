using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidsrapporteringssystem.Model;

namespace Tidsrapporteringssystem.Services
{
    public class EmployeeRepository : ITimeReportRepository<Employee>
    {
        private AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Employee> Add(Employee newEntity)
        {
            var result = await _appDbContext.Employees.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> Delete(int id)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(o => o.EmployeeId == id);
            if (result != null)
            {
                _appDbContext.Employees.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetSingle(int id)
        {

            //var result = await (from Employee in _appDbContext.Employees
            //                    join TimeReport in _appDbContext.TimReports on Employee.EmployeeId equals TimeReport.EmployeeId
            //                    select new { Employee, TimeReport }).FirstOrDefaultAsync(e => e.Employee.EmployeeId == id);
            //if (result != null)
            //{
            //    return result.Employee;
            //}
            //return null;

            return await _appDbContext.Employees.Include(t => t.TimeReports).FirstOrDefaultAsync(e => e.EmployeeId == id);

        }

        public async Task<Employee> Update(Employee Entity)
        {
            var result = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == Entity.EmployeeId);
            if (result != null)
            {
                result.FirstName = Entity.FirstName;
                result.LastName = Entity.LastName;
                result.Phonenumber = Entity.Phonenumber;
                result.ProjectId = Entity.ProjectId;

                await _appDbContext.SaveChangesAsync();
            }
            return result;
        }

        public Task<Employee> WorkedHours(int id, int week)
        {
            throw new NotImplementedException();
        }
    }
}
