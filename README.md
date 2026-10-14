# BizTrack

A REST API for managing tasks and projects, built with ASP.NET Core (.NET 8), Entity Framework Core and SQLite. It was developed as a university project and is meant to be explored with an API client such as Postman.

## Features

- Create, list, delete and complete / uncomplete tasks
- Tasks have a priority, a status, an assignee, a due date and belong to a project
- Projects with participating users; tasks can be attached to projects
- Comments on tasks, written by a user
- Per-user overview of the tasks and projects they take part in
- Database created and migrated automatically on startup, with seed data

## Tech stack

| Area | Technology |
|------|------------|
| Language / framework | C#, ASP.NET Core (.NET 8) |
| Data access | Entity Framework Core 9 with migrations |
| Database | SQLite (`todo.db`, created on first run) |
| Testing | Postman |

## Data model

`TodoItem`, `Project`, `User`, `Role`, `Status`, `Priority`, `Comment`, plus join entities `UserRole`, `ProjectUser`, `TodoItemProject` and `CommentUser`. Priorities (Low, Medium, High) are seeded by the model; other seed data is added by `SeedData` on startup.

## Getting started

Requirements: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```bash
git clone https://github.com/berkluciano/BizTrack.git
cd BizTrack
dotnet run
```

The API listens on `http://localhost:5160` (and `https://localhost:7049`). On first run the SQLite database is created and migrations are applied automatically. There is no web page at `/`; use Postman or any HTTP client.

## API endpoints

Base URL: `http://localhost:5160`

| Method | Path | Description |
|--------|------|-------------|
| GET | `/api/todoitems` | List all tasks with priority, status and project name |
| POST | `/api/todoitems` | Create a task (returns 400 if the priority, status or project does not exist) |
| DELETE | `/api/todoitems/{id}` | Delete a task |
| POST | `/api/todoitems/{id}/complete` | Mark a task as completed |
| POST | `/api/todoitems/{id}/uncomplete` | Mark a task as not completed |
| GET | `/api/todoitems/check-status` | List all statuses |
| GET | `/api/todoitems/comments` | List all comments |
| GET | `/api/todoitems/tasks/{taskId}/comments` | List the comments of a task |
| POST | `/api/todoitems/tasks/{taskId}/comments` | Add a comment to a task |
| GET | `/api/todoitems/users/{userId}/tasks-projects` | Tasks and projects of a user |
| GET | `/api/todoitems/projects/{projectId}/users` | Users in a project |
| GET | `/api/todoitems/projects/{projectId}/tasks` | Tasks in a project |
| POST | `/api/projects/{projectId}/users/{userId}` | Add a user to a project |
| POST | `/api/projects/{projectId}/tasks/{todoId}` | Add a task to a project |

### Examples (Postman)

**Create a task**: `POST /api/todoitems`

```json
{
  "title": "Test task",
  "description": "Test from Postman",
  "assignedTo": "Ivan",
  "dueDate": "2026-01-21T00:00:00",
  "priorityId": 2,
  "statusId": 1,
  "projectId": 1,
  "isCompleted": false
}
```

**Add a comment to task 1**: `POST /api/todoitems/tasks/1/comments`

```json
{
  "content": "This is a new comment from Postman",
  "userId": 1
}
```

**Other requests** (no body needed):

- Complete / uncomplete task 3: `POST /api/todoitems/3/complete` or `/uncomplete`
- All comments: `GET /api/todoitems/comments`
- Comments for a task: `GET /api/todoitems/tasks/1/comments`
- Tasks in project 1: `GET /api/todoitems/projects/1/tasks`
- Users in project 1: `GET /api/todoitems/projects/1/users`
- Projects and tasks of user 1: `GET /api/todoitems/users/1/tasks-projects`
- Add user 2 to project 1: `POST /api/projects/1/users/2`
- Add task 3 to project 1: `POST /api/projects/1/tasks/3`

Priority ids are `1` (Low), `2` (Medium) and `3` (High). Use `GET /api/todoitems/check-status` to list the available statuses.

## Limitations

- Users and roles exist in the data model, but authentication and authorization are not implemented: every endpoint is open.
- Users, roles and projects cannot yet be created through the API; they come from the seed data.
- The project is a coursework prototype, not production software.
