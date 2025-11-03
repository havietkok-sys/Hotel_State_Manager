namespace App;

class User
{
    public string Username;  // Användar namn
    public string Password;  // Lösen

    public User(string username, string password) // Skapar ny användare
    {
        Username = username;
        Password = password;
    }
}