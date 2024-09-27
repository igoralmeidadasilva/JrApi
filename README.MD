# Jr.API
Welcome to **Jr.Api**! This project was originally developed as part of a technical test for the Junior level and, over time, has evolved into a space where I practice good programming practices. Jr.Api is a RESTful API that allows user management, offering complete CRUD (Create, Read, Update, Delete) operations on a SQLite database. The goal is to apply concepts such as clean architecture, domain-driven design, good coding practices, and ensure an efficient and easy-to-use API. I hope you enjoy it! 😄

## ✔ Implemented Features
* CRUD (Create, Read, Update, and Delete) in a registered users table.
* CQRS with MediatR
* MediatR Behaviors
* Validations with Fluent Validation
* Read and write repositories
* API versioning
* Distributed cache with Redis
* Global Exception Handler
* API Health Check

## 🏃‍♂️ Practices Used
### REST API
Jr.Api utilizes the principles of a REST API (Representational State Transfer), an architectural standard created by **Roy Fielding** in his doctoral thesis in 2000. This architectural style is widely used in web service development, allowing communication between systems through HTTP requests using verbs like GET, POST, PUT, and DELETE.

Here, I chose to strictly follow the **Richardson Maturity Model**, which is a way to assess an API's compliance with REST principles. The model is divided into four maturity levels:

Level 0 - The Swamp of POX: APIs at level 0 use HTTP only as a transport, without correctly using HTTP resources or verbs. Often, requests are treated as remote procedure calls (RPC).

Level 1 - Resources: At level 1, the API already treats system elements as resources. Each resource has its own URI, but it still does not fully leverage HTTP verbs.

Level 2 - HTTP Verbs: Level 2 APIs start to correctly use HTTP verbs (GET, POST, PUT, DELETE), improving the semantics of operations and approaching the RESTful style.

Level 3 - HATEOAS: The highest level of maturity is reached when the API provides hypermedia (HATEOAS) as part of the responses, allowing the client to navigate resources dynamically, making the interaction with the API self-descriptive and more robust.

