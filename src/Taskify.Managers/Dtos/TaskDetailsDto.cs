namespace TaskifyAPI.Dtos
{
    using Taskify.Data.Models;
    public record TaskDetailsDto(TaskModel Task, TaskModel[] ChildTasks);
}
