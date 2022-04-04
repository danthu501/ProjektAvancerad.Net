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

        public Task<Employee> Add(Employee newEntity)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetSingle(int id)
        {

            var result = await (from Employee in _appDbContext.Employees
                                join TimeReport in _appDbContext.TimReports on Employee.EmployeeId equals TimeReport.EmployeeId
                                select new { Employee, TimeReport }).FirstOrDefaultAsync(e => e.Employee.EmployeeId == id);
            if (result != null)
            {
                return result.Employee;
            }
            return null;
                
            //return await _appDbContext.Employees.Include(t => t.TimeReports).FirstOrDefaultAsync(e => e.EmployeeId == id);

        }

        public Task<Employee> Update(Employee Entity)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> WorkedHours(int id, int week)
        {
            throw new NotImplementedException();
        }
    }
}
