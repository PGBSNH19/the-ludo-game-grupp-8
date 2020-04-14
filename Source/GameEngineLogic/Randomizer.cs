using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineLogic
{
    public class Randomizer
    {
        public static string ListRandomizer(List<string> List)
        {

            var RandomName = new Random();
            int r = RandomName.Next(List.Count);

            return (string)List[r];
        }
    }
}
