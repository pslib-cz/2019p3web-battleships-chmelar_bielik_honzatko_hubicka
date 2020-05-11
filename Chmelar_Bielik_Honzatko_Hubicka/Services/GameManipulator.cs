using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Chmelar_Bielik_Honzatko_Hubicka.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public class GameManipulator : IGameManipulator
    {
        readonly ApplicationDbContext _db;
        readonly IHttpContextAccessor _httpContext;

        public GameManipulator(ApplicationDbContext db)
        {
            _db = db;
        }

        public Game AddGame(Game Game)
        {
            var game = _db.Games.SingleOrDefault(g => g.GameId == Game.GameId);
            if (game is null)
            {
                return new Game() { GameId = Game.GameId, OwnerId = Game.OwnerId, PlayerId = Game.PlayerId, CurrentPlayerId = Game.CurrentPlayerId };
            }

            throw new KeyNotFoundException("Game:" + Game + "already exists.");
        }

        public User AddPlayer(User User)
        {
            var player = _db.Users.SingleOrDefault(u => u.UserName == User.UserName);
            if (player is null)
            {
                return new User(); //{ Id = User.Id, UserName = User.UserName, Password = User.Password };
            }

            throw new KeyNotFoundException("Player:" + User + "already exists.");
        }
        public bool End(User User)
        {
            throw new NotImplementedException();
        }

        public List<Game> GamesList()
        {
            throw new NotImplementedException();
        }

        public string GetUserId()
        {
            var result = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return result;
        }

        public void JoinGame(string Joiner, Guid GameId)
        {
            throw new NotImplementedException();
        }

        public List<Game> JoinGamesList()
        {
            return _db.Games.Where(o => o.Gamestate == GameState.Preparing)
            .Include(o => o.Owner)
            .Include(o => o.Player)
            .Include(o => o.CurrentPlayer)
            .AsNoTracking().ToList();
        }

        public bool Login(User User)
        {
            throw new NotImplementedException();
        }

        public List<Game> MyGamesList()
        {
            string userId = GetUserId();
            return _db.Games.Where(o => o.OwnerId == userId)
            .Include(o => o.Owner)
            .Include(o => o.Player)
            .Include(o => o.CurrentPlayer)
            .AsNoTracking().ToList();
        }

        public bool ReadyPlayer(Game Game)
        {
            throw new NotImplementedException();
        }

        public bool Register(User User)
        {
            throw new NotImplementedException();
        }

        public bool RemoveGame(Guid Id)
        {
            try
            {
                var game = _db.Games.SingleOrDefault(g => g.GameId == Id);
                _db.Games.Remove(game);
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool StartGame(Game Game)
        {
            throw new NotImplementedException();
        }

        public bool Turn(User User)
        {
            throw new NotImplementedException();
        }

        public List<User> UsersList()
        {
            throw new NotImplementedException();
        }
    }
}
