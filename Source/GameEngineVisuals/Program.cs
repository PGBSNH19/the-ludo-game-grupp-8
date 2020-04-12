using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseManager;
using GameEngineLogic;

namespace GameEngineVisuals
{
    public class Program
    {

        static void Main(string[] args)
        {
            var game = InitializeGame();
            
            var gui = new GameGui();
            var allPlaying = true;
            var index = 0;
            string[] Colors = new string[] { "Red", "Green", "Blue", "Yellow" };
            

            while (allPlaying)
            {
                var Color = Colors[index];
                gui.Clear();
                gui.ShowStats(game,Color);
                gui.ShowBoard(game);
                if (gui.CheckFinishedStatus(game))
                {
                    allPlaying = false;
                }
                else
                {
                    var col = "Blue";
                    gui.RollDiceNextPlayer(game, col);
                    index++;
                    if (index > 3)
                    {
                        index = 0;
                    }
                }
            }
        }

        private static Game InitializeGame()
        {
            return StartMenu.Menu();
        }
    }
}