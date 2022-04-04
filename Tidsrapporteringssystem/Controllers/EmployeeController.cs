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
    public class EmployeeController : ControllerBase
    {
        private ITimeReportRepository<Employee> _apptimeReport;

        public EmployeeController(ITimeReportRepository<Employee> apptimeReport)
        {
            _apptimeReport = apptimeReport;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployess()
        {
            try
            {
                return Ok(await _apptimeReport.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error unable to get data from database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetSingelEmployee(int id)
        {
            try
            {
                var result = await _apptimeReport.GetSingle(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error unable to get data from database.....");
            }
        }

    }
}
