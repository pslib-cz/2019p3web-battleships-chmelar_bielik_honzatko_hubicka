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
    public class GameManipulator : IGameManipulator, IGameLogic
    {
        readonly ApplicationDbContext _db;
        readonly GameSessionStorage<Guid> _gss;
        readonly Random _rnd;

        public GameManipulator(ApplicationDbContext db, GameSessionStorage<Guid> gss, Random rnd)
        {
            _db = db;
            _gss = gss;
            activeGameId = _gss.LoadGame("GameKey");
            activeUserId = _gss.GetUserId();
            _rnd = rnd;
        }

        public Guid activeGameId { get; private set; }
        public string activeUserId { get; private set; }
        private BattlePieceState state { get; set; }

        public bool InGame()
        {
            return activeGameId != default;
        }

        public void GeneratorPieces()
        {
            Game game = GetGame(activeGameId);
            NavyBattlePiece piece;
            for (int i = 0; i < 100; i++)
            {
                piece = new NavyBattlePiece(); // není to stále jen ten jeden dílek - je jich 100
                piece.State = BattlePieceState.Water;
                piece.GameId = activeGameId;
                piece.UserId = activeUserId;
                piece.PosX = i % 10;
                piece.PosY = i / 10;
                _db.NavyBattlePieces.Add(piece);
            }
            _db.SaveChanges();

            //tohle nemůže fungovat - musí se na to jinak...
            var shipPieces = _db.NavyBattlePieces.AsEnumerable().Where(sP => sP.PosX == _rnd.Next(1, 10) && sP.PosY == _rnd.Next(1, 10) && sP.GameId == activeGameId).ToList();
            foreach (var shipPiece in shipPieces)
            {
                shipPiece.State = BattlePieceState.Ship;
            }
            _db.SaveChanges();
        }

        public void JoinGame(Guid GameId)
        {
            if (InGame())
            {
                Game game = GetGame(GameId);
                game.PlayerId = activeUserId;
                game.PlayerState = PlayerState.PreperingForGame;
                activeGameId = game.GameId;
                _gss.SaveGame("GameKey", activeGameId);
                _db.Update(game);
                _db.SaveChanges();
                GeneratorPieces();
            }
            else
            {
                return;
            }
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
            game.Gamestate = GameState.Preparing;
            game.OwnerId = activeUserId;
            game.CurrentPlayerId = activeUserId;
            game.OwnerState = PlayerState.PreperingForGame;
            _gss.SaveGame("GameKey", activeGameId);
            _db.Games.Add(game);
            _db.SaveChanges();
            GeneratorPieces();
        }

        public List<NavyBattlePiece> GetBattlePieces(Guid GameId)
        {
            var game = GetGame(GameId);
            var UserId = activeUserId;

            return _db.NavyBattlePieces.Where(p => p.GameId == game.GameId && p.UserId != UserId).OrderBy(p => p.PosY).ThenBy(p => p.PosX).ToList();
        }

        public Game GetGame(Guid gameId)
        {
            return _db.Games.Where(g => g.GameId == gameId).Include(g => g.GamePieces).SingleOrDefault();
        }

        public List<NavyBattlePiece> GetBattlefield()
        {
            var battlefield = GetBattlePieces(activeGameId);

            return battlefield;
        }

        public void Hit(int pieceId)
        {
            var piece = _db.NavyBattlePieces.SingleOrDefault(p => p.Id == pieceId);


            Game activeGame = GetGame(activeGameId);

            Game hitUser = _db.Games.SingleOrDefault(u => u.CurrentPlayerId == activeUserId);
            User hittedUser = _db.Users.Where(u => u.Id == piece.UserId).FirstOrDefault();

            List<NavyBattlePiece> UnhittedPieces = _db.NavyBattlePieces.Where(p => p.UserId == piece.UserId && p.State == BattlePieceState.Ship).ToList();

            if (activeGame.Gamestate == GameState.End || (activeGame.OwnerState != PlayerState.PreperingForGame || activeGame.PlayerState != PlayerState.PreperingForGame))
            {
                return;
            }

            else if (InGame())
            {
                if (hitUser.Gamestate != GameState.Fighting)
                {
                    hitUser.Gamestate = GameState.Fighting;
                }

                else if (hitUser.CurrentPlayerId == hittedUser.Id)
                {
                    return;
                }

                else if (piece.State == BattlePieceState.Hitted_Ship || piece.State == BattlePieceState.Hitted_Water)
                {
                    return;
                }

                else if (piece.State == BattlePieceState.Ship)
                {
                    state = BattlePieceState.Hitted_Ship;
                    if (UnhittedPieces.Count() < 2)
                    {
                        hittedUser.PlayerState = PlayerState.Lose;
                        _db.Users.Update(hittedUser);
                        _db.SaveChanges();
                    }
                }

                else if (GameEnd(hitUser))
                {
                    return;
                }

                else if (piece.State == BattlePieceState.Water)
                {
                    state = BattlePieceState.Hitted_Water;
                }

                else if (piece.State != BattlePieceState.Water && piece.State != BattlePieceState.Ship)
                {
                    state = BattlePieceState.Unknown;
                }

                else
                {
                    ContinueInGame(hitUser);
                }
            }

            else
            {
                return;
            }
            piece.State = state;
            _db.NavyBattlePieces.Update(piece);
            _db.SaveChanges();
            _gss.SaveGame("GameKey", activeGameId);
        }

        private void ContinueInGame(Game hitUser)
        {
            int userRound = 0;

            userRound++;
            List<Game> listUsers = _db.Games.Where(u => u.GameId == hitUser.GameId).OrderBy(u => u.CurrentPlayerId).ToList();
            Game nextPlayer = new Game();
            int index = listUsers.FindIndex(u => u.CurrentPlayerId == hitUser.CurrentPlayerId);

            if (userRound == 1)
            {
                nextPlayer.CurrentPlayer = listUsers[index++].CurrentPlayer;
                userRound = 0;
            }

            else
            {
                nextPlayer.CurrentPlayer = listUsers[0].CurrentPlayer;
            }

            hitUser.CurrentPlayerId = nextPlayer.CurrentPlayerId;
            userRound = 0;
            _db.Games.Update(hitUser);
        }

        private bool GameEnd(Game winner)
        {
            if (winner.PlayerState != PlayerState.Lose)
            {
                winner.PlayerState = PlayerState.Win;
                winner.Gamestate = GameState.End;
                _db.Games.Update(winner);
                _db.Users.Update(winner.CurrentPlayer);
                return true;
            }
            return false;
        }
    }
}
