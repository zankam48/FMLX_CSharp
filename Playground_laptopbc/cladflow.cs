



// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;

// namespace LudoConsoleGame
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("Welcome to Ludo Console Game!");
            
//             // Get number of players (2-4)
//             int playerCount = GetPlayerCount();
            
//             // Create and start the game
//             Game game = new Game(playerCount);
//             game.Start();
            
//             Console.WriteLine("Thanks for playing Ludo Console Game!");
//             Console.ReadKey();
//         }
        
//         static int GetPlayerCount()
//         {
//             int count = 0;
//             bool validInput = false;
            
//             while (!validInput)
//             {
//                 Console.Write("Enter number of players (2-4): ");
//                 string input = Console.ReadLine();
                
//                 if (int.TryParse(input, out count) && count >= 2 && count <= 4)
//                 {
//                     validInput = true;
//                 }
//                 else
//                 {
//                     Console.WriteLine("Invalid input. Please enter a number between 2 and 4.");
//                 }
//             }
            
//             return count;
//         }
//     }
    
//     class Game
//     {
//         private List<Player> players;
//         private Board board;
//         private Random random;
//         private bool gameOver;
        
//         public Game(int playerCount)
//         {
//             random = new Random();
//             board = new Board();
//             players = new List<Player>();
//             gameOver = false;
            
//             string[] colors = { "Red", "Green", "Yellow", "Blue" };
//             for (int i = 0; i < playerCount; i++)
//             {
//                 players.Add(new Player(colors[i], i));
//             }
//         }
        
//         public void Start()
//         {
//             while (!gameOver)
//             {
//                 foreach (Player player in players)
//                 {
//                     if (gameOver) break;
                    
//                     PlayTurn(player);
                    
//                     // Check if the player has won
//                     if (player.HasWon())
//                     {
//                         Console.WriteLine($"\n{player.Color} player has won the game!");
//                         gameOver = true;
//                         break;
//                     }
//                 }
//             }
//         }
        
//         private void PlayTurn(Player player)
//         {
//             Console.Clear();
//             board.Display(players);
            
//             Console.WriteLine($"\n{player.Color}'s turn. Press Enter to roll the dice...");
//             Console.ReadLine();
            
//             int roll = random.Next(1, 7);
//             Console.WriteLine($"{player.Color} rolled a {roll}");
            
//             // Check if player has any tokens in play
//             bool hasTokensInPlay = player.Tokens.Any(t => t.IsInPlay);
            
//             // If player rolled a 6 or has tokens in play
//             if (roll == 6 || hasTokensInPlay)
//             {
//                 // If player rolled a 6 and has tokens in home
//                 if (roll == 6 && player.Tokens.Any(t => !t.IsInPlay))
//                 {
//                     // Ask if player wants to bring a new token into play
//                     Console.Write("Do you want to bring a new token into play? (y/n): ");
//                     string choice = Console.ReadLine().ToLower();
                    
//                     if (choice == "y")
//                     {
//                         // Find the first token not in play
//                         Token token = player.Tokens.First(t => !t.IsInPlay);
//                         token.IsInPlay = true;
//                         token.Position = player.StartPosition;
//                         Console.WriteLine($"{player.Color} brought a new token into play!");
//                         roll = 0; // Used up the roll
//                     }
//                 }
                
//                 // If we still have a roll to use and have tokens in play
//                 if (roll > 0 && player.Tokens.Any(t => t.IsInPlay))
//                 {
//                     // If multiple tokens in play, ask which one to move
//                     if (player.Tokens.Count(t => t.IsInPlay) > 1)
//                     {
//                         MoveTokenWithChoice(player, roll);
//                     }
//                     else
//                     {
//                         // Only one token in play, move it automatically
//                         Token token = player.Tokens.First(t => t.IsInPlay);
//                         MoveToken(player, token, roll);
//                     }
//                 }
//             }
//             else
//             {
//                 Console.WriteLine($"{player.Color} cannot move any tokens this turn.");
//             }
            
//             Console.WriteLine("Press Enter to continue...");
//             Console.ReadLine();
//         }
        
//         private void MoveTokenWithChoice(Player player, int roll)
//         {
//             List<Token> tokensInPlay = player.Tokens.Where(t => t.IsInPlay).ToList();
            
//             Console.WriteLine("Which token do you want to move?");
//             for (int i = 0; i < tokensInPlay.Count; i++)
//             {
//                 Console.WriteLine($"{i + 1}. Token at position {tokensInPlay[i].Position}");
//             }
            
//             int choice = 0;
//             bool validChoice = false;
            
//             while (!validChoice)
//             {
//                 Console.Write("Enter token number: ");
//                 string input = Console.ReadLine();
                
//                 if (int.TryParse(input, out choice) && choice >= 1 && choice <= tokensInPlay.Count)
//                 {
//                     validChoice = true;
//                 }
//                 else
//                 {
//                     Console.WriteLine("Invalid choice. Please try again.");
//                 }
//             }
            
//             MoveToken(player, tokensInPlay[choice - 1], roll);
//         }
        
//         private void MoveToken(Player player, Token token, int roll)
//         {
//             int newPosition = token.Position + roll;
            
//             // Check if token is entering home stretch
//             if (token.Position < player.HomeStretchStart && newPosition >= player.HomeStretchStart)
//             {
//                 // Calculate position in home stretch
//                 int homePosition = newPosition - player.HomeStretchStart;
                
