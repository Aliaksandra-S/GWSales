﻿namespace GWSales.Services.Models.User;

public class RegisterUserModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsAdmin { get; set; }
}
