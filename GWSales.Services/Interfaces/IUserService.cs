using GWSales.Services.Models;
using GWSales.WebApi.Models.User;

namespace GWSales.Services.Interfaces;

public interface IUserService
{
    Task<CommandResult<ResultType, RegisterUserDto>> RegisterUserAsync(RegisterUserDto userDto);
    Task<CommandResult<ResultType, RegisterUserDto>> RegisterAdminAsync(RegisterUserDto adminDto);
    Task<CommandResult<ResultType, GetUserRolesDto>> LoginUserAsync(LoginUserDto userDto);
    Task<CommandResult<ResultType, ConfirmUserDto>> ConfirmUserByAdminAsync(ConfirmUserDto confirmUserDto);
    Task<CommandResult<ResultType, GetUserListDto>> GetUsersWithoutConfirmationAsync();
}
