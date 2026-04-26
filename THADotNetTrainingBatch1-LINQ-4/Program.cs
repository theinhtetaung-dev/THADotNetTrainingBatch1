
using THADotNetTrainingBatch1_LINQ_Data;

var products = LINQ4.GetProducts();

var onlyName = products.Select(x => x.Name).ToList();
foreach (var name in onlyName)
{
    Console.WriteLine(name);
}   
Console.WriteLine();
var gpProducts = products.GroupBy(x => x.Category)
                 .Select(x => new
                 {
                     Name = x.Key,
                     Count = x.Count()
                 });

foreach (var item in gpProducts)
{
    Console.WriteLine($"{item.Name}: {item.Count} items");
}

var gpItems = products.GroupBy(x => x.Category)
                .Select(x => new
                {
                    Category = x.Key,
                    items = x.Select(x => x.Name).ToList()
                });

Console.WriteLine();
foreach (var group in gpItems)
{
    Console.WriteLine($"--- {group.Category} ---");
    foreach (var productName in group.items)
    {
        Console.WriteLine($"  > {productName}");
    }
}

Console.ReadLine(); 