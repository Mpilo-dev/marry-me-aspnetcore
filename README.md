### 📁 Back-End (C# .NET 9 + ASP.NET Core Web API + Entity Framework Core)

# Marry-Me API

This is the back-end API for the **Marry-Me** application. It handles user authentication and operations for persons, marriages, and divorces using PostgreSQL and Entity Framework Core.

🚧 This project is a work in progress and will continue to evolve as I actively grow and apply new skills throughout my development journey.

## 🛠 Tech Stack

- C# .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT for authentication
- FluentValidation for validation

## 🛠 DevOps

- CI/CD (Github Actions)
- AWS(EC2)

## 📦 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Mpilo-dev/marry-me-api.git
cd marry-me-api
```

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Configure the Environment Variables

#### Create or edit your appsettings.Development.json:

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=marryme_db;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Key": "your-super-secret-key",
    "Issuer": "MarryMeAPI",
    "Audience": "MarryMeAPIUsers",
    "ExpiresInMinutes": 1440
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}

```

### 4. Run EF Migrations

```bash
dotnet ef database update
```

### 5. Start the Server

```bash
dotnet run

This will run the API locally
```

## API Documentation Link

```bash
https://app.swaggerhub.com/apis-docs/omni-2c8/marry-me/1.0
```
