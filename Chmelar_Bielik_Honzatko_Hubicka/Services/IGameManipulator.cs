using Chmelar_Bielik_Honzatko_Hubicka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public interface IGameManipulator
    {
        User AddPlayer(User User);
        Game AddGame(Game Game);
        bool RemoveGame(Guid id);
        string StartGame();
        void JoinGame(string Joiner, Guid GameId);
        string GetUserId();
        List<Game> MyGamesList();
        List<Game> JoinGamesList();

    }
}
