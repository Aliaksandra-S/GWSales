using GWSales.Services.Models;
using GWSales.WebApi.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GWSales.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
    {
        var result = new CommandResult<ResultType, RegisterUserDto>();
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
        {
            result.ResultType = ResultType.Failed;
            result.Messages?.Add("User already exists!");

            return BadRequest(result);
        }

        var user = new IdentityUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var createResult = await _userManager.CreateAsync(user, model.Password);

        if (!createResult.Succeeded)
        {
            result.ResultType = ResultType.Failed;
            result.Messages?.Add(createResult.ToString());

            return BadRequest(result);
        }

        result.ResultType = ResultType.Success;
        result.Messages?.Add("User created successfuly");

        return Ok(result);
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
    {
        var result = new CommandResult<ResultType, RegisterUserDto>();
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
        {
            result.ResultType = ResultType.Failed;
            result.Messages?.Add("User already exists!");

            return BadRequest(result);
        }

        var user = new IdentityUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var createResult = await _userManager.CreateAsync(user, model.Password);
        if (!createResult.Succeeded)
        {
            result.ResultType = ResultType.Failed;
            result.Messages?.Add("User creation failed! Please check user details.");

            return BadRequest(result);
        }

        if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
        }
        if (!await _roleManager.RoleExistsAsync(UserRole.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRole.User));
        }

        if (await _roleManager.RoleExistsAsync(UserRole.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRole.Admin);
        }
        if (await _roleManager.RoleExistsAsync(UserRole.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRole.User);
        }

        result.ResultType = ResultType.Success;
        result.Messages?.Add("User created successfuly");

        return Ok(result);
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}

