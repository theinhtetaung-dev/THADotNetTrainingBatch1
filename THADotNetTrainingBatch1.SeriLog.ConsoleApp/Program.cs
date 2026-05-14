using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .WriteTo.File(
        path: "logs/app-log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

try
{
    Log.Information("Application started.");

    bool isRunning = true;

    while (isRunning)
    {
        ShowMenu();

        Console.Write("Choose option: ");
        string? input = Console.ReadLine();

        try
        {
            switch (input)
            {
                case "1":
                    TestInformationLog();
                    break;

                case "2":
                    TestDebugLog();
                    break;

                case "3":
                    TestWarningLog();
                    break;

                case "4":
                    TestErrorLog();
                    break;

                case "5":
                    TestFatalLog();
                    break;

                case "6":
                    TestDivideByZeroException();
                    break;

                case "7":
                    TestUserInputLogging();
                    break;

                case "0":
                    Log.Information("User exited the application.");
                    isRunning = false;
                    break;

                default:
                    Log.Warning("Invalid menu option selected: {Input}", input);
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while processing menu option: {Input}", input);
            Console.WriteLine("Something went wrong. Please check the log file.");
        }

        Console.WriteLine();
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application crashed unexpectedly.");
}
finally
{
    Log.Information("Application stopped.");
    Log.CloseAndFlush();
}

static void ShowMenu()
{
    Console.WriteLine("========== Serilog Testing Menu ==========");
    Console.WriteLine("1. Test Information Log");
    Console.WriteLine("2. Test Debug Log");
    Console.WriteLine("3. Test Warning Log");
    Console.WriteLine("4. Test Error Log");
    Console.WriteLine("5. Test Fatal Log");
    Console.WriteLine("6. Test Exception Logging");
    Console.WriteLine("7. Test User Input Logging");
    Console.WriteLine("0. Exit");
    Console.WriteLine("==========================================");
}

static void TestInformationLog()
{
    Log.Information("This is an Information log.");
    Console.WriteLine("Information log written successfully.");
}

static void TestDebugLog()
{
    Log.Debug("This is a Debug log. Used for development and debugging.");
    Console.WriteLine("Debug log written successfully.");
}

static void TestWarningLog()
{
    Log.Warning("This is a Warning log. Something unexpected may happen.");
    Console.WriteLine("Warning log written successfully.");
}

static void TestErrorLog()
{
    Log.Error("This is an Error log. Something failed.");
    Console.WriteLine("Error log written successfully.");
}

static void TestFatalLog()
{
    Log.Fatal("This is a Fatal log. A serious problem happened.");
    Console.WriteLine("Fatal log written successfully.");
}

static void TestDivideByZeroException()
{
    try
    {
        Console.Write("Enter first number: ");
        int firstNumber = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        int secondNumber = Convert.ToInt32(Console.ReadLine());

        int result = firstNumber / secondNumber;

        Log.Information(
            "Division successful. {FirstNumber} / {SecondNumber} = {Result}",
            firstNumber,
            secondNumber,
            result
        );

        Console.WriteLine($"Result: {result}");
    }
    catch (DivideByZeroException ex)
    {
        Log.Error(ex, "Divide by zero error occurred.");
        Console.WriteLine("Cannot divide by zero.");
    }
    catch (FormatException ex)
    {
        Log.Error(ex, "Invalid number format entered by user.");
        Console.WriteLine("Please enter valid numbers only.");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Unexpected error occurred in division process.");
        Console.WriteLine("Unexpected error occurred.");
    }
}

static void TestUserInputLogging()
{
    try
    {
        Console.Write("Enter your name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Log.Warning("User submitted empty name.");
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        Log.Information("User entered name: {Name}", name);
        Console.WriteLine($"Hello, {name}");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error occurred while reading user input.");
        Console.WriteLine("Failed to read user input.");
    }
}