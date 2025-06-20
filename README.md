# Task Management API

A Task Management API built with ASP.NET Core 8.0, featuring user authentication, task management.

## Features

- **User Authentication**: JWT-based authentication system
- **Task Management**: Complete CRUD operations for tasks
- **Secure API**: Protected endpoints with Bearer token authentication
- **Containerized**: Docker and Docker Compose support
- **Database**: SQL Server with Entity Framework Core
- **API Documentation**: Interactive Swagger documentation

## Getting Started

### Prerequisites

- Docker Desktop
- .NET 8.0 SDK (for local development)

### Quick Start with Docker

1. **Clone the repository**

2. **Run with Docker Compose**
   ```bash
   docker-compose up --build
   ```

3. **Access the API**
   - API Base URL: `http://localhost:5000`
   - Swagger Documentation: `http://localhost:5000/swagger/index.html`


## API Documentation

### Authentication

The API uses JWT Bearer token authentication. To access protected endpoints:

1. Register a new user or login with existing credentials
2. Copy the JWT token from the response
3. Click "Authorize" in Swagger UI
4. Enter: `Bearer <jwt-token>`

### Main Endpoints

- **POST** `/api/auth/register` - Register a new user
- **POST** `/api/auth/login` - Login and get JWT token
- **GET** `/api/tasks` - Get all tasks (authenticated)
- **POST** `/api/tasks` - Create a new task (authenticated)
- **GET** `/api/tasks/{id}` - Get task by ID (authenticated)
- **PUT** `/api/tasks/{id}` - Update a task (authenticated)
- **DELETE** `/api/tasks/{id}` - Delete a task (authenticated)

## Development Commands

### Docker Commands
```bash
# Build and start services
docker-compose up --build

# Stop services
docker-compose down

# View logs
docker-compose logs -f taskmanagement-api


```
