namespace TaskTracker.Utility;

public static class ValidateUtility
{
    public static string[]? ParseArgs(string[] args)
    {
        if (args.Length == 0) 
            throw new ArgumentNullException(nameof(args),  "Use \"task-tracker-cli help\" for available commands");

        return args[0] switch
        {
            "add" => ParseAddCommand(args),
            "update" => ParseUpdateCommands(args),
            "delete" => ParseDeleteCommand(args),
            "mark-to-do"
                or "mark-in-progress"
                or "mark-completed"
                or "list"
                or "list-to-do"
                or "list-in-progress"
                or "list-completed"
                or "help" => args,
            _ => null
        };
    }

    private static string[] ParseAddCommand(string[] args)
    {
        if (args.Length != 2) 
            throw new ArgumentNullException(nameof(args), "Usage: task-tracker-cli add <description>");
        
        return args[1].Length > 32
            ? throw new ArgumentException("Task description cannot exceed 32 characters.")
            : args;
    }

    private static string[] ParseUpdateCommands(string[] args)
    {
        if (args.Length != 3) 
            throw new ArgumentNullException(nameof(args), "Usage: task-tracker-cli update <id> <description>");
        
        var isInt = int.TryParse(args[1], out _);
        return isInt 
            ? args 
            : throw new ArgumentException($"Task id should be an integer.");
    }

    private static string[] ParseDeleteCommand(string[] args)
    {
        if (args.Length != 2)
            throw new ArgumentNullException(nameof(args), "Usage: task-tracker-cli delete <id>");
        
        var isInt = int.TryParse(args[1], out _);
        return isInt 
            ? args 
            : throw new ArgumentException("Task id should be an integer.");
    }
}