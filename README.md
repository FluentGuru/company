# Overview

Showcase of a comprehensive API with end-to-end on asp.net core

# Features

- JWT Auth
- REST compliance 
- Uni testing
- Integration testing
- Docker build
- Docker compose deploy

# Stack
.Net Core 3.1
Asp .Net Core 3.1
EntityFrameworkCore 3
MediaTr
FluentValidation
Docker for windows
Docker compose
XUnit
NSubstitute
SQL Server 2019

# Tools
dotnet cli
Docker for Windows
Docker compose
Visual Studio 2019
Visual Studio Code
Sql Server Management Studio


# Getting started

- Install dotnet 3.1
- Install Visual Studio or Visual studio Code
- Install Docker and Docker compose

# Running

## Running local development

- Go to `src/api/Api`
- Restore dependencies by running `dotnet restore`
- Open the configuration file `src/api/Api/appsettings.Development.json`
- Change the `ConnectionString` inside the configuration to your correct sql server database configuration
  ```
  "ConnectionString": "Server=WINDOWS-B21L2V2;Database=Company;Trusted_Connection=True;App=CompanyAPI",
  ```
- Go to `src/api/Api` and run `dotnet run`

## Run with Docker
- Make sure to have `Docker for Windows` installed
- Go to `src/api`
- Run `docker build . -t company:lastest`
- Run `docker run company:lastest -e ConnectionString=YOUR_DATABASE_CONNECTIONSTRING`

## Run with Docker compose
- Move to `src/deploy`
- Run `docker-compose build`
- Run `docker-compose up`

# Testing

## Run unit tests
- Move to `src/tests/Company.UnitTests`
- Run `dotnet test`

## Run integration tests
- Move to `src/tests/Company.IntegrationTests`
- Run `dotnet test`

# Configuration

## Setting adming user
- go to `src/api/Api/appsettings.json`
- Change the username and password on the `Auth` settings
```
"Auth": {
    "UserName": "superadmin",
    "Password": "P@ss123"
  }
```

## Setting up JWT
- go to `src/api/Api/appsettings.json`
- Change the Issuer and key on the `Jwt` Settings
```
"Jwt": {
    "Issuer": "Company",
    "Key": "0d734a1dc94fe5a914185f45197ea846"
  },
```
  
