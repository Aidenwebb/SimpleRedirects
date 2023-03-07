namespace SimpleRedirects.Core.Entities;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetManyAsync(IEnumerable<Guid> ids);
}