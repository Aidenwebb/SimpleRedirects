using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SimpleRedirects.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        configuration.SetBasePath(Directory.GetCurrentDirectory());
        configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        
        var connectionString = configuration.GetConnectionString("Default");

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        dbContextOptionsBuilder.UseNpgsql(connectionString);

        return new ApplicationDbContext(dbContextOptionsBuilder.Options);
    }
}