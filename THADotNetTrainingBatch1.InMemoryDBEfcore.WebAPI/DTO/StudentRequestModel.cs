using System.ComponentModel.DataAnnotations;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.DTO;

public class StudentRequestModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

}

public class StudentResModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class StudentListResModel
{
    public List<StudentResModel> Students { get; set; } = new();
    public PageSettingModel PageSetting { get; set; } = null!;

}
