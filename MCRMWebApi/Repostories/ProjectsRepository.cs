using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.DataContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MCRMWebApi.Repostories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly MCRMDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectsRepository> _logger;


        public ProjectsRepository(MCRMDbContext context, IMapper mapper, ILogger<ProjectsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return projects;
        }

        public async Task<ProjectDTO> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            return project;
        }

        public async Task<ProjectDTO> AddProject(ProjectPartialCreateDTO project)
        {
            var projectToAdd = _mapper.Map<ProjectDTO>(project);
            if (projectToAdd == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return null;
            }
            else
            {
                // check if client exists
                
                var client = await _context.Clients.FindAsync(project.ClientID);
                if (client == null)
                {
                    _logger.LogError("Client id is incorrect.");
                    return null;
                }

                //check if user exists  

                var employee = await _context.Employees.FindAsync(project.UserId);
                if (employee == null)
                {
                    _logger.LogError("Employee id is incorrect.");
                    return null;
                }

                // setting status flags

                projectToAdd.IsDeleted = false;
                projectToAdd.IsArchived = false;

                // adding project to db

                await _context.Projects.AddAsync(projectToAdd);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Project with name: {projectToAdd.ProjectName}, has been added to db.");
                return projectToAdd;
            }
        }

        public async Task<ProjectDTO> UpdateProject(ProjectPartialUpdateDTO project)
        {
            var projectToUpdate = _mapper.Map<ProjectDTO>(project);
            if (projectToUpdate == null)
            {
                _logger.LogError("Project object sent from client is null.");
                return null;
            }
            else
            {
                // check if client ID sent exists
                var client = await _context.Clients.FindAsync(project.ClientID);
                if (client == null)
                {
                    _logger.LogError("Client ID sent is null or incorrect");
                    return null;
                }
                projectToUpdate.ClientID = client.ClientID;
                
                
                //check if the user ID sent exists  
                var employee = await _context.Employees.FindAsync(project.UserId);
                if (employee == null)
                {
                    _logger.LogError("User ID sent is null or incorrect");
                    return null;
                }
                projectToUpdate.UserId = employee.EmployeeID;

                // check what properties are changed and update them

                projectToUpdate.ProjectDescription = ((project.ProjectDescription != projectToUpdate.ProjectDescription)&&(project.ProjectDescription != null)) 
                    ? project.ProjectDescription : projectToUpdate.ProjectDescription;
                projectToUpdate.ProjectName = ((project.ProjectName != projectToUpdate.ProjectName)&&(project.ProjectName != null))
                    ? project.ProjectName : projectToUpdate.ProjectName;
                projectToUpdate.ProjectLocation = ((project.ProjectLocation != projectToUpdate.ProjectLocation)&&(project.ProjectLocation != null))
                    ? project.ProjectLocation : projectToUpdate.ProjectLocation;
                projectToUpdate.ProjectLocationCity = ((project.ProjectLocationCity != projectToUpdate.ProjectLocationCity)&&(project.ProjectLocationCity != null))
                    ? project.ProjectLocationCity : projectToUpdate.ProjectLocationCity;
                projectToUpdate.Region = ((project.Region != projectToUpdate.Region)&&(project.Region != null))
                    ? project.Region : projectToUpdate.Region;

                // updating project in db
                _context.Projects.Update(projectToUpdate);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Project with name: {projectToUpdate.ProjectName}, has been updated in db.");
                return projectToUpdate;
            }
        }

        public async Task<ProjectDTO> DeleteProject(int id)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete == null)
            {
                _logger.LogError($"Project with id: {id}, hasn't been found in db.");
                return null;
            }
            else
            {
                projectToDelete.IsDeleted = true;
                _context.Projects.Update(projectToDelete);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Project with name: {projectToDelete.ProjectName}, has been deleted from db.");
                return projectToDelete;
            }
        }


        public async Task<ProjectDTO> ToggleAchiveFlag(int id)
        {
            // get project from db and
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                _logger.LogError($"Project with id: {id}, hasn't been found in db.");
                return null;
            }
            else
            {
                // toggle archive flag
                project.IsArchived = !project.IsArchived;
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Project with name: {project.ProjectName}, has been archived/unarchived.");
                return project;
            }
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsByUser(Guid userId)
        {
            // get a list of project by user id
            var projects = await _context.Projects.Where(p => p.UserId == userId).ToListAsync();
            return projects;
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsByClient(Guid clientId)
        {
            // get a list of projects by client id
            var projects = await _context.Projects.Where(p => p.ClientID == clientId).ToListAsync();
            return projects;
        }

        public async Task<IEnumerable<ProjectDTO>> GetArchivedProjects(bool status)
        {
            // get all archived projects
            var projects = await _context.Projects.Where(p => p.IsArchived == status).ToListAsync();
            return projects;
        }
    }
}
