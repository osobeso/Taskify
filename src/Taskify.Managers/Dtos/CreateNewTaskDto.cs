namespace TaskifyAPI.Dtos
{
    public record CreateNewTaskDto(string Title, string Description, Guid? ParentId);
}
