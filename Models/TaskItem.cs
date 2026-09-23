namespace TaskTracker.Models;

[Serializable]
public class TaskItem(int id, string description, TaskStatus status, DateTime createdAt)
{
    public int Id { get; set; } = id;
    public string Description { get; set; } = description;
    public TaskStatus Status { get; set; } = status;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = createdAt;
}