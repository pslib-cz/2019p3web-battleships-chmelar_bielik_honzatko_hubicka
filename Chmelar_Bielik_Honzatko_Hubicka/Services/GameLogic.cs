using Chmelar_Bielik_Honzatko_Hubicka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public class GameLogic : IGameLogic
    {
        readonly ApplicationDbContext _db;

        public GameLogic(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<NavyBattlePiece> GetBattlePieces(Guid GameId)
        {
            var game = _db.Games.SingleOrDefault(g => g.GameId == GameId);

            return _db.NavyBattlePieces.Where(p => p.GameId == game.GameId).OrderBy(p => p.PosX).OrderBy(p => p.PosY).ToList();
        }

        public Game GetGame(Guid GameId)
        {
            return _db.Games.SingleOrDefault(g => g.GameId == GameId);
        }
    }
}
