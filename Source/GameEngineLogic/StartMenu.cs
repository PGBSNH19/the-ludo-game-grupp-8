using DatabaseManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineLogic
{
    public class StartMenu
    {
        public static void Menu()
        {
            List<string> MenuAlternatives = new List<string>();
            MenuAlternatives.AddRange(new string[] { "New Game", "Load Game", "Scoreboard", "Exit" });
            var Choice = MenuNavigator.Menu.ShowMenu(MenuAlternatives);
            switch (Choice)
            {
                case "New Game":
                    GameCreation.GameCreate();
                    break;
                case "Load Game":
                 LoadGame.Loading();
                    break;
                case "Scoreboard":
                    Scoreboard.CreateScoreboard();
                    break;
                case "Exit":
                    System.Environment.Exit(0);
                    break;
            }
        }
    }
}


