using Calendar;
using static Calendar.Meeting;

/*
 finish Calendar solution. User should be able to:

add/view rooms list
book meeting
see booked meetings in selected room
program supports 2 modes: readonly and RW mode
readonly: users cannot add room, book meeting
RW: permits all actions
 */

DateAndTime date = new DateAndTime();

User user = new User();
Room room = new Room();
Meeting meeting = new Meeting();

meeting.Meetings = Serialization.DeserializeMeeting("C:\\Users\\Atsurkan\\source\\repos\\ConsoleApp1\\ConsoleApp1\\meeting.json");

var userList = user.UsersGenerate(50);
var roomList = room.RoomsGenerate(5);

user.PrintUsers(userList);

room.PrintRooms(roomList);

room.AddRoom(userList, roomList);

var currentMettings = meeting.Meetings;

meeting.BookMeeting(roomList, currentMettings);

meeting.PrintRoomMeetingList(currentMettings);

Serialization.SerializeMeeting("C:\\Users\\Atsurkan\\source\\repos\\ConsoleApp1\\ConsoleApp1\\meeting.json", currentMettings);








