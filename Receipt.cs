using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Receipt
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public CustomerStatus status { get; set; }

        private string BirthDay;
        private string UserName;
        private string Password;



        

        public static void PrintReceipt(List <string> order, string path)
        {
            var orderList = new List <(string Id, string Title, string Price)>();
            (string Id, string Title, string Price) orderItem;
            int totalPrice = 0;
            int totalQuantity = 0;

            if (order.Count == 0)
            {

                Console.WriteLine("No products in order");
            }
            else if (order.Count > 0)
            {
                Console.WriteLine("Print Receipt: ");
                Console.WriteLine("Product ID - Title - Price ");

                var tupleProducts = Products.GetProductList(path);
                var splitComma = new string[2];
                for (int i = 0; i < tupleProducts.Count; i++)
                {
                    for (int s = 0; s < order.Count; s++)
                    { 
                        splitComma = order[s].Split(",");

                        if (splitComma.Length > 0 && splitComma[0].Trim() == tupleProducts[i].Id.Trim())
                        {
                            totalQuantity += int.Parse(splitComma[1]);
                            totalPrice = int.Parse(tupleProducts[i].Price) * totalQuantity;
                            orderItem = (tupleProducts[i].Id, tupleProducts[i].Title, tupleProducts[i].Price);
                            orderList.Add(orderItem);
                            Console.WriteLine("{0} - {1} - {2}", orderItem.Id, orderItem.Title, orderItem.Price);
                        }
                    }
                }
                Console.WriteLine($"Total products quantity: {totalQuantity}");
                Console.WriteLine($"Total products price: {totalPrice}");
            }
        }
    }
}