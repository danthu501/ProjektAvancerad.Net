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
    public class ProjectController : ControllerBase
    {
        private ITimeReportRepository<Project> _timeReportRepository;

        public ProjectController(ITimeReportRepository<Project> timeReportRepository)
        {
            _timeReportRepository = timeReportRepository;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Project>> GetAllEmployees(int id)
        {
            try
            {
                var result = await _timeReportRepository.GetSingle(id);
                if (result == null)
                {
                    return NotFound(); 
                }
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to retrive data from server");
            }

        }
    }
}
