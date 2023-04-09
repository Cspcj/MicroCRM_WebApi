using MCRMWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MCRMWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly IClientsService _clientsService;
        private readonly IEmployeesService _employeesService;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService usersService,
            IClientsService clientsService,
            IEmployeesService employeesService,
            ILogger<UsersController> logger)
        {
            _logger = logger;
            _usersService = usersService;
            _clientsService = clientsService;
            _employeesService = employeesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("GetAllUsers method begining....");
                var users = await _usersService.GetAllUsers();
                if (users == null || !users.Any())
                {
                    _logger.LogInformation("No users found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning the list of users");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllUsers method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                _logger.LogInformation("GetUser method begining....");
                var user = await _usersService.GetUserAsync(id);
                if (user == null)
                {
                    _logger.LogInformation("No used found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning user");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUser method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        //[HttpPost]
        //public async Task<IActionResult> AddUser(UserPartialCreateDTO user)
        //{
        //    try
        //    {
        //        _logger.LogInformation("AddUser method begining....");
        //        var userAdded = await _usersService.AddUserAsync(user);
        //        if (userAdded == null)
        //        {
        //            _logger.LogInformation("No user added");
        //            return BadRequest();
        //        }
        //        _logger.LogInformation("Returning user added");
        //        return Ok(userAdded);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error in AddUser method");
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //    return Ok();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // delete connected client or employee when deleting user

            try
            {
            
                _logger.LogInformation("DeleteUser method begining....");
                var userDeleted = await _usersService.GetUserAsync(id);

                List<ClientDTO> clients = (List<ClientDTO>)await _clientsService.GetAllClients();
                ClientDTO clientDeleted = clients.FirstOrDefault(x => x.ClientName == userDeleted.UserName);

                List<EmployeeDTO> employyes = (List<EmployeeDTO>)await _employeesService.GetAllEmployees();
                EmployeeDTO employeeDeleted = employyes.FirstOrDefault(x => x.EmployeeName == userDeleted.UserName);

                userDeleted = await _usersService.DeleteUserAsync(id);

                if (clientDeleted != null)
                {
                    _logger.LogInformation($"Client: {clientDeleted.ClientName} deleted from clients table");
                    await _clientsService.DeleteClient(clientDeleted.ClientID);
                }
                if(employeeDeleted != null)
                {
                    _logger.LogInformation($"Employee: {employeeDeleted.EmployeeName} deleted from employees table");
                    await _employeesService.DeleteEmployee(employeeDeleted.EmployeeID);
                }

                if (userDeleted == null)
                {
                    _logger.LogInformation("No user deleted");
                    return BadRequest();
                }
                _logger.LogInformation("Returning user deleted");

                // check for the user in clients



                return Ok(userDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteUser method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialUser(Guid id, UserDTOUpdate user)
        {
            try
            {
                _logger.LogInformation("UpdatePartialUser method begining....");
                var userUpdated = await _usersService.UpdatePartialUserAsync(id, user);
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