using THADotNetTrainingBatch1_LINQ_Data;
using static THADotNetTrainingBatch1_LINQ_Data.LINQ3;

var data = LINQ3.GetProducts();

Console.Write("1:Electronic \n 2:Furniture \nEnter Option :   ");
var inputOption = Console.ReadLine();
bool isIntOption = int.TryParse(inputOption,out int opt);
if(!isIntOption)
{
    Console.WriteLine("Invalid Integer!");
}

string Searching = opt == 1 ? "Electronics" : "Furniture";
var searchItems = data.Where( x => x.Category == Searching)
    .Select( x => new  {x.Name , x.Price}).ToList();

foreach(var item in searchItems)
{
    Console.WriteLine("Name : " + item.Name + " , Price : " + item.Price);
}