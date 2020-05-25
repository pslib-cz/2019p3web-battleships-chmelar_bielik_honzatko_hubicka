using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public class GameLogic : IGameLogic
    {
        readonly ApplicationDbContext _db;
        readonly GameSessionStorage<Guid> _gss;
        public Guid activeGameId { get; private set; }
        public string activeUserId { get; set; }
        private BattlePieceState state { get; set; }
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

        public List<NavyBattlePiece> GetBattlefield() 
        {
            var battlefield = GetBattlePieces(activeGameId);

            return battlefield;
        }

        public void Hit(int pieceId)
        {
            var piece = _db.NavyBattlePieces.SingleOrDefault(p => p.Id == pieceId);
            

            Game activeGame = GetGame(activeGameId);

            Game hitUser = _db.Games.SingleOrDefault(u => u.CurrentPlayer.Id == activeUserId);
            User hittedUser = _db.Users.Where(u => u.Id == activeUserId).Where(u => u.Id == piece.UserId).FirstOrDefault();

            List<NavyBattlePiece> UnhittedPieces = _db.NavyBattlePieces.Where(p => p.UserId == piece.UserId && p.State == BattlePieceState.Ship).Take(2).AsNoTracking().ToList();

            if (activeGame.Gamestate == GameState.End)
            {
                return;   
            }

            else if (InGame())
            {
                hitUser.Gamestate = GameState.Fighting;

                if (hitUser.CurrentPlayerId == hittedUser.Id)
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
