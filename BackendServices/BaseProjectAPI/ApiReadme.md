# Base Project
This project give us a common base for fastest staring on a new project, and tries to avoid spend a lot of time setup all basic plugging and usual integrations

## Setup

### Windows

- Install NetCore SDK
- Configure local variable with below name 'BaseProjectDefaultConnectionString' this will store the connection string to your PostgreSQL database
- Update your database on base the migrations
- Enjoy!!!!

### Production environments
you can leverage several cloud services like [Key Vault on Azure](https://azure.microsoft.com/en-us/services/key-vault/) or [Secret Manager on AWS](https://aws.amazon.com/secrets-manager/) to store sensitive data like connection strings.

## API
This layer implement all services, data retrieves from database, and expose the endpoints to be consumed by the clients

### Patterns
Used patterns in this project, to give a context about you can find here

#### Mediator
- Define an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently.
- Design an intermediary to decouple many peers.
- Promote the many-to-many relationships between interacting peers to "full object status".

A handler can perform complex operations with several services, in this example we use just one entity but can be more useful for complex scenarios

[Official Documentation](https://sourcemaking.com/design_patterns/mediator)
[Official Repository](https://github.com/jbogard/MediatR)


### End point documentation
[Official Documentation](https://swagger.io)
[Official Repository](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

### Healtcheck
[Official Documentation](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/monitor-app-health)

https://www.c-sharpcorner.com/article/health-check-using-asp-net-core/
https://volosoft.com/Blog/Using-Health-Checks-in-ASP.NET-Boilerplate
https://blog.zhaytam.com/2020/04/30/health-checks-aspnetcore/

### Monitoring
[Official Documentation](https://serilog.net)
[Official Repository](https://github.com/serilog/serilog)
[Official Repository]()
[Official Repository]()
[Official Repository]()

### Input Model Validations
FluenValidator
[Official Documentation](https://docs.fluentvalidation.net/en/latest/installation.html)

### Mapping data between objects
[Official Documentation](https://automapper.org)


### DataBase

### Enitity Framework Core

#### PostgreSql
Npgsql
[Official Documentation](https://www.npgsql.org/efcore/index.html)
[Official Repository](https://github.com/npgsql/npgsql)

### Seed Data



## Testing
This layer is on charge to perform several kind of tests to the base project

##Source Packages

[Nuget.org](https://www.nuget.org)

