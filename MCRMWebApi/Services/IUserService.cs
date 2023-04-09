using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserAsync(Guid id);
        Task<UserDTO> AddUserAsync(UserPartialCreateDTO user);
        Task<UserDTO> DeleteUserAsync(Guid id);
        Task<UserDTO> UpdatePartialUserAsync(Guid id, UserDTOUpdate user);
    }
}
