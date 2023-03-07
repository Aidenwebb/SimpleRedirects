using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Psa.Core.Utilities;

namespace SimpleRedirects.Core.Entities;

public class AppUser : IdentityUser<Guid>
{
    public void SetNewId()
    {
        Id = CoreHelpers.GenerateComb();
    }
    
    public Guid? GetUserId()
    {
        return Id;
    }
    
}