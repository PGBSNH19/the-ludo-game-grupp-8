using System;
using System.Collections.Generic;
using DatabaseManager;

namespace GameEngineLogic
{
    public class StartMenu
    {
        public static LudoDbContext Menu()
        {
            List<string> MenuAlternatives = new List<string>();
            MenuAlternatives.AddRange(new string[] {"New Game", "Load Game", "Scoreboard", "Exit"});
            var Choice = MenuNavigator.Menu.ShowMenu(MenuAlternatives);
            var context = new LudoDbContext();
            switch (Choice)
            {
                case "New Game":
                    //var game = GameCreation.GameCreate();
                    //return game;
                    context = GameCreation.GameCreate();
                    return context;
                case "Load Game":
                    //game = LoadGame.Loading();
                    //return game;
                    context = LoadGame.Loading();
                    return context;

                case "Scoreboard":
                    Scoreboard.CreateScoreboard();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    break;
                case "Exit":
                    System.Environment.Exit(0);
                    break;
            }

            return null;

        }
    }
}


