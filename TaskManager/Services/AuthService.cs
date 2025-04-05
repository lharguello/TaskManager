using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Entities;

namespace TaskManager.Services;

public class AuthService(AppDbContext context, IMapper mapper, IConfiguration config) : IAuthService
{
    public async Task<string> Register(UserRegisterDto dto)
    {
        var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = mapper.Map<User>(dto);
        user.PasswordHash = hashed;

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return GenerateJwt(user);
    }

    public async Task<string> Login(UserLoginDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        return GenerateJwt(user);
    }

    private string GenerateJwt(User user)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}