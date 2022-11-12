using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StackLesson
{
    public class StackTest<T>
    {
        private T[] items;              // current cells in stack
        private int count;             // current number of stack elements
        const int n = 10;             // number of cells in start stack size and portion to increase/decrease stack size
        public StackTest()
        {
            items = new T[n];
        }
        public StackTest(int length)
        {
            items = new T[length];
        }

        public bool IsEmpty 
        {
            get { return count == 0; }
        }
        public int Count
        {
            get { return count; }

        }

        public void Push(T item)
        {
            if (count == items.Length)
                Resize(items.Length + n);                              // gets stack lager 

            items[count++] = item;
            Print(items);
        }
        public T Pop()
        {
            if (IsEmpty)
                throw new Exception("Stack is empty");                // If stack is empty - throw exeption
            T item = items[--count];
            items[count] = default(T); 
            if (count > 0 && count < items.Length - n)
                Resize(items.Length - 10);                            // gets stack size smaller
            Console.WriteLine($"Pop returns value: {item}");
            Print(items);
            return item;
        }
            public T Peek()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Stack is empty");
                throw new Exception("Stack is empty");                  // If stack is empty - throw exeption
            }
            Console.WriteLine($"Peek returns value: {items[count - 1]}");
            Print(items);
            return items[count - 1];
        }

        public  T Clear()
        {
            Array.Clear(items, 0, items.Length);
            Resize(n);    
            Print(items);
            return items[0];
            }

        public T CopyToArray()
        {
            if (items.Length == 0)
            { 
                Console.WriteLine("Stack is empty");
                return items[0]; 
            }
            T[] arrayForCopy = new T[items.Length];
            Array.Copy(items, arrayForCopy, items.Length);
            Console.WriteLine("Array with copied stack items: ");
            foreach (T t in arrayForCopy)
            {
                Console.WriteLine(t);
            }
            Print(items);
            return items[0];
        }

        private void Resize(int max)
        {
            T[] tempItems = new T[max];
            for (int i = 0; i < count; i++)
                tempItems[i] = items[i];
            items = tempItems;
        }

        private void Print( T[] items)
        {
            Console.WriteLine("Current Stack after made operation:");
            Console.WriteLine($"Stack size = {items.Length}, Stack elements: {count}, Print Stack items:");
            int indexer = 0;
            foreach (T item in items)
            {
                Console.WriteLine($"T[{indexer}] = {item}");
                indexer++;
            }
        }
    }
}
