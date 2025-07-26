# Vertical-Slice-Architecture-in-.NET-9

This project demonstrates the Vertical Slice Architecture pattern in a .NET 9 Web API for managing video games. It uses MediatR for request/response handling, Entity Framework Core for data access, and exposes RESTful endpoints for CRUD operations.

## Features

- **Create Video Game**: `POST /api/games`
- **Get All Video Games**: `GET /api/games`
- **Update Video Game**: `PUT /api/games/{id}`
- **Delete Video Game**: `DELETE /api/games/{id}`

## Technologies

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- MediatR
- Scalar.AspNetCore (API Documentation)
- OpenAPI (Swagger)

## Getting Started

1. **Clone the repository**
2. **Configure the connection string** in `appsettings.json` under `DefaultConnection`.
3. **Run database migrations** (if needed):


## License

This project is provided for educational purposes.