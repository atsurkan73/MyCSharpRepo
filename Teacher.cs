using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_project;


public class Teacher
{
    string TeacherId { get; set; }
    string FirstName;
    string LastName;
    string Subject;
    string UserName;
    string UserId;


    public Teacher(string FistName, string LastName, string Subject)
    { }


     static List<(string TeacherId, string FistName, string LastName, string Subject)> GetTeacherList(string path)
    {
        (string TeacherId, string FistName, string LastName, string Subject) teacher;
        var teacherList = new List<(string TeacherId, string FistName, string LastName, string Subject)>();
        var linesToKeep = File.ReadAllLines(path).ToList();

        Console.WriteLine();
        Console.WriteLine("Written data from file to List");
        foreach (string lines in linesToKeep)
            Console.WriteLine(lines);


        foreach (string line in linesToKeep)
        {
            var splitComma = line.Split(",");
            teacher.TeacherId = splitComma[0];
            teacher.FistName = splitComma[1];
            teacher.LastName = splitComma[2];
            teacher.Subject = splitComma[3];
            teacherList.Add(teacher);
        }
            teacherList = teacherList.OrderBy(c => c.TeacherId).ToList();
            return teacherList;
        }

        public static void CreateNewTeacher ( string path)
    {
        Console.WriteLine("You are about to add new teacher in data base");

        Console.WriteLine("Enter new Id:");
        var id = Console.ReadLine();
        Console.WriteLine("Enter new FirstName:");
        var firstName = Console.ReadLine();
        Console.WriteLine("Enter new LasttName:");
        var lastName = Console.ReadLine();
        Console.WriteLine("Enter new Subject:");
        var subject = Console.ReadLine();

        (string TeacherId, string FirstName, string LastName, string Subject) newTeacherData = (id, firstName, lastName, subject);

        File.AppendAllLines(path, new[] { $" \n{newTeacherData.TeacherId}, {newTeacherData.FirstName},  {newTeacherData.LastName}, {newTeacherData.Subject}" });
        Console.WriteLine($"Contact \"{newTeacherData}\" has been added in the file \"{path}\"");
    }

    public static string [] GetTeacherById (string id, string path)
    {
        string[] newList = null;
        var tupleList = GetTeacherList(path);
        string idItem;

        PrintTeacherList(tupleList);

        foreach (var tuple in tupleList)
        {
            idItem = tuple.TeacherId.Trim().ToLower();
            if (idItem.Equals(id.Trim().ToLower()))
                newList = new [] { tuple.TeacherId, tuple.FistName, tuple.LastName, tuple.Subject};
        }
        return newList;
    }


    public static void PrintTeacherData(string Id, string path)
    {
        
        Console.WriteLine($"Current data for teacher Id '{Id}':") ;
        var teacherData = GetTeacherById(Id, path);
        for (int i = 0; i < teacherData.Length; i++)
            Console.WriteLine(teacherData[i]);
    }

    public static void  UpdateTeacherById(string path)
    {
        Console.WriteLine("You are  going to update teacher data. Enter Teacher Id:");
        var id = Console.ReadLine();
        var tupleList = GetTeacherList(path);

        
        Console.WriteLine("Current teachers list:");
        PrintTeacherList(tupleList);

        var newList = new List<string>();

        for (int i = 0; i < tupleList.Count; i++)
        {
            if (tupleList[i].TeacherId.Trim() == id)
            {
                Console.WriteLine("Enter new Id:");
                var newId = Console.ReadLine();
                Console.WriteLine("Enter new FirstName:");
                var firstName = Console.ReadLine();
                Console.WriteLine("Enter new LasttName:");
                var lastName = Console.ReadLine();
                Console.WriteLine("Enter new Subject:");
                var subject = Console.ReadLine();

                var oldTeacherData = (tupleList[i].TeacherId, tupleList[i].FistName, tupleList[i].LastName, tupleList[i].Subject);
                (string TeacherId, string FirstName, string LastName, string Subject) newTeacherData = (newId, firstName, lastName, subject);
                    
                GetTeacherList(path);
                var tempFile = Path.GetTempFileName();
                var linesToKeep = File.ReadLines(path).Where(l => !l.Contains(oldTeacherData.TeacherId.Trim()));

                File.WriteAllLines(tempFile, linesToKeep);

                File.Delete(path);
                
                File.Move(tempFile, path);

                File.AppendAllLines(path, new[] { $" {newTeacherData.TeacherId}, {newTeacherData.FirstName},  {newTeacherData.LastName}, {newTeacherData.Subject}" });
                Console.WriteLine($"Contact \"{newTeacherData}\" has been added in the file \"{path}\"");
               
            }
            else if (tupleList[i].TeacherId.Trim() != id)
            { Console.Write("Teacher Id not found - ");  }
            
            break;
        }
        Console.WriteLine("Exit");
        Environment.Exit(0);
    }

    static void PrintTeacherList(List<(string TeacherId, string Firstname, string Secondname, string Subject)> list)
    {
        Console.WriteLine("TeacherId - Firstname - Lastname - Subject");
        foreach (var row in list)
            Console.WriteLine("{0} - {1} -  {2} - {3}", row.TeacherId, row.Firstname, row.Secondname, row.Subject);
    }





}