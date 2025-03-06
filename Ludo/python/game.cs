using System;
using System.Collections.Generic;
using System.Linq;

namespace GameNamespace
{
    public class Pawn
    {
        public int Index { get; set; }
        public string Colour { get; set; }
        public string Id { get; set; }

        public Pawn(int index, string colour)
        {
            Index = index;
            Colour = colour;
            Id = $"{colour[0].ToString().ToUpper()}{index}";
        }
    }

    public class Player
    {
        public string Colour { get; set; }
        public string Name { get; set; }
        public Func<List<Pawn>, int> ChoosePawnDelegate { get; set; }
        public bool Finished { get; set; }
        public List<Pawn> Pawns { get; set; }

        public Player(string colour, string name = null, Func<List<Pawn>, int> choosePawnDelegate = null)
        {
            Colour = colour;
            ChoosePawnDelegate = choosePawnDelegate;
            Name = name ?? (choosePawnDelegate == null ? "computer" : null);
            Finished = false;

            // Initialize four pawns with id (first letter from colour and index from 1 to 4)
            Pawns = Enumerable.Range(1, 4).Select(i => new Pawn(i, colour)).ToList();
        }

        public override string ToString()
        {
            return $"{Name}({Colour})";
        }

        public int ChoosePawn(List<Pawn> pawns)
        {
            int index;

            if (pawns.Count == 1)
            {
                index = 0;
            }
            else
            {
                if (ChoosePawnDelegate == null)
                {
                    // Randomly choose if there is no delegate function
                    Random rand = new Random();
                    index = rand.Next(pawns.Count);
                }
                else
                {
                    index = ChoosePawnDelegate(pawns);
                }
            }

            return index;
        }
    }

    public class Board
    {
        // Common (shared) squares for all pawns
        public static readonly int BOARD_SIZE = 56;

        // Save (private) positions (squares) for each colour
        // This is squares just before pawn finished
        public static readonly int BOARD_COLOUR_SIZE = 7;

        public static readonly List<string> COLOUR_ORDER = new List<string> { "yellow", "blue", "red", "green" };

        // Distance between two neighbour colours
        // (The distance from start square of one colour to start square of next colour)
        public static readonly int COLOUR_DISTANCE = 14;

        // Start and end position dictionaries for every colour
        public static Dictionary<string, int> COLOUR_START { get; private set; }
        public static Dictionary<string, int> COLOUR_END { get; private set; }

        private readonly Dictionary<Pawn, Tuple<int, int>> pawnsPosition;
        private readonly PaintBoard painter;

        // Pool means before start
        private readonly Tuple<int, int> boardPoolPosition;

        static Board()
        {
            // Initialize static dictionaries
            COLOUR_START = COLOUR_ORDER.Select((colour, index) => new { colour, index })
                                       .ToDictionary(x => x.colour, x => 1 + x.index * COLOUR_DISTANCE);
            COLOUR_END = COLOUR_ORDER.Select((colour, index) => new { colour, index })
                                     .ToDictionary(x => x.colour, x => x.index * COLOUR_DISTANCE);
            COLOUR_END["yellow"] = BOARD_SIZE;
        }

        public Board()
        {
            pawnsPosition = new Dictionary<Pawn, Tuple<int, int>>();
            painter = new PaintBoard();
            boardPoolPosition = new Tuple<int, int>(0, 0);
        }

        public void SetPawn(Pawn pawn, Tuple<int, int> position)
        {
            pawnsPosition[pawn] = position;
        }

        public void PutPawnOnBoardPool(Pawn pawn)
        {
            SetPawn(pawn, boardPoolPosition);
        }

        public bool IsPawnOnBoardPool(Pawn pawn)
        {
            return pawnsPosition[pawn] == boardPoolPosition;
        }

        public void PutPawnOnStartingSquare(Pawn pawn)
        {
            int start = COLOUR_START[pawn.Colour.ToLower()];
            var position = new Tuple<int, int>(start, 0);
            SetPawn(pawn, position);
        }

        public bool CanPawnMove(Pawn pawn, int rolledValue)
        {
            var position = pawnsPosition[pawn];
            int commonPosition = position.Item1;
            int privatePosition = position.Item2;

            if (privatePosition + rolledValue > BOARD_COLOUR_SIZE)
                return false;
            return true;
        }

        public void MovePawn(Pawn pawn, int rolledValue)
        {
            var position = pawnsPosition[pawn];
            int commonPosition = position.Item1;
            int privatePosition = position.Item2;

            int end = COLOUR_END[pawn.Colour.ToLower()];
            if (privatePosition > 0)
            {
                // Pawn has already reached own final squares
                privatePosition += rolledValue;
            }
            else if (commonPosition <= end && commonPosition + rolledValue > end)
            {
                // Pawn is entering its own square
                privatePosition += rolledValue - (end - commonPosition);
                commonPosition = end;
            }
            else
            {
                // Pawn will be still in common square
                commonPosition += rolledValue;
                if (commonPosition > BOARD_SIZE)
                    commonPosition = commonPosition - BOARD_SIZE;
            }

            var newPosition = new Tuple<int, int>(commonPosition, privatePosition);
            SetPawn(pawn, newPosition);
        }

