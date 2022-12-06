
namespace Calendar;

public class DateAndTime
{
    DateTime DateTime{ get; set; }


    public DateTime SetMeetingTime()
    {
       
        DateTime now = DateTime.Now;

        Console.WriteLine("Enter Day in the curent month 1...31 to book meeting");
        var day = int.Parse(Console.ReadLine());
        if (now.Day <= day && day < 31)
        {
        Console.WriteLine("Enter Hour within working time 08...17 to book meeting");
        var hour = int.Parse(Console.ReadLine());
            if (hour >= 8 && hour < 17)
            {
                var  newTime = new DateTime(now.Year, now.Month, day, hour, 0, 0);
                Console.WriteLine($"Requested meeting time: {newTime}");
                DateTime = newTime;
            }
        }
        else
        {
            Console.WriteLine("Not correct time input");
            Console.WriteLine("Try Again");
        }
        return DateTime;
    }
}

