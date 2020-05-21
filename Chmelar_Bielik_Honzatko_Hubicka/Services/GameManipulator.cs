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
        readonly Random _rnd;

        public GameManipulator(ApplicationDbContext db, IHttpContextAccessor httpContext, GameLogic gl, GameSessionStorage<Guid> gss, Random rnd)
        {
            _db = db;
            _httpContext = httpContext;
            _gl = gl;
            _gss = gss;
            activeGameId = _gss.LoadGame("GameKey");
            activeUserId = _gss.GetUserId();
            _rnd = rnd;
        }

        public Guid activeGameId { get; private set; }
        public string activeUserId { get; private set; }

        public bool InGame()
        {
            return activeGameId != default;
        }

        public void GeneratorPieces()
        {
            Game game = _gl.GetGame(activeGameId);
            NavyBattlePiece piece = new NavyBattlePiece();
            for (int i = 0; i < 100; i++)
            {
                piece.Id = piece.Id + i;
                for (int q = 0; q < 20; q++)
                {
                    piece.Id = piece.Id - q;
                    piece.State = BattlePieceState.Ship;
                    piece.PosX = _rnd.Next(0, 10);
                    piece.PosY = _rnd.Next(0, 10);
                }
                piece.State = BattlePieceState.Water;
                piece.GameId = game.GameId;
                piece.UserId = game.CurrentPlayerId;
                piece.PosX = _rnd.Next(0, 10);
                piece.PosY = _rnd.Next(0, 10);
                game.GamePieces.Add(piece);
                _db.NavyBattlePieces.Add(piece);
            }
            _db.SaveChanges();

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

        public void JoinGame(string Joiner, Guid GameId)
        {
            Game game = _gl.GetGame(GameId);
            game.PlayerId = Joiner;
            GeneratorPieces();
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
            return _db.Games.Where(o => o.OwnerId == activeUserId)
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

        public void StartGame()
        {

            Game activeUser = _db.Games.SingleOrDefault(u => u.CurrentPlayerId == activeUserId);
            Game game = new Game();
            game.GameId = Guid.NewGuid();
            activeGameId = game.GameId;
            _gss.SaveGame("GameKey", activeGameId);
            game.Owner = activeUser.CurrentPlayer;
            _db.Games.Add(game);
            _db.SaveChanges();
        }
    }
}
