using Microsoft.AspNetCore.Mvc;
using THADotNetTrainingBatch1.Logging.WebAPI.Attributes;
using THADotNetTrainingBatch1.Logging.WebAPI.Services.Teacher;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    [Permission("Teacher", "Read")]
    public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetTeachers()
    {
        var teachers = await _teacherService.GetAllTeachersAsync();
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    [Permission("Teacher", "Read")]
    public async Task<ActionResult<TeacherResponseDto>> GetTeacher(int id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);

        if (teacher == null)
        {
            return NotFound();
        }

        return Ok(teacher);
    }

    [HttpPost]
    [Permission("Teacher", "Create")]
    public async Task<ActionResult<TeacherResponseDto>> PostTeacher(TeacherRequestDto request)
    {
        var createdTeacher = await _teacherService.CreateTeacherAsync(request);
        if (createdTeacher == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetTeacher), new { id = createdTeacher.Id }, createdTeacher);
    }

    [HttpPut("{id}")]
    [Permission("Teacher", "Update")]
    public async Task<IActionResult> PutTeacher(int id, TeacherRequestDto request)
    {
        var result = await _teacherService.UpdateTeacherAsync(id, request);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Permission("Teacher", "Delete")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        var result = await _teacherService.DeleteTeacherAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
