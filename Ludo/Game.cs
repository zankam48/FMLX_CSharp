class Game 
{
    private LudoBoard board;
    // private Player[] players;
    private List<Player> players;
    private Dice? dice;
    private GameState state;
    private int currentPlayerIdx;
    private bool isGameOver = false;

    public Game(int numberOfPlayers)
    {
        players = new List<Player>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players.Add(new Player($"Player {i + 1}", new List<Piece>()));
        }
        dice = new Dice();
        state = GameState.NOT_STARTED;
        currentPlayerIdx = 0;
    }
    

    public void ChoosePiece(Piece piece)
    {

    }

    

    public void MovePiece(Player player, int diceValue)
    {
        // piece.MoveWith
    }

    // public void SetBoard(Board board)
    // {
        
    // }

    public void StartGame()
    {
        while (!isGameOver){
            // draw board

        }
    }

    // method ini bisa dipake kalo semisal butuh buat ngecek2 score bbrp saat, isAtGoal ntar jadi middleware gtu pake boolean terus ntar di assign ke setGoal
    // public void UpdateScore(Player player)
    // {
    //     foreach (var piece in player.GetPieces())
    //     {
    //         if (board.IsAtGoal(piece) && !piece.IsAtGoal())
    //         {
    //             player.IncrementScore();
    //             piece.SetAtGoal(); // Set a flag to prevent double scoring
    //         }
    //     }
    // }
}
