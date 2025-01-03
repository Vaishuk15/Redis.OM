# Redis.OM in PersonalPasswordManagerAPI

PersonalPasswordManagerAPI is a .NET 8.0 API designed for managing passwords and Redis.OM object mapping is used in order to store the value as json and key as string.
## Table of Contents

- [PersonalPasswordManagerAPI](#personalpasswordmanagerapi)
  - [Table of Contents](#table-of-contents)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Application Setup](#application-setup)

## Prerequisites

Ensure you have the following installed on your machine:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started) (if using Docker)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or another compatible IDE (optional)

## Installation

1. **Clone the repository:**

   ```
   git clone repo-url
   ```
2. ** Run Docker file:**
      ```
     Build: docker build -t <image-name> .
     Run: docker run -d -p 1433:1433 --name <container-name> -e ACCEPT_EULA=Y -e SA_PASSWORD=vaishu@15 -v sqlserver-data:/var/opt/mssql <image-name>
       ```
3. ** Run Sql Script to create database and tables:**
     Run the script in the SqlServerManagementStudio
     File: init.sql
4. ** For Redis-stack docker set-up **
docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 redis/redis-stack:latest   

##  Application Setup

Scaffold the database(db first approach):**
   1.  Scaffold the database using the following command:
     ```
     Scaffold-DbContext "Server=localhost;Database=PasswordManagerDB;User Id=sa;Password=vaishu@15;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
     ```
   2.  The models and dbContext are created.
     The nuget packages are installed.
     ```Microsoft.EntityFrameworkCore.Tools
        Microsoft.EntityFrameworkCore.SqlServer
        Microsoft.EntityFrameworkCore.Design
     ```
   3.  Project Reference is added for the class library project like Services,Repositories.
     Run the application using IISExpress.
     





  
