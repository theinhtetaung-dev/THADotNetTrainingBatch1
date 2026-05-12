using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.DTO;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Features.Student;

public class StudentResModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class StudentRequestModel
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class StudentListResModel
{
    public List<StudentResModel> Students { get; set; }
    public PageSettingModel PageSetting { get; set; }
}

