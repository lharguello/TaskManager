using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.DTO;
using TaskManager.DTOs;
using TaskManager.Entities;

namespace TaskManager.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController(AppDbContext context, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get All task
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(typeof(List<TaskResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tasks = await context.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();

        List<TaskResponse> result = mapper.Map<List<TaskResponse>>(tasks);

        return Ok(result);
    }

    /// <summary>
    /// Create a Task
    /// </summary>
    /// <param name="dto">task dto</param>
    /// <returns></returns>
    [ProducesResponseType(typeof(TaskId), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost]
    public async Task<IActionResult> Create(TaskDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var task = mapper.Map<TaskItem>(dto);
        task.UserId = userId;
        task.CreatedAt = DateTime.UtcNow;

        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        return Created("", new TaskId{ Id = task.Id });
    }

    /// <summary>
    /// Update a Task
    /// </summary>
    /// <param name="id">task id</param>
    /// <param name="dto">task object</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskDto dto)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        mapper.Map(dto, task);
        task.ModifiedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a task
    /// </summary>
    /// <param name="id">task id</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized, "text/plain")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes("application/json")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        context.Tasks.Remove(task);
        await context.SaveChangesAsync();

        return NoContent();
    }
}

