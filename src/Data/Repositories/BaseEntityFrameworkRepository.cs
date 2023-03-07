using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleRedirects.Data.Repositories;

public abstract class BaseEntityFrameworkRepository
{
    public BaseEntityFrameworkRepository(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Mapper = mapper;
    }

    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IMapper Mapper { get; }

    public ApplicationDbContext GetDatabaseContext(IServiceScope serviceScope)
    {
        return serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}