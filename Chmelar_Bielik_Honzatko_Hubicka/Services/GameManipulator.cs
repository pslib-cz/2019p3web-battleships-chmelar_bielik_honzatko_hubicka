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
        readonly GameLogic _gl;
        readonly GameSessionStorage<Guid> _gss;
        readonly Random _rnd;

        public GameManipulator(ApplicationDbContext db, GameLogic gl, GameSessionStorage<Guid> gss, Random rnd)
        {
            _db = db;
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
                piece.State = BattlePieceState.Water;
                piece.GameId = activeGameId;
                piece.UserId = activeUserId;
                piece.PosX = i % 10;
                piece.PosY = i / 10;
                game.GamePieces.Add(piece);
                _db.NavyBattlePieces.Add(piece);
            }
            _db.SaveChanges();

            var shipPieces = _db.NavyBattlePieces.Where(sP => sP.PosX == _rnd.Next(1, 10) && sP.PosY == _rnd.Next(1, 10) && sP.GameId == activeGameId);
            foreach (var shipPiece in shipPieces)
            {
                shipPiece.State = BattlePieceState.Ship;
            }
            _db.SaveChanges();
        }

        public void JoinGame(Guid GameId)
        {
            Game game = _gl.GetGame(GameId);
            game.PlayerId = activeUserId;
            game.PlayerState = PlayerState.PreperingForGame;
            GeneratorPieces();
            activeGameId = game.GameId;
            _gss.SaveGame("GameKey", activeGameId);
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
            Game game = new Game();
            game.GameId = Guid.NewGuid();
            activeGameId = game.GameId;
            _gss.SaveGame("GameKey", activeGameId);
            game.Gamestate = GameState.Preparing;
            game.OwnerId = activeUserId;
            game.OwnerState = PlayerState.PreperingForGame;
            GeneratorPieces();
            _db.Games.Add(game);
            _db.SaveChanges();
        }
    }
}
