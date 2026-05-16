using Serilog;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Services.Logging;

public class SeriLogService : ILogService
{
    private readonly Serilog.ILogger _logger;

    public SeriLogService(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warning(message);
    }

    public void LogError(string message, Exception? ex = null)
    {
        if (ex != null)
        {
            _logger.Error(ex, message);
        }
        else
        {
            _logger.Error(message);
        }
    }
}
