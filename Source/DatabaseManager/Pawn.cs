using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    public class Pawn
    {
        public int id { get; set; }
        public virtual Player Player { get; set; }
        public int position { get; set; }

    }
}
