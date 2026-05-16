using Microsoft.AspNetCore.Mvc;
using THADotNetTrainingBatch1.Logging.WebAPI.Attributes;
using THADotNetTrainingBatch1.Logging.WebAPI.Services.Student;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    [Permission("Student", "Read")]
    public async Task<ActionResult<IEnumerable<StudentResponseModel>>> GetStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    [Permission("Student", "Read")]
    public async Task<ActionResult<StudentResponseModel>> GetStudent(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    [Permission("Student", "Create")]
    public async Task<ActionResult<StudentResponseModel>> PostStudent(StudentRequestModel request)
    {
        var createdStudent = await _studentService.CreateStudentAsync(request);
        return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
    }

    [HttpPut("{id}")]
    [Permission("Student", "Update")]
    public async Task<IActionResult> PutStudent(int id, StudentRequestModel request)
    {
        var result = await _studentService.UpdateStudentAsync(id, request);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Permission("Student", "Delete")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var result = await _studentService.DeleteStudentAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
