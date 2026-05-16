using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace THADotNetTrainingBatch1.SeriLog.ConsoleApp;

public static class DevCode
{
    public static bool IsNullOrEmptyDev(this string input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            Log.Logger.Warning("Input string is null, empty, or whitespace.");
            return true;
        }
        return false;
    }
}
