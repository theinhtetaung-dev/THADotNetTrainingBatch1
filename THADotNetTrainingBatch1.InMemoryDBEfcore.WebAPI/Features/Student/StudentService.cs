using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.DTO;
using Student = THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database.Tbl_Students;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Student;

public class StudentService
{
    private readonly AppDbContext _db;

    public StudentService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<StudentResModel>> GetStudents()
    {
        return await _db.Students
            .Select(s => new StudentResModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            })
            .ToListAsync();
    }

    public async Task<StudentListResModel> GetStudentList(int pageNo, int pageSize)
    {
        if (pageNo <= 0) pageNo = 1;
        if (pageSize <= 0) pageSize = 10;

        int totalRow = await _db.Students.CountAsync();
        int pageCount = totalRow / pageSize;
        if(totalRow % pageSize > 0) pageCount++;    

        var students = await _db.Students
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentResModel
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            })
            .ToListAsync();

        return new StudentListResModel
        {
            Students = students.Select(x => new StudentResModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            }).ToList(),

            PageSetting = new PageSettingModel
            {
                PageNo = pageNo,
                PageSize = pageSize,
                PageCount = pageCount
            }
        };
    }

    public async Task<StudentResModel?> GetStudentById(int id)
    {
        var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student == null) return null;

        return new StudentResModel
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email
        };
    }

    public async Task<StudentResModel> CreateStudent(StudentRequestModel requestModel)
    {
        Tbl_Students student = new Tbl_Students
        {
            Name = requestModel.Name,
            Email = requestModel.Email
        };
        await _db.Students.AddAsync(student);
        await _db.SaveChangesAsync();

        return new StudentResModel
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email
        };
    }

    public async Task<StudentResModel?> UpdateStudent(int id, StudentRequestModel requestModel)
    {
        var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student == null) return null;

        student.Name = requestModel.Name;
        student.Email = requestModel.Email;

        await _db.SaveChangesAsync();

        return new StudentResModel
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email
        };
    }

    public async Task<bool?> DeleteStudent(int id)
    {
        var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student == null) return null;

        _db.Students.Remove(student);
        var result = await _db.SaveChangesAsync();
        return result > 0;
    }
}
