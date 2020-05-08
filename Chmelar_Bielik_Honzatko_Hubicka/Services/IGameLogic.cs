using Chmelar_Bielik_Honzatko_Hubicka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    interface IGameLogic
    {
        Game GetGame(Guid GameId);
        Game GetGame(string gameKey);
        List<NavyBattlePiece> GetBattlePieces(Guid GameId);
        List<NavyBattlePiece> GetBattlefield(string gameKey);
        public string Hit(string gameKey, int pieceId);
    }
}
