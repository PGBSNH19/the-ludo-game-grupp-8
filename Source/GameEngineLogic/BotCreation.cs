using DatabaseManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameEngineLogic
{
    public class BotCreation
    {
        public static void CreateBot(List<string> CurrentColors, Game GameId)
        {
            using var context = new LudoDbContext();
            List<string> BotNames = new List<string>();
            BotNames.AddRange(new string[] { "Lion", "Panda", "Tiger" });
            var BotAmount = CurrentColors.Count;
            for(int i = 0; i < BotAmount;i++)
            {
                var Name = BotRandomizer(BotNames);
                BotNames = Banner.ListBan(Name, BotNames);
                var Color = BotRandomizer(CurrentColors);
                CurrentColors = Banner.ListBan(Color, CurrentColors);
                var bot = new Player
                {
                    Name = Name,
                    Color = Color,
                    Game = GameId,
                    Won = false
                };

                context.Add(bot);
                context.SaveChanges();

            }
   
        }
       static string BotRandomizer(List<string> List)
        {

            var RandomName = new Random();
            int r = RandomName.Next(List.Count);

            return (string)List[r];
        }
    }
}
