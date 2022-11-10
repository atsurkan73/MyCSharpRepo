using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public static class Order
    {
        public static void MakeOrder(string productDB)
        {
            string inputYn = "";
            var products = new List<string>();
            int count = 0;
           
            Console.WriteLine("Put new product in order? Y/N");
            inputYn = Console.ReadLine();
            if (inputYn == "Y")
            {
                products.Add(InviteToBuy());
                count++;
            }
            else if (inputYn == "N")
            {
                PrintOrder(products);
                Receipt.PrintReceipt(products, productDB);
            }
            if (count == 0)
                { 
                    Console.WriteLine("No products in order");
                }
            PrintOrder(products);
            Receipt.PrintReceipt(products, productDB);
        }

        public static string InviteToBuy()
        {
            Console.WriteLine("Please specify product Id to order");
            string product = Console.ReadLine();
            Console.WriteLine("Please specify quantity of product");
            string quantity = Console.ReadLine();
            return $"{product}, {quantity}";
        }

        public static void PrintOrder(List<string> orderList)
        {
            Console.WriteLine("Print Order");
            Console.WriteLine("ProductId, Quantity:");
            foreach (string item in orderList)
                Console.WriteLine(item);
        }
    }
}