using System;

{

long longVar = 987654321098787;
int integerVar = 56565;
short shortVar = 1234;
byte byteVar = 127;
bool boolType = false;
char oneChar = 'a';
float floatVar = 543.654F;
double doubleVar = 987124.12D;
decimal decimalVar = 20.001M;

long substraction;
short sum;
double multiplying;
double dividing;

substraction = longVar - integerVar;
sum = Convert.ToInt16(shortVar + byteVar);
multiplying = doubleVar*floatVar;
dividing = doubleVar/floatVar;

Console.WriteLine("substraction is equal to: " + substraction);
Console.WriteLine("sum is equal to: " + sum);
Console.WriteLine("multiplying is equal to: " + multiplying);
Console.WriteLine("dividing is equal to: " + dividing);
Console.WriteLine();


double x = -1800d;
double y = 500d;
double firstTask;
double secondTask;
double thirdTask;
double fourthTask;


firstTask = Convert.ToDouble(-6*Math.Pow(x,3) + 5*Math.Pow(x,2) - 10*x + 15);
secondTask = Convert.ToDouble(Math.Abs(x)*Math.Sin(x));
thirdTask = Convert.ToDouble(2*Math.PI*x);
fourthTask = Math.Max(x, y);

Console.WriteLine("firstTask is equal to: " + firstTask);
Console.WriteLine("secondTask is equal to: " + secondTask);
Console.WriteLine("thirdTask is equal to: " + thirdTask);
Console.WriteLine("fourthTask is equal to: " + fourthTask);

DateTime ddt1 = new  DateTime(2022, 01, 01);
DateTime ddt2 = new  DateTime(2023, 01, 01);
DateTime ddt3 =  DateTime.Now;

TimeSpan result1 =  ddt2.Subtract(ddt3);
TimeSpan result2 = ddt3.Subtract(ddt1);
		
	
Console.WriteLine($"{result1.Days}  days left to New Year");
Console.WriteLine($"{result2.Days}  days passed from New Year: ");

//Console.WriteLine($"{"Date String",-37} {"Date",-19}\n");

		
		DateTime dt1 = new DateTime(2015, 12, 31);
		DateTime dt2 = new DateTime(2016, 2, 2);
		TimeSpan result = dt2.Subtract(dt1);
		Console.WriteLine(result);

}