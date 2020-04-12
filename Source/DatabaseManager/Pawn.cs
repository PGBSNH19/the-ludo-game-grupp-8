using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    public class Pawn
    {
        public enum State { Base, Playing, Finished }
        public int Id { get; set; }
        public int Position { get; set; }
        public State PawnState { get; set; }
    }
}
