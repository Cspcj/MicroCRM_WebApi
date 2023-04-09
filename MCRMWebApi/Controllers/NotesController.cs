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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly IClientsService _clientsService;
        private readonly IEmployeesService _employeesService;
        private readonly ILogger<NotesController> _logger;


        public NotesController(INotesService NotesService,
            IClientsService clientsService,
            IEmployeesService employeesService,
            INotesService notesService,
            ILogger<NotesController> logger)
        {
            _logger = logger;
            _notesService = NotesService;
            _clientsService = clientsService;
            _employeesService = employeesService;
            _notesService = notesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                _logger.LogInformation("GetAllNotes method begining....");
                var notes = await _notesService.GetAllNotes();
                if (notes == null || !notes.Any())
                {
                    _logger.LogInformation("No Notes found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning the list of Notes");
                return Ok(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllNotes method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            try
            {
                _logger.LogInformation("GetNote method begining....");
                var note = await _notesService.GetNoteByID(id);
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
                _logger.LogError(ex, "Error in GetNoteById method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(NotePartialCreateDTO note)
        {
            try
            {
                _logger.LogInformation("AddNote method begining....");
                var userAdded = await _notesService.AddNoteAsync(note);
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
                _logger.LogError(ex, "Error in AddNote method");
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
                NoteDTO noteDeleted = await _notesService.DeleteNoteAsync (id);
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
        public async Task<IActionResult> UpdatePartialUser(Guid id, NotePartialUpdateDTO user)
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