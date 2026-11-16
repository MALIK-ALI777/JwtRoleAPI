# JwtRoleAPI

A role-based ASP.NET Core Web API with JWT authentication, built with Entity Framework Core and SQL Server.

Login returns a JWT; Admin and Employee endpoints are protected by role, so only a token with the right role can call them.

## What it does

- `POST /api/auth/login` — log in with a username and password, get back a JWT with the user's role in its claims.
- `AdminController` — endpoints only an `Admin` role token can call.
- `EmployeeController` — endpoints for the `User` role.
- JWT Bearer authentication wired up in `Program.cs`, with Swagger showing the Bearer login.
- Entity Framework Core with SQL Server, code-first migrations, and seeded roles and demo users.

## Stack

ASP.NET Core 10 · Entity Framework Core · SQL Server · JWT Bearer auth · BCrypt password hashing · Swagger / Swashbuckle

## Run it

1. Set your JWT signing key — replace the placeholder in `appsettings.json`, or better, set it with User Secrets or an environment variable (`Jwt:Key`). It must be a long random secret.
2. Point `DefaultConnection` at your SQL Server (it defaults to a local `JwtRoleDB`).
3. `dotnet ef database update` to create the database and seed the roles and demo users.
4. `dotnet run`, then open Swagger at `https://localhost:7228/swagger`.

Demo logins (seeded): `admin` / `admin123` (Admin role), `user1` / `user123` (User role). Passwords are stored as BCrypt hashes, never in plaintext.

## Note

This is a demo/learning project: the seed logins above are public, and the JWT key placeholder in `appsettings.json` must be replaced with a long random secret before use. Keep the real key out of source control (User Secrets or an environment variable).
