using THADotNetTrainingBatch1_LINQ_Data;

var data = LINQ1.GenerateRandomNumsV2(20, 0, 100);
var ascorder = data.OrderByDescending(x => x).ToList();
//foreach(var item in data)
//{
//    Console.Write(item +" ");
//}   
Console.Write("Enter val to filter : ");
var inputNum = Console.ReadLine();
bool isInt = int.TryParse(inputNum, out var val);
if(!isInt)
{
    Console.WriteLine("Invalid Int!");
    return;
}
int minNum = data.Min();
int maxNum = data.Max();
bool isGreater50 = data.Any(x => x > 50);
if(isGreater50)
{
    Console.WriteLine("There is a number greater than 50");
}
else
{
    Console.WriteLine("There is no number greater than 50");
}
    var filterNums = data.Where(x => x >= val).ToList();
var evenData = data.Where(x => x % 2 == 0).ToList();
var oddData = data.Where( x=> x % 2 != 0).ToList();
Console.WriteLine("Numbers : " + String.Join(", ", data));
Console.WriteLine("Numbers in Descending Order : " + String.Join(", ", ascorder));
Console.WriteLine("Min and Max Number : " + minNum + " , " + maxNum);
Console.WriteLine("Number Filtered : " + String.Join(" , ", filterNums));
Console.WriteLine("Odd Numbers : " + String.Join(", ", oddData));
Console.WriteLine("Even Numbers : " + String.Join(", ", evenData));



Console.ReadLine();