using Chmelar_Bielik_Honzatko_Hubicka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Service
{
    interface IGame
    {
        bool AddPlayer(User User);
        bool AddGame(Game Game);
        bool RemoveGame(Game Game);
        bool PieceNow(NavyBattlePiece Piece);
        
    }
}
