using System.ComponentModel.DataAnnotations;
using Psa.Core.Utilities;

namespace SimpleRedirects.Core.Entities;

public class User : ITableObject<Guid>
{
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public void SetNewId()
    {
        Id = CoreHelpers.GenerateComb();
    }
    
    public Guid? GetUserId()
    {
        return Id;
    }
    
}