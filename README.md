# User Service Docker Setup

This document provides essential commands for managing the Docker setup of the User Service application. Below are the commands listing and an overview of the project's structure.

## Project Structure

- **TSMWS.UserService.Api**: The main entry point for the User Service application, hosting the API interfaces.
- **TSMWS.UserService.Business**: Contains business logic and services used by the application.
- **TSMWS.UserService.Data**: Manages data access and interactions with the underlying database.
- **TSMWS.UserService.Configurations**: Holds configuration settings and service setup extensions.
- **TSMWS.UserService.Shared**: Contains shared models and utilities used across different projects.

## Docker Commands

### 1. Docker Compose for docker-compose.prod.yml (All services).

```bash
docker-compose -f docker-compose.prod.yml -p tswms up --pull always --detach
```
