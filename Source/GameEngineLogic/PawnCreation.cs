using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DatabaseManager;

namespace GameEngineLogic
{
    class PawnCreation
    {
        public static void CreatePawn(Game GameId)
        {
            var context = new LudoDbContext();
            var Players = context.players.Where(p => p.Id == GameId.Id);
            List<string> Colors = new List<string>();
            Colors.AddRange(new string[] { "Red", "Green", "Blue", "Yellow" });
            for (int i = 0; i < 4;i++)
            {
                var Color = Colors[i];
                var Player = Players.FirstOrDefault(p => p.Color == Color);
                Pawn pawn = new Pawn()
                {
                    Player = Player,
                    Position = 1 + 10 * i,
                    State = "Base"

                };
            }
        }
    }
}
