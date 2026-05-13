namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;

public class Tbl_User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public List<string> Permissions { get; set; } = new List<string>();
}
