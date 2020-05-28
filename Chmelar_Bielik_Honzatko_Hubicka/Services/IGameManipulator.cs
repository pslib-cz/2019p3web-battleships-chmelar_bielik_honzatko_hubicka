using Chmelar_Bielik_Honzatko_Hubicka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public interface IGameManipulator
    {
        bool RemoveGame(Guid id);
        void JoinGame(Guid GameId);
        List<Game> MyGamesList();
        List<Game> JoinGamesList();
    }
}
