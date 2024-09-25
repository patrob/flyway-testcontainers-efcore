# Flyway - Testcontainers - EF Core Integration Testing

This project demonstrates how to set up **integration tests** using **Flyway**, **Testcontainers**, and **Entity Framework Core (EF Core)** with .NET.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Running Tests](#running-tests)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

To run this project, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started) (tested with Docker Desktop 4.32 on Mac)

## About

This project integrates Flyway for database migrations and Testcontainers for creating disposable PostgreSQL containers, allowing for isolated and repeatable **integration tests**. EF Core is used as the ORM to interact with the database. The setup enables easy database seeding, migration management, and clean up during integration testing.

## Getting Started

1. Clone the repository:
    ```bash
    git clone https://github.com/patrob/flyway-testcontainers-efcore.git
    ```

2. Navigate to the project directory:
    ```bash
    cd flyway-testcontainers-efcore
    ```

3. Set up and run Docker containers for PostgreSQL:
    ```bash
    docker-compose up -d
    ```

4. Restore NuGet packages and build the solution:
    ```bash
    dotnet restore
    dotnet build
    ```

## Running Tests

To run the integration tests:

1. Ensure Docker is running.
2. Use the following command:
    ```bash
    dotnet test
    ```

This will run tests that use **Testcontainers** to create a temporary PostgreSQL database, execute **Flyway** migrations to set up the schema, and perform the required **integration tests** using EF Core.

## Project Structure

- **SimpleBlog.Api**: Contains the main API code.
- **SimpleBlog.IntegrationTests**: Contains integration test cases for the application.
- **flyway/sql**: Flyway migration files for setting up the database.
- **docker-compose.yml**: Configuration to run PostgreSQL in a container.

## Contributing

Feel free to submit pull requests to improve this project. If you find any issues or have suggestions, please [open an issue](https://github.com/patrob/flyway-testcontainers-efcore/issues).

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.