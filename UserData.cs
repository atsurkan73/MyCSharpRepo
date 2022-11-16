using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CollectionLesson
{

    public class UserData
    {
       string UserName { get; set; }
        List <string> userData = new List<string>();
        string yesOrNot;

        internal List<string> CreateUser()
        {
            do
            {
                Console.WriteLine("Creating user for voting");
                Console.WriteLine("Input user name");
                string userName = Console.ReadLine();
                userData.Add(userName);
                Console.WriteLine($"Username {userName} approved");
                Console.WriteLine("Input another user? -Y/N");
                yesOrNot = Console.ReadLine();
            }
            while (!yesOrNot.Equals("N"));

            PrintUsersList(userData);
          return userData;
        }

        public void PrintUsersList(List<string> users)
        {
            Console.WriteLine("Printing users list");
            int index = 1;
            foreach (var item in users)
            {
                Console.WriteLine($"{index++}. {item}");
            }
        }
    }
}
