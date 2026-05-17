using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.Logging.WebAPI.Data;
using TeacherModel = THADotNetTrainingBatch1.Logging.WebAPI.Data.Teacher;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Teacher;

public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TeacherService> _logService;

    public TeacherService(AppDbContext context, ILogger<TeacherService> logService)
    {
        _context = context;
        _logService = logService;
    }

    public async Task<IEnumerable<TeacherResponseDto>> GetAllTeachersAsync()
    {
        try
        {
            var teachers = await _context.Teachers.AsNoTracking()
                .Select(t => new TeacherResponseDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Age = t.Age,
                    Email = t.Email,
                    Address = t.Address
                }).ToListAsync();

            if (teachers.Count == 0)
            {
                _logService.LogWarning("No teachers found in the database.");
                return teachers; // empty list
            }

            _logService.LogInformation($"{teachers.Count} teachers retrieved successfully.");
            return teachers;
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, "An error occurred while retrieving all teachers.");
            throw;
        }
    }

    public async Task<TeacherResponseDto?> GetTeacherByIdAsync(int id)
    {
        try
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                _logService.LogWarning($"Teacher with ID {id} not found.");
                return null;
            }

            _logService.LogInformation($"Teacher with ID {id} retrieved successfully.");
            return new TeacherResponseDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Age = teacher.Age,
                Email = teacher.Email,
                Address = teacher.Address
            };
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"An error occurred while retrieving teacher with ID {id}.");
            throw;
        }
    }

    public async Task<TeacherResponseDto> CreateTeacherAsync(TeacherRequestDto request)
    {
        try
        {
            if (request == null)
            {
                _logService.LogError("Teacher creation request is null.");
                return null!;
            }

            var teacher = new TeacherModel
            {
                Name = request.Name,
                Age = request.Age,
                Email = request.Email,
                Address = request.Address
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            _logService.LogInformation("Teacher created successfully with Name : " + teacher.Name);
            return new TeacherResponseDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Age = teacher.Age,
                Email = teacher.Email,
                Address = teacher.Address
            };
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, "An error occurred while creating a new teacher.");
            throw;
        }
    }

    public async Task<bool> UpdateTeacherAsync(int id, TeacherRequestDto request)
    {
        try
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                _logService.LogWarning($"Teacher with ID {id} not found for update.");
                return false;
            }

            teacher.Name = request.Name;
            teacher.Age = request.Age;
            teacher.Email = request.Email;
            teacher.Address = request.Address;

            _context.Entry(teacher).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
                _logService.LogInformation($"Teacher with ID {id} updated successfully.");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id)) return false;
                throw;
            }
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"An error occurred while updating teacher with ID {id}.");
            throw;
        }
    }

    public async Task<bool> DeleteTeacherAsync(int id)
    {
        try
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                _logService.LogWarning($"Teacher with ID {id} not found for deletion.");
                return false;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            _logService.LogInformation($"Teacher with ID {id} deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"An error occurred while deleting teacher with ID {id}.");
            throw;
        }
    }

    private bool TeacherExists(int id)
    {
        return _context.Teachers.Any(e => e.Id == id);
    }
}
