using System;

public class Game
{
    private bool isEnd = false;
    // private Player player1 = new Player("halo");
    // private Piece piece1 = new Piece();
    private List<Piece> pieces = new List<Piece>();
    private List<Player> players = new List<Player>();
    private Board board;

    public Game(int boardSize)
    {
        board = new Board(boardSize); // Initialize the board with the given size
    }

    public void Run()
    {
        while (!isEnd)
        {
            // Show the current state of the board
            board.DrawBoard();

            // Prompt the user to input the jump distance
            Console.WriteLine("Press 'y' to continue rolling dice, 'n' to end the game.");
            string input = Console.ReadLine();
            if (input == "y")
            {
                // Simulate the dice roll
                Dice dice = new Dice();
                int result = dice.Roll();
                Console.WriteLine($"Rolling dice: {result}");

                if (result == 6)
                {
                    players[0].canMakeMove = true; // Set flag for making a move
                    players[0].ChoosePiece(pieces[0]);  // Let player make the move
                    board.MoveX(result);       // Move 'X' by the rolled dice result

                    // Check if the game is over
                    if (board.IsGameOver())
                    {
                        Console.WriteLine("The 'X' has reached the last position!");
                        isEnd = true;
                    }
                }
            }
            else
            {
                // End the game if 'n' is pressed
                isEnd = true;
            }
        }
    }
}
