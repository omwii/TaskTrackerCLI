using System.Text.Json;
using TaskTracker.Models;
using TaskStatus = TaskTracker.Models.TaskStatus;

namespace TaskTracker.Services;

public class TaskService
{
    private readonly Dictionary<int, TaskItem> _tasks = new();
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public void LoadTasks()
    {
        _tasks.Clear();
        
        if (!Directory.Exists("Tasks")) 
            return;
        var files = Directory.GetFiles("Tasks", "task-*.json");
        
        foreach (var file in files)
        {
            var task = JsonSerializer.Deserialize<TaskItem>(File.ReadAllText(file));
            if (task != null)
                _tasks.Add(task.Id, task);
        }
    }

    public TaskItem AddTask(string description)
    {
        if (description == string.Empty) throw new ArgumentException("Description cannot be empty");

        var taskId = 0;
        if (_tasks.Count > 0)
             taskId = _tasks.Keys.Last() + 1;
        var creationDate = DateTime.Now;
        
        var task = new TaskItem(taskId, description, creationDate);
        _tasks.Add(task.Id, task);

        var json = JsonSerializer.Serialize(task, _options);
        if (!Directory.Exists("Tasks")) 
            Directory.CreateDirectory("Tasks");
        File.WriteAllText($"Tasks/task-{taskId}.json", json);
        
        return task;
    }

    public void UpdateTask(int id, string description)
    {
        if (_tasks.TryGetValue(id, out var task))
        {
            task.Description = description;
            task.UpdatedAt = DateTime.Now;
            
            var json = JsonSerializer.Serialize(task, _options);
            if (!Directory.Exists("Tasks"))
                Directory.CreateDirectory("Tasks");

            File.WriteAllText($"Tasks/task-{id}.json", json);
        }
        else
        {
            throw new KeyNotFoundException($"Task with id {id} not found");
        }
    }

    public void DeleteTask(int id)
    {
        _tasks.Remove(id);
        File.Delete($"Tasks/task-{id}.json");
    }
    
    public void MarkTask(int id, TaskStatus status)
    {
        if (_tasks.TryGetValue(id, out var task))
        {
            task.Status = status;
            task.UpdatedAt = DateTime.Now;
            
            var json = JsonSerializer.Serialize(task, _options);
            if (!Directory.Exists("Tasks")) 
                Directory.CreateDirectory("Tasks");
            File.WriteAllText($"Tasks/task-{id}.json", json);
        }
        else
        {
            throw new KeyNotFoundException($"Task with id {id} not found");
        }
    }
    
    public TaskItem[] GetTasksByStatus(TaskStatus status) => _tasks.Values.Where(x => x.Status == status).ToArray();
}