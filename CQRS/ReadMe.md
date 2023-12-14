
# CQRS

This project showcases simple CQRS usage.

You can use docker compose to run API and Postgres Database.

Projects aims to use CQRS pattern by completely seperating Read and Write operations, creating seperate Repositories for Reads and Writes and seperating models into Commands and Queries.
MediatR library is used in order to find Handler for specific Query or Command and decouple handling of request from Minimal API.

| Command | Endpoint | Description
|--|--|--|
| GET | http://localhost:8080/movies | Gets all Movies |
| GET | http://localhost:8080/movies/{id} | Gets Movie with specific {id} |
| POST | http://localhost:8080/movies | Creates a new Movie |
| PUT | http://localhost:8080/movies/{id} | Updates a Movie with specific {id} |
| DELETE | http://localhost:8080/movies/{id}| Deletes a Movie with specific {id} |

Note: I am aware of Enum DB Type in Postgres Databases. Unfortunately currently it's not possible to map Postgres enum types with Dapper, even using custom SQL Type Handler: https://github.com/DapperLib/Dapper/issues/259