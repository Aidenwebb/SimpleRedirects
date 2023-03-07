using Microsoft.AspNetCore.Identity;
using Psa.Core.Utilities;

namespace SimpleRedirects.Core.Entities;

public class Role : IdentityRole<Guid>, ITableObject<Guid>
{
    public void SetNewId()
    {
        Id = CoreHelpers.GenerateComb();
    }
}