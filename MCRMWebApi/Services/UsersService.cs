using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.Repostories;

namespace MCRMWebApi.Services
{
    public class UsersService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository repository)
        {
            _usersRepository = repository;
        }

        public async Task<UserDTO> AddUserAsync(UserPartialCreateDTO user)
        {
            return await _usersRepository.AddUserAsync(user);
        }

        public async Task<UserDTO> DeleteUserAsync(Guid id)
        {
            return await _usersRepository.DeleteUserAsync(id);    
        }

        public async Task<UserDTO> GetUserAsync(Guid id)
        {
            return await _usersRepository.GetUserAsync(id);
        }

        public async Task<UserDTO> UpdatePartialUserAsync(Guid id, UserDTOUpdate user)
        {
            return await _usersRepository.UpdatePartialUserAsync(id, user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _usersRepository.GetAllUsers();
        }
    }
}
