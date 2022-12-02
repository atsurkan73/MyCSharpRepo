using System.Reflection;


/*
 Create application that will load assembly and show all it’s classes and their methods with arguments needed to pass and return types
 */

string path = "D:\\MRA2\\icui\\autotests\\ET\\libs\\bins\\Navigation.dll";


Assembly assembly = Assembly.LoadFrom(path);

string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));

var splitComma = availableTypes.Split(",");

Console.WriteLine($"Class used in : {assembly}\n");

foreach (var item in splitComma)
{ Console.WriteLine(item); }

Type navigation = assembly.GetType("Navigation.HMINavigation");

Console.WriteLine($"\nClass name: {navigation}\n");

MethodInfo[] methods = navigation.GetMethods();

int i = 0;
int j = 0;

foreach (MethodInfo method in methods)
{
    i++;
    Console.WriteLine("Method[{0}] = {1}", i, method.Name);
    ParameterInfo[] parameters = method.GetParameters();

    Console.WriteLine($"Arguments to pass in method {method}:");
    foreach (ParameterInfo parameter in parameters)
    {
        j++;
        if (parameters.Length == 0)
            Console.WriteLine($"No arguments degined to pass in {method}");
        Console.WriteLine("Argument[{0}] = {1} of type {2}", j, parameter.Name, parameter.ParameterType.Name);
    }

    string returnType = method.ReturnType.FullName;

    Console.WriteLine("The returned type of method {0} is {1} \n", method, returnType);
}