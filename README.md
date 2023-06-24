# Order Management System REST API

This project is a simplified Order Management System developed using C#, .NET, Microsoft SQL Server, Entity Framework, T-SQL, and stored procedures. The system includes REST API endpoints to create and read data for two entities: Products, and Orders.

The project adheres to SOLID principles and utilizes the following design patterns: Dependency Injection (DI), Repository Pattern, and Unit of Work.

It also uses JWT with Identify as an Authentication strategy.

## Project Setup and Running

### Prerequisites

1. Docker
2. .NET SDK (6.0)

### Instructions

1. Clone the repository: 
    ```bash 
    git clone https://github.com/Caminare/order-mngmt.git
2. Navigate to the project directory: 
    ```bash 
    cd order-mngmt
3. Run Docker Compose: 
    ```bash 
    docker-compose up
This will start the application and SQL Server in separate Docker containers. The application will be accessible at http://localhost:7269.


## API Documentation
You can access the API documentation (provided by Swagger UI) by navigating to http://localhost:7269/swagger in your web browser.

### Database Access
The application uses Microsoft SQL Server. The database credentials are defined in docker compose file by default:

Server: localhost
Port: 1433
User: sa
Password: r00t.R00T

But feel free to change it!

## Authentication
The API utilizes JWT (JSON Web Token) for authentication. To authenticate, first create an account if you don't have in  /api/UserAuth and after that generate an bearer token in /api/UserAuth/login to use in subsequent requests.

## Running Tests
This project uses xUnit for unit testing. To run the tests, execute the following command in the root project directory:
```bash 
dotnet test