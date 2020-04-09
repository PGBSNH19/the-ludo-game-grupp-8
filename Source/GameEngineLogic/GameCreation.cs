using DatabaseManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace GameEngineLogic
{
    public class GameCreation
    {
        public static void GameCreate()
        {
           
            Console.WriteLine("What do you want your session to be called?");
            var GameName = Console.ReadLine();
            using var context = new LudoDbContext();
            var game = new Game()
            {
                Name = GameName,
                CreationTime = DateTime.Now,
                GameEnded = false,
                Turn = 1,
            };
            context.games.Add(game);            
            context.SaveChanges();

            Console.WriteLine("How many players are there?");
            List<string> PlayerAmountList = new List<string>();
            PlayerAmountList.AddRange(new String[] { "1", "2", "3", "4" });
            var PlayerAmount = MenuNavigator.Menu.ShowMenu(PlayerAmountList);

            List<string> CurrentColors = new List<string>();
            CurrentColors.AddRange(new string[] { "Red", "Green", "Blue", "Yellow" });          
            CurrentColors = PlayerCreation.PlayerCreate(GameName, CurrentColors,Int32.Parse(PlayerAmount));
            var Id = context.games.FirstOrDefault(p => p.Name == GameName);
            if (PlayerAmount != "4")
            {
                var BotAmount = BotDialogue(int.Parse(PlayerAmount));
                if (BotAmount > 0)
                {
                    BotCreation.CreateBot(CurrentColors, Id);
                }
            }
            //Create Pawns
            PawnCreation.CreatePawn(Id);


            //StartGame
            GameStart.StartOfGame(Id, true);
            int BotDialogue(int Choice)
            {
                var BotAmount = 0;
                
                List<string> Alternatives = new List<string>();
                Alternatives.AddRange(new string[]{"Yes", "No" });
                Console.WriteLine("Do You want bots?");
                string Answer = MenuNavigator.Menu.ShowMenu(Alternatives);        
                if (Answer == "Yes")
                {
                    var AvailableBots = 4 - Choice;
                    string[] BotArray = new string[AvailableBots];
                    Console.WriteLine("How many do you want?");
                    for (int i = 0; i < AvailableBots; i++)
                    {
                        BotArray[i] += i + 1;
                    }
                    List<string> BotList = new List<string>();
                    BotList.AddRange(BotArray);
                    BotAmount = Int32.Parse(MenuNavigator.Menu.ShowMenu(BotList));
                }
                return BotAmount;              
            }
        }
    }
}
