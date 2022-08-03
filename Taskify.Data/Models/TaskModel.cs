namespace Taskify.Data.Models
{
    public record TaskModel
    {
        public Guid Id { get; set; }
        public Guid? ParentTask { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
