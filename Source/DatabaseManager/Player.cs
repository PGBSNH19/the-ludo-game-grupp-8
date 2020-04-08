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
        public bool Won { get; set; }
        public virtual ICollection<Pawn> Pawns { get; set; }
        public virtual Game Game { get; set; }
    }
}
