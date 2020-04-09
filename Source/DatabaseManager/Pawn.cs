using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    public class Pawn
    {
        public int Id { get; set; }
        public virtual Player Player { get; set; }
        public int Position { get; set; }
        public string State { get; set; }

    }
}
