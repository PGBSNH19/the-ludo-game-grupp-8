using System;
using System.Collections.Generic;

namespace DatabaseManager
{
    public class Game
    {
#nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }       
        public bool GameEnded { get; set; }
        public int Turn { get; set; }
        public ICollection<Player> Players { get; set; }
#nullable enable
        public DateTime LastCheckpointTime { get; set; }
    }
}
