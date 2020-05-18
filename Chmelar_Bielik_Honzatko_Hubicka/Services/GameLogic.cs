using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public class GameLogic : IGameLogic //Almost complete Game logic => just small changes needed.
    {
        readonly ApplicationDbContext _db;
        readonly GameSessionStorage<Guid> _gss;
        public Guid activeGameId { get; private set; }
        public string activeUserId { get; set; }
        public GameLogic(ApplicationDbContext db, GameSessionStorage<Guid> gss)
        {
            _db = db;
            _gss = gss;
            activeGameId = _gss.LoadGame("GameKey");
            activeUserId = _gss.GetUserId();
        }

        public bool InGame() 
        {
            return activeGameId != default;
        }

        public List<NavyBattlePiece> GetBattlePieces(Guid GameId)
        {
            var game = _db.Games.SingleOrDefault(g => g.GameId == GameId);

            return _db.NavyBattlePieces.Where(p => p.GameId == game.GameId).OrderBy(p => p.PosX).OrderBy(p => p.PosY).ToList();
        }

        public Game GetGame(Guid GameId)
        {
            return _db.Games.SingleOrDefault(g => g.GameId == GameId);
        }

        //public Game GetGame(string gameKey)
        //{
        //    Guid gameId = _gss.LoadGame(gameKey);
        //    return _db.Games.SingleOrDefault(g => g.GameId == gameId);
        //}

        public List<NavyBattlePiece> GetBattlefield(string gameKey) 
        {
            Guid gameId = _gss.LoadGame(gameKey);
            var battlefield = GetBattlePieces(gameId);

            return battlefield;
        }

        public string Hit(int pieceId)
        {
            string result = "Something went wrong. Try it again.";
            var piece = _db.NavyBattlePieces.SingleOrDefault(p => p.Id == pieceId);

            Game activeGame = GetGame(activeGameId);

            Game activeUser = _db.Games.SingleOrDefault(u => u.CurrentPlayer.Id == activeGame.CurrentPlayer.Id);
            Game hitUser = _db.Games.SingleOrDefault(u => u.CurrentPlayer.Id == activeUserId);

            if (activeGame.Gamestate == GameState.End)
            {
                return "This game already ended, so you can't play.";
            }

            if (hitUser.CurrentPlayerId != activeUser.CurrentPlayer.Id)
            {
                return "It is not your turn.";
            }

            if (piece.UserId == hitUser.CurrentPlayer.Id)
            {
                return "You can't hit your own piece!";
            }

            if (piece.State == BattlePieceState.Hitted_Ship || piece.State == BattlePieceState.Hitted_Water)
            {
                return "You have already hitted that piece.";
            }

            BattlePieceState state;

            if (piece.State == BattlePieceState.Ship)
            {
                state = BattlePieceState.Hitted_Ship;
                result = "You have distroyed a ship of your enemy.";
            }

            else if (piece.State == BattlePieceState.Water)
            {
                state = BattlePieceState.Hitted_Water;
                result = "You hitted a water.";
            }

            else
            {
                state = BattlePieceState.Unknown;
            }
            piece.State = state;
            _db.NavyBattlePieces.Update(piece);

            if (state == BattlePieceState.Hitted_Ship)
            {
                User hittedUser = _db.Users.Where(u => u.Id == activeUserId).Where(u => u.Id == piece.UserId).FirstOrDefault();

                List<NavyBattlePiece> UnhittedPieces = _db.NavyBattlePieces.Where(p => p.UserId == piece.UserId && p.State == BattlePieceState.Ship).Take(2).AsNoTracking().ToList();

                if (UnhittedPieces.Count() < 2)
                {
                    result = $"You have destroyed the last piece in {hittedUser.UserName}s fleet.";

                    hittedUser.PlayerState = PlayerState.Lose;
                    _db.Users.Update(hittedUser);
                    _db.SaveChanges();

                    if (GameEnd(hitUser))
                    {
                        result = "Congrats, you have won!";
                    }

                    else
                    {
                        ContinueInGame(hitUser);
                    }
                }

                else
                {
                    ContinueInGame(hitUser);
                }
            }

            else
            {
                ContinueInGame(hitUser);
            }

            _db.SaveChanges();
            return result;
        }

        private void ContinueInGame(Game hitUser)
        {
            int userRound = 0;

            userRound++;
            List<Game> listUsers = _db.Games.Where(u => u.GameId == hitUser.GameId).OrderBy(u => u.CurrentPlayer.Id).ToList();
            Game nextPlayer = new Game();
            int index = listUsers.FindIndex(u => u.CurrentPlayer.Id == hitUser.CurrentPlayer.Id);

            if (userRound == 1)
            {
                nextPlayer.CurrentPlayer = listUsers[index++].CurrentPlayer;
                userRound = 0;
            }

            else
            {
                nextPlayer.CurrentPlayer = listUsers[0].CurrentPlayer;
            }

            hitUser.CurrentPlayer.Id = nextPlayer.CurrentPlayer.Id;
            userRound = 0;
            _db.Games.Update(hitUser);
        }

        private bool GameEnd(Game winner)
        {
            if (winner.PlayerState != PlayerState.Lose)
            {
                winner.PlayerState = PlayerState.Win;
                //winner.GameState = GameState.End;
                _db.Games.Update(winner);
                _db.Users.Update(winner.CurrentPlayer);
                return true;
            }
            return false;
        }
    }
}
