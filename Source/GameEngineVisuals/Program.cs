using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DatabaseManager;
using GameEngineLogic;

namespace GameEngineVisuals
{
    public class Program
    {

        static void Main(string[] args)
        {           
            var context = InitializeGame();
            var game = context.games.First(g => g.InProgress == true);
            game.InProgress = false;
            context.SaveChanges();
            var gui = new GameGui();
            var allPlaying = true; 
            var index = 0;
            for (int i = 1; i < game.Turn; i++)
            {
                index++;
                if (index > 4)
                {
                    index = 0;
                }
            }
            string[] colors = new string[] { "Red", "Green", "Blue", "Yellow" };
            var lastPawnPlayed = new Pawn();

            while (allPlaying)
            {
                var color = colors[index];
                gui.Clear();
                gui.ShowBoard(game);
                gui.ShowStats(game,color);      
                if (gui.CheckFinishedStatus(game))
                {
                    allPlaying = false;
                    Console.Clear();
                    var winner = game.Players.First(p => p.Color == color);
                    Console.WriteLine(winner.Name);
                    Console.ReadLine();
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadKey();
                    lastPawnPlayed = gui.RollDiceNextPlayer(context, game, color);                 
                    index++;
                    if (index > 3)
                    {
                        index = 0;
                    }
                }
                game.Turn += 1;
                var plater = new Player()
                {
                    Name = "But Why"
                };               
            }
        }

        private static LudoDbContext InitializeGame()
        {
            return StartMenu.Menu();
        }
    }
}