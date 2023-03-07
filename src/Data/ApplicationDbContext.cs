using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleRedirects.Core.Extensions;
using SimpleRedirects.Data.Models;

namespace SimpleRedirects.Data;

public class ApplicationDbContext : DbContext // IdentityDbContext<User, Role, Guid>
{
    public const string postgresIndetermanisticCollation = "postgresIndetermanisticCollation";

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Scans and loads all configurations implementing the `IEntityTypeConfiguration` interface.
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        var eUser = builder.Entity<User>();

        eUser.Property(c => c.Id).ValueGeneratedNever();


        eUser.ToTable(nameof(User));

        if (Database.IsNpgsql())
        {
            // the postgres provider doesn't currently support database level non-deterministic collations.
            // see https://www.npgsql.org/efcore/misc/collations-and-case-sensitivity.html#database-collation
            builder.HasCollation(postgresIndetermanisticCollation, "en-u-ks-primary", "icu",
                false);
            eUser.Property(e => e.Email).UseCollation(postgresIndetermanisticCollation);
            //

            base.OnModelCreating(builder);

            // Convert database entities to snake case
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                // Replace column names            
                foreach (var property in entity.GetProperties()) property.SetColumnName(property.Name.ToSnakeCase());

                // Replace key names
                foreach (var key in entity.GetKeys()) key.SetName(key.GetName().ToSnakeCase());

                // Replace ForeignKey names
                foreach (var key in entity.GetForeignKeys())
                    key.PrincipalKey.SetName(key.PrincipalKey.GetName().ToSnakeCase());

                // Replace index names
                foreach (var index in entity.GetIndexes()) index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
            }
        }

        ConfigureDateTimeUtcQueries(builder);
    }

    private void ConfigureDateTimeUtcQueries(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (entityType.IsKeyless) continue;
            foreach (var property in entityType.GetProperties())
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    property.SetValueConverter(
                        new ValueConverter<DateTime, DateTime>(
                            v => v,
                            v => new DateTime(v.Ticks, DateTimeKind.Utc)));
        }
    }
}