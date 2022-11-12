using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StackLesson
{
    internal class OperationString
    {
        public void ChoooseOperation(StackTest<Object> objStack)
        {
            string input = null;
            do
            {
                Console.WriteLine("Stack Operations with String values");
                Console.WriteLine("Pick operation: \n1 - Push; \n2 - Pop; \n3 - Clear; \n4 - Count; \n5 - CopyToArray; \n6 -Peek;  \n0 - Exit.");
                input = Console.ReadLine();
                string inputString;
                switch (input)
                {
                    case "1":
                        Console.WriteLine($"Input {input} - Push");
                        Console.WriteLine("Enter string value to push it in stack");
                        inputString = Console.ReadLine();
                        objStack.Push(inputString);
                        break;
                    case "2":
                        Console.WriteLine($"Input {input} - Pop");
                        objStack.Pop();
                        break;
                    case "3":
                        Console.WriteLine($"Input {input} - Clear");
                        objStack.Clear();
                        break;
                    case "4":
                        Console.WriteLine($"Input {input} - Count");
                        Console.WriteLine($"Counting Stack items: {objStack.Count}");
                        break;
                    case "5":
                        Console.WriteLine($"Input {input} - CopyToArray");
                        objStack.CopyToArray();
                        break;
                    case "6":
                        Console.WriteLine($"Input {input} - Peek");
                        objStack.Peek();
                        break;
                    case "0":
                        Console.WriteLine($"Input {input} - Exit");
                        break;
                    default:
                        Console.WriteLine($"Input {input} has not been recognized");
                        break;
                }
            }
            while (!input.Equals("0"));
            return;
        }
    }
}
