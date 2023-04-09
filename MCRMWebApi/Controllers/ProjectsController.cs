using Microsoft.AspNetCore.Mvc;
using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.Services;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MCRMWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(IProjectsService projectsService, ILogger<ProjectsController> logger)
        {
            _logger = logger;
            _projectsService = projectsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProjects() 
        {

            try
            {
                _logger.LogInformation("GetAllProjects method begining....");
                var projects = await _projectsService.GetAllProjects();
                if (projects == null || !projects.Any())
                {
                    _logger.LogInformation("No project found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No project found in the table");
                }
                _logger.LogInformation("Returning the list of projects");
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllProjects method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetProject(int id)
        {
            try
            {
                _logger.LogInformation("GetProject method begining....");
                var project = await _projectsService.GetProject(id);
                if (project == null)
                {
                    _logger.LogInformation("No project found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No project found in the table");
                }
                _logger.LogInformation("Returning the project");
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProject method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        //[Route("api/byClient/[controller]")]
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetProjectByClientId(Guid id)
        //{
        //    try
        //    {
        //        _logger.LogInformation("GetProjectByClient method begining....");
        //        var project = await _projectsService.GetProjectsByClient(id);
        //        if (project == null)
        //        {
        //            _logger.LogInformation("No project found in the table");
        //            return StatusCode((int)HttpStatusCode.NoContent, "No project found in the table");
        //        }
        //        _logger.LogInformation("Returning the project");
        //        return Ok(project);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in GetProjectByClientId method");
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //    return Ok();
        //}

        //[Route("api/byEmployee/[controller]")]
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetProjectByEmployeeId(Guid id)
        //{
        //    try
        //    {
        //        _logger.LogInformation("GetProjectByEmployeeId method begining....");
        //        var project = await _projectsService.GetProjectsByUser(id);
        //        if (project == null)
        //        {
        //            _logger.LogInformation("No project found in the table");
        //            return StatusCode((int)HttpStatusCode.NoContent, "No project found in the table");
        //        }
        //        _logger.LogInformation("Returning the project");
        //        return Ok(project);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in GetProjectByEmployeeId method");
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //    return Ok();
        //}

        //[Route("api/archived/[controller]")]
        //[HttpGet("{status}")]
        //public async Task<ActionResult> GetArchivedProjects(bool status)
        //{
        //    try
        //    {
        //        _logger.LogInformation("GetArchivedProjects method begining....");
        //        var project = await _projectsService.GetArchivedProjects(status);
        //        if (project == null)
        //        {
        //            _logger.LogInformation("No project found in the table");
        //            return StatusCode((int)HttpStatusCode.NoContent, "No project found in the table");
        //        }
        //        _logger.LogInformation("Returning the project");
        //        return Ok(project);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in GetArchivedProjects method");
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //    return Ok();
        //}

        [HttpPost]
        public async Task<ActionResult> AddProject(ProjectPartialCreateDTO project)
        {
            try
            {
                _logger.LogInformation("AddProject method begining....");
                var projectToAdd = await _projectsService.AddProject(project);
                if (projectToAdd == null)
                {
                    _logger.LogInformation("Project object sent from client is null.");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Project object sent from client is null.");
                }
                _logger.LogInformation("Project added successfully");
                return Ok(projectToAdd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddProject method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateProject(ProjectPartialUpdateDTO project)
        {
            try
            {
                _logger.LogInformation("UpdateProject method begining....");
                var projectToUpdate = await _projectsService.UpdateProject(project);
                if (projectToUpdate == null)
                {
                    _logger.LogInformation("Project object sent from client is null.");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Project object sent from client is null.");
                }
                _logger.LogInformation("Project updated successfully");
                return Ok(projectToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProject method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> ToggleIsArchivedState([FromRoute]int id, [FromBody] bool status)
        {
            try
            {
                _logger.LogInformation("Toggle IsArchived method begining....");
                var projectToUpdate = await _projectsService.ToggleAchiveFlag(id);
                if (projectToUpdate == null)
                {
                    _logger.LogInformation("Project object sent from client is null.");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Project object sent from client is null.");
                }
                _logger.LogInformation("Project updated successfully");
                return Ok(projectToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProjectStatus method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                _logger.LogInformation("DeleteProject method begining....");
                var projectToDelete = await _projectsService.DeleteProject(id);
                if (projectToDelete == null)
                {
                    _logger.LogInformation("Project object sent from client is null.");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Project object sent from client is null.");
                }
                _logger.LogInformation("Project deleted successfully");
                return Ok(projectToDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteProject method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

    }
}
