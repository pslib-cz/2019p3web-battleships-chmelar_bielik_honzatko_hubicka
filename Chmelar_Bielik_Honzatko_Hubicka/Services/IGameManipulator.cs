using Chmelar_Bielik_Honzatko_Hubicka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    interface IGameManipulator
    {
        User AddPlayer(User User);
        Game AddGame(Game Game);
        bool RemoveGame(Game Game);
        bool ReadyPlayer(UserGame UserGame);
        bool Hit(NavyBattlePiece Piece);
        bool Turn(User User);
        bool End(User User);
        bool StartGame(UserGame UserGame);
        bool Login(User User);
        bool Register(User User);

    }
}
