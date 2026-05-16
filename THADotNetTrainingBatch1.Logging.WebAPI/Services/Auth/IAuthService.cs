namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Auth;

public interface IAuthService
{
    Task<LoginResponseModel?> LoginAsync(LoginRequestModel request);
}
