using RT.Comb;

namespace Psa.Core.Utilities;


public class CoreHelpers
{
    /// <summary>
    /// Generate sequential Guid for PostgreSql Server.
    /// ref: https://github.com/richardtallent/RT.Comb
    /// </summary>
    /// <returns>A comb Guid.</returns>
    public static Guid GenerateComb()
    {
        return GenerateComb(Guid.NewGuid(), DateTime.UtcNow);
    }
    
    /// <summary>
    /// Implementation of <see cref="GenerateComb()" /> with input parameters to remove randomness.
    /// This should NOT be used outside of testing.
    /// </summary>
    /// <remarks>
    /// You probably don't want to use this method and instead want to use <see cref="GenerateComb()" /> with no parameters
    /// </remarks>
    public static Guid GenerateComb(Guid guid, DateTime timestamp)
    {
        return Provider.PostgreSql.Create(guid, timestamp);
    }
    
    public static DateTime GetTimestamp(Guid comb)
    {
        return Provider.PostgreSql.GetTimestamp(comb);
    }
}