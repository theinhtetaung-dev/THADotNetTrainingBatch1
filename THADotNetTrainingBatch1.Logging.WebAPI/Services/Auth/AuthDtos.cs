namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Auth;

public class LoginRequestModel
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginResponseModel
{
    public string Token { get; set; } = null!;
    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}
