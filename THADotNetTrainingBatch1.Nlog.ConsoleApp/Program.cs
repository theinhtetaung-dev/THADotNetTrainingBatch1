using NLog;

Console.WriteLine("Hello, World!");

var _logger = LogManager.GetCurrentClassLogger();

bool isnum = false;
string inputNum1 = Console.ReadLine()!;
isnum = int.TryParse(inputNum1, out int num1);

if(!isnum)
{
    _logger.Error("Invalid input for num1: {Input}", inputNum1);
    Console.WriteLine("Please enter a valid integer for num1.");
    return;
};
_logger.Info("User entered num1: {Num1}", num1);

string inputNum2 = Console.ReadLine()!;
isnum = int.TryParse(inputNum2, out int num2);

if(!isnum)
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
