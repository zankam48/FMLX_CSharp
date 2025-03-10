namespace LudoGame;

using LudoGame.Controller;
using LudoGame.Enums;
using LudoGame.Interface;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Console Ludo Game!");

        int playerCount = 0;
        while (playerCount < 2 || playerCount > 4)
        {
            Console.Write("Enter the number of players (2-4): ");
            if (int.TryParse(Console.ReadLine(), out playerCount) && playerCount >= 2 && playerCount <= 4)
                break;
            Console.WriteLine("Invalid input. Please enter a number between 2 and 4.");
        }

        // Create players
        List<Player> players = new List<Player>();
        PieceColor[] availableColors = { PieceColor.RED, PieceColor.BLUE, PieceColor.YELLOW, PieceColor.GREEN };

        for (int i = 0; i < playerCount; i++)
        {
            Console.Write($"Enter name for Player {i + 1}: ");
            string playerName = Console.ReadLine();
            players.Add(new Player(playerName, availableColors[i]));
        }

        // Create dice and game controller
        Dice dice = new Dice();
        GameController gameController = new GameController(players, dice);

        // Start game loop
        bool gameInProgress = true;
        while (gameInProgress)
        {
            Player currentPlayer = gameController.currentPlayer;
            Console.WriteLine($"\nIt's {currentPlayer.Name}'s turn ({currentPlayer.Color})!");

            // Roll dice
            int rollValue = gameController.RollDice();
            Console.WriteLine($"Player {currentPlayer.Name} rolled a {rollValue}.");

            // Display pieces
            Console.WriteLine("Your pieces:");
            for (int i = 0; i < currentPlayer.Pieces.Length; i++)
            {
                IPiece piece = currentPlayer.Pieces[i];
                string status = gameController.GetPieceStatus(piece);
                Console.WriteLine($"  [{i + 1}] Piece {i + 1}: {status}");
            }

            // Select piece to move
            int selectedPieceIndex = -1;
            while (selectedPieceIndex < 1 || selectedPieceIndex > 4)
            {
                Console.Write("Select a piece to move (1-4): ");
                if (int.TryParse(Console.ReadLine(), out selectedPieceIndex) && selectedPieceIndex >= 1 && selectedPieceIndex <= 4)
                    break;
                Console.WriteLine("Invalid input. Please select a piece between 1 and 4.");
            }

            IPiece selectedPiece = currentPlayer.Pieces[selectedPieceIndex - 1];
            bool moveSuccessful = gameController.MovePiece(selectedPiece, rollValue);

            if (!moveSuccessful)
                Console.WriteLine("Invalid move. Try again.");
            else if (selectedPiece.Status == PieceStatus.AT_HOME && rollValue != 6)
                Console.WriteLine("You need a 6 to move a piece out of home.");

            // Check if game should continue
            Console.Write("Continue playing? (y/n): ");
            string continueInput = Console.ReadLine().Trim().ToLower();
            if (continueInput != "y")
                gameInProgress = false;

            // Next player's turn
            gameController.NextPlayerTurn();
        }

        Console.WriteLine("Thanks for playing! 🎮");
    }
}
