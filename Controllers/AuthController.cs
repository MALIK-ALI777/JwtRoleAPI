using JwtRoleAPI.Data;
using JwtRoleAPI.DTOs;
using JwtRoleAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtRoleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(
        AppDbContext context,
        JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == login.Username);

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
        {
            return Unauthorized("Invalid username or password");
        }

        var token = _jwtService.GenerateToken(user);

        return Ok(new
        {
            message = "Login successful",
            username = user.Username,
            role = user.Role!.Name,
            token = token
        });
    }
}