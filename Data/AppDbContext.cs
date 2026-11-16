using Microsoft.EntityFrameworkCore;
using JwtRoleAPI.Models;

namespace JwtRoleAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Roles
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "Admin"
            },
            new Role
            {
                Id = 2,
                Name = "User"
            }
        );

        // Users - passwords are BCrypt hashes (cost 12), never plaintext.
        // Demo logins still work: admin / admin123, user1 / user123,
        // user2 / user123. The hashes below were generated once from
        // those passwords, so the seed data stays stable across runs.
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "$2b$12$kaJJp7tERkzn8LTGOR3v3OaIvzMI3xDGWCQEpF8UCdRHp9tz0izPy",
                RoleId = 1
            },
            new User
            {
                Id = 2,
                Username = "user1",
                Password = "$2b$12$GEEXWPxpJoTC06QbhOGEs.RPq5HvNo/MbI3f8FO.cEzoN/h8BOVB6",
                RoleId = 2
            },
            new User
            {
                Id = 3,
                Username = "user2",
                Password = "$2b$12$GEEXWPxpJoTC06QbhOGEs.RPq5HvNo/MbI3f8FO.cEzoN/h8BOVB6",
                RoleId = 2
            }
        );
    }
}