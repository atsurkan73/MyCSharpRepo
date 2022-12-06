using System.Data;
using Bogus;

namespace Calendar;

public class Room
{

    public int Id { get; set; }
    public string RoomNumber { get; set; }
    public int PlaceNumber { get; set; }
    public  List<Room> Rooms { get; set; }

    public Room() { }
    public Room(int id, string roomNumber, int placeNumber)
    {
        Id = id;
        RoomNumber = roomNumber;
        PlaceNumber = placeNumber;
    }

    record BookedMeetings(int MeetingId, Room room, DateTime meetingTime);
    int userInputId;

        public List<Room> RoomsGenerate(int quantity)
        {
        var faker = new Faker<Room>()
            .RuleFor(u => u.Id, f => f.IndexFaker + 1)
            .RuleFor(u => u.RoomNumber, f => f.Address.BuildingNumber())
            .RuleFor(u => u.PlaceNumber, f => f.PickRandom(10,20,30))
            .UseSeed(01234);

            var Rooms = faker.Generate(quantity);
            return Rooms;
        }

    public void PrintRooms(List<Room> rooms)
    {
        Console.WriteLine("Rooms printout: Id - Number - Max attendant number");
        foreach (var room in rooms)
            Console.WriteLine("{0} - {1} - {2}", room.Id, room.RoomNumber, room.PlaceNumber);
    }

    public List<Room> AddRoom(List<User> userList, List<Room> roomList)
    {
        do
        {
            Console.WriteLine("Enter your user Id");
            userInputId = int.Parse(Console.ReadLine());
            if (userList.Select(u => u.Id).Contains(userInputId))
            {
                Console.WriteLine($"Hello {userList[userInputId - 1].Name}");
                Console.WriteLine("Are you going to add room? - choose Y/N");
                {

                    if (Console.ReadLine().Equals("Y") && userList[userInputId - 1].RestrictedPermission == false)
                    {
                        Console.WriteLine("Enter new room Id");
                        var roomId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new room number");
                        var roomNum = Console.ReadLine();
                        Console.WriteLine("Enter place number ");
                        var placeNum = int.Parse(Console.ReadLine());
                        Console.WriteLine("Your input: ");
                        Console.WriteLine($" Id : {roomId}, Room Number: {roomNum}, Place in room: {placeNum}");
                        Console.WriteLine("Input OK? - choose Y/N");
                        if (Console.ReadLine().Equals("Y"))
                        {
                            roomList.Add(new Room(roomId, roomNum, placeNum));
                            PrintRooms(roomList);
                        }
                    }
                    else if (Console.ReadLine().Equals("Y") && userList[userInputId].RestrictedPermission == true)
                        Console.WriteLine("You are not allowed to perform this operation");
                    else if (Console.ReadLine().Equals("N"))
                        Console.WriteLine("You canceled adding room");
                }
            }
            else if (!userList.Select(u => u.Id).Contains(userInputId))
                Console.WriteLine($"User with Id {userInputId} is not in the list");
        }
        while (!userList.Select(u => u.Id).Contains(userInputId));

        return roomList;
    }
}


