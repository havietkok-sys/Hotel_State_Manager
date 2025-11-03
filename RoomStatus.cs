namespace App
{

    public enum RoomStatus //Enum för Rumstatus
    {
        Ledigt,
        Upptaget,
        Städning
    }

    public class Room //Class för rum 
    {
        public int RoomNumber;
        public RoomStatus Status;
        public string GuestName;

        public Room(int roomNumber, RoomStatus status, string guestName = "")
        {
            RoomNumber = roomNumber;
            Status = status;
            GuestName = guestName;
        }

        public override string ToString() //Skickar tillbaka det som jag vill det ska sparas
        {
            return $"{RoomNumber},{Status},{GuestName}";
        }

        public static Room FromString(string line)
        {
            string[] parts = line.Split(';', 3, StringSplitOptions.TrimEntries);
            int number = int.Parse(parts[0]);
            RoomStatus status = Enum.Parse<RoomStatus>(parts[1]);
            string guest = parts.Length > 2 ? parts[2] : "";
            return new Room(number, status, guest);
        }


        public static List<Room> GenerateFixedRooms() // För att skapa alla fasta rum
        {
            List<Room> rooms = new List<Room>();

            // Rum 101–110
            for (int i = 101; i <= 110; i++)
            {
                rooms.Add(new Room(i, RoomStatus.Ledigt));
            }

            // Rum 201–210
            for (int i = 201; i <= 210; i++)
            {
                rooms.Add(new Room(i, RoomStatus.Ledigt));
            }

            return rooms;
        }
    }
}