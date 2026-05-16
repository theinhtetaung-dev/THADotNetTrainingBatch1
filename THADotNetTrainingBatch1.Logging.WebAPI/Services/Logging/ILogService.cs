namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Logging;

public interface ILogService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex = null);
}
