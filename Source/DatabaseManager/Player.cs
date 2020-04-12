using System.Collections.Generic;

namespace DatabaseManager
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Won { get; set; }
        public ICollection<Pawn> Pawns { get; set; }
        public List<string> MovementPattern { get; set; }
    }
}
