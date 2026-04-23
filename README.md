# Task Manager API

ASP.NET Core 8 Web API for managing tasks, backed by MongoDB.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) running locally, or a [MongoDB Atlas](https://www.mongodb.com/atlas) connection string

## Configuration

Open `appsettings.json` and update the `MongoDbSettings` section:

```json
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "TaskManagerDb",
  "CollectionName": "Tasks"
}
```

For Atlas, replace `ConnectionString` with your Atlas URI:
```
mongodb+srv://<username>:<password>@cluster0.xxxxx.mongodb.net/?retryWrites=true&w=majority
```

> **Tip:** For local development, override the connection string in `appsettings.Development.json` or via an environment variable:
> ```
> MONGODBSETTINGS__CONNECTIONSTRING=mongodb://localhost:27017
> ```

## Running the Project

```bash
# Restore packages
dotnet restore

# Run in development mode (enables Swagger UI)
dotnet run
```

The API starts on `https://localhost:5001` (HTTPS) and `http://localhost:5000` (HTTP) by default.

Open the Swagger UI at: `https://localhost:5001/swagger`

## API Endpoints

| Method | Route               | Description         |
|--------|---------------------|---------------------|
| GET    | `/api/tasks`        | Get all tasks       |
| GET    | `/api/tasks/{id}`   | Get task by ID      |
| POST   | `/api/tasks`        | Create a new task   |
| PUT    | `/api/tasks/{id}`   | Update a task       |
| DELETE | `/api/tasks/{id}`   | Delete a task       |

## Task Model

```json
{
  "id": "664f1a2b3c4d5e6f7a8b9c0d",
  "title": "Buy groceries",
  "description": "Milk, eggs, bread",
  "isComplete": false,
  "priority": "Medium",
  "createdAt": "2026-04-22T10:00:00Z"
}
```

**Priority values:** `Low`, `Medium`, `High`

## Example Requests

**Create a task:**
```bash
curl -X POST https://localhost:5001/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"title":"Fix bug","description":"Null ref in auth","priority":"High"}'
```

**Mark a task complete:**
```bash
curl -X PUT https://localhost:5001/api/tasks/{id} \
  -H "Content-Type: application/json" \
  -d '{"title":"Fix bug","description":"Null ref in auth","isComplete":true,"priority":"High"}'
```

## Project Structure

```
task-manager/
├── Controllers/
│   └── TasksController.cs   # REST endpoints
├── Models/
│   ├── TaskItem.cs          # Task entity + Priority enum
│   └── MongoDbSettings.cs   # Config binding model
├── Repositories/
│   ├── ITaskRepository.cs   # Repository interface
│   └── TaskRepository.cs    # MongoDB implementation
├── Program.cs               # App bootstrap & DI
├── appsettings.json         # Configuration (update MongoDB URI here)
└── TaskManager.csproj       # Project file
```
