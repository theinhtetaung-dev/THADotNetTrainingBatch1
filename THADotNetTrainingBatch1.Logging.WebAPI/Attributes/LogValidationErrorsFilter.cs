using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace THADotNetTrainingBatch1.Logging.WebAPI.Attributes;

public class LogValidationErrorsFilter : IActionFilter
{
    private readonly ILogger<LogValidationErrorsFilter> _logService;

    public LogValidationErrorsFilter(ILogger<LogValidationErrorsFilter> logService)
    {
        _logService = logService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = string.Join(" | ", context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            _logService.LogWarning($"Validation failed for {context.ActionDescriptor.DisplayName}: {errors}");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
