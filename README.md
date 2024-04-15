# Project Setup
1. Create your own ```appsettings.personal.json``` and fill it with the connection string to your database and JWT secret:
```csharp
{
  "Logging": {
    "LogLevel": {
      "Default": "Trace",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "<connection string to db>"
  },
  "JWT": {
    "ValidAudience": "<Audience>",
    "ValidIssuer": "<Issuer>",
    "Secret": "<secret>"
  },
  "Features": {
    "EnableAdminRegistration": "<bool>"
  }
```
2. Apply migrations to database using following commands in cmd (from GWSales.Data.Npgsql project folder):
```
dotnet ef migrations add "<migration name>"
dotnet ef migrations bundle --output migration-tool.exe
migration-tool.exe --connection "<connection string>"
```
3. Run WebApi project and open ```https://localhost:7151/swagger``` :)
