# School Attendance API — Learning Context

## User goal

The user is coming from a Spring Boot background and wants to properly learn C# and ASP.NET Core by building a student attendance REST service. The target timeline is 10 days, with emphasis on job-interview readiness, architecture, performance debugging, testing, database design, Docker, and practical implementation.

The user is a beginner in C#. Explain C# syntax and .NET concepts as they appear. Use Spring Boot comparisons when helpful, but do not assume C# knowledge.

## Collaboration rules

- The user writes and implements the code.
- The assistant teaches, gives focused next tasks, reviews files, diagnoses errors, and verifies builds/tests when requested.
- Do not modify project files unless the user explicitly asks the assistant to fix or create something.
- Prefer small, understandable steps over large code dumps.
- Explain why a command, project, dependency, or architectural decision exists before asking the user to run it.
- Do not rush into testing before the user understands core C# and ASP.NET Core development; testing will be introduced after the main development flow is understood.
- The user uses VS Code, PowerShell, .NET 10, Docker, and PostgreSQL.

## Current environment

- Workspace: `C:\Users\USER\Documents\dotnet\SchoolAttendance`
- .NET SDK: 10.0.402
- ASP.NET Core runtime: 10.0.13
- Docker: installed
- Docker Compose: installed
- PostgreSQL container: `school-attendance-postgres`
- PostgreSQL image: `postgres:17-alpine`
- Database: `attendance`
- Database user: `attendance_app`
- Local database port: `5432`
- Local API port: `5000` when launched from the API directory with the Development environment.

## Current solution

```text
SchoolAttendance
├── SchoolAttendance.Api
├── SchoolAttendance.Application
├── SchoolAttendance.Domain
├── SchoolAttendance.Infrastructure
└── SchoolAttendance.UnitTests
```

Dependency direction:

```text
Api -> Application -> Domain
Api -> Infrastructure -> Domain
Infrastructure -> Application
UnitTests -> Application/Domain
```

Architecture intent:

- Domain: entities and business rules; no ASP.NET Core or EF Core dependencies.
- Application: use cases and application interfaces.
- Infrastructure: EF Core, PostgreSQL, migrations, and external integrations.
- API: controllers, HTTP models, routing, middleware, configuration, and composition root.

## Completed work

- Created the five-project solution targeting `net10.0`.
- Added project references and standardized Infrastructure naming.
- Ran the generated ASP.NET Core API.
- Learned `Program.cs`, middleware, controllers, routes, and dependency injection.
- Created `IWelcomeService` and `WelcomeService`.
- Created `WelcomeController` and verified `GET /api/welcome`.
- Created the `Student` domain entity with `Guid`, properties, constructor, nullable `DateOnly`, and `ChangeFullName` validation.
- Created `IStudentService` and `StudentService`.
- Created student request/response models and `StudentsController`.
- Added validation attributes to `CreateStudentRequest`.
- Verified `POST /api/students` with Postman.
- Added Docker Compose PostgreSQL and confirmed the container is healthy.
- Added EF Core 10.0.13, EF Core Design 10.0.13, and Npgsql EF Core provider 10.0.3.
- Created `AttendanceDbContext` in `SchoolAttendance.Infrastructure.Persistence`.
- Added the development PostgreSQL connection string and registered `AttendanceDbContext` with `UseNpgsql`.
- Installed `dotnet-ef` 10.0.13.
- Created and applied the `InitialCreate` migration.
- Confirmed PostgreSQL contains `Students` and `__EFMigrationsHistory` tables.
- Replaced the in-memory student creation path with an asynchronous repository flow using `IStudentRepository`, `StudentRepository`, `AttendanceDbContext`, and `SaveChangesAsync`.
- Verified a student can be created through the API and confirmed PostgreSQL contains one row in `Students`.
- Latest build succeeds with 0 warnings and 0 errors.
- Latest test run succeeds: 1 test passed, 0 failed. The test is still the default template test.
- Added a unique PostgreSQL index for `Students.StudentCode` and applied the `AddUniqueStudentCode` migration.
- Translated PostgreSQL unique-violation `23505` errors into `DuplicateStudentCodeException`.
- Added and verified the global `IExceptionHandler`, returning `409 Conflict` with `ProblemDetails` for duplicate student codes.
- Verified the duplicate student response through Postman: HTTP `409` with title and detail.
- Completed Student CRUD for the MVP: create, list, get by ID, patch profile, and soft delete with active-record filtering.
- Added `School` and `Branch` domain entities with soft-delete behavior and validated school/branch ownership modeling.
- Registered `Schools` and `Branches` in EF Core, configured unique school codes, school-scoped branch codes, and restricted branch foreign keys.
- Created and applied the `AddSchoolsAndBranches` migration.

