using MCRMWebApi.DTOs;

namespace MCRMWebApi.Services
{
    public interface ITasksService
    {
        // generate interface for TasksService

        Task<TaskDTO> AddTaskAsync(TaskPartialCreateDTO task);
        Task<TaskDTO> DeleteTaskAsync(Guid id);
        Task<IEnumerable<TaskDTO>> GetAllTasks();
        Task<TaskDTO> GetTaskByID(Guid id);
        Task<TaskDTO> UpdatePartial(Guid id, TaskPartialUpdateDTO task);
    }
}
