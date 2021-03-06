# Introduction

Hi there, I am doing a complete API but with a simple Domain in order to finish an exrecise.  
I forked (technically I got the commits) from [my previous repository](https://github.com/kenny-reyes/DotNetCqrsApi) which was not finished yet.

## :ballot_box_with_check: &nbsp;ToDo

- [x] Finish writting README
- [x] Swagger customization
- [x] Contenize API with Docker watcher
- [x] Add more unit tests and use mocks
- [x] Finish integration tests
- [ ] Develop a SPA, not choosen the framework yet (React, VUE or [Svelte](https://svelte.dev/))
- [x] Fix all bugs

## :rocket: &nbsp;Getting Started

### 1. Installation process

1. [Install Docker](https://www.docker.com/products/docker-desktop)
2. [Install DotNet Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)

### 2. Run and use watching

The project is ready to develop in the watching mode, the dotnet watching process will detect every change in source code and rebuild and run the application automatically.

#### Running from docker command line

Start SqleServer first

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password_123' -e 'MSSQL_PID=Express' -p 1433:1433 --name sqlserver mcr.microsoft.com/mssql/server
```

**NOTE:** Be careful you aren't running the local SqlServer instance in the port 1433

Build container

```bash
docker build -t apiexercise -f Dockerfile-watching .
```

Start Application

```bash
docker run -p:5000:5000 -p:5001:5001 --mount type=bind,source="$(pwd)",target=/app -t --link sqlserver --name apiexercise apiexercise
```

**NOTE:** Be careful you are sharing the unit with docker for create the volume, this is used for enable watching in DotNet Core.

#### Running from docker-compose

From the root type in the bash

```bash
docker-compose.exe -f docker-compose-watching.yaml up
```

#### App docker url

In any cases the app will be running in https://localhost:5000 or http://localhost:5001 with docker and if we prefer to run it locally (start button) it will be on https://localhost:3000 or https://localhost:3001 as you can see in the **launchSettings.json** file.

**NOTE:** Take care of don't have the docker and the local running at the same time because docker uses the same folder and it won't copy the files.

### 3. Some useful commands

Stop all containers

```bash
docker stop $(docker ps -a -q)
```

Delete all containers

```bash
docker rm $(docker ps -a -q)
```

Connect with running apiexercise container

```bash
docker exec -it apiexercise bash
```

### 4. Migrations

Install Entity Framework cli:

```bash
dotnet tool install --global dotnet-ef
```

For creating new megrations use the next commnad.

```bash
dotnet ef migrations add "MigrationNameCamelCase" --project "src\NetCoreApiScaffolding.Infrastructure\NetCoreApiScaffolding.Infrastructure.csproj"  --startup-project "src\NetCoreApiScaffolding.Host"
```

