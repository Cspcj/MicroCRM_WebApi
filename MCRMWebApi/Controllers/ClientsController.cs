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
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientsService clientsService, ILogger<ClientsController> logger)
        {
            _clientsService = clientsService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                _logger.LogInformation("GetAllClients method begining....");
                var clients = await _clientsService.GetAllClients();
                if (clients == null || !clients.Any())
                {
                    _logger.LogInformation("No clients found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning the list of clients");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllClients method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            try
            {
                _logger.LogInformation("GetClient method begining....");
                var client = await _clientsService.GetClient(id);
                if (client == null)
                {
                    _logger.LogInformation("No client found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning the client");
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetClient method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientPartialCreateDTO client)
        {
            try
            {
                _logger.LogInformation("AddClient method begining....");
                if (client == null)
                {
                    _logger.LogInformation("Client is null");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Client is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Client is not valid");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Client is not valid");
                }
                var clientToCreate = await _clientsService.AddClient(client);
                if (clientToCreate == null)
                {
                    _logger.LogInformation("Client is not created");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Client is not created");
                }
                _logger.LogInformation("Client is created");
                return StatusCode((int)HttpStatusCode.Created, clientToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddClient method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] ClientPartialUpdateDTO client)
        {
            try
            {
                _logger.LogInformation("UpdateClient method begining....");
                if (client == null)
                {
                    _logger.LogInformation("Client is null");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Client is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Client is not valid");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Client is not valid");
                }
                var clientToUpdate = await _clientsService.UpdatePartialClient(id, client);
                if (clientToUpdate == null)
                {
                    _logger.LogInformation("Client is not updated");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Client is not updated");
                }
                _logger.LogInformation("Client is updated");
                return StatusCode((int)HttpStatusCode.Created, clientToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateClient method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                _logger.LogInformation("DeleteClient method begining....");
                var clientToDelete = await _clientsService.DeleteClient(id);
                if (clientToDelete == null)
                {
                    _logger.LogInformation("Client is not deleted");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Client is not deleted");
                }
                _logger.LogInformation("Client is deleted");
                return StatusCode((int)HttpStatusCode.Created, clientToDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteClient method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

    }
}
