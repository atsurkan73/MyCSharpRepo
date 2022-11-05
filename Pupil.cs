using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace School_project;

public class Pupil
{
    string PupilId;
    string ClassId;
    string FirstName;
    string LastName;
    string Description;
    string UserName;
    string UserId;
    string Email { get; set; }
    string Login { get; set; }
    string Password { get; set; }
    string Address { get; set; }

    static Pupil()
    {}

    public Pupil(string fistName, string lastName, string classId, string pupilId, string userName, string gender)
    {}

    public static void AddOrViewPupilData (List<(string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender)> classList, string pathToFile)
    {
        Console.WriteLine($"Add or View pupil data? 1 - add; 2 - view Pupil by Pupil Id; 3- view Pupils of class by Class Id");
        var input = Console.ReadLine();
        if (input == "1")
        {
            classList = AddPupilToClassList(classList, pathToFile);
        }
        else if (input == "2")
        {
            Console.WriteLine($"Input Pupil Id");
            var inputId = Console.ReadLine();
            Pupil.PrintPupilDataByPupilId(classList, inputId);
        }

        else if (input == "3")
        {
            Console.WriteLine($"Input Class Id");
            var inputId = Console.ReadLine();
            Pupil.PrintClassListByClassId(classList, inputId);
        }

        else
        {
            Console.WriteLine("Not corect input");
            Environment.Exit(1);
        }
    }

    public static  List<(string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender)> AddPupilToClassList (List<(string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender)> classList, string pathToFile)
    {
        (string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender) pupilDataList = InputPupilDataAddToClass(pathToFile);
        classList.Add(pupilDataList);

        return classList;
    }

    static (string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender) InputPupilDataAddToClass(string path)
    {
        (string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender) inputData = (null, null, null, null, null, null);
        string pathFile = path;

        Console.WriteLine("Enter FirstName:");
        var firstName = Console.ReadLine();
        Console.WriteLine($"First name submitted: {firstName}");
        inputData.FirstName = firstName;
        Console.WriteLine("Enter LastName:");
        var lastName = Console.ReadLine();
        Console.WriteLine($"Second name submitted: {lastName}");
        inputData.LastName = lastName;
        var userName = firstName + "." + lastName;
        Console.WriteLine($"UserName is : {userName}");
        inputData.UserName = userName;
        Console.WriteLine("Enter ClassId:");
        var classId = Console.ReadLine();
        Console.WriteLine($"ClassId submitted: {classId}");
        inputData.ClassId = classId;
        Console.WriteLine("Enter PupilId:");
        string pupilId = Console.ReadLine();
        inputData.PupilId = pupilId;
        Console.WriteLine("Enter gender: male; female");
        var genderInput = Console.ReadLine();
        inputData.Gender = genderInput;
        Console.WriteLine($"Gender submitted: {genderInput}");

        AddPupilToFile(inputData, pathFile);

        return inputData;
    }

    public static void PrintPupilDataByPupilId(List<(string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender)> classList, string pupilId)
    {
        int count = 0;

        Console.WriteLine($"Print data for pupilId: {pupilId}");

        foreach (var row in classList)
        {
            if (row.PupilId == pupilId)
            {
                count++;
                Console.WriteLine("First name: " + row.FirstName);
                Console.WriteLine("Last name: " + row.LastName);
                Console.WriteLine("User name: " + row.UserName);
                Console.WriteLine("Class Id: " + row.ClassId);
                Console.WriteLine("Pupil Id: " + row.PupilId);
                Console.WriteLine("Gender: " + row.Gender);
            }
        }
        if (count == 0)
        {
            Console.Write("Data not found");
            Environment.Exit(1);
        }
    }

    static void PrintClassListByClassId(List<(string FirstName, string LastName, string UserName, string ClassId, string PupilId, string Gender)> classList, string classId)
    {
        int count = 0;
        Console.WriteLine($"Data for classId: {classId}");
        Console.WriteLine("Firstname - Lastname - UserName - ClassId - PupilId - Gender:");

        if (classList.Count != 0)
        {
            for (int i = 0; i < classList.Count; i++)
            {
                if (classList[i].ClassId == classId)
                {
                    ++count;
                    Console.WriteLine($"{classList[i].FirstName}, {classList[i].LastName}, {classList[i].UserName}, {classList[i].ClassId}, {classList[i].PupilId}, {classList[i].Gender}");
                    continue;
                }
            }
            if (count == 0)
                Console.WriteLine("Data not found");
            else
                Console.WriteLine(($"Found {count} entries"));
        }
        else if (classList.Count == 0)
            Console.WriteLine("classList collection is empty");
    }

    static void AddPupilToFile((string Firstname, string Lastname, string UserName, string ClassId, string PupilId, string Gender) input, string path)
    {
        File.AppendAllLines(path, new[] {  $" {input.Firstname},  {input.Lastname}, {input.UserName}, {input.ClassId}, {input.Firstname}, {input.PupilId}, {input.Gender}"});
        Console.WriteLine($"Contact \"{input}\" has been added in the file \"{path}\"");
    }

    public static List<(string Firstname, string Lastname, string UserName, string ClassId, string PupilId, string Gender)> ReadCsvFile(string path)
    {
        if (!File.Exists(path)) return null;
        var classList = new List<(string Firstname, string Lastname, string UserName, string ClassId, string PupilId, string Gender)>();
        var rows = File.ReadAllLines(path);
        for (int i = 1; i < rows.Count(); i++)
        {
            if (rows[i].Length != 0)
            {
                rows[i].Trim();
                var splitComma = rows[i].Split(",");
                classList.Add((splitComma[0], splitComma[1], splitComma[2], splitComma[3], splitComma[4], splitComma[5]));
            }
        }
        return classList;
    }
}

