using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.Logging.WebAPI.Data;
using StudentModel = THADotNetTrainingBatch1.Logging.WebAPI.Data.Student;
namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Student;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentResponseModel>> GetAllStudentsAsync()
    {
        return await _context.Students
            .Select(s => new StudentResponseModel
            {
                Id = s.Id,
                StudentName = s.StudentName,
                StudentEmail = s.StudentEmail,
                StudentAge = s.StudentAge
            })
            .ToListAsync();
    }

    public async Task<StudentResponseModel?> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return null;

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
        var student = new StudentModel
        {
            StudentName = request.StudentName,
            StudentEmail = request.StudentEmail,
            StudentAge = request.StudentAge
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

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
        if (student == null) return false;

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
        if (student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
