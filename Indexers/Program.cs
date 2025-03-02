class Program
{
    static void Main()
    {
        Heroes heroes1 = new Heroes();

        Console.WriteLine($"{heroes1[1]}");
        Console.WriteLine($"{heroes1["Healer"]}");

        heroes1[1] = "Dr Stone";
        Console.WriteLine($"{heroes1[1]}");
    }
}