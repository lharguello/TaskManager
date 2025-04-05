namespace TaskManager.DTO;

public class TaskDto
{
    /// <summary>
    /// Title
    /// </summary>
    /// <example>manage website</example>
    public string? Title { get; set; }
    /// <summary>
    /// Description task
    /// </summary>
    /// <example>review the details of the website</example>
    public string? Description { get; set; }
    /// <summary>
    /// Status with possible values:
    /// - Pending
    /// - InProgress
    /// - Completed
    /// </summary>
    /// <example>Pending</example>
    public string? Status { get; set; }
}
