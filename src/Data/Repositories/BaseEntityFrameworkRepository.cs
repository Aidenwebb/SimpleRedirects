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

    protected IServiceScopeFactory ServiceScopeFactory { get; private set; }
    protected IMapper Mapper { get; private set; }

    public ApplicationDbContext GetDatabaseContext(IServiceScope serviceScope)
    {
        return serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}