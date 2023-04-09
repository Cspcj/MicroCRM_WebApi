using MCRMWebApi.DTOs;
using MCRMWebApi.Repostories;

namespace MCRMWebApi.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _repositoty;
        private readonly ILogger<TasksRepository> _logger;

        public TasksService(ITasksRepository repositoty, ILogger<TasksRepository> logger)
        {
            _logger = logger;
            _repositoty = repositoty;
        }

        public async Task<TaskDTO> AddTaskAsync(TaskPartialCreateDTO note)
        {
            return await _repositoty.AddTask(note);
        }

        public async Task<TaskDTO> DeleteTaskAsync(Guid id)
        {
            return await _repositoty.DeleteTask(id);
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTasks()
        {
             return await _repositoty.GetAllTasks();
        }

        public async Task<TaskDTO> GetTaskByID(Guid id)
        {
            return await _repositoty.GetTaskByID(id);
        }

        public async Task<TaskDTO> UpdatePartial(Guid id, TaskPartialUpdateDTO note)
        {
            return await _repositoty.UpdatePartial(id, note);   
        }
    }
}
