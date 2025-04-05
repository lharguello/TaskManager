using TaskManager.DTOs;

namespace TaskManager.Services;

public interface IAuthService
{
    Task<string> Register(UserRegisterDto dto);
    Task<string> Login(UserLoginDto dto);
}
