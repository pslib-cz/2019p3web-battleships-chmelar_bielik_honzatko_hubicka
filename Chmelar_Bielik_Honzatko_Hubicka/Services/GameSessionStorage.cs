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

        public GameSessionStorage(IHttpContextAccessor httpContext)
        {
            _session = httpContext.HttpContext.Session;
        }

        public Guid LoadGame(string key)
        {
            Guid result = _session.Get<Guid>(key);
            if (result == null)
            {
                result = default(Guid);
            }
            return result;
        }

        public void SaveGame(string key, Guid data)
        {
            _session.Set(key, data);
        }
    }
}
