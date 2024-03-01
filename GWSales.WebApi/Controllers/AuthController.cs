using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
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
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthController(
        IConfiguration configuration,
        IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
    {
        var result = await _userService.LoginUserAsync(loginDto);

        if (result.ResultType == ResultType.NotFound)
        {
            return NotFound(result);
        }

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }

        if (result.ResultType == ResultType.Success)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Value.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in result.Value.Roles)
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
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var result = await _userService.RegisterUserAsync(registerDto);

        if (result.ResultType == ResultType.Failed)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto registerDto)
    {
        if (!_configuration.GetValue<bool>("Features:EnableAdminRegistration"))
        {
            return Unauthorized();
        }

        var result = await _userService.RegisterAdminAsync(registerDto);

        if (result.ResultType == ResultType.Failed)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("confirm-user")]
    public async Task<IActionResult> ConfirmUserByAdmin([FromBody] ConfirmUserDto confirmDto)
    {
        var result = await _userService.ConfirmUserByAdminAsync(confirmDto);

        if (result.ResultType == ResultType.NotFound)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("get-users-for-confirm")]
    public async Task<IActionResult> GetUsersWithoutConfirmation()
    {
        var result = await _userService.GetUsersWithoutConfirmationAsync();

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

