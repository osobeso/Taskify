namespace Taskify.Data.Repositories
{
    using Taskify.Data.Models;
    public interface ITaskRepository
    {
        Task<TaskModel[]> GetRootAsync();
        Task<TaskModel[]> GetChildrenAsync(Guid parent);
        Task<TaskModel?> GetAsync(Guid id, Guid? parentId);
        Task<TaskModel> UpsertAsync(TaskModel task);
        Task<bool> DeleteAsync(Guid id, Guid? parentId = null);
    }
}
