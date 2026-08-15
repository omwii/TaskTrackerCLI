using TaskTracker.Models;
using TaskTracker.Services;
using TaskTracker.Utility;
using TaskStatus = TaskTracker.Models.TaskStatus;

var taskService = new TaskService();
taskService.LoadTasks();

try
{
    var command = ValidateUtility.ParseArgs(args);
    if (command == null)
        throw new ArgumentNullException(nameof(args),  "Use \"task-tracker-cli help\" for available commands");
    
    switch (command[0])
    {
        case "add":
            AddTask(command[1]);
            break;
        case "update":
            UpdateTask(command[1], command[2]);
            break;
        case "delete":
            DeleteTask(command[1]);
            break;
        case "mark-to-do":
            MarkTask(command[1], TaskStatus.ToDo);
            break;
        case "mark-in-progress":
            MarkTask(command[1], TaskStatus.InProgress);
            break;
        case "mark-completed":
            MarkTask(command[1], TaskStatus.Completed);
            break;
        case "list":
            ListAllTasks();
            break;
        case "list-to-do":
            ListTasksByStatus(TaskStatus.ToDo);
            break;
        case "list-in-progress":
            ListTasksByStatus(TaskStatus.InProgress);
            break;
        case "list-completed":
            ListTasksByStatus(TaskStatus.Completed);
            break;
        case "help":
            break;
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}

return;


void AddTask(string description)
{
    var task = taskService.AddTask(description);
    Console.WriteLine($"Task with id {task.Id} added successfully!");
}

void UpdateTask(string stringId, string description)
{
    var id = int.Parse(stringId);
    taskService.UpdateTask(id, description);
    Console.WriteLine($"Task with id {id} updated successfully!");
}

void DeleteTask(string stringId)
{
    var id = int.Parse(stringId);
    taskService.DeleteTask(id);
    Console.WriteLine($"Task with id {id} deleted successfully!");
}

void MarkTask(string stringId, TaskStatus status)
{
    var id = int.Parse(stringId);
    taskService.MarkTask(id, status);
    Console.WriteLine($"Task with id {id} marked {status.ToString()} successfully!");
}

void ListAllTasks()
{
    var tasks = new List<TaskItem>();
    tasks.AddRange(taskService.GetTasksByStatus(TaskStatus.ToDo));
    tasks.AddRange(taskService.GetTasksByStatus(TaskStatus.InProgress));
    tasks.AddRange(taskService.GetTasksByStatus(TaskStatus.Completed));
    foreach (var task in tasks)
    {
        Console.WriteLine(
            $"Id: {task.Id}, " +
            $"Description: {task.Description},  " +
            $"Status: {task.Status}, " +
            $"Created at: {task.CreatedAt}, " +
            $"Updated at: {task.UpdatedAt}");
    }
}

void ListTasksByStatus(TaskStatus status)
{
    var tasks = taskService.GetTasksByStatus(status);
    foreach (var task in tasks)
    {
        Console.WriteLine(
            $"Id: {task.Id}, " +
            $"Description: {task.Description}, " +
            $"Status: {task.Status}, " +
            $"Created at: {task.CreatedAt}, " +
            $"Updated at: {task.UpdatedAt}");
    }
}