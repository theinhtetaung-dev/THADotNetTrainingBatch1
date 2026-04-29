using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THADotNetTrainingBatch1_LINQ_Data
{
    public static class LINQ5
    {
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public double Salary { get; set; }
        }
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
        {
            new Employee { Id = 1, Name = "Aung Aung", Department = "IT", Salary = 1200000 },
            new Employee { Id = 2, Name = "Su Su", Department = "HR", Salary = 800000 },
            new Employee { Id = 3, Name = "Kyaw Kyaw", Department = "IT", Salary = 1500000 },
            new Employee { Id = 4, Name = "Mya Mya", Department = "Sales", Salary = 600000 },
            new Employee { Id = 5, Name = "Hla Hla", Department = "HR", Salary = 950000 },
            new Employee { Id = 6, Name = "Zaw Zaw", Department = "IT", Salary = 1100000 },
            new Employee { Id = 7, Name = "Thida", Department = "Sales", Salary = 1300000 },
            new Employee { Id = 8, Name = "Nilar", Department = "Marketing", Salary = 850000 },
            new Employee { Id = 9, Name = "Tun Tun", Department = "IT", Salary = 1800000 },
            new Employee { Id = 10, Name = "Aye Aye", Department = "HR", Salary = 700000 },
            new Employee { Id = 11, Name = "Bo Bo", Department = "Sales", Salary = 1100000 },
            new Employee { Id = 12, Name = "Chit Oo", Department = "Marketing", Salary = 900000 },
            new Employee { Id = 13, Name = "Dway", Department = "IT", Salary = 900000 },
            new Employee { Id = 14, Name = "Ei Ei", Department = "HR", Salary = 1100000 },
            new Employee { Id = 15, Name = "Phyo Phyo", Department = "Sales", Salary = 750000 },
            new Employee { Id = 16, Name = "Goke Ge", Department = "IT", Salary = 2000000 },
            new Employee { Id = 17, Name = "Htet Htet", Department = "Marketing", Salary = 1200000 },
            new Employee { Id = 18, Name = "Imon", Department = "HR", Salary = 1050000 },
            new Employee { Id = 19, Name = "Jue Jue", Department = "Sales", Salary = 550000 },
            new Employee { Id = 20, Name = "Kaung Kaung", Department = "IT", Salary = 1350000 },
            new Employee { Id = 21, Name = "Lwin Lwin", Department = "Marketing", Salary = 950000 },
            new Employee { Id = 22, Name = "Min Min", Department = "HR", Salary = 880000 },
            new Employee { Id = 23, Name = "Nan Nan", Department = "IT", Salary = 1600000 },
            new Employee { Id = 24, Name = "Ommar", Department = "Sales", Salary = 1400000 },
            new Employee { Id = 25, Name = "Pyae Pyae", Department = "Marketing", Salary = 1100000 },
            new Employee { Id = 26, Name = "Queenie", Department = "HR", Salary = 920000 },
            new Employee { Id = 27, Name = "Rose", Department = "IT", Salary = 1250000 },
            new Employee { Id = 28, Name = "Sann Sann", Department = "Sales", Salary = 820000 },
            new Employee { Id = 29, Name = "Taryar", Department = "Marketing", Salary = 780000 },
            new Employee { Id = 30, Name = "U Myat", Department = "IT", Salary = 1450000 },
            new Employee { Id = 31, Name = "Vicky", Department = "HR", Salary = 650000 },
            new Employee { Id = 32, Name = "Win Win", Department = "Sales", Salary = 980000 },
            new Employee { Id = 33, Name = "Xylia", Department = "Marketing", Salary = 1300000 },
            new Employee { Id = 34, Name = "Yati", Department = "IT", Salary = 1700000 },
            new Employee { Id = 35, Name = "Zarni", Department = "HR", Salary = 1250000 },
            new Employee { Id = 36, Name = "Khin Khin", Department = "Sales", Salary = 1050000 },
            new Employee { Id = 37, Name = "Lin Lin", Department = "Marketing", Salary = 1450000 },
            new Employee { Id = 38, Name = "Myo Myo", Department = "IT", Salary = 1150000 },
            new Employee { Id = 39, Name = "Nway Nway", Department = "HR", Salary = 990000 },
            new Employee { Id = 40, Name = "Oakkar", Department = "Sales", Salary = 1150000 }
        };
        }
    }
}
