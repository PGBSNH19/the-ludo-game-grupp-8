using DatabaseManager;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GameEngineLogic
{
    public class LoadGame
    {
        /*
        [Name]
        Gamename
        (Turn)
        12
        (Players)
        player1: 3/4 
        player2: 2/4
        player3: 2/4
        player4: 0/4
        (LastCheckpoint)
        2020-03-20 13:55
        */
        public static Game Loading()
        {
            var context = new LudoDbContext();
            var gamesInProgress = context.games.Where(g => g.GameEnded == false).Include(g => g.Players).ThenInclude(p => p.Pawns).ToList();
            var gamenames = new List<string>();

            foreach (var game in gamesInProgress)
            {
                gamenames.Add(game.Name);
                Console.WriteLine($"[Name]\n {game.Name}\n (Turn)\n{game.Turn}\n (Players)");

                foreach (var player in game.Players)
                {
                    Console.WriteLine(player.Name);
                }

                Console.WriteLine($"(LastCheckpoint)\n{game.LastCheckpointTime}\n");
            }

            var chosenName = MenuNavigator.Menu.ShowMenu(gamenames);

            var chosenGame = gamesInProgress.FirstOrDefault(p => p.Name == chosenName);

            //Resume Game

            return chosenGame;
        }

    }
}