## Important current limitation

The student CRUD read flow and duplicate-code error handling are working. Windows Smart App Control currently blocks locally compiled API `.dll`/`.exe` files under the `VerifiedAndReputableDesktop` policy; this is an operating-system restriction, not an application-code problem. The next major implementation step is to continue the domain model and later add authentication/authorization.

## Planned 10-day learning path

1. Foundation and C#/.NET project structure — completed.
2. ASP.NET Core pipeline, DI, REST flow, and C# fundamentals — mostly completed.
3. EF Core persistence: async DbContext usage, saving/querying students, migrations — next.
4. Multi-branch school domain and database schema: schools, branches, academic sessions, classes, enrollments.
5. Attendance capture: statuses, uniqueness, idempotency, transactions, and business rules.
6. Queries and reports: pagination, filtering, projections, summaries, indexes.
7. Authentication/authorization and branch-aware access.
8. Testing: domain/application unit tests, API integration tests, PostgreSQL test strategy.
9. Performance, logging, debugging, query plans, concurrency, and operational concerns.
10. Docker hardening, documentation, architecture walkthrough, trade-offs, and interview preparation.

## Scope discipline

The 10-day target is a credible MVP, not a complete commercial school platform. Prioritize:

- Multi-branch modeling.
- Student enrollment and attendance capture.
- Correct REST behavior and validation.
- PostgreSQL persistence and migrations.
- Authentication/authorization basics.
- Focused tests and measurable performance checks.

Defer advanced features such as biometric integration, notifications, payroll, complex scheduling, and production cloud deployment unless the core MVP is stable.

## Teaching preferences

- Explain the difference between C# language features and ASP.NET Core framework features.
- Explain interfaces, classes, properties, constructors, namespaces, async/await, LINQ, records, and nullable reference types when encountered.
- Explicitly compare concepts to Spring Boot where useful: DI container, controllers, filters/middleware, configuration, JPA/EF Core, repositories, and migrations.
- Review the user's code before moving to the next step.
- Treat successful builds and runtime behavior as checkpoints, not proof that the architecture is complete.

## Learning approach

- The user is currently often typing instructed code while learning unfamiliar C# and ASP.NET Core concepts. This is acceptable and should not be treated as a failure.
- The user sometimes implements recurring steps independently to verify understanding.
- Continue explaining every important concept and revisit difficult topics.
- Gradually reduce complete code samples and shift toward exercises, prediction, explanation, and independent implementation.
- Frequently ask the user to explain what code does in their own words, but do not overload them with quizzes.
- The goal is genuine understanding of the project and interview-level architecture, not merely a working copied implementation.
- The user prefers instructor-led learning and does not want a separate self-study curriculum. Teach required C#/.NET concepts directly during the project, using external documentation only when useful for verification or reference.
- After the initial 10-day MVP, the user plans to continue approximately 2 hours per day alongside other work for long-term .NET and C# growth.
- Use spaced repetition, progressively harder exercises, code reviews, debugging sessions, and interview questions to reinforce knowledge over the long term.
