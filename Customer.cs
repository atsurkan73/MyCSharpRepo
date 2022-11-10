using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Customer
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public CustomerStatus status { get; set; }

        private string BirthDay;
        private string UserName;
        private string Password;



        public static void OperationWithCustomers(string path)
        {
            Console.WriteLine("Please pick operation:  \nCreate new customer - press 1 + Enter, \nGet customer details by Id - press 2 + Enter \nPrint all customer list - press 3 + Enter  \nRemove cusomer by Id from file - press 4 + Enter");
        switch(Console.ReadLine())
            {
                case "1":
                    Create(path);
                    break;
                case "2":
                    PrintCustomerData(path);
                    break;
                case "3":
                    PrintCustomerList(path);
                    break;
                case "4":
                    Remove(path);
                    break;
                default:
                    Console.WriteLine("Input is not recognized");
                    break;
            }
        }

        public static void Create(string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Creating new customer");
            Console.WriteLine("Enter cusnomer data separated by comma: Id,Name,Username, Password,Phone,Email");
            string input = Console.ReadLine();
            File.AppendAllLines(path, new[] { $"\n{input}" });
            Console.WriteLine($"Contact \"{input}\" has been added in the file \"{path}\"");
        }

        public static void Remove(string path)
        {
            Console.WriteLine("Enter customer Id to remove customer");
            string input = Console.ReadLine();

            var tuplesCustomer = GetCustomerList(path);

            var customers = new List<string>();

            var count = 0;

            for (int i = 1; i < tuplesCustomer.Count; i++)
            {
                if (tuplesCustomer[i].Id == input)
                {
                    var toRemove = (tuplesCustomer[i].Id, tuplesCustomer[i].Name, tuplesCustomer[i].UserName, tuplesCustomer[i].Password);

                    var tempFile = Path.GetTempFileName();
                    var linesToKeep = File.ReadLines(path).Where(l => !l.Contains(toRemove.Id));

                    File.WriteAllLines(tempFile, linesToKeep);

                    File.Delete(path);

                    File.Move(tempFile, path);

                    Console.WriteLine($"Customer \"{toRemove.Id} - {toRemove.Name} \" has been removed from the file \"{path}\"");
                    
                    count++;
                    
                    break;
                }
            }
            if (count == 0)
                Console.Write($"Customer Id {input} not found");

        }

        public static string[] GetCustomerById(string path,  ref string customerId)
        {
            string[] newList = null;
            var tupleList = GetCustomerList(path);
            string idItem;

            foreach (var tuple in tupleList)
            {
                idItem = tuple.Id.Trim().ToLower();
                if (idItem.Equals(customerId.Trim().ToLower()))
                    newList = new[] { tuple.Id, tuple.Name, tuple.UserName, tuple.Password };
            }
            return newList;
        }

        public static List<(string Id, string Name, string UserName, string Password)> GetCustomerList(string path)
        {
            (string Id, string Name, string UserName, string Password) customer;
            var customerList = new List<(string Id, string Name, string UserName, string Password)>();
            var linesToKeep = File.ReadAllLines(path).ToList();

            for (int line = 1; line < linesToKeep.Count; line++)
            if (!linesToKeep[line].Equals(""))
            {
                var splitComma = linesToKeep[line].Split(",");
                customer.Id = splitComma[0].Trim();
                customer.Name = splitComma[1].Trim();
                customer.UserName = splitComma[2].Trim();
                customer.Password = splitComma[3].Trim();
                customerList.Add(customer);
            }
            customerList = customerList.OrderBy(c => c.Id).ToList();
            return customerList;
        }

         public static void PrintCustomerData(string path)
    {
            Console.WriteLine("Input customer Id");
            string IdCustomer = Console.ReadLine();

            var teacherData = GetCustomerById(path, ref IdCustomer);

            for (int i = 0; i < teacherData.Length; i++)

                if (teacherData[0].Equals(IdCustomer))
                {
                    Console.WriteLine($"Id - Name - UserName - Password");
                    Console.WriteLine("{0} - {1} - {2} - {3}", teacherData[0], teacherData[1], teacherData[2], teacherData[3]);
                    break;
                }
    }

        public static void PrintCustomerList(string path)
        {
            var list = GetCustomerList(path);
            Console.WriteLine("Id - Name - UserName - Passsword");
            foreach (var row in list)
                Console.WriteLine("{0} - {1} -  {2} - {3}", row.Id, row.Name, row.UserName, row.Password);
        }
    }
}

public enum CustomerStatus
{
    Active,
    Blocked,
    Inactive
}
