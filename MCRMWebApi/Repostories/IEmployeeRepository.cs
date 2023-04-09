using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi.Repostories
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployee(Guid id);
        Task<EmployeeDTO> AddEmployee(EmployeePartialCreateDTO Employee);
        Task<EmployeeDTO> UpdateEmployee(Guid id, EmployeePartialUpdateDTO Employee);
        Task<EmployeeDTO> DeleteEmployee(Guid id);
    }
}
