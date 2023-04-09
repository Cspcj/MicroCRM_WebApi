using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DTOs.PartialDTOCreate;

namespace MCRMWebApi.Repostories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserAsync(Guid id);
        Task<UserDTO> AddUserAsync(UserPartialCreateDTO user);
        Task<UserDTO> DeleteUserAsync(Guid id);
        Task<UserDTO> UpdatePartialUserAsync(Guid id, UserDTOUpdate user);
    }
}
