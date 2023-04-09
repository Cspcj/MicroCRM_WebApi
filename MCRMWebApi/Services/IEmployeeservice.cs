using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi.Services
{
    public interface IEmployeesService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployee(Guid id);
        Task<EmployeeDTO> AddEmployee(EmployeePartialCreateDTO Employee);
        Task<EmployeeDTO> UpdatePartialEmployee(Guid id, EmployeePartialUpdateDTO Employee);
        Task<EmployeeDTO> DeleteEmployee(Guid id);
    }
}
