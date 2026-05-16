namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Student;

public class StudentRequestModel
{
    public string StudentName { get; set; } = null!;
    public string StudentEmail { get; set; } = null!;
    public int StudentAge { get; set; }
}

public class StudentResponseModel
{
    public int Id { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentEmail { get; set; } = null!;
    public int StudentAge { get; set; }
}
