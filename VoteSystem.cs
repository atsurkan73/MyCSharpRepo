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
    public class VoteSystem
    {
         public static string Category { get; set; }

        public Dictionary<int, string> DefineVoteSystem()
        {
            Dictionary<int, string> voteOptions = new Dictionary<int, string>();
            string option = null;
            string input = null;
            int count = 1;

            Category = inputConfirm("Input voting topic");
            Console.WriteLine($"After confirmation Topic : \"{Category}\"");
            if (Category != null)
                do
                {
                    option = inputConfirm("Input voting option title");

                    voteOptions.Add(count, option);

                    Console.WriteLine($"Option {count}. {option} has been added in the list");
                    PrintOneOption(count++, option);

                    Console.WriteLine("");
                    Console.WriteLine("Creating new option? - Y/N");
                    input = Console.ReadLine();
                }
                while (!input.Equals("N"));
            
            Console.WriteLine("Vote system has been created");
            Console.WriteLine($"Input voting topic: {Category}");
            PrintOptionsList(voteOptions);
            return voteOptions;
        }


        public string SelectVoteOption (Dictionary<int, string> options)
        {
            string input = null;

            Console.WriteLine("Print voting options");
            PrintOptionsList(options);
            Console.WriteLine("Pick option name");
            input = Console.ReadLine();
         
            return input;
        }



        public string inputConfirm(string input)
        {
            Console.WriteLine(input);
            var output = Console.ReadLine();
            Console.WriteLine($"Confirm input \"{output}\" - Y/N");
            if (Console.ReadLine() == "Y")
                return output;
            else if (Console.ReadLine() == "N")
                Console.WriteLine("Input is not confirmed");
            else   InputNotRecognized();
            return output;
        }

        public void InputNotRecognized()
        { Console.WriteLine("Input not regognized");
        Console.WriteLine("Exit program");
        Environment.Exit(0);
        }

        public void PrintOneOption(int key, string value)
        {
            Console.WriteLine("Print voting option");
            Console.WriteLine($"Number: {key}  Option: {value}");
        }

        public void PrintOptionsList(Dictionary<int, string> dict)
        {
            Console.WriteLine("Print voting options");
            foreach (var item in dict)
            {
                Console.WriteLine($"Number: {item.Key}  Option: {item.Value}");
            }
        }
    }
}
