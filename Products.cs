using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Products
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }


        public static void OperationWithProducts(string path)
            //string Id = null;
        {
            Console.WriteLine("Please pick operation:  \nCreate new product - press 1 + Enter, \nGet product details by Id - press 2 + Enter \nPrint products list and its total number - press 3 + Enter  \nRemove product from file - press 4 + Enter");
        switch(Console.ReadLine())
            {
                case "1":
                    CreateProduct(path);
                    break;
                case "2":
                    PrintProduct(path);
                    break;
                case "3":
                    PrintProductListAndQuantity(path);
                    break;
                case "4":
                    RemoveProduct(path);
                    break;
                default:
                    Console.WriteLine("Input is not recognized");
                    break;
            }
        }

        public static void CreateProduct(string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Creating new product");
            Console.WriteLine("Enter product data separated by comma: Id,Title,Price");
            string input = Console.ReadLine();
            File.AppendAllLines(path, new[] { $"\n{input}" });
            Console.WriteLine($"Product \"{input}\" has been added in the file \"{path}\"");
        }

        public static void RemoveProduct(string path)
        {
            Console.WriteLine("Enter product Id you are going to remove");
            string input = Console.ReadLine();

            var tuplesCustomer = GetProductList(path);

            var customers = new List<string>();

            var count = 0;

            for (int i = 1; i < tuplesCustomer.Count; i++)
            {
                if (tuplesCustomer[i].Id == input)
                {
                    var toRemove = (tuplesCustomer[i].Id, tuplesCustomer[i].Title, tuplesCustomer[i].Price);

                    var tempFile = Path.GetTempFileName();
                    var linesToKeep = File.ReadLines(path).Where(l => !l.Contains(toRemove.Id));

                    File.WriteAllLines(tempFile, linesToKeep);

                    File.Delete(path);

                    File.Move(tempFile, path);

                    Console.WriteLine($"Customer \"{toRemove.Id} - {toRemove.Title} \" has been removed from the file \"{path}\"");
                    
                    count++;
                    
                    break;
                }
            }
            if (count == 0)
                Console.Write($"Customer Id {input} not found");
        }

        public static string[] GetProductById(string path,  ref string customerId)
        {
            string[] newList = null;
            var tupleList = GetProductList(path);
            string idItem;

            foreach (var tuple in tupleList)
            {
                idItem = tuple.Id.Trim().ToLower();
                if (idItem.Equals(customerId.Trim().ToLower()))
                    newList = new[] { tuple.Id, tuple.Title, tuple.Price};
            }
            return newList;
        }


        public static List<(string Id, string Title, string Price)> GetProductList(string path)
        {
            (string Id, string Title, string Price) product;
            var productList = new List<(string Id, string Title, string Price)>();
            var linesToKeep = File.ReadAllLines(path).ToList();

            for (int line = 1; line < linesToKeep.Count; line++)
            if (!linesToKeep[line].Equals(""))
            {
                var splitComma = linesToKeep[line].Split(",");
                product.Id = splitComma[0].Trim();
                product.Title = splitComma[1].Trim();
                product.Price = splitComma[2].Trim();
                productList.Add(product);
            }
            productList = productList.OrderBy(p => p.Title).ThenBy(p => p.Id).ToList();
            return productList;
        }

         public static void PrintProduct(string path)
    {
            Console.WriteLine("Input product Id");
            string IdCustomer = Console.ReadLine();

            var productData = GetProductById(path, ref IdCustomer);

            for (int i = 0; i < productData.Length; i++)

                if (productData[0].Equals(IdCustomer))
                {
                    Console.WriteLine($"Id - Title - Price");
                    Console.WriteLine("{0} - {1} - {2}", productData[0], productData[1], productData[2]);
                    break;
                }
    }

        public static void PrintProductListAndQuantity(string path)
        {
            var list = GetProductList(path);
            int quantity = 0;
            Console.WriteLine("Id - Title - Price");
            foreach (var row in list)
            {
                Console.WriteLine("{0} - {1} -  {2}", row.Id, row.Title, row.Price);
                quantity++;
            }
            Console.WriteLine($"Total products quantity: {quantity}");
        }
    }
}