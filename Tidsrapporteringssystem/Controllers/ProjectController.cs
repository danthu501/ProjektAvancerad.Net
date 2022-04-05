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

        [HttpGet]
        public async Task<ActionResult<Project>> GetAllProjects()
        {
            return Ok(await _timeReportRepository.GetAll());
        }
        [HttpPost]
        public async Task<ActionResult<Project>> Addproject(Project project)
        {
            try
            {
                if (project != null)
                {
                    var AddProject = await _timeReportRepository.Add(project);
                    return CreatedAtAction(nameof(GetAllEmployees), new { id = AddProject.ProjectId }, AddProject);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to add project to database.....");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
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
        public async Task<ActionResult<Project>> UpdateProject(int id, Project project)
        { 
            try
            {
                if (id != project.ProjectId)
                {
                    return BadRequest($"Employee with id: {id}, do not exists in the database");
                }
                var EmployeeToUpdate = await _timeReportRepository.GetSingle(id);
                if (EmployeeToUpdate == null)
                {
                    return NotFound($"A order with {id} was not found");
                }
                return await _timeReportRepository.Update(project);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, unable to update employee in database......");
            }
        }

    }
}
