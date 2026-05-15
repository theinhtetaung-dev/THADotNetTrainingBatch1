using System;
using System.Collections.Generic;
using System.Text;

namespace THADotNetTrainingBatch1.Nlog.ConsoleApp;

public static class Devcode
{
    public static bool IsNullOrEmptyDev(this string input)
    {
        if( string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            NLog.LogManager.GetCurrentClassLogger().Warn("Input string is null, empty, or whitespace.");
            return true;
        }
        return false;
    }
}
