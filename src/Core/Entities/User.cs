using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Psa.Core.Utilities;

namespace SimpleRedirects.Core.Entities;

public class User : ITableObject<Guid>
{
    [MaxLength(50)] public string Name { get; set; }

    [Required] [MaxLength(256)] public string Email { get; set; }

    public bool EmailVerified { get; set; }

    [Required] [MaxLength(50)] public string SecurityStamp { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime RevisionDate { get; set; } = DateTime.UtcNow;
    public Guid Id { get; set; }

    public void SetNewId()
    {
        Id = CoreHelpers.GenerateComb();
    }

    public Guid? GetUserId()
    {
        return Id;
    }

    public bool IsUser()
    {
        return true;
    }
}