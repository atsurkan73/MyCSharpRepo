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


int x = 0;
int y = 0;
int sum = 0;

{
	try
	{
		Console.Write("Enter integer X: ");
		x = Convert.ToInt32(Console.ReadLine());
		Console.WriteLine($"X =  {x}");

		Console.Write("Enter integer Y: ");
		y = Convert.ToInt32(Console.ReadLine());
		Console.WriteLine($"Y =  {y}");
	}

	catch
	{
		Console.Write("Invalid input");
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


