using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.DTO;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Student;

public class StudentResModel
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}

public class StudentRequestModel
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}

public class StudentListResModel
{
    public List<StudentResModel> Students { get; set; } = null!;
    public PageSettingModel PageSetting { get; set; } = null!;
}

