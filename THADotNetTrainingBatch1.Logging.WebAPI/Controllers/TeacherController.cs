using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.Logging.WebAPI.Attributes;
using THADotNetTrainingBatch1.Logging.WebAPI.Data;
using THADotNetTrainingBatch1.Logging.WebAPI.Services.Teacher;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<TeacherController> _logger;

    public TeacherController(AppDbContext context,ILogger<TeacherController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [Permission("Teacher", "Read")]
    public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetTeachers()
    {

        var teachers = await _context.Teachers
            .Select(t => new TeacherResponseDto
            {
                Id = t.Id,
                Name = t.Name,
                Age = t.Age,
                Email = t.Email,
                Address = t.Address
            }).ToListAsync();

        if(teachers.Count == 0)
        {
            _logger.LogWarning("No teachers found in the database.");
            return Ok(teachers);
        }

        _logger.LogInformation($"{teachers.Count} teachers retrieved successfully.");
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    [Permission("Teacher", "Read")]
    public async Task<ActionResult<TeacherResponseDto>> GetTeacher(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);

        if (teacher == null)
        {
            _logger.LogWarning($"Teacher with ID {id} not found.");
            return NotFound();
        }

        _logger.LogInformation($"Teacher with ID {id} retrieved successfully.");
        return Ok(new TeacherResponseDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Age = teacher.Age,
            Email = teacher.Email,
            Address = teacher.Address
        });
    }

    [HttpPost]
    [Permission("Teacher", "Create")]
    public async Task<ActionResult<TeacherResponseDto>> PostTeacher(TeacherRequestDto request)
    {
        if(request == null)
        {
            _logger.LogError("Teacher creation request is null.");
            return BadRequest();
        }
        var teacher = new Teacher
        {
            Name = request.Name,
            Age = request.Age,
            Email = request.Email,
            Address = request.Address
        };

        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Teacher created successfully with Name : " + teacher.Name);
        var response = new TeacherResponseDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Age = teacher.Age,
            Email = teacher.Email,
            Address = teacher.Address
        };

        return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, response);
    }

    [HttpPut("{id}")]
    [Permission("Teacher", "Update")]
    public async Task<IActionResult> PutTeacher(int id, TeacherRequestDto request)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            _logger.LogWarning($"Teacher with ID {id} not found for update.");
            return NotFound();
        }

        teacher.Name = request.Name;
        teacher.Age = request.Age;
        teacher.Email = request.Email;
        teacher.Address = request.Address;

        _context.Entry(teacher).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Teacher with ID {id} updated successfully.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Permission("Teacher", "Delete")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            _logger.LogWarning($"Teacher with ID {id} not found for deletion.");
            return NotFound();
        }

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Teacher with ID {id} deleted successfully.");
        return NoContent();
    }
}
