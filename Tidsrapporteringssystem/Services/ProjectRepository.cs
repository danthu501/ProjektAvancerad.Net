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
        public async Task<Project> Add(Project newEntity)
        {
            var result = await _appDbContext.Projects.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Project> Delete(int id)
        {
            var result = await _appDbContext.Projects.FirstOrDefaultAsync(o => o.ProjectId == id);
            if (result != null)
            {
                _appDbContext.Projects.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {

            return await _appDbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetSingle(int id)
        {
            return await _appDbContext.Projects.Include(e => e.Employees).FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<Project> Update(Project Entity)
        {
            var result = await _appDbContext.Projects.FirstOrDefaultAsync(p => p.ProjectId == Entity.ProjectId);
            if (result != null)
            {
                result.ProjectName = Entity.ProjectName;
                result.Discription = Entity.Discription;

                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public Task<Project> WorkedHours(int id, int week)
        {
            throw new NotImplementedException();
        }
    }
}
