/*
Task:

Provide ability to search in phone book by any criteria (first/last name or phone number)

Extra:

Update phone number program that will store rows in alphabetical order (order by last name, first name and then phone number)

Use binary search algorithm to search for row in phone book

*/



using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Channels;


var pathToPhonebook = "MyPhonebook.csv";

var phoneBook = ReadCsvFile(ref pathToPhonebook); //read data from phonebook file and pack them to list

SelectOperation(phoneBook, ref pathToPhonebook);

OrderContactsBy(phoneBook);



static void SelectOperation(List<(string Firstname, string Secondname, string Phonenumber, string Group)> phoneList, ref string pathToPhonebook)
{
    Console.WriteLine($"Select operation. " +
        $"\n Press '1' and Enter - Input new entry in phonebook; " +
        $"\n Press '2' and Enter - Search contact by any criteria " +
        $"\n Press '3' and Enter - Skip input  and pass to sorting phonebook" +
        $"\n Press '4' and Enter - Cancel input");

    string choiceSelect = Console.ReadLine();
    if (choiceSelect == "1")
    {
        string inputContact = InputValue(); // introduction to input new contact
        ConfirmInput(inputContact, ref pathToPhonebook); // creation one contact in the list and file
        var contactList = ReadCsvFile(ref pathToPhonebook);
        PrintPhonebook(contactList, ref pathToPhonebook);
    }
    else if (choiceSelect == "2")
    {
        SearchContactsBy(searchInput(), phoneList);
    }
    else if (choiceSelect == "3")
    {
        Console.WriteLine("Input and Search are skipped. Move to sorting method.");
    }
    else if (choiceSelect == "4")
    {
        CancelInput();
    }
}


static List<(string Firstname, string Secondname, string Phonenumber, string Group)> ReadCsvFile(ref string path)
{
    if (!File.Exists(path)) return null;
    var bookList = new List<(string Firstname, string Secondname, string Phonenumber, string Group)>();
    var rows = File.ReadAllLines(path);
    for (int i = 1; i < rows.Count(); i++)
    {
        if (rows[i].Length != 0)
        {
            var splitComma = rows[i].Split(",");
            bookList.Add((splitComma[0], splitComma[1], splitComma[2], splitComma[3]));
        }
    }
    return bookList;
}

static void PrintPhonebook(List<(string Firstname, string Secondname, string Phonenumber, string Group)> contacts, ref string path)
{
    Console.WriteLine($"Read data from {path}:");
    Console.WriteLine("Firstname - Secondname - Phonenumber - Group");

    foreach (var row in contacts)
        Console.WriteLine("{0}, {1}, {2}, {3}", row.Firstname, row.Secondname, row.Phonenumber, row.Group);
}

static string InputValue()
{
    Console.WriteLine("Enter contact data separated by comma: Firstname,Secondname,Phonenumber,Group");
    string input = Console.ReadLine();
    Console.WriteLine($"Input: {input}");
    return input;
}

static void ConfirmInput(string inputData, ref string path)
{
    var confirmInvitation = "Please confirm Input. Press Y - yes or N - no";
    string choice;
    do
    {
        Console.WriteLine(confirmInvitation);
        choice = Console.ReadLine();
        if (choice == "N" || choice == "Y")
            break;
    }
    while (choice != "N" || choice != "Y");
    {
        if (choice == "Y") { AddContact(inputData, ref path); }
        else { CancelInput(); }
    }
}

static void CancelInput()
{
    Console.WriteLine("Input rejected by user");
    Environment.Exit(1);
}

static void AddContact(string input, ref string path)
{
    File.AppendAllLines(path, new[] { $"\n{input}" });
    Console.WriteLine($"Contact \"{input}\" has been added in the file \"{path}\"");
}

static List<(string Firstname, string Secondname, string Phonenumber, string Group)> SearchContactsBy(string input, List<(string Firstname, string Secondname, string Phonenumber, string Group)> collection)
{
    var searchResult = new List<(string Firstname, string Secondname, string Phonenumber, string Group)>();
    for (int row = 0; row < collection.Count; row++)
    {
        if (collection[row].Firstname.Contains(input, StringComparison.OrdinalIgnoreCase) || collection[row].Secondname.Contains(input, StringComparison.OrdinalIgnoreCase) || collection[row].Phonenumber.Contains(input))
            searchResult.Add(collection[row]);
    }
    if (searchResult.Count != 0)
    {
        foreach (var row in searchResult)
            Console.WriteLine("{0}, {1}, {2}", row.Firstname, row.Secondname, row.Phonenumber);
    }
    else { Console.WriteLine($"Nothing found for \"{input}\""); }
    return searchResult;
}

static string searchInput()
{
    string search;
    string inputInvitation = "Enter search word or number and press Enter";
    do
    {
        Console.WriteLine(inputInvitation);
        search = Console.ReadLine();
    }
    while (search.Length == 0);
    return search;
}

static List<(string Firstname, string Secondname, string Phonenumber, string Group)> OrderContactsBy(List<(string Firstname, string Secondname, string Phonenumber, string Group)> collection)
{
    string newPath = "MyPhonebook_Ordered.csv";
    var collectionOrdered = collection.OrderBy(c => c.Secondname).ThenBy(c => c.Firstname).ThenBy(c => c.Phonenumber).ToList();
    File.Delete(newPath);

    foreach (var row in collectionOrdered)
    {
        File.AppendAllLines(newPath, new[] { $"\n{row}" }); 
    }

    var contactList = ReadCsvFile(ref newPath);
    Console.WriteLine();
    Console.WriteLine("Sorted by last name, first name and then phone number:");
    PrintPhonebook(contactList, ref newPath);
    return contactList;
}


