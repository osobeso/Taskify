namespace TaskifyAPI.Managers
{
    using Taskify.Data.Models;
    using Taskify.Data.Repositories;
    using TaskifyAPI.Dtos;
    public class TaskifyManager : ITaskifyManager
    {
        private readonly ITaskRepository Tasks;
        public TaskifyManager(ITaskRepository tasks)
        {
            Tasks = tasks;
        }
        public async Task<TaskModel> CreateNewTaskAsync(CreateNewTaskDto dto)
        {
            var taskDto = new TaskModel
            {
                Description = dto.Description,
                Title = dto.Title,
                ParentTask = dto.ParentId,
                Id = Guid.NewGuid(),
            };
            return await Tasks.UpsertAsync(taskDto);
        }

        public Task<TaskModel[]> GetRootTasksAsync()
        {
            return Tasks.GetRootAsync();
        }

        public async Task<TaskDetailsDto> GetTaskDetailsAsync(TaskKey key)
        {
            var task = await FetchTaskOrThrowAsync(key);
            var subTasks = await Tasks.GetChildrenAsync(task.Id);
            return new TaskDetailsDto(task, subTasks);
        }

        public async Task<TaskModel> SetParentAsync(SetParentTaskDto dto)
        {
            var task = await FetchTaskOrThrowAsync(dto.Key);
            await Tasks.DeleteAsync(task.Id, task.ParentTask);
            task.ParentTask = dto.NewParentId;
            return await Tasks.UpsertAsync(task);
        }

        public async Task<TaskModel> UpdateTaskAsync(UpdateTaskDto dto)
        {
            var task = await FetchTaskOrThrowAsync(dto.Key);
            task.Title = dto.Title;
            task.Description = dto.Description;
            return await Tasks.UpsertAsync(task);
        }

        private async Task<TaskModel> FetchTaskOrThrowAsync(TaskKey key)
        {
            var task = await Tasks.GetAsync(key.Id, key.ParentId);
            if (task == null)
            {
                throw new KeyNotFoundException("Could not fetch task with given id");
            }
            return task;
        }
    }
}
