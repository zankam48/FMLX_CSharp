using System;
using System.Collections.Generic;
using System.IO;

public class CLIGame
{
    private string promptEnd = "> ";
    private Game game;
    private bool promptedForPawn = false;
    private MakeRecord recordMaker;
    private RunRecord recordRunner;

    public CLIGame()
    {
        this.game = new Game();
        this.recordMaker = new MakeRecord();
        this.recordRunner = null;
    }

    public T ValidateInput<T>(string prompt, Func<string, T> desireType, List<T> allowedInput = null, string errorMess = "Invalid Option!", (int, int)? strLen = null)
    {
        // Loop while receiving the correct value
        prompt += Environment.NewLine + promptEnd;
        while (true)
        {
            Console.Write(prompt);
            string choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice))
            {
                Console.WriteLine(Environment.NewLine + errorMess);
                continue;
            }

            try
            {
                choice = desireType(choice).ToString();
            }
            catch (Exception)
            {
                Console.WriteLine(Environment.NewLine + errorMess);
                continue;
            }

            if (allowedInput != null)
            {
                if (allowedInput.Contains(desireType(choice)))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option!");
                    continue;
                }
            }
            else if (strLen.HasValue)
            {
                var (minLen, maxLen) = strLen.Value;
                if (minLen <= choice.Length && choice.Length <= maxLen)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + errorMess);
                }
            }
            else
            {
                break;
            }
        }
        Console.WriteLine();
        return desireType(choice);
    }

    public int GetUserInitialChoice()
    {
        string text = Environment.NewLine + string.Join(Environment.NewLine,
            "Choose option",
            "0 - Start new game",
            "1 - Continue game",
            "2 - Run (review) recorded game");
        int choice = ValidateInput(text, int.Parse, new List<int> { 0, 1, 2 });
        return choice;
    }

    public FileStream PromptForFile(string mode = "rb")
    {
        string text = "Enter filename (name of the record)";
        while (true)
        {
            string filename = ValidateInput(text, str => str, null);
            try
            {
                FileStream fileDescr = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                return fileDescr;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try again");
            }
        }
    }

    public bool DoesUserWantSaveGame()
    {
        string text = Environment.NewLine + string.Join(Environment.NewLine,
            "Save game?",
            "0 - No",
            "1 - Yes");
        int choice = ValidateInput(text, int.Parse, new List<int> { 0, 1 });
        return choice == 1;
    }

    public void PromptForPlayer()
    {
        List<string> availableColours = game.GetAvailableColours();
        string text = Environment.NewLine + string.Join(Environment.NewLine,
            "Choose type of player",
            "0 - Computer",
            "1 - Human");
        int choice = ValidateInput(text, int.Parse, new List<int> { 0, 1 });

        if (choice == 1)
        {
            string name = ValidateInput("Enter name for player", str => str, null);
            List<int> availableOptions = new List<int>();
            for (int i = 0; i < availableColours.Count; i++) availableOptions.Add(i);

            if (availableOptions.Count > 1)
            {
                List<string> options = new List<string>();
                for (int i = 0; i < availableColours.Count; i++)
                {
                    options.Add($"{i} - {availableColours[i]}");
                }
                text = "Choose colour" + Environment.NewLine + string.Join(Environment.NewLine, options);
                choice = ValidateInput(text, int.Parse, availableOptions);
                string colour = availableColours[choice];
                availableColours.RemoveAt(choice);
                Player player = new Player(colour, name, PromptChoosePawn);
                game.AddPlayer(player);
            }
            else
            {
                string colour = availableColours[0];
                availableColours.RemoveAt(0);
                Player player = new Player(colour, name, PromptChoosePawn);
                game.AddPlayer(player);
            }
        }
        else if (choice == 0)
        {
            string colour = availableColours[0];
            availableColours.RemoveAt(0);
            Player player = new Player(colour);
            game.AddPlayer(player);
        }
    }
    public void PromptForPlayers()
    {
        // Put all players in the game
        string[] counts = { "first", "second", "third", "fourth last" };
        string textAdd = "Add {0} player";

        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine(string.Format(textAdd, counts[i]));
            PromptForPlayer();
            Console.WriteLine("Player added");
        }

        string text = string.Join(Environment.NewLine,
            "Choose option:",
            "0 - Add player",
            "1 - Start game with {0} players");

        for (int i = 2; i < 4; i++)
        {
            int choice = ValidateInput(string.Format(text, i), int.Parse, new List<int> { 0, 1 });
            if (choice == 1)
                break;
            else if (choice == 0)
            {
                Console.WriteLine(string.Format(textAdd, counts[i]));
                PromptForPlayer();
                Console.WriteLine("Player added");
            }
        }
    }

    public int PromptChoosePawn()
    {
        // Used when player (human) has more than one possible pawn to move.
        // This method is passed as a callable during player instantiation
        string text = Present6DieName(this.game.rolledValue, this.game.currPlayer.ToString());
        text += Environment.NewLine + "has more than one possible pawns to move.";
        text += " Choose pawn" + Environment.NewLine;

        List<string> pawnOptions = new List<string>();
        for (int i = 0; i < this.game.allowedPawns.Count; i++)
        {
            pawnOptions.Add($"{i + 1} - {this.game.allowedPawns[i].id}");
        }

        text += string.Join(Environment.NewLine, pawnOptions);
        int index = ValidateInput(text, int.Parse, Enumerable.Range(1, this.game.allowedPawns.Count).ToList());
        this.promptedForPawn = true;
        return index - 1;
    }

    public void PromptToContinue()
    {
        string text = "Press Enter to continue" + Environment.NewLine;
        Console.ReadLine(); // Wait for user to press Enter
    }

    public void PrintPlayersInfo()
    {
        string word = this.game.rolledValue == null ? "start" : "continue";
        Console.WriteLine($"Game {word} with {this.game.players.Count} players:");
        foreach (var player in this.game.players)
        {
            Console.WriteLine(player.ToString());
        }
        Console.WriteLine();
    }

    public void PrintInfoAfterTurn()
    {
        // Used game attributes to print info
        List<string> pawnsId = this.game.allowedPawns.Select(pawn => pawn.id).ToList();
        string message = Present6DieName(this.game.rolledValue, this.game.currPlayer.ToString());
        message += Environment.NewLine;

        if (this.game.allowedPawns.Count > 0)
        {
            string messageMoved = $"{this.game.pickedPawn.id} is moved. ";
            if (this.promptedForPawn)
            {
                this.promptedForPawn = false;
                Console.WriteLine(messageMoved);
                return;
            }

            message += $"{string.Join(" ", pawnsId)} possible pawns to move. ";
            message += messageMoved;

            if (this.game.jogPawns.Count > 0)
            {
                message += "Jog pawn ";
                message += string.Join(" ", this.game.jogPawns.Select(pawn => pawn.id));
            }
        }
        else
        {
            message += "No possible pawns to move.";
        }

        Console.WriteLine(message);
    }

    public void PrintStanding()
    {
        List<string> standingList = this.game.standing
            .Select((player, index) => $"{index + 1} - {player}")
            .ToList();

        string message = "Standing:" + Environment.NewLine + string.Join(Environment.NewLine, standingList);
        Console.WriteLine(message);
    }

    public void PrintBoard()
    {
        Console.WriteLine(this.game.GetBoardPic());
    }

    public void RunRecordedGame()
    {
        // Get history of game (rolled_value and index's allowed pawn) from record_runner to replay game
        LoadRecordedPlayers();
        PrintPlayersInfo();
        PromptToContinue();

        foreach (var (rolledValue, index) in this.recordRunner)
        {
            this.game.PlayTurn(index, rolledValue);
            PrintInfoAfterTurn();
            PrintBoard();
            PromptToContinue();
            PrintBoard();
        }
    }

    public void ContinueRecordedGame()
    {
        // Move forward the game by calling play_turn method to the moment where game was interrupted
        LoadRecordedPlayers();
        RecordPlayers();

        foreach (var (rolledValue, index) in this.recordRunner)
        {
            this.game.PlayTurn(index, rolledValue);
            this.recordMaker.AddGameTurn(this.game.rolledValue, this.game.index);
        }

        PrintPlayersInfo();
        PrintInfoAfterTurn();
        PrintBoard();
    }

    public void RecordPlayers()
    {
        // Save players on recorder
        foreach (var player in this.game.players)
        {
            this.recordMaker.AddPlayer(player);
        }
    }

    public void LoadRecordedPlayers()
    {
        // Get recorded (saved) players from recorder and put them in game
        if (this.recordRunner == null)
        {
            var fileDescr = PromptForFile();
            this.recordRunner = new RunRecord(fileDescr);
            fileDescr.Close();
        }

        foreach (var player in this.recordRunner.GetPlayers(PromptChoosePawn))
        {
            this.game.AddPlayer(player);
        }
    }

    public void LoadPlayersForNewGame()
    {
        PromptForPlayers();
        PrintPlayersInfo();
        RecordPlayers();
    }

    public void PlayGame()
    {
        // Main method, calling play_turn while game is not finished
        try
        {
            while (!this.game.finished)
            {
                this.game.PlayTurn();
                PrintInfoAfterTurn();
                PrintBoard();
                this.recordMaker.AddGameTurn(this.game.rolledValue, this.game.index);
                PromptToContinue();
            }

            Console.WriteLine("Game finished");
            PrintStanding();
            OfferSaveGame();
        }
        catch (KeyboardInterruptException)
        {
            Console.WriteLine(Environment.NewLine + "Exiting game. Save game and continue same game later?");
            OfferSaveGame();
            throw;
        }
        catch (EOFException)
        {
            Console.WriteLine(Environment.NewLine + "Exiting game. Save game and continue same game later?");
            OfferSaveGame();
            throw;
        }
    }

    public void OfferSaveGame()
    {
        // Offer user to save game
        if (DoesUserWantSaveGame())
        {
            var fileDescr = PromptForFile("wb");
            this.recordMaker.Save(fileDescr);
            fileDescr.Close();
            Console.WriteLine("Game is saved");
        }
    }

    public void Start()
    {
        // Main method, starting CLI
        Console.WriteLine();
        try
        {
            int choice = GetUserInitialChoice();
            if (choice == 0) // Start new game
            {
                LoadPlayersForNewGame();
                PlayGame();
            }
            else if (choice == 1) // Continue game
            {
                ContinueRecordedGame();
                if (this.game.finished)
                {
                    Console.WriteLine("Could not continue. Game is already finished\nExit");
                }
                else
                {
                    PromptToContinue();
                    PlayGame();
                }
            }
            else if (choice == 2) // Review played game
            {
                RunRecordedGame();
            }
        }
        catch (KeyboardInterruptException)
        {
            Console.WriteLine(Environment.NewLine + "Exit Game");
        }
        catch (EOFException)
        {
            Console.WriteLine(Environment.NewLine + "Exit Game");
        }
    }
}

// Start the game
public static void Main()
{
    new CLIGame().Start();
}
