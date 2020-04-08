using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineLogic
{
    public class BotCreation
    {
        public static void CreateBot(List<string> CurrentColors)
        {
            List<string> BotNames = new List<string>();
            BotNames.AddRange(new string[] { "Lion", "Panda", "Tiger" });
            var BotAmount = CurrentColors.Count;
            for(int i = 0; i < BotAmount;i++)
            {
                var Name = BotRandomizer(BotNames);
                var Color = BotRandomizer(CurrentColors);               
            }
           
            
            //Add bots to Db
            Console.WriteLine("Create " + CurrentColors.Count + " Bots!");
        }
       static string BotRandomizer(List<string> List)
        {

            var RandomName = new Random();
            int r = RandomName.Next(List.Count);

            return (string)List[r];
        }
    }
}
