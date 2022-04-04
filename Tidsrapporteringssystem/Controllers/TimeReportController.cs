using ClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidsrapporteringssystem.Services;

namespace Tidsrapporteringssystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeReportController : ControllerBase
    {
        private ITimeReportRepository<TimeReport> _timeReportRepository;

        public TimeReportController(ITimeReportRepository<TimeReport> timeReportRepository)
        {
            _timeReportRepository = timeReportRepository;
        }
        [HttpGet("{id:int}/{week:int}")]

        public async Task<ActionResult<TimeReport>> HoursWorkedWeek(int id, int week)
        {
            var result = _timeReportRepository.WorkedHours(id, week);
            
            return Ok(await result);
        }
    }
}
