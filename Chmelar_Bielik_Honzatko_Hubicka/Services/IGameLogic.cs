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
        List<NavyBattlePiece> GetBattlePieces(Guid GameId);
    }
}
