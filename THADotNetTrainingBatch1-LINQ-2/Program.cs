using THADotNetTrainingBatch1_LINQ_Data;

var data = LINQ2.GetNames(10);

Console.Write("Enter a Char To search : ");
char searchChar = Console.ReadLine()[0];

var FilterName = data.Where(x => x.StartsWith(char.ToLower(searchChar))).ToList();

if(FilterName.Count == 0)
{
    Console.WriteLine("No Name Found Starting With " + searchChar);
    return;
}

var UppercaseData = data.Select(x => x.ToUpper());
Console.WriteLine("Names : " + String.Join(" , ", data));
Console.WriteLine("UpperCase Data : " + String.Join(" , ", UppercaseData));
Console.WriteLine("Names Starting With " + searchChar + " : " + String.Join(" , ", FilterName));

Console.ReadKey();