# Features

The Domain is very tiny but the API have and will have many fully features.

## API

- Added **Pagination**
- Filters
- Separate the C-R-UD with mediator, Dapper and EntityFramework
- Apply migrations by code.
- Exception management.
- Added **Serilog**

## Validation

- **Domain validation:** this throw exceptions which are managed and served by _Problems details_ as 500's errors
- **Request validations:** with _fluent validations_, this validations served also by _problems details_ as 400's errors.
- **BD validations:** Via IEntityTypeConfiguration.
  ![](images/ValidationError.PNG)

## Tests

- Added some examples **unit tests and structure** for adding more.
- Added **integration tests** and structure (Not working yet).
- **Seed** database (No coded yet).

## 🚧Containers

- I will **contenize the application** for working in local and hosted.
- I will run the tests inside the container.
- I will activate the .net core **watching** inside the container via sharing volumes.
- I will add **enviroment variables** for the containers.

## 🚧SPA

- I will add a SPA with some framework which support redux for the states management or if it's **React** I could use reactcontext + hooks.
- I will authomatice the building and the launching of the SPA in the whole solution via docker-compose.
- I will use a **NGIX** container to host the static files.

[Go back](Index.md)
