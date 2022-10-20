/*
Task:

create next methods:

Compare that will return true if 2 strings are equal, otherwise false, but do not use build-in method
Analyze that will return number of alphabetic chars in string, digits and another special characters
Sort that will return string that contains all characters from input string sorted in alphabetical order (e.g. 'Hello' -> 'ehllo')
Duplicate that will return array of characters that are duplicated in input string (e.g. 'Hello and hi' -> ['h', 'l'])

*/



using System.Collections.Generic;

string myFirstRow = "!Do not stop doing#$%*1212"; //Setting first string
string mySecondRow = "Do not stop doing"; //Setting second string

int alphSymbol;
int numSymbol;
int specSymbol;


Compare("This is string", "This is string");

Analyze(myFirstRow, out alphSymbol, out numSymbol, out specSymbol);

Sort(mySecondRow);

Duplicate(mySecondRow);



static bool Compare(string firstString, string secondString)
{
    if (firstString.Length == secondString.Length)
    {
        for (int i = 0; i < firstString.Length; i++)
        {
            if (firstString[i] != secondString[i])
            {
                break;
            }
        }
        Console.WriteLine($"First string \"{firstString}\" is equal to Second string \"{secondString}\"");
        return true;
    }
    Console.WriteLine($"myFirstRow \"{firstString}\" is NOT equal to mySecondRow \"{secondString}\"");
    return false;
}

static void Analyze(string toAnalyze, out int alphabetSymbol, out int numbertSymbol, out int specialSymbol)
{
    alphabetSymbol = 0;
    numbertSymbol = 0;
    specialSymbol = 0;
    string specialChar = @"\|!#$%^*&/()=?@{}[].-+;:'<>_,";
    for (int index = 0; index < toAnalyze.Length; index++)
    {
        if (Char.IsLetter(toAnalyze[index]))
            alphabetSymbol++;
        else if (Char.IsDigit(toAnalyze[index]))
            numbertSymbol++;
        else
            foreach (var item in specialChar)
                if (toAnalyze[index].Equals(item)) specialSymbol++;
    }
    Console.WriteLine($"The string \"{toAnalyze}\" contains: \n {alphabetSymbol} alphabet symbols\n {numbertSymbol} digits\n {specialSymbol} other symbols like special, punctuation, etc.");
}

static string Sort(string toAnalyze)
{
    char[] characters = toAnalyze.ToArray();
    Array.Sort(characters);
    string afterSort = new string(characters).Trim().ToLower();
    Console.WriteLine($"The string \"{toAnalyze}\" sorted in alphabetical order without spaces -> \"{afterSort}\"");
    return afterSort;
}

static List<char> Duplicate(string toAnalyze)
{
    string withoutSpaces = toAnalyze.Trim().ToLower();
    var duplicates = new List<char>();
    foreach (var item in toAnalyze)
    {
        int charCount = 0;
        foreach (var chars in toAnalyze)
        {
            if (item == chars && !Char.IsWhiteSpace(item))
            {
                charCount++;
            }
        }
        if (charCount > 1 && !duplicates.Contains(item))
        {
            duplicates.Add(item);
        }
    }

    Console.Write($"'{toAnalyze}' -> ['");
    Console.Write(string.Join("', '", duplicates));
    Console.Write("']");

    return duplicates;
}
