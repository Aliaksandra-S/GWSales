using GWSales.Data.Entities.User;
using GWSales.Data.Interfaces;
using GWSales.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace GWSales.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserRepository(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateUserAsync(RegisterUserModel userModel)
    {
        var user = new UserEntity
        {
            Email = userModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userModel.Username,
            IsConfirmedByAdmin = userModel.IsAdmin,
        };

        return await _userManager.CreateAsync(user, userModel.Password);
    }

    public async Task<UserEntity> FindUserByNameAsync(string? username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<bool> CheckPasswordAsync(UserEntity user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IList<UserEntity>> GetUsersWithoutConfirmationAsync()
    {
        return await _userManager.Users
             .Where(x => x.IsConfirmedByAdmin == false)
             .ToListAsync();
    }

    public async Task<IdentityResult> AddToRoleAsync(UserEntity user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IdentityResult> CreateRoleAsync(string role)
    {
        return await _roleManager.CreateAsync(new IdentityRole(role));
    }

    public async Task<bool> RoleExistsAsync(string role)
    {
        return await _roleManager.RoleExistsAsync(role);
    }

    public async Task<IList<string>> GetUserRolesAsync(UserEntity user)
    {
        return await _userManager.GetRolesAsync(user);
    }

}
