using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DatabaseManager;

namespace GameEngineLogic
{
    public class ForeignKey
    {
        public static IQueryable<Player> PlayersInGame(int GameId)
        {
            var Context = new LudoDbContext();
            var Result = Context.games.First(g => g.Id == GameId).Players.AsQueryable();
            return Result;
        }

        public static IQueryable<Pawn> PawnsInPlayer(int PlayerId)
        {
            var Context = new LudoDbContext();
            var Result = Context.players.First(p => p.Id == PlayerId).Pawns.AsQueryable();
            return Result;

        }
    }
}
