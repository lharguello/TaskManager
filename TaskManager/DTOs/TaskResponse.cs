namespace TaskManager.DTOs;

public class TaskResponse
{
    /// <summary>
    /// Task identifier
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
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
    /// <summary>
    /// Created at
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Modified at
    /// </summary>
    public DateTime? ModifiedAt { get; set; }
}
