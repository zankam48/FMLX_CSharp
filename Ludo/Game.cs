using System;

// public bool IsOverlapped(Piece piece1, Piece piece2)
//     {
//         return piece1.Position == piece2.Position && piece1.Player != piece2.Player;
//     }

// class Game {
//         +Game(players: List~Player~, board: Board)
//         +startGame() : void
//         +endGame() : void
//         +nextPlayerTurn() : void
//         +currentPlayer : Player
//         +players : List~Player~
//         +getWinner() : Player
//         +SetBoard() : void
//         +Dice dice
//         -Board board
//         +isEnd : bool
//         +MovePiece(Piece piece) : void
//         +ChoosePiece(Piece piece) : piece
//         +isOverlapped(Player player1, Player player2) : bool
//         +RollDice() : int
//         +Action~Player~OnNextPlayerTurn()
//         +Action<Dice> OnDiceRoll
//     }


class Game 
{
    Dice dice = new Dice();
    Player player = new Player();

    private bool isEnd = false;
    
    public bool isOverlapped(Piece piece1, Piece piece2)
    {
        return piece1.Position == piece2.Position && piece1.Player != piece2.Player;
    }

    public void ChoosePiece(Piece piece)
    {

    }

    public void MovePiece(Piece piece)
    {
        // piece.MoveWith
    }

    public void SetBoard(Board board)
    {
        
    }

    public void StartGame()
    {
        while (!isEnd){
            // draw board

        }
    }
}
