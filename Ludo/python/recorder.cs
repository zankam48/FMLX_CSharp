using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameNamespace
{
    public class Player
    {
        public string Colour { get; set; }
        public string Name { get; set; }
        public Func<string> ChoosePawnDelegate { get; set; }

        public Player(string colour, string name = null, Func<string> choosePawnDelegate = null)
        {
            Colour = colour;
            Name = name;
            ChoosePawnDelegate = choosePawnDelegate;
        }
    }

    public class RunRecord : IEnumerable<Tuple<int, int>>
    {
        private readonly List<Tuple<string, string, bool>> players;
        private readonly List<Tuple<int, int>> gameHistory;

        public RunRecord(Stream fileStream)
        {
            var formatter = new BinaryFormatter();
            var data = (Tuple<List<Tuple<string, string, bool>>, List<Tuple<int, int>>>)formatter.Deserialize(fileStream);
            players = data.Item1;
            gameHistory = data.Item2;
        }

        public List<Player> GetPlayers(Func<string, string> func = null)
        {
            var res = new List<Player>();
            foreach (var playerData in players)
            {
                var colour = playerData.Item1;
                var name = playerData.Item2;
                var isComputer = playerData.Item3;
                if (isComputer)
                {
                    res.Add(new Player(colour));
                }
                else
                {
                    res.Add(new Player(colour, name, func));
                }
            }
            return res;
        }

        public List<Tuple<int, int>> GetGameHistory()
        {
            return gameHistory;
        }

        public IEnumerator<Tuple<int, int>> GetEnumerator()
        {
            foreach (var history in gameHistory)
            {
                yield return history;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MakeRecord
    {
        private readonly List<Tuple<string, string, bool>> players;
        private readonly List<Tuple<int, int>> gameHistory;

        public MakeRecord()
        {
            players = new List<Tuple<string, string, bool>>();
            gameHistory = new List<Tuple<int, int>>();
        }

        public void AddPlayer(Player playerObj)
        {
            bool isComputer = playerObj.ChoosePawnDelegate == null;
            players.Add(new Tuple<string, string, bool>(playerObj.Colour, playerObj.Name, isComputer));
        }

        public void AddGameTurn(int rolledValue, int index)
        {
            gameHistory.Add(new Tuple<int, int>(rolledValue, index));
        }

        public void Save(Stream fileStream)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, new Tuple<List<Tuple<string, string, bool>>, List<Tuple<int, int>>>(players, gameHistory));
        }
    }
}
