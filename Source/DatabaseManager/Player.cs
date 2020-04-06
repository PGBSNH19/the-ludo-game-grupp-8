using System;
using System.Collections.Generic;

namespace DatabaseManager
{
    public class Player
    {
#nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Position { get; set; }
        public int pawns { get; set; }
#nullable enable
        public int GameId { get; set; }
    }
}
