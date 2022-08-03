namespace TaskifyAPI.Dtos
{
    public record UpdateTaskDto(TaskKey Key, string Title, string Description);
}
