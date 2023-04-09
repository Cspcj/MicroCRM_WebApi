using MCRMWebApi.DTOs;
using MCRMWebApi.DTOs.PartialDTOCreate;
using MCRMWebApi.DTOs.PartialDTOUpdate;
using MCRMWebApi.Services;
using AutoMapper;
using MCRMWebApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace MCRMWebApi.Repostories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly MCRMDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProjectsRepository _projectsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly ILogger<TasksRepository> _logger;

        public TasksRepository(MCRMDbContext context, 
            IProjectsRepository projectRepository,
            IClientsRepository clientsRepository,
            IEmployeesRepository employeesRepository,
            IMapper mapper,
            ILogger<TasksRepository> logger)
        {
            _context = context;
            _projectsRepository = projectRepository;
            _logger = logger;
            _mapper = mapper;
            _clientsRepository = clientsRepository;
            _employeesRepository = employeesRepository;
        }


        public async Task<IEnumerable<TaskDTO>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskDTO> GetTaskByID(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(n => n.TaskId == id);
        }

        public async Task<TaskDTO> AddTask(TaskPartialCreateDTO task)
        {
            TaskDTO taskEntity = _mapper.Map<TaskDTO>(task);
            taskEntity.DateCreated = DateTime.Now;

            if (taskEntity.ProjectId == -1)
            {
                // we asume that if project id is -1 then we are adding a standalone note
                taskEntity.ProjectId = -1;
            }
            else
            {
                var project = await _projectsRepository.GetProject(task.ProjectId);
                if (project == null)
                {
                    _logger.LogError("Project id is incorrect.");
                    return null;
                }
            }

            var client = await _clientsRepository.GetClient(task.OwnerId);
            var employee = await _employeesRepository.GetEmployee(task.OwnerId);
            if ((client == null)&&(employee == null))
            {
                _logger.LogInformation("The client / employee ID is incorect. The note entry wasn't added");
                return null;
            }
            
            await _context.Tasks.AddAsync(taskEntity);
            await _context.SaveChangesAsync();
            return taskEntity;
        }

        public async Task<TaskDTO> UpdatePartial(Guid id, TaskPartialUpdateDTO task)
        {
            TaskDTO taskToBeUpdated = await GetTaskByID(id);
            if (taskToBeUpdated == null)
            {
                return null;
            }

          
            taskToBeUpdated.Text = (task.Text != null) && (task.Text != taskToBeUpdated.Text)
                ? task.Text : taskToBeUpdated.Text;
            taskToBeUpdated.ProjectId = (task.ProjectId != null) && (task.ProjectId != taskToBeUpdated.ProjectId)
                ? (int) task.ProjectId : taskToBeUpdated.ProjectId;
            taskToBeUpdated.IsCompleted= (task.IsCompleted != null) && (task.IsCompleted != taskToBeUpdated.IsCompleted) 
                ? (bool)task.IsCompleted : taskToBeUpdated.IsCompleted;

            _context.Tasks.Update(taskToBeUpdated);
            await _context.SaveChangesAsync();
            return taskToBeUpdated;
        }
        public async Task<TaskDTO> DeleteTask(Guid id)
        {
            var note = await _context.Tasks.FirstOrDefaultAsync(n => n.TaskId == id);
            if (note == null)
            {
                return null;
            }

            _context.Tasks.Remove(note);
            await _context.SaveChangesAsync();
            return note;
        }
    }
}
