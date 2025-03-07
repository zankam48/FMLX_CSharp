// public class Game
// {
//     private bool isEnd = false;
//     private List<Player> players = new List<Player>();
//     private Board board;


//     public void Run()
//     {
//         while (!isEnd)
//         {
//             board.DrawBoard();

//             // Prompt the user to input the jump distance
//             Console.WriteLine("Press 'y' to continue rolling dice, 'n' to end the game.");
//             string input = Console.ReadLine();
//             if (input == "y")
//             {
//                 // Simulate the dice roll
//                 Dice dice = new Dice();
//                 int result = dice.Roll();
//                 Console.WriteLine($"Rolling dice: {result}");

//                 if (result == 6)
//                 {
//                     players[0].canMakeMove = true; // Set flag for making a move
//                     players[0].ChoosePiece(pieces[0]);  // Let player make the move
//                     board.MoveX(result);       // Move 'X' by the rolled dice result

//                     // Check if the game is over
//                     if (board.IsGameOver())
//                     {
//                         Console.WriteLine("The 'X' has reached the last position!");
//                         isEnd = true;
//                     }
//                 }
//             }
//             else
//             {
//                 // End the game if 'n' is pressed
//                 isEnd = true;
//             }
//         }
//     }
// }