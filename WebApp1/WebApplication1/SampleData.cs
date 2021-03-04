using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    static public class SampleData
    {
        public static void InitializeWithData(Models.TransactionContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Models.Product { ProductName = "Samsung EVO 960", Cost = 300 },
                    new Models.Product { ProductName = "Ryzen 5 3600x", Cost = 200 },
                    new Models.Product { ProductName = "RTX 2060 Super", Cost = 400 }
                    );
                context.SaveChanges();
            }
        }
    }
}
