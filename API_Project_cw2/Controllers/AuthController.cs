using API_Project_cw2.Auth;
using API_Project_cw2.Requests;
using API_Project_cw2.Interfaces;
using API_Project_cw2.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_cw2.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> RegisterUser(LoginUserRequest request, Role role)
    {
        var userDb = await _userService.Register(request.Nickname, request.Password, role);
        var tokenKey = _configuration.GetValue<string>("TokenKey")!;
        var expiryDate = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("SessionTimeout"));
        
        var jwt = JwtGenerator.GenerateJwt(userDb, tokenKey, expiryDate);
        
        HttpContext.Session.SetInt32("id", userDb.Id);

        return Created("token", jwt);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUserRequest request)
    {
        var user = await _userService.Login(request.Nickname, request.Password);
        var tokenKey = _configuration.GetValue<string>("TokenKey")!;
        var expiryDate = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("SessionTimeout"));
        
        var jwt = JwtGenerator.GenerateJwt(user, tokenKey, expiryDate);

        return Created("token", jwt);
    }
}
