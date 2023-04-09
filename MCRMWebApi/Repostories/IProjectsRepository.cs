using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;


namespace MCRMWebApi.Repostories
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO> GetProject(int id);
        Task<ProjectDTO> AddProject(ProjectPartialCreateDTO project);
        Task<ProjectDTO> UpdateProject(ProjectPartialUpdateDTO project);
        Task<ProjectDTO> DeleteProject(int id);
        Task<ProjectDTO> ToggleAchiveFlag(int id);
        Task<IEnumerable<ProjectDTO>> GetProjectsByUser(Guid userId);
        Task<IEnumerable<ProjectDTO>> GetProjectsByClient(Guid clientId);
        Task<IEnumerable<ProjectDTO>> GetArchivedProjects(bool status);

    }
}
