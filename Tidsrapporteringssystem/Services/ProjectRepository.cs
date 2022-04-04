using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidsrapporteringssystem.Model;

namespace Tidsrapporteringssystem.Services
{
    public class ProjectRepository : ITimeReportRepository<Project>
    {
        private AppDbContext _appDbContext;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<Project> Add(Project newEntity)
        {
            throw new NotImplementedException();
        }

        public Task<Project> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Project> GetSingle(int id)
        {
            return await _appDbContext.Projects.Include(e => e.Employees).FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public Task<Project> Update(Project Entity)
        {
            throw new NotImplementedException();
        }

        public Task<Project> WorkedHours(int id, int week)
        {
            throw new NotImplementedException();
        }
    }
}
