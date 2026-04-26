# Student Information System

ASP.NET Core MVC (net9.0) sample application implementing a Student Information System with Entity Framework Core (Code-First) and SQL Server.

## Features
- 6 domain entities: Department, Course, Instructor, Student, Subject, Enrollment.
- Relationship mapping:
  - One-to-many: Department→Courses, Department→Instructors, Course→Students, Instructor→Subjects.
  - Many-to-many between Student and Subject through Enrollment.
- Full CRUD controllers and Razor views for all modules.
- Bootstrap 5 UI with shared navbar for all modules.
- Client-side and server-side validation using Data Annotations + jQuery unobtrusive validation.

## Prerequisites
- .NET SDK 9.0 (or 8.0 with package/version adjustments).
- SQL Server / SQL Server LocalDB.

## Connection String Configuration
Default connection string is in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentInformationSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Update it for your environment if needed.

## Run & Database Setup
1. Restore and build:
   ```bash
   dotnet restore
   dotnet build
   ```
2. Create/update database from migration:
   ```bash
   dotnet ef database update
   ```
3. Run:
   ```bash
   dotnet run
   ```

## Migrations
Initial schema migration is included under `Migrations/`.
