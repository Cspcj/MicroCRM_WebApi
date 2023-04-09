using MCRMWebApi.DTOs;
using MCRMWebApi.Repostories;
using AutoMapper;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientsService> _logger;

        public ClientsService(IClientsRepository clientsRepository, IMapper mapper, ILogger<ClientsService> logger)
        {
            _clientsRepository = clientsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClients()
        {
            var clients = await _clientsRepository.GetAllClients();
            return clients;
        }

        public async Task<ClientDTO> GetClient(Guid id)
        {
            var client = await _clientsRepository.GetClient(id);
            return client;
        }


        public async Task<ClientDTO> AddClient(ClientPartialCreateDTO client)
        {
            return await _clientsRepository.AddClient(client);

        }

        public async Task<ClientDTO> UpdatePartialClient(Guid id, ClientPartialUpdateDTO client)
        {
            return await _clientsRepository.UpdateClient(id, client);
        }

        public async Task<ClientDTO> DeleteClient(Guid id)
        {
            return await _clientsRepository.DeleteClient(id);
        }
    }
}
