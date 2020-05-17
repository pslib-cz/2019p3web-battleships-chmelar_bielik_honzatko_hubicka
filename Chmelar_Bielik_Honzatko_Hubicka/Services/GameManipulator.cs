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
        readonly GameLogic _gl;
        readonly GameSessionStorage<Guid> _gss;

        public GameManipulator(ApplicationDbContext db, IHttpContextAccessor httpContext, GameLogic gl, GameSessionStorage<Guid> gss)
        {
            _db = db;
            _httpContext = httpContext;
            _gl = gl;
            _gss = gss;
        }

        public void GeneratorPieces(string gameKey)
        {
            Game game = _gl.GetGame(gameKey);
            string userId = _gss.GetUserId();

            Game activeUser = _db.Games.SingleOrDefault(u => u.CurrentPlayer.Id == userId && u.CurrentPlayer.Id == game.CurrentPlayer.Id);
            Random r = new Random();
            NavyBattlePiece piece = new NavyBattlePiece();
            for (int i = 0; i < 20; i++)
            {
                piece.Id = i;
                piece.GameId = game.GameId;
                piece.UserId = activeUser.CurrentPlayer.Id;
                piece.PosX = r.Next(0, 10);
                piece.PosY = r.Next(0, 10);
                _db.NavyBattlePieces.Add(piece);
                _db.SaveChanges();
            }

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

        public string GetUserId()
        {
            var result = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return result;
        }

        public void JoinGame(string Joiner, Guid GameId)
        {
            Game game = _gl.GetGame(GameId);
            game.Player.Id = Joiner;
            _db.Update(game);
        }

        public List<Game> JoinGamesList()
        {
            return _db.Games.Where(o => o.Gamestate == GameState.Preparing)
            .Include(o => o.Owner)
            .Include(o => o.Player)
            .Include(o => o.CurrentPlayer)
            .AsNoTracking().ToList();
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

        public string StartGame()
        {
            string userId = _gss.GetUserId();

            Game activeUser = _db.Games.SingleOrDefault(u => u.CurrentPlayer.Id == userId);
            Game game = new Game();
            game.Owner = activeUser.CurrentPlayer;
            _db.Games.Add(game);
            _db.SaveChanges();

            return game.GameId.ToString();
        }
    }
}
