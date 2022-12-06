using Bogus.DataSets;
using System.Data;
using System.Text.Json;
using static Calendar.Meeting;

namespace Calendar;

public static class Serialization
{
  //  List<MeetingData> meetingDatas1;

  //  List<MeetingData> Meetings
    //public static void Serialization()
    //{
    //    SerializeMeeting("meeting.json");
    //    DeserializeMeeting("meeting.json");
    //}

    public static void SerializeMeeting(string path, List<MeetingData> meetingDatas)
    {
        using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
        JsonSerializer.Serialize(fileStream, meetingDatas);
    }

    public static List<MeetingData> DeserializeMeeting(string path)
    {
        using var fileStream = new FileStream(path,
            FileMode.OpenOrCreate);
        var meetingDatas = JsonSerializer
            .Deserialize<List<MeetingData>>(fileStream);
        return meetingDatas;
      //  SomeCompanies = companies ?? SomeCompanies;
    }

}


