using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CollectionLesson
{
    public class Vote
    {
        public string Name { get; set; }
        public int VoteResult { get; set; }
        string input;
      

        VoteSystem voteSystem = new VoteSystem();
        UserData userData = new UserData();


        public string SelectUser(List <string> users)
        {
            Console.WriteLine("");
            Console.WriteLine("Select user name from the list who should vote. Print username + Enter");
            userData.PrintUsersList(users);
            input = Console.ReadLine();
            foreach (var user in users)
            {
                if (user == input && !user.Equals(""))
                {
                    Name = input;
                    Console.WriteLine($"User {Name} has been selected");
                }
                else if (user.Equals(""))
                { 
                    Console.WriteLine("Empty input. Stop program");
                    Environment.Exit(0);
                }
            }
            return Name;
        }


        public void VotingResult (Dictionary<int, string> votingOptions)
        {
            VoteResult = 0;

            Console.WriteLine($"Hello {Name}");

            Console.WriteLine($"Your voting topic is {VoteSystem.Category}");

            Console.WriteLine($"Option List");
           
            voteSystem.PrintOptionsList(votingOptions);


            VoteResult = int.Parse(Console.ReadLine());

            foreach (var votingOption in votingOptions)
            { if (VoteResult == votingOption.Key)
                    Console.WriteLine($"Your result is {votingOption}");
            }
         }
    }
}
