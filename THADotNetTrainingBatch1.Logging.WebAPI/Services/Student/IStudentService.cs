namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Student;

public interface IStudentService
{
    Task<IEnumerable<StudentResponseModel>> GetAllStudentsAsync();
    Task<StudentResponseModel?> GetStudentByIdAsync(int id);
    Task<StudentResponseModel> CreateStudentAsync(StudentRequestModel request);
    Task<bool> UpdateStudentAsync(int id, StudentRequestModel request);
    Task<bool> DeleteStudentAsync(int id);
}
