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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _EmployeesService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeesService EmployeesService, ILogger<EmployeesController> logger)
        {
            _EmployeesService = EmployeesService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                _logger.LogInformation("GetAllEmployees method begining....");
                var Employees = await _EmployeesService.GetAllEmployees();
                if (Employees == null || !Employees.Any())
                {
                    _logger.LogInformation("No Employees found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning the list of Employees");
                return Ok(Employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllEmployees method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            try
            {
                _logger.LogInformation("GetEmployee method begining....");
                var Employee = await _EmployeesService.GetEmployee(id);
                if (Employee == null)
                {
                    _logger.LogInformation("No Employee found in the table");
                    return StatusCode((int)HttpStatusCode.NoContent, "No records in the table");
                }
                _logger.LogInformation("Returning the Employee");
                return Ok(Employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployee method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeePartialCreateDTO Employee)
        {
            try
            {
                _logger.LogInformation("AddEmployee method begining....");
                if (Employee == null)
                {
                    _logger.LogInformation("Employee is null");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Employee is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Employee is not valid");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Employee is not valid");
                }
                var EmployeeToCreate = await _EmployeesService.AddEmployee(Employee);
                if (EmployeeToCreate == null)
                {
                    _logger.LogInformation("Employee is not created");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Employee is not created");
                }
                _logger.LogInformation("Employee is created");
                return StatusCode((int)HttpStatusCode.Created, EmployeeToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddEmployee method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeePartialUpdateDTO Employee)
        {
            try
            {
                _logger.LogInformation("UpdateEmployee method begining....");
                if (Employee == null)
                {
                    _logger.LogInformation("Employee is null");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Employee is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Employee is not valid");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Employee is not valid");
                }
                var EmployeeToUpdate = await _EmployeesService.UpdatePartialEmployee(id, Employee);
                if (EmployeeToUpdate == null)
                {
                    _logger.LogInformation("Employee is not updated");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Employee is not updated");
                }
                _logger.LogInformation("Employee is updated");
                return StatusCode((int)HttpStatusCode.Created, EmployeeToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateEmployee method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                _logger.LogInformation("DeleteEmployee method begining....");
                var EmployeeToDelete = await _EmployeesService.DeleteEmployee(id);
                if (EmployeeToDelete == null)
                {
                    _logger.LogInformation("Employee is not deleted");
                    return StatusCode((int)HttpStatusCode.BadRequest, "Employee is not deleted");
                }
                _logger.LogInformation("Employee is deleted");
                return StatusCode((int)HttpStatusCode.Created, EmployeeToDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteEmployee method");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

    }
}
