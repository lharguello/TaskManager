namespace TaskManager.DTOs;

public class UserLoginDto
{
    /// <summary>
    /// Email
    /// </summary>
    /// <example>jhon.doe@example.com</example>
    public string? Email { get; set; }
    /// <summary>
    /// Password
    /// </summary>
    /// <example>jhon123</example>
    public string? Password { get; set; }
}