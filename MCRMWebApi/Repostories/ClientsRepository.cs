using AutoMapper;
using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace MCRMWebApi.Repostories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly MCRMDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientsRepository> _logger;

        public ClientsRepository(MCRMDbContext context, IMapper mapper, ILogger<ClientsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClients()
        {
            var clients = await _context.Clients.ToListAsync();
            return clients;
        }

        public async Task<ClientDTO> GetClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client;
        }

        public async Task<ClientDTO> AddClient(ClientPartialCreateDTO client)
        {

            var clientToAdd = _mapper.Map<ClientDTO>(client);

            // should check for duplcates

            if (clientToAdd == null)
            {
                _logger.LogError("Client object sent from client is null.");
                return null;
            }
            else
            {
                UserDTO user = new() { Id = Guid.NewGuid(), UserName = client.ClientName, Password = "Default" };
                await _context.Users.AddAsync(user);
                _logger.LogInformation($"User with name: {clientToAdd.ClientName}, has been added to db.");

                clientToAdd.UserId = user.Id;
                await _context.Clients.AddAsync(clientToAdd);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Client with name: {clientToAdd.ClientName}, has been added to db.");
                return clientToAdd;
            }
            return clientToAdd;
        }

        public async Task<bool> ClientExists(Guid id)
        {
            return await _context.Clients.AnyAsync(e => e.ClientID == id);
        }

        public async Task<ClientDTO> UpdateClient(Guid id, ClientPartialUpdateDTO client)
        {
            if (!await ClientExists(id))
            {
                _logger.LogError($"Client with id: {id}, hasn't been found in db.");
                return null;
            }

            ClientDTO clientUpdated = await GetClient(id);


            clientUpdated.ClientCompany = ((client.ClientCompany != clientUpdated.ClientCompany) && (client.ClientCompany != null)) ?
                client.ClientCompany : clientUpdated.ClientCompany;
            clientUpdated.ClientEmail = ((client.ClientEmail != clientUpdated.ClientEmail) && (client.ClientEmail != null)) ?
                client.ClientEmail : clientUpdated.ClientEmail;
            clientUpdated.ClientAdress = ((client.ClientAdress != clientUpdated.ClientAdress) && (client.ClientAdress != null)) ?
                client.ClientAdress : clientUpdated.ClientAdress;
            clientUpdated.ClientOtherInformation = ((client.ClientOtherInformation != clientUpdated.ClientOtherInformation) && (client.ClientOtherInformation != null)) ?
                client.ClientOtherInformation : clientUpdated.ClientOtherInformation;
            clientUpdated.ClientPhoneNumber = ((client.ClientPhoneNumber != clientUpdated.ClientPhoneNumber) && (client.ClientPhoneNumber != null)) ?
                client.ClientPhoneNumber : clientUpdated.ClientPhoneNumber;

            // if clientname changed, change username in user table
            if ((client.ClientName != clientUpdated.ClientName) && client.ClientName != null)
            {
                clientUpdated.ClientName = client.ClientName;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == clientUpdated.UserId);
                if (user != null)
                {
                    user.UserName = client.ClientName;
                    _context.Users.Update(user);
                }
            }

            _context.Clients.Update(clientUpdated);
            await _context.SaveChangesAsync();

            return clientUpdated;
        }

        public async Task<ClientDTO> DeleteClient(Guid id)
        {
            var clientToDelete = await _context.Clients.FindAsync(id);

            // find and delete the user in user table
            var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == clientToDelete.UserId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Username {userToDelete} deleted from user db");
            }
            _context.Clients.Remove(clientToDelete);
            _logger.LogInformation($"Deleted {clientToDelete.ClientName} from clients db");
            await _context.SaveChangesAsync();
            return clientToDelete;
        }
    }
}
