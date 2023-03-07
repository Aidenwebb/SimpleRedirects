using SimpleRedirects.Core.Entities;

namespace SimpleRedirects.Core.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetManyAsync(IEnumerable<Guid> ids);
}