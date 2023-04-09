using AutoMapper;
using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace MCRMWebApi.Repostories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly MCRMDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesRepository> _logger;

        public EmployeesRepository(MCRMDbContext context, IMapper mapper, ILogger<EmployeesRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var Employees = await _context.Employees.ToListAsync();
            return Employees;
        }

        public async Task<EmployeeDTO> GetEmployee(Guid id)
        {
            var Employee = await _context.Employees.FindAsync(id);
            return Employee;
        }

        public async Task<EmployeeDTO> AddEmployee(EmployeePartialCreateDTO Employee)
        {

            var EmployeeToAdd = _mapper.Map<EmployeeDTO>(Employee);

            // should check for duplcates

            if (EmployeeToAdd == null)
            {
                _logger.LogError("Employee object sent from Employee is null.");
                return null;
            }
            else
            {
                UserDTO user = new() { Id = Guid.NewGuid(), UserName = Employee.EmployeeName, Password = "Default" };
                await _context.Users.AddAsync(user);
                _logger.LogInformation($"User with name: {EmployeeToAdd.EmployeeName}, has been added to db.");

                EmployeeToAdd.UserId = user.Id;
                await _context.Employees.AddAsync(EmployeeToAdd);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Employee with name: {EmployeeToAdd.EmployeeName}, has been added to db.");
                return EmployeeToAdd;
            }
            return EmployeeToAdd;
        }

        public async Task<bool> EmployeeExists(Guid id)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeID == id);
        }

        public async Task<EmployeeDTO> UpdateEmployee(Guid id, EmployeePartialUpdateDTO Employee)
        {
            if (!await EmployeeExists(id))
            {
                _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                return null;
            }

            EmployeeDTO EmployeeUpdated = await GetEmployee(id);

            EmployeeUpdated.EmployeeEmail = ((Employee.EmployeeEmail != EmployeeUpdated.EmployeeEmail) && (Employee.EmployeeEmail != null)) ?
                Employee.EmployeeEmail : EmployeeUpdated.EmployeeEmail;
            EmployeeUpdated.EmployeeAdress = ((Employee.EmployeeAdress != EmployeeUpdated.EmployeeAdress) && (Employee.EmployeeAdress != null)) ?
                Employee.EmployeeAdress : EmployeeUpdated.EmployeeAdress;
            EmployeeUpdated.EmployeeOtherInformation = ((Employee.EmployeeOtherInformation != EmployeeUpdated.EmployeeOtherInformation) && (Employee.EmployeeOtherInformation != null)) ?
                Employee.EmployeeOtherInformation : EmployeeUpdated.EmployeeOtherInformation;
            EmployeeUpdated.EmployeePhoneNumber = ((Employee.EmployeePhoneNumber != EmployeeUpdated.EmployeePhoneNumber) && (Employee.EmployeePhoneNumber != null)) ?
                Employee.EmployeePhoneNumber : EmployeeUpdated.EmployeePhoneNumber;

            // if Employeename changed, change username in user table
            if ((Employee.EmployeeName != EmployeeUpdated.EmployeeName) && Employee.EmployeeName != null)
            {
                EmployeeUpdated.EmployeeName = Employee.EmployeeName;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == EmployeeUpdated.UserId);
                if (user != null)
                {
                    user.UserName = Employee.EmployeeName;
                    _context.Users.Update(user);
                }
            }

            _context.Employees.Update(EmployeeUpdated);
            await _context.SaveChangesAsync();

            return EmployeeUpdated;
        }

        public async Task<EmployeeDTO> DeleteEmployee(Guid id)
        {
            var EmployeeToDelete = await _context.Employees.FindAsync(id);

            // find and delete the user in user table
            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == EmployeeToDelete.UserId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Username {userToDelete} deleted from user db");
            }
            _context.Employees.Remove(EmployeeToDelete);
            _logger.LogInformation($"Deleted {EmployeeToDelete.EmployeeName} from Employees db");
            await _context.SaveChangesAsync();
            return EmployeeToDelete;
        }
    }
}
