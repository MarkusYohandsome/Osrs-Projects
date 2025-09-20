using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Old_School_Runescape_Project.Models;

namespace Old_School_Runescape_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("fix-user-id-column")]
    public async Task<IActionResult> FixUserIdColumn()
    {
        try
        {
            // Make user_id column nullable
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE tasklists ALTER COLUMN user_id DROP NOT NULL;");

            return Ok(new { message = "Successfully made user_id column nullable" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Failed to update column", error = ex.Message });
        }
    }
}