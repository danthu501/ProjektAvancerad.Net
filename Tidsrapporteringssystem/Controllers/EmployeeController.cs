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
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    var AddEmployee = await _apptimeReport.Add(employee);
                    return CreatedAtAction(nameof(GetSingelEmployee), new { id = AddEmployee.EmployeeId }, employee);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to add employee to database.....");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var result = await _apptimeReport.GetSingle(id);
                if (result == null)
                {
                    return NotFound();
                }
                return await _apptimeReport.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to delete from database......");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateOrder(Employee employee)
        {
            //try
            //{
            //    if (id != employee.EmployeeId)
            //    {
            //        return BadRequest($"Employee with id: {id}, do not exists in the database");
            //    }
            //    var EmployeeToUpdate = await _apptimeReport.GetSingle(id);
            //    if (EmployeeToUpdate == null)
            //    {
            //        return NotFound($"A employee with {id} was not found");
            //    }
                return await _apptimeReport.Update(employee);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to update employee in database......");
            //}
        }
    }
}
