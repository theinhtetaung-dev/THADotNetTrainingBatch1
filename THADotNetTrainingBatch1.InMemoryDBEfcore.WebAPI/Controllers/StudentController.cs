using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.DTO;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Model;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Controllers;

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
    public async Task<IActionResult> GetStudentList([FromQuery] int pageNo,[FromQuery] int pageSize)
    {

        var students = await _context.Students.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        int totalRow = await _context.Students.CountAsync();
        int pageCount = totalRow / pageSize;
        if (totalRow % pageSize > 0) pageCount++;

        StudentListResModel resModel = new StudentListResModel()
        {
            Students = students.Select(s => new StudentResModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            }).ToList(),

            pageSetting = new PageSettingModel
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
        var Student = new Student
        {
            Id = reqmodel.Id,
            Name = reqmodel.Name,
            Email = reqmodel.Email
        };

        await _context.Students.AddAsync(Student);
        await _context.SaveChangesAsync();
        return Ok(Student);
    }

    [HttpGet("detail")]

    public IActionResult GetStudentById(StudentRequestModel requestModel)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == requestModel.Id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpDelete]

    public IActionResult DeleteStudent(int id)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        _context.Students.Remove(student);
        _context.SaveChanges();
        return NoContent();
    }

}
