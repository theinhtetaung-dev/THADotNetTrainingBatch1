using System.ComponentModel.DataAnnotations;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Teacher;

public class TeacherRequestDto
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
}

public class TeacherResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
}
