using DatabaseManager;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GameEngineLogic
{
    public class LoadGame
    {
        public static void Loading()
        {
            var context = new LudoDbContext();            
            List <Game> GamesInProgress = context.games.Where(g => g.GameEnded == false).ToList();
            String[] GameName = new String[GamesInProgress.Count];

            for (int k = 0; k < GamesInProgress.Count(); k++)
            {
                Console.WriteLine("[Name]\n" + GamesInProgress[k].Name + "\n" +
                "(Turn)\n" + GamesInProgress[k].Turn + "\n" +
                "(Players)");
                GameName[k] = GamesInProgress[k].Name;
                var Players = context.players.Where(p => p.Game == GamesInProgress[k]).ToList();
                for(int i = 0; i < Players.Count; i++)
                {
                    Console.WriteLine(Players[i].Name);
                }
                Console.WriteLine("(LastCheckpoint)\n" + GamesInProgress[k].LastCheckpointTime + "\n");
            }
            List<string> GameNames = new List<string>();
            GameNames.AddRange(GameName);
            var hello = MenuNavigator.Menu.ShowMenu(GameNames);
            GamesInProgress.Where(p => p.Name == hello);
            //Resume Game

        }



    }
}

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
