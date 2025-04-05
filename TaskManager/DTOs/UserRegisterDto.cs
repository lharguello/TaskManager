namespace TaskManager.DTOs;

public class UserRegisterDto
{
    /// <summary>
    /// Name
    /// </summary>
    /// <example>Jhon Doe</example>
    public string? Name { get; set; }
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