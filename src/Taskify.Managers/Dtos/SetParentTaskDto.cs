namespace TaskifyAPI.Dtos
{
    public record SetParentTaskDto(TaskKey Key, Guid? NewParentId);
}
