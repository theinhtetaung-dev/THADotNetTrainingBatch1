namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Teacher;

public interface ITeacherService
{
    Task<IEnumerable<TeacherResponseDto>> GetAllTeachersAsync();
    Task<TeacherResponseDto?> GetTeacherByIdAsync(int id);
    Task<TeacherResponseDto> CreateTeacherAsync(TeacherRequestDto request);
    Task<bool> UpdateTeacherAsync(int id, TeacherRequestDto request);
    Task<bool> DeleteTeacherAsync(int id);
}
