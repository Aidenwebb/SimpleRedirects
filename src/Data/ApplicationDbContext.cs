using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleRedirects.Core.Entities;

namespace SimpleRedirects.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(x => x.MigrationsHistoryTable("__ef_migrations_history"))
            .UseSnakeCaseNamingConvention();
    
    public DbSet<User> Users { get; set; }
}