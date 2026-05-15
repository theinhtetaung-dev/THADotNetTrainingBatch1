using NLog;
using THADotNetTrainingBatch1.Nlog.ConsoleApp;

Console.WriteLine("Hello, World!");
var _logger = LogManager.GetCurrentClassLogger();

try
{

    bool isnum = false;
    Console.Write("Enter your name : ");
    string username = Console.ReadLine()!;
    if(username.IsNullOrEmptyDev())
    {
        Console.WriteLine("Input cannot be empty. Please enter a valid name.");
    }
    Console.Write("Enter the num 1 : ");
    string inputNum1 = Console.ReadLine()!;
    if (inputNum1.IsNullOrEmptyDev())
    {
        Console.WriteLine("Input cannot be empty. Please enter a valid integer for num1.");
    }
    isnum = int.TryParse(inputNum1, out int num1);


    if (!isnum)
    {
        _logger.Error("Invalid input for num1: {Input}", inputNum1);
        Console.WriteLine("Please enter a valid integer for num1.");
        return;
    }
    ;
    _logger.Info("User entered num1: {Num1}", num1);

    Console.Write("Enter the num 2 : ");
    string inputNum2 = Console.ReadLine()!;

    if(inputNum2.IsNullOrEmptyDev())
    {
        Console.WriteLine("Input cannot be empty. Please enter a valid integer for num2.");
    }

    isnum = int.TryParse(inputNum2, out int num2);

    if (!isnum)
    {
        _logger.Error("Invalid input for num2: {Input}", inputNum2);
        Console.WriteLine("Please enter a valid integer for num2.");
        return;
    }
    _logger.Info("User entered num2: {Num2}", num2);

    int result = num1 + num2;
    Console.WriteLine("Result: " + result);
    _logger.Info("Calculated result of {Num1} + {Num2} = {Result}", num1, num2, result);

    Console.ReadLine();

}
catch (Exception ex)
{
    _logger.Fatal(ex.Message);

}
