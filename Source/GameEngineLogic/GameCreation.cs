using DatabaseManager;
using System;
using System.Collections.Generic;


namespace GameEngineLogic
{
    public class GameCreation
    {
        public static LudoDbContext GameCreate()
        {
            Console.WriteLine("What do you want your session to be called?");
            var GameName = Console.ReadLine();
            var context = new LudoDbContext();
            var game = new Game()
            {
                Name = GameName,
                CreationTime = DateTime.Now,
                Complete = false,
                InProgress = true,
                Turn = 1,
            };
            context.games.Add(game);
            context.SaveChanges();

            Console.WriteLine("How many players are there?");
            var PlayerAmountList = new List<string>();
            PlayerAmountList.AddRange(new String[] { "1", "2", "3", "4" });
            var PlayerAmount = MenuNavigator.Menu.ShowMenu(PlayerAmountList);

            var CurrentColors = new List<string>();
            CurrentColors.AddRange(new string[] { "Red", "Green", "Blue", "Yellow" });
            CurrentColors = PlayerCreation.PlayerCreate(context, game, CurrentColors, Int32.Parse(PlayerAmount));
            
            if (PlayerAmount != "4")
            {
                var BotAmount = BotDialogue(int.Parse(PlayerAmount));
                if (BotAmount > 0)
                {
                    BotCreation.CreateBots(context, game, CurrentColors);
                }
            }

            context.SaveChanges();
            return context;
        }

        private static int BotDialogue(int Choice)
        {
            var BotAmount = 0;
            if (Choice > 1)
            {
                List<string> Alternatives = new List<string>();
                Alternatives.AddRange(new string[] { "Yes", "No" });
                Console.WriteLine("Do You want bots?");
                string Answer = MenuNavigator.Menu.ShowMenu(Alternatives);
                if (Answer == "Yes")
                {
                    BotAmount = BotAmountChoice();
                }
            }
            else
            {
                BotAmount = BotAmountChoice();
            }
            return BotAmount;

            int BotAmountChoice()
            {
                var AvailableBots = 4 - Choice;
                string[] BotArray = new string[AvailableBots];
                Console.WriteLine("How many bots do you want?");
                for (int i = 0; i < AvailableBots; i++)
                {
                    BotArray[i] += i + 1;
                }
                List<string> BotList = new List<string>();
                BotList.AddRange(BotArray);
                BotAmount = Int32.Parse(MenuNavigator.Menu.ShowMenu(BotList));
                return BotAmount;
            }
        }
    }
}
