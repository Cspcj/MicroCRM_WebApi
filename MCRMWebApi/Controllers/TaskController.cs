using MCRMWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MCRMWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _notesService;
        private readonly IClientsService _clientsService;
        private readonly IEmployeesService _employeesService;
        private readonly ILogger<TasksController> _logger;


        public TasksController(ITasksService TasksService,
            IClientsService clientsService,
            IEmployeesService employeesService,
            ITasksService notesService,
            ILogger<TasksController> logger)
        {
            _logger = logger;
            _notesService = TasksService;
            _clientsService = clientsService;
            _employeesService = employeesService;
            _notesService = notesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                _logger.LogInformation("GetAllTasks method begining....");
                var notes = await _notesService.GetAllTasks();
                if (notes == null || !notes.Any())
                {
                    _logger.LogInformation("No Tasks found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning the list of Tasks");
                return Ok(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllTasks method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            try
            {
                _logger.LogInformation("GetTask method begining....");
                var note = await _notesService.GetTaskByID(id);
                if (note == null)
                {
                    _logger.LogInformation("No used found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning note");
                return Ok(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTaskById method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskPartialCreateDTO note)
        {
            try
            {
                _logger.LogInformation("AddTask method begining....");
                var userAdded = await _notesService.AddTaskAsync(note);
                if (userAdded == null)
                {
                    _logger.LogInformation("No note added");
                    return BadRequest();
                }
                _logger.LogInformation("Returning note added");
                return Ok(userAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddTask method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletenote(Guid id)
        {
            // delete note by id
            try
            {
                _logger.LogInformation("Deletenote method begining....");
                TaskDTO noteDeleted = await _notesService.DeleteTaskAsync(id);
                if (noteDeleted == null)
                {
                    _logger.LogInformation("No note deleted");
                    return BadRequest();
                }
                _logger.LogInformation("Returning note deleted");
                return Ok(noteDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Deletenote method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialUser(Guid id, TaskPartialUpdateDTO user)
        {
            try
            {
                _logger.LogInformation("UpdatePartialUser method begining....");
                var userUpdated = await _notesService.UpdatePartial(id, user);
                if (userUpdated == null)
                {
                    _logger.LogInformation("No user updated");
                    return BadRequest();
                }
                _logger.LogInformation("Returning user updated");
                return Ok(userUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdatePartialUser method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

    }
}