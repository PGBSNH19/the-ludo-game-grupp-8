using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DatabaseManager;

namespace GameEngineLogic
{
    
    public class GameEngine
    {
        public static void Game(IQueryable<Player> players)
        {
            //var RedPositions = PopulateBoardFromDb(Colors[i]);
            //var GreenPawnPositions = PopulateBoardFromDb(Colors[i]); ;
            //var BluePawnPositions = PopulateBoardFromDb(Colors[i]); ;
            //var YellowPawnPositions = PopulateBoardFromDb(Colors[i]); ;

            var context = new LudoDbContext();
            List<string> Colors = new List<string>();
            Colors.AddRange(new string[] { "Red", "Green", "Blue", "Yellow"});

            void GameLoop()
            {
                //
                for (int i = 0; i < Colors.Count; i++)
                {
                    
                }
                

            }
            void PopulateBoardFromDb(string Color)
            {
                var Player = players.FirstOrDefault(p => p.Color == Color);
                var Pawns = context.pawns.Where(p => p.Player == Player);
                //for (int i= 0; i < Pawns.Count; i++)
                {

                }
            }            
        }
    }
}
