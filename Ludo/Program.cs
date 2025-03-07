using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("----Ludo Game----");
        Game game = new Game(4);
        Console.WriteLine("Choose Player Amount1\n 1. 2 Player\n 2. 3 Player\n 3. 4 Player\n"); // alternatif bisa add first player -> added -> add second player dst -> start game
        Console.WriteLine("Create your name and choose piece color"); // dipisah bisa ntar ngeprompt lagi
        Console.WriteLine("Add player or start game with {x} players");
        Console.WriteLine("Press any key to roll the dice..."); // no possible pawn to move kl gk 6
        Console.WriteLine("You get 6!");
        Console.WriteLine("Choose piece to move!");
    }
}


