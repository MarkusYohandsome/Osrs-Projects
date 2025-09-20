using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Old_School_Runescape_Project.Models;

namespace Old_School_Runescape_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskListsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TaskListsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskLists()
    {
        var taskLists = await _context.TaskLists.ToListAsync();
        return Ok(taskLists);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskList([FromBody] CreateTaskListRequest request)
    {
        // For now, let's not set UserId to avoid the foreign key constraint issue
        // We'll set it to null and handle the constraint later
        var taskList = new TaskList
        {
            UserId = null, // Explicitly set to null to avoid foreign key issues
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.TaskLists.Add(taskList);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTaskLists), new { id = taskList.Id }, taskList);
    }
}

public class CreateTaskListRequest
{
    public Guid? UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}