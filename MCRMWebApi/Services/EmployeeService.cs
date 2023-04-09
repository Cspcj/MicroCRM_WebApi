using MCRMWebApi.DTOs;
using MCRMWebApi.Repostories;
using AutoMapper;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _EmployeesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesService> _logger;

        public EmployeesService(IEmployeesRepository EmployeesRepository, IMapper mapper, ILogger<EmployeesService> logger)
        {
            _EmployeesRepository = EmployeesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var Employees = await _EmployeesRepository.GetAllEmployees();
            return Employees;
        }

        public async Task<EmployeeDTO> GetEmployee(Guid id)
        {
            var Employee = await _EmployeesRepository.GetEmployee(id);
            return Employee;
        }


        public async Task<EmployeeDTO> AddEmployee(EmployeePartialCreateDTO Employee)
        {
            return await _EmployeesRepository.AddEmployee(Employee);

        }

        public async Task<EmployeeDTO> UpdatePartialEmployee(Guid id, EmployeePartialUpdateDTO Employee)
        {
            return await _EmployeesRepository.UpdateEmployee(id, Employee);
        }

        public async Task<EmployeeDTO> DeleteEmployee(Guid id)
        {
            return await _EmployeesRepository.DeleteEmployee(id);
        }
    }
}
