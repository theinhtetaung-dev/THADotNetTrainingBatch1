using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.DTO;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Model;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Student;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetStudentList([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
    {
        if (pageNo <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be greater than zero.");
        }

        var students = await _context.Students
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        int totalRow = await _context.Students.CountAsync();
        int pageCount = (int)Math.Ceiling((double)totalRow / pageSize);

        StudentListResModel resModel = new StudentListResModel()
        {
            Students = students.Select(s => new StudentResModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            }).ToList(),

            PageSetting = new PageSettingModel
            {
                PageNo = pageNo,
                PageSize = pageSize,
                PageCount = pageCount
            }
        };
        return Ok(resModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(StudentRequestModel reqmodel)
    {
        var student = new Student
        {
            Name = reqmodel.Name,
            Email = reqmodel.Email
        };

        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, StudentRequestModel reqmodel)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        student.Name = reqmodel.Name;
        student.Email = reqmodel.Email;

        await _context.SaveChangesAsync();
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
