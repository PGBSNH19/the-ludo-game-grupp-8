using DatabaseManager;
using System;
using System.Collections.Generic;

namespace GameEngineLogic
{
    public class BotCreation
    {
        public static void CreateBots(LudoDbContext context, Game game, List<string> currentColors)
        {
            List<string> BotNames = new List<string>();
            BotNames.AddRange(new string[] { "Lion", "Panda", "Tiger" });
            var BotAmount = currentColors.Count;
            for(int i = 0; i < BotAmount;i++)
            {
                var Name = Randomizer.ListRandomizer(BotNames);
                BotNames = Banner.ListBan(Name, BotNames);
                var Color = Randomizer.ListRandomizer(currentColors);
                currentColors = Banner.ListBan(Color, currentColors);
                var bot = PlayerFactory.Create(Name, Color ,true);

                game.Players.Add(bot);
            }
   
        }
       
    }
}
