/*
CDefine and call with different parameters next methods:

Method that will return max value among all the parameters
… min value …
Method TrySumIfOdd that accepts 2 parameters and returns true if sum of numbers between inputs is odd, otherwise false, sum return as out parameter
Overload first two methods with 3 and 4 parameters
Extra:

Define and call with different parameters next methods:

Method Repeat that will accept string X and number N and return X repeated N times (e.g. Repeat(‘str’, 3) returns ‘strstrstr’ = ‘str’ three times)
*/


//Setting numbers from console
int quantity = quantityOfNumbers ();
int [] numArray  = enterNumbers(quantity);

// Method that will return max value among all the parameters. All parameters are defined from console
maxValueCalc(numArray);

// Method that will return min value among all the parameters. All parameters are defined from console
minValueCalc(numArray);

// Method that accepts array of three numbers and returns max value  
int [] nums3Array = {0, 99, -7};
maxValueCalc(nums3Array);

// Method that accepts array of four numbers and returns min value  
int [] nums4Array = {0, 99, -7, 22};
minValueCalc(nums4Array);

// Method that accepts two numbers and returns true if sum of numbers between inputs is odd or false if sum is even   
TrySumIfOdd(3,1);

//Method that accepts from console string X and number N and returns X repeated N times
Repeat ();



static int quantityOfNumbers ()
{
	Console.Write("Specify  quantity of numbers: ");
	return enterNum ();
}

static int enterNum ()
{
		Console.Write("Enter number: ");
		string? str1 = Console.ReadLine();
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
	return x;
}

static int [] enterNumbers (int quantityOfNum)
{
	int [] numbers = new int[quantityOfNum];  
	for (int i = 0; i < quantityOfNum; i++)
	{
		Console.WriteLine($"Specify number #{i}");
		numbers[i] = enterNum ();
	}
	return numbers;
}


static void maxValueCalc (int [] numbers)
{
int maxValue = numbers[0];
	for (int i = 0; i < numbers.Length; i++)  
	{
	if (numbers[i] > maxValue)
	{
		maxValue = numbers[i];
	}	
}
Console.WriteLine("Max Value is " + maxValue);
}

static void minValueCalc (int [] numbers)
{
int minValue = numbers[0];
	for (int i = 0; i < numbers.Length; i++)  
	{
	if (numbers[i] < minValue)
	{
		minValue = numbers[i];
	}	
}
Console.WriteLine("Min Value is " + minValue);
}

static bool TrySumIfOdd (int x, int y)
{
int sum = 0;
{
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
		Console.WriteLine("SUM between numbers = " + (x + sum));
		}
	}

	else
	{
	Console.WriteLine("SUM between numbers = " + x);
	}
}
bool ifOdd = sum%2 != 0 ? true : false;
Console.WriteLine("TrySumIfOdd is " + ifOdd);
return ifOdd;
}

static void Repeat ()
{
	Console.Write("This method repeats string N times. ");
	int numRepeat = enterNum ();
	Console.Write("Enter string to be repeated: ");
	string? strRepeat = Console.ReadLine();
	Console.Write (string.Concat(Enumerable.Repeat(strRepeat, numRepeat)));

}
