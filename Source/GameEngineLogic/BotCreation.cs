using DatabaseManager;
using System;
using System.Collections.Generic;

namespace GameEngineLogic
{
    public class BotCreation
    {
        public static void CreateBots(List<string> currentColors, Game game)
        {
            using var context = new LudoDbContext();
            List<string> BotNames = new List<string>();
            BotNames.AddRange(new string[] { "Lion", "Panda", "Tiger" });
            var BotAmount = currentColors.Count;
            for(int i = 0; i < BotAmount;i++)
            {
                var Name = BotRandomizer(BotNames);
                BotNames = Banner.ListBan(Name, BotNames);
                var Color = BotRandomizer(currentColors);
                currentColors = Banner.ListBan(Color, currentColors);
                var bot = PlayerFactory.Create(Name, Color);

                game.Players.Add(bot);
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
