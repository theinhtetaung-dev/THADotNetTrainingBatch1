using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THADotNetTrainingBatch1_LINQ_Data;

public class LINQ4
{
    public class Product
    {
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
    }

        public static List<Product> GetProducts()
        {
            return new List<Product> {
                new Product { Name = "Espresso", Category = "Coffee" },
                new Product { Name = "Latte", Category = "Coffee" },
                new Product { Name = "Green Tea", Category = "Tea" },
                new Product { Name = "Cake", Category = "Bakery" },
                new Product { Name = "Croissant", Category = "Bakery" },
                new Product { Name = "Brownie", Category = "Bakery" }
            };
        }


}
