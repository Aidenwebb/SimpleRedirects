using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleRedirects.Core.Entities;
using SimpleRedirects.Core.Extensions;
using SimpleRedirects.Data.Configurations;

namespace SimpleRedirects.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(x => x.MigrationsHistoryTable("__ef_migrations_history"))
            .UseSnakeCaseNamingConvention();

      protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Convert database entities to snake case
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            // Replace table names
            entity.SetTableName(entity.GetTableName().ToSnakeCase());

            // Replace column names            
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.Name.ToSnakeCase());
            }
            
            // Replace key names
            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName().ToSnakeCase());
            }

            // Replace ForeignKey names
            foreach (var key in entity.GetForeignKeys())
            {
                key.PrincipalKey.SetName(key.PrincipalKey.GetName().ToSnakeCase());
            }

            // Replace index names
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
            }
        }
        
        // Configure snake case naming convention on Identity models
        builder
            .ApplyConfiguration(new IdentityUserTokenConfiguration())
            .ApplyConfiguration(new IdentityUserLoginConfiguration())
            .ApplyConfiguration(new IdentityUserClaimConfiguration())

            .ApplyConfiguration(new IdentityUserRoleConfiguration())
            .ApplyConfiguration(new IdentityRoleClaimConfiguration());

        // Configure custom models
        
    }
}