**References**: [1](https://www.redhat.com/pt-br/topics/api/what-is-a-rest-api), [2](https://www.sensedia.com.br/pillar/historia-das-apis#:~:text=REST%20foi%20como%20um%20sopro,recursos%20web%20de%20forma%20harmoniosa.), and [3](https://rivaildojunior.medium.com/modelo-de-maturidade-de-richardson-para-apis-rest-8845f93b288)

---

### Domain Driven Design
Jr.Api adopts the principles of Domain Driven Design (DDD) to organize business logic efficiently and scalably. DDD focuses on creating a robust domain model, where the application's rules and operations are centralized. This facilitates code understanding and evolution, allowing business concepts to be represented clearly and concisely.

**References**: [1](https://medium.com/beelabacademy/implementando-na-pr%C3%A1tica-rest-api-com-conceitos-de-ddd-net-2160291a44b7), [2](https://www.macoratti.net/20/07/aspnc_ucddd1.htm), [3](https://medium.com/beelabacademy/implementando-na-pr%C3%A1tica-rest-api-com-conceitos-de-ddd-net-2160291a44b7), and [4](https://www.milanjovanovic.tech/blog/value-objects-in-dotnet-ddd-fundamentals)

---

### Clean Architecture
The project structure follows Clean Architecture standards, which aim to separate application responsibilities into independent layers. This promotes decoupled and maintainable code, facilitating testability and allowing the exchange of frameworks and technologies without impacting business rules. This approach ensures that implementation decisions are isolated from domain rules. DDD and Clean Architecture have a close relationship, with Clean Architecture using many DDD principles in its lower layer.

**References**: [1](https://www.youtube.com/watch?v=1Lcr2c3MVF4&feature=youtu.be), [2](https://www.milanjovanovic.tech/blog/clean-architecture-folder-structure), and [3](https://www.linkedin.com/pulse/como-estruturo-pastas-utilizando-o-clean-architecture-william-g-silva/)

---

### CQRS
Jr.Api implements the CQRS (Command Query Responsibility Segregation) pattern to separate read and write operations, ensuring better organization and clarity in executing operations. With CQRS, commands (write operations) and queries (read operations) are handled independently, allowing for more granular optimization and facilitating the API's evolution without compromising code integrity.

**References**: [1](https://codewithmukesh.com/blog/cqrs-and-mediatr-in-aspnet-core/) and [2](https://www.milanjovanovic.tech/blog/cqrs-pattern-with-mediatr)

---

### Result Pattern
Jr.Api uses the Result Pattern to standardize operation returns, ensuring that methods return a result object instead of throwing exceptions directly. This approach allows for centralized and more efficient handling of errors and successes in operations, increasing the API's robustness and predictability. A custom library was developed to facilitate response handling, containing detailed information about success, failure, and possible error messages.

**References**: [1](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern), [2](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern), [3](https://code-maze.com/aspnetcore-result-pattern/), [4](https://medium.com/@wgyxxbf/result-pattern-a01729f42f8c), and [5](https://github.com/altmann/FluentResults)

---

### Soft Delete
The API implements the Soft Delete strategy in the database, meaning that instead of permanently deleting records, they are marked as deleted. This allows data to be recovered in case of accidental deletion or maintained for auditing and history. An additional field is added to tables to indicate the deletion status of records, and global filters in Entity Framework Core ensure that deleted records are not returned in regular queries.

**References**: [1](https://www.milanjovanovic.tech/blog/implementing-soft-delete-with-ef-core)

---

### Repository Pattern
Jr.Api follows the Repository Pattern to decouple data access logic from upper layers. The project uses Entity Framework Core to implement two types of repositories: ReadOnlyRepository and PersistenceRepository.

* ReadOnlyRepository: Responsible for read queries, where performance and simplicity are sought. Besides using Entity Framework Core, this repository can use Dapper for faster and optimized queries, especially in scenarios where performance is critical.

* PersistenceRepository: Used for operations that modify the application's state, such as insertions, updates, and deletions. This repository manages the lifecycle of database transactions, ensuring data consistency and integrity.

The separation of these repositories allows for a more flexible and optimized architecture, where read and write operations are handled independently, allowing for better optimization and performance of the API.

**References**: [1](https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core), [2](https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/), and [3](https://blog.balta.io/dapper-unit-of-work-repository-pattern/)

---

### Unit Of Work
Jr.Api adopts the Unit of Work pattern to manage transactions within the database context. This pattern ensures that all operations performed in a unit of work (such as multiple write operations) are treated as a single transaction. If all operations are successful, the changes are persisted in the database; otherwise, they are rolled back. This pattern offers greater control and security, preventing data inconsistencies and ensuring that modifications are applied atomically. The Unit of Work implementation is built on top of Entity Framework Core, which facilitates transaction management and provides an additional layer of abstraction for data manipulation.

**References**: [1](https://blog.balta.io/dapper-unit-of-work-repository-pattern/)

---

### API Versioning
Versioning a REST API is essential to ensure that changes and improvements can be introduced without negatively impacting clients that depend on previous versions of the API. At **Jr.Api**, we follow best practices for versioning to ensure compatibility and stability over time.

#### Versioning Strategies Used
At **Jr.Api**, versioning is done through the URL. This way, each new version of the API is accessed through a distinct path, as shown below:

- `/api/v1/users`: Accesses version 1 of the API for user-related resources.
- `/api/v2/users`: Accesses version 2, with possible changes or additions.

This approach ensures that clients using version 1 of the API can continue to function even after newer versions are released, without the need for immediate adjustments.

Additionally, API versioning allows for the introduction of new features or changes in the data structure while maintaining compatibility with clients that still depend on previous versions.

**References**: [1](https://dev.to/iamrule/comprehensive-guide-to-api-versioning-in-net-8-1i9j) e [2](https://github.com/dotnet/aspnet-api-versioning/tree/main)

---

### Documentation via Swagger

Documentation is a fundamental aspect of API development, ensuring that consumers can understand and correctly interact with the available endpoints. At **Jr.Api**, we use **Swagger** to provide clear and interactive documentation.

**Swagger** offers a graphical interface where developers can view and test all API endpoints directly from the browser, facilitating the development and integration process.

#### Accessing the Documentation

The documentation generated by Swagger can be accessed directly when running the project. To view the interface, simply access the following address in your browser:

```
https://localhost:5230/swagger/index.html
```

In the Swagger interface, you will find detailed information about:

- All available endpoints (GET, POST, PUT, DELETE).
- Parameters required for each request.
- Response structure and possible returned HTTP status codes.
- Usage examples and expected results.

Swagger documentation makes understanding the API easier, making it more accessible for developers and users.

**References**: [1](https://medium.com/@celery_liu/asp-net-core-web-api-with-swagger-api-versioning-for-dotnet-8-c8ce2fd7808c) e [2](https://github.com/renatogroffe/ASPNETCore7-REST_API-Swagger-Versioning-Dockerfile_ContagemAcessos)

---

### Distributed Cache with Redis
Distributed caching is a technique used to improve the performance and scalability of an API by storing data in memory for quick and efficient access. In Jr.Api, Redis was chosen as the distributed cache solution due to its high performance and easy integration with .NET applications.

**Performance**: Redis stores data in memory, allowing for extremely fast read and write times.
**Scalability**: As a distributed solution, Redis allows the cache to be shared among multiple instances of the API, making the system more robust and efficient under high load scenarios.

The integration with Redis in Jr.Api is done using the StackExchange.Redis package, which is the official client for .NET.

---

### Health Checks

To ensure the availability and proper functioning of Jr.Api components, we use the Health Checks functionality offered by ASP.NET Core. Through the AspNetCore.HealthChecks and AspNetCore.HealthChecks.UI packages, we can monitor the health of the application and display an interactive control panel. The endpoint to check the health of the application is:
```
http://localhost:5230/health
```

You can also check the Health Check through the Dashboard at:
```
http://localhost:5230/dashboard
```

Currently, JrApi has health checks implemented for SQLite and Redis.

**References**: [1](https://blog.balta.io/aspnet-health-check/) e [2](https://www.youtube.com/watch?v=fuECFrryH68)

## 🎯 Prerequisites
Before setting up and running JrApi, make sure the following prerequisites are met:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) or another IDE of your choice.

---

## 🎥 Running the Project

### Environments

#### Development
To run the project in a development environment, follow these steps:

1. Clone the repository: Clone the project to your local machine using the command:

```console
git clone https://github.com/igoralmeidadasilva/JrApi.git
```

2. Install dependencies: Navigate to the project folder and run the following command to restore NuGet packages:

```console
cd ./src
dotnet restore
```

3. Configure environment variables: Inside the [appsettings.json](src\JrApi.Presentation\appsettings.json) and [appsettings.Development.json](src\JrApi.Presentation\appsettings.Development.json) files, configure the environment variables (such as the database connection string).

4. Run the project: Use the following command to start the server in development mode, or if you are using IDEs like Visual Studio or Visual Studio Code, simply press F5:

```console
dotnet run --launch-profile "Development"
```

5. Access the API: The API will be available at:
```console
http://localhost:5230/swagger/index.html
```

---

#### Production
Not yet implemented.

---

### Migrations
The project uses Entity Framework Core to manage database migrations. To apply migrations, make sure you are inside `src/JrApi.Presentation` and follow the steps below:

1. Add a new migration:
```console
cd ./src/JrApi.Presentation

dotnet ef migrations add YourMigrationName --project ..\JrApi.Infrastructure\JrApi.Infrastructure.csproj
```
2. To update the database with the modifications, you have two options:

* Update the database via command line, still inside `src/JrApi.Presentation`:
```console
dotnet ef database update
```

* Simply run the program with the "IsMigrationActive" option set to "true":
```json
"DatabaseSeedOptions": {
    "IsMigrationActive": true
}
```

---

### Data Seeder
To populate the database with initial data (such as default users, permissions, etc.), the project includes a Data Seeder. To run the seeder, follow the steps below:

Run the Data Seeder: After the project starts, the Data Seeder will automatically run if the "IsUserSeedingActive" option is set to "true".

Predefined Data: The seeder will include basic data to test the API in a development environment, such as example users and default roles. These data can be modified directly in the [appsettings.Development.json](src\JrApi.Presentation\appsettings.Development.json) file by filling in the DatabaseSeedOptions as follows:

```json
"DatabaseSeedOptions": {
    "IsUserSeedingActive": true,
    "Users": [
      {
        "CreatedOnUtc": "01/01/0001",
        "FirstName": { 
          "Value": "John"
        },
        "LastName": {
          "Value": "Doe"
        },
        "Email": {
          "Value": "john.doe@email.com"
        },
        "HashedPassword": {
          "Value": "@Teste123"
        },
        "BirthDate": "01/01/2000",
        "Role": 1,
        "Address": { // Optional
            "Street": "Rua das flores",
            "City": "Cidade Nova",
            "District": "Bairro Residêncial",
            "Number": "99",
            "State": "Espírito Santo",
            "Country": "Brasil",
            "ZipCode": "XXXXX-XXX"
        }
      }
    ]
}
```

Note that the "Users" object is a list, so feel free to create more objects 😃.

---

## ⚙ Endpoints

### User

#### Insert
URL: /api/users

HTTP Method: POST

Description: Creates a new user in the system.

Headers:
* Content-Type: application/json

##### Request Body:
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string",
  "birthDate": "0001-01-01T00:00:00.550Z",
  "address": {
    "street": "string",
    "city": "string",
    "district": "string",
    "number": 0,
    "state": "string",
    "country": "string",
    "zipCode": "string"
  }
}
```

##### Responses:
201 Created: 
The user was successfully created.
```json
No content for response
```

400 Bad Request: Validation errors or missing data.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

409 Conflict: An email already exists.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

#### Update
URL: /api/users/{id}

HTTP Method: PUT

Description: Updates an existing user's data.

Parameters:
*  id (required): User ID.

Headers:
* Content-Type: application/json

##### Request Body:
```json 
{
  "firstName": "string",
  "lastName": "string",
  "birthDate": "2024-09-24T14:20:59.793Z",
  "address": {
    "street": "string",
    "city": "string",
    "district": "string",
    "number": 0,
    "state": "string",
    "country": "string",
    "zipCode": "string"
  }
}
```

##### Responses:

204 No Content: The user was successfully updated.
```json
No content for response
```

400 Bad Request: Validation errors or missing data.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

404 Not Found: User not found.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

#### Delete
URL: /api/users/{id}

HTTP Method: DELETE

Description: Removes an existing user.

Parameters:
*  id (required): User ID.

Headers:
* Content-Type: application/json

##### Request Body:
```json 
No content in request body
```

##### Responses:
204 No Content: 
The user was successfully removed.
```json
No content for response
```

404 Not Found: User not found.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

#### GetAll
URL: /api/users

HTTP Method: GET

Description: Retrieves a list of existing users.

Headers:
* Content-Type: application/json

##### Request Body:
```json 
No content in request body
```

##### Responses:
200 Ok: The list of users was successfully retrieved.
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "createdOnUtc": "2024-09-24T14:31:03.342Z",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "birthDate": "2024-09-24T14:31:03.342Z"
  }
]
```

---

#### GetById
URL: /api/users/{id}

HTTP Method: GET

Description: Retrieves an existing user by ID.

Parameters:
*  id (required): User ID.

Headers:
* Content-Type: application/json

##### Request Body:
```json 
No content in request body
```

##### Responses:
200 Ok: The user was successfully retrieved.
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdOnUtc": "2024-09-24T14:33:24.551Z",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "role": "string",
  "street": "string",
  "city": "string",
  "district": "string",
  "number": 0,
  "state": "string",
  "country": "string",
  "zipCode": "string",
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

404 Not Found: User not found.
```json
[
  {
    "code": "",
    "message": "",
    "type": 0
  },
  {
    "code": "",
    "message": "",
    "type": 0
  }
]
```

---

## 🤲 Acknowledgements
I would like to express my gratitude to everyone who contributed to the development of this project:

* Mentors and colleagues: For their guidance and support throughout the journey, sharing valuable knowledge about good programming practices and software architecture.
* Developer community: For the countless resources, tutorials, and forums that helped overcome technical challenges.
* Family and friends: For their continuous support and encouragement during the creation of this project.
* You: For taking the time to explore this project. I hope it serves as a learning experience or inspiration in some way.
To all, my sincere thank you!

---

## 🧔 About Me
My name is **Igor Almeida da Silva**, I am a backend developer specializing in the **.NET** platform and a student of **Bachelor of Information Systems at the Federal Institute of Espírito Santo (IFES)**, expected to graduate in December 2024.

I am passionate about good development practices and always strive to improve my skills by applying concepts such as Domain-Driven Design (DDD), Clean Architecture, and CQRS. My focus is on creating robust and scalable solutions with clean and well-organized code. Additionally, I have experience with SQL and NoSQL databases, messaging services, and microservices.

Programming started as a curiosity but quickly became a passion. In my projects, I try to apply everything I learn and continue learning, whether in APIs, software architecture, or integrating complex systems.

When I'm not programming, I enjoy watching movies, playing games, and studying to continue growing as a professional and person.

Feel free to explore my projects and get in touch. I am always open to feedback, collaborations, and new learning opportunities!

[![Linkedin](https://img.shields.io/badge/-Linkedin-blue?style=for-the-badge&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/igoralmeidadasilva/)](https://www.linkedin.com/in/igoralmeidadasilva/)
[![Gmail](https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:igor.almeidadasilva13@gmail.com)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/igoralmeidadasilva)

