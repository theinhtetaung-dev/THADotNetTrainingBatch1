using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.Logging.WebAPI.Data;
using THADotNetTrainingBatch1.Logging.WebAPI.Services.Logging;
using StudentModel = THADotNetTrainingBatch1.Logging.WebAPI.Data.Student;
namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Student;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;

    public StudentService(AppDbContext context,ILogService logService)
    {
        _context = context;
        _logService = logService;

    }

    public async Task<IEnumerable<StudentResponseModel>> GetAllStudentsAsync()
    {
        var students = await _context.Students
                .Select(s => new StudentResponseModel
                {
                    Id = s.Id,
                    StudentName = s.StudentName,
                    StudentEmail = s.StudentEmail,
                    StudentAge = s.StudentAge
                })
                .ToListAsync();

        if (students.Count == 0)
        {
            _logService.LogInformation("No students found in the database.");
            return null!;
        }

        return students;
        
    }
    public async Task<StudentResponseModel?> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            _logService.LogWarning($"Student with ID {id} not found.");
            return null;
        }

        _logService.LogInformation($"Student with ID {id} retrieved successfully.");
        return new StudentResponseModel
        {
            Id = student.Id,
            StudentName = student.StudentName,
            StudentEmail = student.StudentEmail,
            StudentAge = student.StudentAge
        };
    }

    public async Task<StudentResponseModel> CreateStudentAsync(StudentRequestModel request)
    {
        if( request.StudentName == null || request.StudentEmail == null || request.StudentAge <= 0)
        {
            _logService.LogError("Invalid student data provided.");
            return null!;
        }
        var student = new StudentModel
        {
            StudentName = request.StudentName,
            StudentEmail = request.StudentEmail,
            StudentAge = request.StudentAge
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        _logService.LogInformation($"Student with ID {Convert.ToString(student)}  created successfully.");
        return new StudentResponseModel
        {
            Id = student.Id,
            StudentName = student.StudentName,
            StudentEmail = student.StudentEmail,
            StudentAge = student.StudentAge
        };
    }

    public async Task<bool> UpdateStudentAsync(int id, StudentRequestModel request)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            _logService.LogWarning($"Student with ID {id} not found for update.");
            return false;
        }

        student.StudentName = request.StudentName;
        student.StudentEmail = request.StudentEmail;
        student.StudentAge = request.StudentAge;

        _context.Entry(student).State = EntityState.Modified;

        try
        { 
        await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id)) return false;
            throw;
        }
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            _logService.LogWarning($"Student with ID {id} not found for deletion.");
            return false;
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
