using MCRMWebApi.DTOs;

namespace MCRMWebApi.Repostories
{
    public interface ITasksRepository
    {
        Task<IEnumerable<TaskDTO>> GetAllTasks();
        Task<TaskDTO> GetTaskByID(Guid id);
        Task<TaskDTO> AddTask(TaskPartialCreateDTO note);
        Task<TaskDTO> DeleteTask(Guid id);
        Task<TaskDTO> UpdatePartial(Guid id, TaskPartialUpdateDTO note);
    }   
}
