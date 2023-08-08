# JrAPI
Welcome to the Jr API built using ASP.NET, Entity Framework, and SQLite. This API provides endpoints to manage resources related to a SQLite Database named "Users". The following directories are present in this project:

* JrApi: Main directory containing the entire API and the database.
* JrApiTest: Directory containing unit tests for the API.

### Environment Setup
Make sure you have the following software installed in your development environment:

Visual Studio (recommended version)

.NET SDK (recommended version)

SQLite (recommended version)


### API Endpoints
The API has the following endpoints to manage its resources:

GET /api/users: Returns a list of all records in the database.

GET /api/users/{id}: Returns a specific record based on the ID.

POST /api/users: Inserts a new record.

PUT /api/users/{id}: Updates an existing record based on the ID.

DELETE /api/users/{id}: Deletes a record based on the ID.


### Testing Environment
The testing environment has been set up to test all requests that interact with the database, meaning that all endpoints have been tested.





