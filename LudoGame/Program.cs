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
            string playerName = Console.ReadLine() ?? $"Player{i + 1}"; // Ensures no null values
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
                continueRolling = false; // Reset for each roll

                // Manual dice roll
                int rollValue = gameController.RollDice();
                Console.WriteLine($"🎲 {currentPlayer.Name} rolled a {rollValue}.");

                // Check if player has a valid move
                if (!gameController.CanPlayerMove(currentPlayer, rollValue))
                {
                    Console.WriteLine("❌ No available moves. Turn skipped.");
                    break; // Skip to next player
                }

                // Display pieces
                Console.WriteLine("Your pieces:");
                for (int i = 0; i < currentPlayer.Pieces.Length; i++)
                {
                    IPiece piece = currentPlayer.Pieces[i];
                    string status = gameController.GetPieceStatus(piece);
                    Console.WriteLine($"  [{i + 1}] Piece {i + 1}: {status}");
                }

                // Ensure player picks a valid piece
                IPiece selectedPiece;
    
                while (true) // Loop until a valid piece is selected
                {
                    Console.Write("Select a piece to move (1-4): ");
                    if (!int.TryParse(Console.ReadLine(), out int selectedPieceIndex) ||
                        selectedPieceIndex < 1 || selectedPieceIndex > 4)
                    {
                        Console.WriteLine("❌ Invalid selection. Try again.");
                        continue;
                    }

                    selectedPiece = currentPlayer.Pieces[selectedPieceIndex - 1];

                    // Enforce valid move: Can't move a piece from home unless roll is 6
                    if (selectedPiece.Status == PieceStatus.AT_HOME && rollValue < 6 &&
                        gameController.HasPieceInPlay(currentPlayer))
                    {
                        Console.WriteLine("❌ Invalid move! Move a piece that is already in play.");
                        continue; // Keep looping until a valid piece is chosen
                    }

                    break; // Valid selection, exit loop
                }

                if (rollValue == 6)
                {
                    gameController.HandleSixRoll(selectedPiece, rollValue);
                    continueRolling = true; // Allow another turn if 6 is rolled
                }
                else
                {
                    gameController.MovePiece(selectedPiece, rollValue);
                }
                

            } while (continueRolling); // Repeat if player rolled a 6

            gameController.NextPlayerTurn();
        }
    }
}
