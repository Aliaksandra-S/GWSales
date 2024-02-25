using GWSales.Data.Entities.User;
using GWSales.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace GWSales.Data.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult> CreateUserAsync(RegisterUserModel userModel);
    Task<UserEntity> FindUserByNameAsync(string? username);
    Task<bool> CheckPasswordAsync(UserEntity user, string password);
    Task<IList<UserEntity>> GetUsersWithoutConfirmationAsync();
    Task<IdentityResult> CreateRoleAsync(string role);
    Task<bool> RoleExistsAsync(string role);
    Task<IdentityResult> AddToRoleAsync(UserEntity userModel, string role);
    Task<IList<string>> GetUserRolesAsync(UserEntity user);
}
