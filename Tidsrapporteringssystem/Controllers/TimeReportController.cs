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
        public async Task<IActionResult> GetAllTimeReports()
        {
            try
            {
                return Ok(await _timeReportRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error unable to get data from database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TimeReport>> GetSingelTimeReport(int id)
        {
            try
            {
                var result = await _timeReportRepository.GetSingle(id);
                if (result == null)
                {
                    return NotFound($"Time report with {id} was not found");
                }
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error unable to get data from database.....");
            }
        }
        [HttpPost]
        public async Task<ActionResult<TimeReport>> Addproject(TimeReport timeReport)
        {
            try
            {
                if (timeReport != null)
                {
                    var AddTimeReport = await _timeReportRepository.Add(timeReport);
                    return CreatedAtAction(nameof(GetSingelTimeReport), new { id = AddTimeReport.TimeReportId }, AddTimeReport);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to add project to database.....");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TimeReport>> DeleteTimeReport(int id)
        {
            try
            {
                var result = await _timeReportRepository.GetSingle(id);
                if (result == null)
                {
                    return NotFound();
                }
                return await _timeReportRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to delete from database......");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TimeReport>> UpdateOrder(int id, TimeReport timeReport)
        {
            try
            {
                if (id != timeReport.TimeReportId)
                {
                    return BadRequest($"Time report with id: {id}, do not exists in the database");
                }
                var TimeReportToUpdate = await _timeReportRepository.GetSingle(id);
                if (TimeReportToUpdate == null)
                {
                    return NotFound($"A order with {id} was not found");
                }
                return await _timeReportRepository.Update(timeReport);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to update employee in database......");
            }
        }
    }
}
