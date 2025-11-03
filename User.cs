namespace App;

public class User
{
    public string Username;  // Användar namn
    public string Password;  // Lösen

    public static readonly User Admin = new User("admin", "123");
    public User(string username, string password) // Skapar ny användare
    {
        Username = username;
        Password = password;
    }
}