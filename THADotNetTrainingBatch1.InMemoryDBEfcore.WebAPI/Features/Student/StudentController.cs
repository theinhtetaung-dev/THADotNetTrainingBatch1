using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.DTO;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Student;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly StudentService _service;

 
    public StudentController(AppDbContext context,StudentService service)
    {
        _context = context;
        _service = service;

    }

    [HttpGet("all")]
    [Authorize(Roles = "admin,staff")]
    public async Task<IActionResult> GetStudents()
    {
        var student = await _service.GetStudents();

        if(student is null) return NotFound();
        return Ok(student);
    }

    [HttpGet("list")]
    [Authorize(Roles = "admin,staff")]
    public async Task<IActionResult> GetStudentList([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
    {
        if (pageNo <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be greater than zero.");
        }

        var students = await _service.GetStudentList(pageNo, pageSize);

        if (students == null) return NotFound();

        return Ok(students);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateStudent(StudentRequestModel reqmodel)
    {
        var student = await _service.CreateStudent(reqmodel);
        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin,staff")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _service.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateStudent(int id, StudentRequestModel reqmodel)
    {
       var student = await _service.UpdateStudent(id, reqmodel);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var isDeleted = await _service.DeleteStudent(id);

        return Ok("Deleted success!");
    }
}
