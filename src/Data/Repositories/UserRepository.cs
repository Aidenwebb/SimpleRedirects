using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleRedirects.Core.Entities;
using SimpleRedirects.Core.Repositories;

namespace SimpleRedirects.Data.Repositories;

public class UserRepository : Repository<User, Models.User, Guid>, IUserRepository
{
    public UserRepository(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        : base(serviceScopeFactory, mapper, context => context.Users)
    {
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var dbContext = GetDatabaseContext(scope);
            var entity = await GetDbSet(dbContext).FirstOrDefaultAsync(e => e.Email == email);
            return Mapper.Map<User>(entity);
        }
    }

    public async Task<IEnumerable<User>> GetManyAsync(IEnumerable<Guid> ids)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var dbContext = GetDatabaseContext(scope);
            var users = dbContext.Users.Where(x => ids.Contains(x.Id));
            return await users.ToListAsync();
        }
    }

    public override async Task DeleteAsync(User user)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var dbContext = GetDatabaseContext(scope);

            var transaction = await dbContext.Database.BeginTransactionAsync();

            var mappedUser = Mapper.Map<Models.User>(user);
            dbContext.Users.Remove(mappedUser);

            await transaction.CommitAsync();
            await dbContext.SaveChangesAsync();
        }
    }
}