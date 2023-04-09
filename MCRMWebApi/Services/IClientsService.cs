using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi.Services
{
    public interface IClientsService
    {
        Task<IEnumerable<ClientDTO>> GetAllClients();
        Task<ClientDTO> GetClient(Guid id);
        Task<ClientDTO> AddClient(ClientPartialCreateDTO client);
        Task<ClientDTO> UpdatePartialClient(Guid id, ClientPartialUpdateDTO client);
        Task<ClientDTO> DeleteClient(Guid id);
    }
}
