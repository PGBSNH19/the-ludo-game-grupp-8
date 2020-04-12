using System.Collections.Generic;
using System.Linq;
using DatabaseManager;

namespace GameEngineLogic
{
    class PawnCreation
    {
        public static void CreatePawn(string GameName)
        {
            var context = new LudoDbContext();
            var game = context.games.FirstOrDefault(p => p.Name == GameName);
            
            var Colors = new List<string>();
            Colors.AddRange(new string[] { "Red", "Green", "Blue", "Yellow" });
            for (var i = 0; i < game.Players.Count(); i++)
            {
                var Color = Colors[i];
                
                for (var j = 0; j < Colors.Count; j++)
                {
                    Pawn pawn = new Pawn()
                    {
                        Position = 1 + 10 * j,
                        PawnState = Pawn.State.Base 

                    };
                    context.pawns.Add(pawn);
                    context.SaveChanges();
                }
                 
            }
        }
    }
}
