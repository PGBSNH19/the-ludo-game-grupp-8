using System;
using System.Collections.Generic;
using DatabaseManager;

namespace GameEngineLogic
{
    public class StartMenu
    {
        public static Game Menu()
        {
            List<string> MenuAlternatives = new List<string>();
            MenuAlternatives.AddRange(new string[] {"New Game", "Load Game", "Scoreboard", "Exit"});
            var Choice = MenuNavigator.Menu.ShowMenu(MenuAlternatives);
            switch (Choice)
            {
                case "New Game":
                    var game = GameCreation.GameCreate();
                    return game;
                case "Load Game":
                    game = LoadGame.Loading();
                    return game;

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


