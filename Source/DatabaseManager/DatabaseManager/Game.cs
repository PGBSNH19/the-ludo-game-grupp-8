using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    public class Game
    {
#nullable disable
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }       
        public bool GameEnded { get; set; }
        public ICollection<Player> Players { get; set; }
#nullable enable
        public DateTime LastCheckpointTime { get; set; }
    }
}
