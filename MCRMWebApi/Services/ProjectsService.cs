
using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.Repostories;
using System.Threading.Tasks;

namespace MCRMWebApi.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly ILogger<ProjectsService> _logger;

        public ProjectsService(IProjectsRepository repository, ILogger<ProjectsService> logger)
        {
            _logger = logger;
            _projectsRepository = repository;
        }

        public async Task<ProjectDTO> AddProject(ProjectPartialCreateDTO project)
        {
            return await _projectsRepository.AddProject(project);
        }

        public async Task<ProjectDTO> DeleteProject(int id)
        {
            return await _projectsRepository.DeleteProject(id);
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            return await _projectsRepository.GetAllProjects();
        }

        public async Task<IEnumerable<ProjectDTO>> GetArchivedProjects(bool status)
        {
            return await _projectsRepository.GetArchivedProjects(status);
        }

        public async Task<ProjectDTO> GetProject(int id)
        {
            return await _projectsRepository.GetProject(id);
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsByClient(Guid clientId)
        {
            return await _projectsRepository.GetProjectsByClient(clientId);
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsByUser(Guid userId)
        {
            return await _projectsRepository.GetProjectsByUser(userId);
        }

        public async Task<ProjectDTO> ToggleAchiveFlag(int id)
        {
            return await _projectsRepository.ToggleAchiveFlag(id);
        }

        public Task<ProjectDTO> UpdateProject(ProjectPartialUpdateDTO project)
        {
            return _projectsRepository.UpdateProject(project);
        }
    }
}
