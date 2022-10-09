/*
Create a program that will start with declaration of two constants (X and Y) and will count the sum of all numbers between these constants. If they are equal then sum should be one of them

Example:

X=10
Y=12
Sum=10+11+12=33

X=5
Y=2
Sum=2+3+4+5=14

X=10
Y=10
Sum=10

Extra:

Read values of X and Y from the console. If output is invalid - write to console Invalid input and exit the program.
*/



int sum = 0;

{
	
		Console.Write("Enter integer X: ");
		string str1 = Console.ReadLine();
		int x; 
		if (int.TryParse(str1, out x) == true)
    {
		Console.WriteLine($"Succesfull input: {x}");
	}
else
    {
		Console.WriteLine($"Invalid input: {str1}");
		Console.WriteLine("Press Enter to exit");
		Console.ReadLine();
		System.Environment.Exit(1);
	}
	
		Console.Write("Enter integer Y: ");
		string str2 = Console.ReadLine();
		int y;
if (int.TryParse(str2, out y) == true)
    {
		Console.WriteLine($"Succesfull input: {str2}");
	}
else
    {
		
		Console.WriteLine($"Invalid input: {str2}");
		Console.WriteLine("Press Enter to exit");
		Console.ReadLine();
		System.Environment.Exit(1);
	}

	if (x != y)
	{
		if (x < y)
		{
			while (x < y)
			{
			sum = sum + x++;
			}
		Console.WriteLine("SUM = " + (y + sum));

		}

		else if (x > y)
		{
			while (y < x)
			{
			sum = sum + y++;
			}
		Console.WriteLine("SUM = " + (x + sum));
		}
	}

	else
	{
	Console.WriteLine("SUM = " + x);
	}
}


