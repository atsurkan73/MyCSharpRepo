{
int n = 5;
Console.WriteLine($"Factorial ({n}) = {Factorial (n)}");
Console.WriteLine($"Method ({n}) = {Method (n)}");
Console.WriteLine($"Fibonachi ({n}) = {Fibonachi (n)}");




int Factorial(int n)
{
    if (n == 1) return 1;
 
    return n * Factorial(n - 1);
}

int Method(int num) => num > 0 ? num + Method(num - 1) : 1;

int Fibonachi (int n)
{
    if (n == 0 || n== 1) 
	return 1;
	else
    return (n-1)  +  (n - 2);
}


}