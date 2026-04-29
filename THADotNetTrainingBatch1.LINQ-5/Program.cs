

using THADotNetTrainingBatch1_LINQ_Data;

var data = LINQ5.GetEmployees();

var groupedByDepartment = data.GroupBy(x => x.Department)
                                .OrderBy(x => x.Key)
                                .Select(x => new
                                {
                                    Department = x.Key,
                                    Employees = x.OrderByDescending(e => e.Name).ToList()
                                });

foreach (var group in groupedByDepartment)
{
    Console.WriteLine($"\n--- Department: {group.Department} ---");
    Console.WriteLine($"{"ID",-5} | {"Name",-15} | {"Salary",-10}");
    Console.WriteLine(new string('-', 35));

    foreach (var emp in group.Employees)
    {
        Console.WriteLine($"{emp.Id,-5} | {emp.Name,-15} | {emp.Salary,-10:N0}");
    }
}

var sortedEmployees = data.OrderBy(x => x.Department)
                            .ThenByDescending(x => x.Salary)
                            .ToList();

//Console.WriteLine($"{"ID",-5} | {"Name",-15} | {"Department",-15} | {"Salary",-10}");
//Console.WriteLine(new string('-', 55));

//foreach (var emp in sortedEmployees)
//{
//    Console.WriteLine($"{emp.Id,-5} | {emp.Name,-15} | {emp.Department,-15} | {emp.Salary,-10:N0}");
//}