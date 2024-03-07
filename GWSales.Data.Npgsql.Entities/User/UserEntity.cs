using Microsoft.AspNetCore.Identity;

namespace GWSales.Data.Entities.User;

public class UserEntity : IdentityUser
{
    public bool IsConfirmedByAdmin { get; set; }
}
