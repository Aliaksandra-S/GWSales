using AutoMapper;
using GWSales.Data.Interfaces;
using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.Services.Models.User;
using GWSales.WebApi.Models.User;

namespace GWSales.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<CommandResult<ResultType, RegisterUserDto>> RegisterAdminAsync(RegisterUserDto adminDto)
    {
        var result = new CommandResult<ResultType, RegisterUserDto>();
        var hasAdminRole = await _userRepository.RoleExistsAsync(UserRole.Admin);
        if (hasAdminRole)
        {
            if ((await _userRepository.GetUsersInRoleAsync(UserRole.Admin)).Any())
            {
                result.ResultType = ResultType.Failed;
                result.Messages?.Add("Admin already exist!");

                return result;
            }
        }
        else
        {
            await _userRepository.CreateRoleAsync(UserRole.Admin);
        }

        var hasUserRole = await _userRepository.RoleExistsAsync(UserRole.User);
        if (!hasUserRole)
        {
            await _userRepository.CreateRoleAsync(UserRole.User);
        }

        var model = _mapper.Map<RegisterUserModel>(adminDto);
        model.IsAdmin = true;
        
        var createResult = await _userRepository.CreateUserAsync(model);
        if (!createResult.Succeeded)
        {
            result.ResultType = ResultType.Failed;
            result.Messages?.AddRange(createResult.Errors.Select(x => x.Description).ToArray());

            return result;
        }

        var user = await _userRepository.FindUserByNameAsync(model.Username);
        await _userRepository.AddToRoleAsync(user, UserRole.Admin);
        await _userRepository.AddToRoleAsync(user, UserRole.User);
       
        result.ResultType = ResultType.Success;
        result.Messages?.Add("User created successfuly");

        return result;
    }

    public async Task<CommandResult<ResultType, RegisterUserDto>> RegisterUserAsync(RegisterUserDto userDto)
    {
        var result = new CommandResult<ResultType, RegisterUserDto>();
        var model = _mapper.Map<RegisterUserModel>(userDto);

        var createResult = await _userRepository.CreateUserAsync(model);
        if (!createResult.Succeeded)
        {
            result.ResultType = ResultType.Failed;
            result.Messages?.AddRange(createResult.Errors.Select(x => x.Description).ToArray());

            return result;
        }

        result.ResultType = ResultType.Success;

        result.Messages?.Add("User created successfuly.");

        return result;
    }

    public async Task<CommandResult<ResultType, GetUserRolesDto>> LoginUserAsync(LoginUserDto userDto)
    {
        var result = new CommandResult<ResultType, GetUserRolesDto>();
        var user = await _userRepository.FindUserByNameAsync(userDto.Username);
        if (user == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("User doesn't exists");

            return result;
        }

        if (!await _userRepository.CheckPasswordAsync(user, userDto.Password))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Wrong password");

            return result;
        }

        var userRoles = await _userRepository.GetUserRolesAsync(user);
        if (userRoles == null || userRoles.Count == 0)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("User doesn't have any role. Please, wait for confirmation by admin.");

            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = new GetUserRolesDto
        {
            Username = user.UserName,
            Roles = userRoles,
        };

        return result;
    }

    public async Task<CommandResult<ResultType, ConfirmUserDto>> ConfirmUserByAdminAsync(ConfirmUserDto confirmUserDto)
    {
        var result = new CommandResult<ResultType, ConfirmUserDto>();
        var userExists = await _userRepository.FindUserByNameAsync(confirmUserDto.Username);
        if (userExists == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("User doesn't exists!");

            return result;
        }

        userExists.IsConfirmedByAdmin = true;
        var addedResult = await _userRepository.AddToRoleAsync(userExists, UserRole.User);

        if (!addedResult.Succeeded)
        {
            result.ResultType = ResultType.Failed;
            result.Messages?.Add(addedResult.ToString());

            return result;
        }

        result.ResultType = ResultType.Success;
        result.Messages?.Add("User confirmed successfuly");

        return result;
    }

    public async Task<CommandResult<ResultType, GetUserListDto>> GetUsersWithoutConfirmationAsync()
    {
        var result = new CommandResult<ResultType, GetUserListDto>();
        var users = await _userRepository.GetUsersWithoutConfirmationAsync();

        var userDtos = users.Select(user => new GetUserDto
        {
            Username = user.UserName,
            Email = user.Email,
        }).ToList();

        result.ResultType = ResultType.Success;
        result.Value = new GetUserListDto
        {
            Users = userDtos,
        };

        return result;
    }
}
