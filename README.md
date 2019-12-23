# Introduction

A simple dockerized ASP.NET Core App also with Kubernetes

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

### 3. Some useful commands

Stop all containers

```bash
docker stop $(docker ps -a -q)
```

Delete all containers

```bash
docker rm $(docker ps -a -q)
```
