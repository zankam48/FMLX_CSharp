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

        // Set up delegates
        gameController.OnDiceRoll = (d) => d.Roll();

        gameController.OnNextPlayerTurn = (player) =>
        {
            Console.WriteLine($"\n🔄 It's now {player.Name}'s turn ({player.Color})!");
        };

        gameController.OnSixRoll = (player, piece, rollResult) =>
        {
            Console.WriteLine($"🎉 {player.Name} rolled a 6!");

            if (piece.Status == PieceStatus.AT_HOME)
            {
                Console.WriteLine("🏠 Bringing a piece out of home!");
                piece.Status = PieceStatus.IN_PLAY;
                piece.Position = new Position(0, 0);
            }
        };

        // Start game loop
        while (true)
        {
            Player currentPlayer = gameController.currentPlayer;
            Console.WriteLine($"\nIt's {currentPlayer.Name}'s turn ({currentPlayer.Color})!");

            bool continueRolling;
            do
            {
                continueRolling = false;  // Reset for each roll

                // Roll dice
                int rollValue = gameController.RollDice();
                Console.WriteLine($"🎲 {currentPlayer.Name} rolled a {rollValue}.");

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

                if (rollValue == 6)
                {
                    gameController.HandleSixRoll(selectedPiece, rollValue);
                    continueRolling = true;  // Allow another turn if 6 is rolled
                }
                else if (!gameController.MovePiece(selectedPiece, rollValue))
                {
                    Console.WriteLine("Invalid move. Try again.");
                }

            } while (continueRolling);  // Repeat if player rolled a 6

            // Next player's turn
            gameController.NextPlayerTurn();
        }
    }
}
