using MCRMWebApi.DataContext;
using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DTOs.PartialDTOCreate;

using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MCRMWebApi.Repostories
{
    public class UsersRepository:IUsersRepository
    {
        private readonly MCRMDbContext _context;
        private readonly IMapper _mapper;
        

        public UsersRepository(MCRMDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Adds a new user to the table, and returns the user model with the Id field populated
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserDTO> AddUserAsync(UserPartialCreateDTO user)
        {
            if (user != null) 
            {
                var userToAdd = _mapper.Map<UserDTO>(user);

                await _context.Users.AddAsync(userToAdd);
                await _context.SaveChangesAsync();
                return userToAdd;
            }
            return null;
        }

        /// <summary>
        /// Deletes the user identified by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDTO> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user!= null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }    

            return user;
        }

        /// <summary>
        /// Retrieves the a user model for the user identified with id, otherwise null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetUserAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Retrieves all the users in the table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public bool userExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        /// <summary>
        /// Updates the entry for a user identified by Id, with the information supplied in User
        /// The method accespts partial updating of the user
        /// </summary>
        /// <param name="id"></param> Id of the user to be updated
        /// <param name="user"></param> Information to be updated 
        /// <returns></returns>
        public async Task<UserDTO> UpdatePartialUserAsync(Guid id, UserDTOUpdate user)
        {
            if (!userExists(id))
            {
                return null;
            }

            var userUpdated = await GetUserAsync(id);

            userUpdated.UserName = ((user.UserName != userUpdated.UserName)&&(user.UserName!=null))?user.UserName:userUpdated.UserName;
            userUpdated.Password = ((user.Password != userUpdated.Password)&&(user.Password!=null))?user.Password:userUpdated.Password;
            _context.Users.Update(userUpdated);
            await _context.SaveChangesAsync();

            return userUpdated;
        }
    }
}
