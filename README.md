# Task Tracker CLI

A simple command-line application for managing personal tasks. This tool allows you to create, update, delete, and organize tasks through various command-line interfaces.

## Features

- **Add tasks** - Create new tasks with descriptions
- **Update tasks** - Modify task descriptions
- **Delete tasks** - Remove tasks from the list
- **Mark tasks** - Change task status (To Do, In Progress, Completed)
- **List tasks** - View all tasks with filtering by status
- **Filter by status** - See only To Do, In Progress, or Completed tasks

## Installation

### Prerequisites

- .NET 10.0 or later
- C# 10 SDK

### Steps to Install

1. Clone or download the project
2. Navigate to the project directory:
   ```bash
   cd task-tracker-cli
   ```
3. Restore and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

Alternatively, you can compile and run manually:
```bash
dotnet task-tracker-cli.dll
```

## Usage

### Available Commands

| Command | Description |
|---------|-------------|
| `add <description>` | Create a new task |
| `update <id> <description>` | Update a task's description |
| `delete <id>` | Delete a task by ID |
| `mark-to-do` | Mark a task as To Do |
| `mark-in-progress` | Mark a task as In Progress |
| `mark-completed` | Mark a task as Completed |
| `list` | Show all tasks |
| `list-to-do` | Show only To Do tasks |
| `list-in-progress` | Show only In Progress tasks |
| `list-completed` | Show only Completed tasks |
| `help` | Display this help message |

### Examples

```bash
# Add a new task
./task-tracker-cli add Buy groceries

# List all tasks
./task-tracker-cli list

# List only To Do tasks
./task-tracker-cli list-to-do

# Mark a task as in progress
./task-tracker-cli mark-in-progress 1

# Update a task description
./task-tracker-cli update 1 "Buy groceries and cook dinner"

# Complete a task
./task-tracker-cli mark-completed 1

# Delete a task
./task-tracker-cli delete 1
```

## Project Structure

```
task-tracker-cli/
├── Program.cs                  # Entry point, command routing
├── task-tracker-cli.csproj     # Project file (.NET 10.0)
├── Models/
│   ├── TaskItem.cs            # Task data model
│   └── TaskStatus.cs          # Task status enum
├── Services/
│   └── TaskService.cs         # Business logic (CRUD operations)
├── Utilities/
│   └── ValidateUtility.cs     # Argument validation
└── Tasks/                     # Directory for task JSON files
```

## How It Works

- Tasks are stored in memory and persisted to JSON files in the `Tasks/` directory
- Each task has an ID, description, creation timestamp, and last update timestamp
- Task status can be changed via the `mark-*` commands
- The application loads existing tasks from JSON files on startup

## License

This project is licensed under the MIT License.
