using PasswordHashAndSalt;
using System.Security.Cryptography;
using System.Text;

Prompt();

void Prompt()
{
    Console.Clear();

    Console.WriteLine("[R] Register\n[L] Login");

    while (true)
    {
        // Just takes the first letter that was entered, incase user wrote Register or Login
        var input = Console.ReadLine().ToUpper()[0];

        switch (input)
        {
            case 'R': Register(); break;
            case 'L': Login(); break;
            default:
                break;
        }
    }
}

void Login()
{
    Console.Clear();
    Console.WriteLine("================ LOGIN ================");

    Console.Write("User Name: ");
    var name = Console.ReadLine();

    Console.Write("Password: ");
    var password = Console.ReadLine();

    using PasswordAppDbContext context = new PasswordAppDbContext();

    // Checking if user exists in database, returns a bool
    var userFound = context.Users.Any(u => u.Name == name);

    if (userFound)
    {   
        // Grab the user with that name
        var loginUser = context.Users.FirstOrDefault(u => u.Name == name);

        // If the hashed password that user provided at login matches the hashed salted password in database
        if (HashPassword($"{password}{loginUser.Salt}") == loginUser.Password)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Login Successful");
            Console.ReadLine();
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Login Failed");
            Console.ReadLine();
        }
    }
}

void Register()
{
    Console.Clear();
    Console.WriteLine("================ REGISTER ================");

    Console.Write("User Name: ");
    var name = Console.ReadLine();

    Console.Write("Password: ");
    var password = Console.ReadLine();

    using PasswordAppDbContext context = new PasswordAppDbContext();

    var salt = DateTime.Now.ToString();

    // Hashing the password that user gave to register
    var hashedPassword = HashPassword($"{password}{salt}");

    context.Users.Add(new User() { Name = name, Password = hashedPassword, Salt = salt });
    context.SaveChanges();

    while (true)
    {
        Console.Clear();
        Console.WriteLine("Registration Complete");
        Console.WriteLine("[B] Back");
        if (Console.ReadKey().Key == ConsoleKey.B)
        {
            Prompt();
        }
    }
}

string HashPassword(string password)
{
    SHA256 hash = SHA256.Create();

    // Takes in a string and returns an array of Bytes
    var passwordBytes = Encoding.Default.GetBytes(password);

    // ComputeHash takes in an array of Bytes and returns an array of Bytes of the hashed password
    var hashedPasswordInBytes = hash.ComputeHash(passwordBytes);

    // Converts Bytes into String
    var hashedPassword = Convert.ToHexString(hashedPasswordInBytes);

    return hashedPassword;
}
