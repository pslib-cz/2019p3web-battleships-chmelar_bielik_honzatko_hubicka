using Chmelar_Bielik_Honzatko_Hubicka.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public class GameSessionStorage<Guid>
    {
        readonly ISession _session;
        readonly IHttpContextAccessor _httpContext;

        public GameSessionStorage()
        {
            _session = _httpContext.HttpContext.Session;
        }

        public Guid LoadGame()
        {
            return _httpContext.HttpContext.Session.Get<Guid>("GameKey");
        }

        public void SetGameId(Guid data)
        {
            Guid GameId = LoadGame();
            GameId = data;
            _httpContext.HttpContext.Session.Set("GameKey", data);
        }
    }
}