        public bool DoesPawnReachEnd(Pawn pawn)
        {
            var position = pawnsPosition[pawn];
            return position.Item2 == BOARD_COLOUR_SIZE;
        }

        public List<Pawn> GetPawnsOnSamePosition(Pawn pawn)
        {
            var position = pawnsPosition[pawn];
            return pawnsPosition.Where(x => x.Value.Equals(position))
                                .Select(x => x.Key)
                                .ToList();
        }

        public void PaintBoard()
        {
            var positions = new Dictionary<Tuple<int, int>, List<Pawn>>();
            foreach (var kvp in pawnsPosition)
            {
                var position = kvp.Value;
                if (position.Item2 != BOARD_COLOUR_SIZE)
                {
                    if (!positions.ContainsKey(position))
                        positions[position] = new List<Pawn>();

                    positions[position].Add(kvp.Key);
                }
            }

            painter.Paint(positions);
        }
    }

    // Assuming PaintBoard class is defined somewhere else
    public class PaintBoard
    {
        public void Paint(Dictionary<Tuple<int, int>, List<Pawn>> positions)
        {
            // Paint logic
        }
    }

    // Assuming Pawn class is defined somewhere else
    public class Pawn
    {
        public string Colour { get; set; }
        public string Id { get; set; }

        public Pawn(string colour, string id)
        {
            Colour = colour;
            Id = id;
        }
    }

class Die():

MIN = 1
MAX = 6

@staticmethod
def throw():
    return random.randint(Die.MIN, Die.MAX)


class Game():
    '''Knows the rules of the game.
    Knows for example what to do when 
    one pawn reach another
    or pawn reach end or 
    player roll six and so on
    '''

    def __init__(self):
        self.players = deque()
        self.standing = []
        self.board = Board()
        # is game finished
        self.finished = False
        # last rolled value from die (dice)
        self.rolled_value = None
        # player who last rolled die
        self.curr_player = None
        # curr_player's possible pawn to move
        self.allowed_pawns = []
        # curr_player's chosen pawn to move
        self.picked_pawn = None
        # chosen index from allowed pawn 
        self.index = None
        # jog pawn if any 
        self.jog_pawns = []

    def add_palyer(self, player):
        self.players.append(player)
        for pawn in player.pawns:
            self.board.put_pawn_on_board_pool(pawn)

    def get_available_colours(self):
        '''if has available colour on boards'''
        used = [player.colour for player in self.players]
        available = set(self.board.COLOUR_ORDER) - set(used)
        return sorted(available)

    def _get_next_turn(self):
        '''Get next player's turn.
        It is underscore because if called 
        outside the class will break order
        '''
        if not self.rolled_value == Die.MAX:
            self.players.rotate(-1)
        return self.players[0]

    def get_pawn_from_board_pool(self, player):
        '''when pawn must start'''
        for pawn in player.pawns:
            if self.board.is_pawn_on_board_pool(pawn):
                return pawn
    
    public object GetBoardPic()
    {
        return board.PaintBoard();
    }

    private void JogForeignPawn(Pawn pawn)
    {
        List<Pawn> pawns = board.GetPawnsOnSamePosition(pawn);
        foreach (var p in pawns)
        {
            if (p.Colour != pawn.Colour)
            {
                board.PutPawnOnBoardPool(p);
                jogPawns.Add(p);
            }
        }
    }

    private void MakeMove(Player player, Pawn pawn)
    {
        if (rolledValue == Die.MAX && board.IsPawnOnBoardPool(pawn))
        {
            board.PutPawnOnStartingSquare(pawn);
            JogForeignPawn(pawn);
            return;
        }

        board.MovePawn(pawn, rolledValue);

        if (board.DoesPawnReachEnd(pawn))
        {
            player.Pawns.Remove(pawn);
            if (player.Pawns.Count == 0)
            {
                standing.Add(player);
                players.Remove(player);
                if (players.Count == 1)
                {
                    standing.AddRange(players);
                    finished = true;
                }
            }
        }
        else
        {
            JogForeignPawn(pawn);
        }
    }

    public void PlayTurn(int? ind = null, int? rolledVal = null)
    {
        jogPawns.Clear();
        currPlayer = GetNextTurn();

        // Roll die or use given rolled value
        rolledValue = rolledVal ?? Die.Throw();

        allowedPawns = GetAllowedPawnsToMove(currPlayer, rolledValue);

        if (allowedPawns.Any())
        {
            if (ind == null)
            {
                index = currPlayer.ChoosePawn(allowedPawns);
            }
            else
            {
                index = ind.Value;
            }

            pickedPawn = allowedPawns[index];
            MakeMove(currPlayer, pickedPawn);
        }
        else
        {
            index = -1;
            pickedPawn = null;
        }
    }

    private Player GetNextTurn()
    {
        // Get next player's turn. This method may need to rotate through players if needed.
        // Assuming a circular list of players, which is similar to deque
        // If it's not the rolled value of 6, rotate players list
        if (rolledValue != Die.MAX)
        {
            var first = players[0];
            players.RemoveAt(0);
            players.Add(first);
        }
        return players[0];
    }

    private Pawn GetPawnFromBoardPool(Player player)
    {
        foreach (var pawn in player.Pawns)
        {
            if (board.IsPawnOnBoardPool(pawn))
            {
                return pawn;
            }
        }
        return null;
    }
}
