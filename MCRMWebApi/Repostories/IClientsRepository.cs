using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;

namespace MCRMWebApi.Repostories
{
    public interface IClientsRepository
    {
        Task<IEnumerable<ClientDTO>> GetAllClients();
        Task<ClientDTO> GetClient(Guid id);
        Task<ClientDTO> AddClient(ClientPartialCreateDTO client);
        Task<ClientDTO> UpdateClient(Guid id, ClientPartialUpdateDTO client);
        Task<ClientDTO> DeleteClient(Guid id);
    }
}
