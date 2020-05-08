using Chmelar_Bielik_Honzatko_Hubicka.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public class GameSessionStorage<Guid>
    {
        readonly ISession _session;
        readonly IHttpContextAccessor _httpContext;

        public GameSessionStorage(IHttpContextAccessor httpContext)
        {
            _session = httpContext.HttpContext.Session;
            _httpContext = httpContext;
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

        public string GetUserId() 
        {
            var result = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return result;
        }
    }
}
