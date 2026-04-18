using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THADotNetTrainingBatch1_LINQ_Data
{
    public  class LINQ3
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Category { get; set; } = null!;
            public decimal Price { get; set; }
        }

        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200 },
                new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 25 },
                new Product { Id = 3, Name = "Keyboard", Category = "Electronics", Price = 550 },
                new Product { Id = 4, Name = "Smartphone", Category = "Electronics", Price = 800 },
                new Product { Id = 5, Name = "Desk Chair", Category = "Furniture", Price = 600 },
                new Product { Id = 6, Name = "Monitor", Category = "Electronics", Price = 1600 },
                new Product { Id = 7, Name = "Table Lamp", Category = "Furniture", Price = 45 },
                new Product { Id = 8, Name = "Headphones", Category = "Electronics", Price = 1100 },
                new Product { Id = 9, Name = "Webcam", Category = "Electronics", Price = 450 },
                new Product { Id = 10, Name = "Power Bank", Category = "Electronics", Price = 1400 }
            };
        }
    }
}
