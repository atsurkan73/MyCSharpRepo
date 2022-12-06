using System.Data;

namespace Calendar;

public class Meeting
{
    public string RoomId { get; set; }
    public List<MeetingData> Meetings { get; set; } = new List<MeetingData>();


    public Meeting() { }
    public Meeting(string room, DateTime dateTime, List<MeetingData> meetings)
    {
        RoomId = room;
        Meetings = meetings;
    }
    Room room = new Room();
    DateAndTime dateAndTime = new DateAndTime();
    public record MeetingData(int roomId, DateTime meetingTime);
 
    public void BookMeeting(List<Room> roomList, List<MeetingData> meetingList)
    {
        Console.WriteLine("You are going to book meeting by room and time");
        do
        {
            room.PrintRooms(roomList);
            Console.WriteLine("Enter one room Id from the above list");
            int roomId = int.Parse(Console.ReadLine());
            if (roomList.Select(r => r.Id).Contains(roomId))
            {

                var setTime = dateAndTime.SetMeetingTime();
                var meeting = new MeetingData(roomId, setTime);
                PrintMeeting(meeting);
                if (Meetings.Contains(meeting))
                {
                    Console.WriteLine($"Room {meeting.roomId} is busy on {meeting.meetingTime}");
                    Console.WriteLine("Select another time or room");
                }
                Meetings.Add(meeting);
                PrintAllMeetingList(Meetings);
            }
            else if (!roomList.Select(r => r.Id).Contains(roomId))
            Console.WriteLine($"Room Id {roomId} does not exist in the list");
            Console.WriteLine("Create new meeting? - Y/N");
        }
        while (Console.ReadLine().Equals ("Y"));
    }
    
    public void PrintMeeting(MeetingData meetingAppoinment)
    {
        Console.WriteLine("Meeting Appoinment: Room - Time");
        Console.WriteLine("{0} - {1} ", meetingAppoinment.roomId, meetingAppoinment.meetingTime);
        return;
    }

    public void PrintAllMeetingList(List<MeetingData> meetingList)
    {
        Console.WriteLine("All meeting appoinments printout:");
        Console.WriteLine("Room - Time");
        foreach(var meeting in meetingList)
        Console.WriteLine("{0} - {1} ", meeting.roomId, meeting.meetingTime);
    }

    public void PrintRoomMeetingList(List<MeetingData> meetingList)
    {
        Console.WriteLine("You are going to look meetings in certain room");
        Console.WriteLine("Enter room Id");
        var roomId = int.Parse(Console.ReadLine());
        var roomMeetingList = meetingList.Where(m => m.roomId == roomId);
        Console.WriteLine($"Room {roomId} meeting appoinments printout:");
        Console.WriteLine("Room - Time");
        foreach (var meeting in roomMeetingList)
            Console.WriteLine("{0} - {1} ", meeting.roomId, meeting.meetingTime);
    }
}


