using App;  // Använd ditt eget namespace (för Room och User)

string filePath = "rooms.txt";
List<Room> rooms = new List<Room>();

// === Inloggning ===
Console.WriteLine("==== Hotell State Manager ====");
Console.Write("Användarnamn: ");
string username = Console.ReadLine() ?? "";
Console.Write("Lösenord: ");
string password = Console.ReadLine() ?? "";

if (username != User.Admin.Username || password != User.Admin.Password)
{
    Console.WriteLine("Fel användarnamn eller lösenord. Avslutar...");
    return;
}

// === Ladda eller skapa rum ===
if (File.Exists(filePath))
{
    foreach (var line in File.ReadAllLines(filePath))
    {
        if (!string.IsNullOrWhiteSpace(line))
            rooms.Add(Room.FromString(line));
    }
}
else
{
    rooms = Room.GenerateFixedRooms();
    SaveRooms();
}

// === Huvudmeny ===
bool running = true;
while (running)
{
    Console.WriteLine("\n=== MENY ===");
    Console.WriteLine("1. Visa alla rum");
    Console.WriteLine("2. Visa lediga rum");
    Console.WriteLine("3. Visa upptagna rum");
    Console.WriteLine("4. Boka in gäst");
    Console.WriteLine("5. Checka ut gäst");
    Console.WriteLine("6. Markera rum för städning");
    Console.WriteLine("7. Avsluta");
    Console.Write("Val: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1": ShowAllRooms(); break;
        case "2": ShowRoomsByStatus(RoomStatus.Ledigt); break;
        case "3": ShowRoomsByStatus(RoomStatus.Upptaget); break;
        case "4": BookGuest(); break;
        case "5": CheckoutGuest(); break;
        case "6": MarkCleaning(); break;
        case "7":
            running = false;
            SaveRooms();
            Console.WriteLine("Avslutar och sparar.");
            break;
        default:
            Console.WriteLine("Fel val, försök igen dude!");
            break;
    }
}

return;

// Metoder för rummen meny

void SaveRooms() //Sparar rumm infon till room.txt
{
    List<string> lines = new List<string>();
    foreach (var room in rooms)
        lines.Add(room.ToString());
    File.WriteAllLines(filePath, lines);
}

void ShowAllRooms() // Loopar alla rum och status
{
    Console.WriteLine("\n--- Alla rum ---");
    foreach (var room in rooms)
        Console.WriteLine($"Rum {room.RoomNumber} - {room.Status} {(room.GuestName != "" ? "- Gäst: " + room.GuestName : "")}");
}

void ShowRoomsByStatus(RoomStatus status) // Visar rum beroende på status
{
    Console.WriteLine($"\n--- Rum med status: {status} ---");
    foreach (var room in rooms)
    {
        if (room.Status == status)
            Console.WriteLine($"Rum {room.RoomNumber} {(room.GuestName != "" ? "- Gäst: " + room.GuestName : "")}");
    }
}

void BookGuest()
{
    Console.Write("\nAnge rumsnummer att boka: ");
    if (!int.TryParse(Console.ReadLine(), out int number))
    {
        Console.WriteLine("Ogiltigt nummer.");
        return;
    }



    Room room = rooms.Find(r => r.RoomNumber == number); //Checkar rums listan om numret finns eller vilken status det har
    if (room == null) //Är det ledig och finns får man boka
    {
        Console.WriteLine("Rummet finns inte.");
        return;
    }

    if (room.Status != RoomStatus.Ledigt)
    {
        Console.WriteLine("Rummet är inte ledigt.");
        return;
    }

    Console.Write("Ange gästens namn: ");
    string guest = Console.ReadLine() ?? "";

    room.GuestName = guest;
    room.Status = RoomStatus.Upptaget;
    SaveRooms();
    Console.WriteLine($"Rummet {number} är nu bokat för {guest}."); //Glömmer alltid vad $ gör att man kan putta in variablar från strängar innanför måsvingarna.
}

void CheckoutGuest()
{
    Console.Write("\nAnge rumsnummer att checka ut: ");
    if (!int.TryParse(Console.ReadLine(), out int number))
    {
        Console.WriteLine("Ogiltigt nummer.");
        return;
    }

    Room room = rooms.Find(r => r.RoomNumber == number); // Tänkte demonstrera bägge sätten och göra detta på men så pröva jag den andra sättet och fick inte det å funka
    if (room == null)
    {
        Console.WriteLine("Rummet finns inte.");
        return;
    }

    if (room.Status != RoomStatus.Upptaget)
    {
        Console.WriteLine("Rummet är inte upptaget.");
        return;
    }

    room.GuestName = "";
    room.Status = RoomStatus.Ledigt;
    SaveRooms();
    Console.WriteLine($"Rummet {number} är nu ledigt.");
}

void MarkCleaning() //Tar in readline checkar om det är heltal om det funakr checkar mot rumen om det finns
{
    Console.Write("\nAnge rumsnummer att markera för städning: ");
    if (!int.TryParse(Console.ReadLine(), out int number))
    {
        Console.WriteLine("Ogiltigt nummer.");
        return;
    }

    Room room = rooms.Find(r => r.RoomNumber == number);
    if (room == null)
    {
        Console.WriteLine("Rummet finns inte.");
        return;
    }

    room.Status = RoomStatus.Städning;
    room.GuestName = "";
    SaveRooms();
    Console.WriteLine($"Rummet {number} är nu markerat för städning.");
}
