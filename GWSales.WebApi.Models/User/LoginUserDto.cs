using System.ComponentModel.DataAnnotations;

namespace GWSales.WebApi.Models.User;

public class LoginUserDto
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
