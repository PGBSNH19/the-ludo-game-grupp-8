using System;
using System.Collections.Generic;

namespace DatabaseManager
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }       
        public bool GameEnded { get; set; }
        public int Turn { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public DateTime LastCheckpointTime { get; set; }


        public Game()
        {
            Players = new List<Player>();
        }
    }
}
