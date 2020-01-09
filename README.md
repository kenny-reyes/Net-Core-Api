# Introduction

A simple Api to show how to pattern CQRS.  
I started from [my previous repository](https://github.com/kenny-reyes/AspNetCoreDockerExample) for take advantage of the Docker configuration.

## Getting Started

### 1. Installation process

1. [Install Docker](https://www.docker.com/products/docker-desktop)
2. Enable Kubernetes in Docker Destop
3. [Install Net Core](https://dotnet.microsoft.com/download/dotnet-core/2.2)

### 2. Run

#### Running from docker command line

Start SqleServer first

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password_123' -e 'MSSQL_PID=Express' -p 1433:1433 --name sqlserver mcr.microsoft.com/mssql/server
```

**NOTE:** Be careful you aren't running the local SqlServer instance in the port 1433

Build container

```bash
docker build -t frontend .
```

Start Application

```bash
docker run -p:5000:5000 -p:5001:5001 --mount type=bind,source="$(pwd)",target=/app -t --link sqlserver --name frontend frontend
```

**NOTE:** Be careful you are sharing the unit with docker for create the volume, this is used for enable watching in DotNet Core.

#### Running from docker-compose

From the root type in the bash

```bash
docker-compose up
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

Connect with running frontend container

```bash
docker exec -it frontend bash
```
