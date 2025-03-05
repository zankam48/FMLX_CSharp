using System;
using System.Xml.XPath;

public class Game
{
    private bool isEnd = false;
    Player player1 = new Player();
    public void Run()
    {
        while (!isEnd)
        {
            Console.WriteLine("Press y to continue rolling dice, n to end.");
            string input = Console.ReadLine();
            if (input == "y")
            {
                Dice dice = new Dice();
                int result = dice.Roll();
                Console.WriteLine($"Rolling dice : {result}");
                
                // isEnd = true;
                if (result == 6){
                    player1.canMakeMove = true;
                    player1.MakeMove(result);
                    isEnd = true;
                }
            } else {
                isEnd = true;
            }
        }
    }
}