//                 // Check if token can enter home
//                 if (homePosition < 6)
//                 {
//                     token.Position = player.HomeStretchStart + homePosition;
//                     token.IsInHomeStretch = true;
//                     Console.WriteLine($"{player.Color} token moved to home stretch position {homePosition + 1}");
//                 }
//                 else if (homePosition == 6)
//                 {
//                     // Token has reached home
//                     token.IsHome = true;
//                     token.IsInPlay = false;
//                     token.IsInHomeStretch = false;
//                     Console.WriteLine($"{player.Color} token has reached home!");
//                 }
//                 else
//                 {
//                     // Can't move (need exact roll to get home)
//                     Console.WriteLine($"{player.Color} token cannot move that far into home stretch.");
//                 }
//             }
//             else
//             {
//                 // Regular movement on the main track
//                 if (token.IsInHomeStretch)
//                 {
//                     // Home stretch movement
//                     int homePosition = token.Position - player.HomeStretchStart + roll;
                    
//                     if (homePosition < 6)
//                     {
//                         token.Position = player.HomeStretchStart + homePosition;
//                         Console.WriteLine($"{player.Color} token moved to home stretch position {homePosition + 1}");
//                     }
//                     else if (homePosition == 6)
//                     {
//                         // Token has reached home
//                         token.IsHome = true;
//                         token.IsInPlay = false;
//                         token.IsInHomeStretch = false;
//                         Console.WriteLine($"{player.Color} token has reached home!");
//                     }
//                     else
//                     {
//                         // Can't move (need exact roll to get home)
//                         Console.WriteLine($"{player.Color} token cannot move that far into home stretch.");
//                     }
//                 }
//                 else
//                 {
//                     // Normal movement on track
//                     token.Position = (token.Position + roll) % 52;
//                     Console.WriteLine($"{player.Color} token moved to position {token.Position}");
                    
//                     // Check for captures
//                     CheckForCaptures(player, token);
//                 }
//             }
//         }
        
//         private void CheckForCaptures(Player player, Token movedToken)
//         {
//             // Check each opponent's tokens
//             foreach (Player opponent in players.Where(p => p != player))
//             {
//                 foreach (Token opponentToken in opponent.Tokens.Where(t => t.IsInPlay && !t.IsInHomeStretch))
//                 {
//                     // If tokens are on the same position and not on a safe square
//                     if (opponentToken.Position == movedToken.Position && !IsSafeSquare(opponentToken.Position))
//                     {
//                         // Capture the opponent's token
//                         opponentToken.IsInPlay = false;
//                         opponentToken.Position = -1;
//                         Console.WriteLine($"{player.Color} captured {opponent.Color}'s token!");
//                     }
//                 }
//             }
//         }
        
//         private bool IsSafeSquare(int position)
//         {
//             // Define safe squares (typically every 8 positions)
//             int[] safeSquares = { 0, 8, 13, 21, 26, 34, 39, 47 };
//             return safeSquares.Contains(position);
//         }
//     }
    
//     class Board
//     {
//         public void Display(List<Player> players)
//         {
//             Console.WriteLine("\n===== LUDO BOARD =====\n");
            
//             // Display a simple representation of the board
//             Console.WriteLine("Home Areas:");
//             foreach (Player player in players)
//             {
//                 Console.Write($"{player.Color}: ");
                
//                 int homeCount = player.Tokens.Count(t => t.IsHome);
//                 Console.Write($"{homeCount} tokens home | ");
                
//                 // Tokens waiting to enter play
//                 int waitingCount = player.Tokens.Count(t => !t.IsInPlay && !t.IsHome);
//                 Console.WriteLine($"{waitingCount} tokens waiting");
//             }
            
//             Console.WriteLine("\nTokens in play:");
//             foreach (Player player in players)
//             {
//                 // Show tokens in the main track
//                 var tokensInMainTrack = player.Tokens.Where(t => t.IsInPlay && !t.IsInHomeStretch);
//                 if (tokensInMainTrack.Any())
//                 {
//                     Console.Write($"{player.Color}: ");
//                     foreach (var token in tokensInMainTrack)
//                     {
//                         Console.Write($"Position {token.Position} | ");
//                     }
//                     Console.WriteLine();
//                 }
                
//                 // Show tokens in home stretch
//                 var tokensInHomeStretch = player.Tokens.Where(t => t.IsInHomeStretch);
//                 if (tokensInHomeStretch.Any())
//                 {
//                     Console.Write($"{player.Color} (Home Stretch): ");
//                     foreach (var token in tokensInHomeStretch)
//                     {
//                         int homePosition = token.Position - player.HomeStretchStart + 1;
//                         Console.Write($"Position {homePosition}/6 | ");
//                     }
//                     Console.WriteLine();
//                 }
//             }
//         }
//     }
    
//     class Player
//     {
//         public string Color { get; }
//         public List<Token> Tokens { get; }
//         public int StartPosition { get; }
//         public int HomeStretchStart { get; }
        
//         public Player(string color, int playerIndex)
//         {
//             Color = color;
//             Tokens = new List<Token>();
            
//             // Each player has 4 tokens
//             for (int i = 0; i < 4; i++)
//             {
//                 Tokens.Add(new Token());
//             }
            
//             // Set start positions (each player starts at a different point around the board)
//             StartPosition = playerIndex * 13;
//             HomeStretchStart = ((playerIndex + 3) % 4) * 13 + 1; // Home stretch starts 1 position before start
//         }
        
//         public bool HasWon()
//         {
//             // A player wins when all 4 tokens are home
//             return Tokens.All(t => t.IsHome);
//         }
//     }
    
//     class Token
//     {
//         public int Position { get; set; }
//         public bool IsInPlay { get; set; }
//         public bool IsInHomeStretch { get; set; }
//         public bool IsHome { get; set; }
        
//         public Token()
//         {
//             Position = -1; // Not on board yet
//             IsInPlay = false;
//             IsInHomeStretch = false;
//             IsHome = false;
//         }
//     }
// }