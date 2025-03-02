
public enum UserRole
{
    Admin = 1,
    User = 2,
    Guest = 3
}

class Program
{
    static void Main()
    {
        // UserRole role = UserRole.Admin;

        // using casting
        int intRole = 2;
        UserRole role = (UserRole)intRole;

        if (role == UserRole.Admin)
        {
            Console.WriteLine("User Admin");
        }
        else if (role == UserRole.User)
        {
            Console.WriteLine("User Regular");
        }
        else if (role == UserRole.Guest)
        {
            Console.WriteLine("User Guest");
        }
    }
}

