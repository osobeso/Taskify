namespace TaskifyAPI.Managers
{
    using Taskify.Data.Models;
    using TaskifyAPI.Dtos;

    public interface ITaskifyManager
    {
        // Tareas necesarias para agregar y regresar los tasks?
        Task<TaskModel> CreateNewTaskAsync(CreateNewTaskDto dto);
        Task<TaskModel> UpdateTaskAsync(UpdateTaskDto dto);
        Task<TaskModel[]> GetRootTasksAsync();
        Task<TaskModel> SetParentAsync(SetParentTaskDto dto);
        Task<TaskDetailsDto> GetTaskDetailsAsync(TaskKey key);
    }
}
