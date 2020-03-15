using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Chmelar_Bielik_Honzatko_Hubicka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Services
{
    public class GameManipulator : IGameManipulator
    {
        readonly ApplicationDbContext _db;

        public GameManipulator(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddGame(Game Game)
        {
            var game = _db.Games.SingleOrDefault(g => g.GameId == Game.GameId);
            if (game is null)
            {
                return new Game() { GameId = Game.GameId, MaxUsers = Game.MaxUsers, OwnerId = Game.OwnerId, GameSize = Game.GameSize, CurrentPlayerId = Game.CurrentPlayerId };
            }

            throw new KeyNotFoundException("Game:" + Game + "already exists.");
        }

        public bool AddPlayer(User User)
        {
            var player = _db.Users.SingleOrDefault(u => u.Name == User.Name);
            if (player is null)
            {
                return new User() { Id = User.Id, Name = User.Name, Password = User.Password };
            }

            throw new KeyNotFoundException("Player:" + User + "already exists.");
        }

        public bool CreateShip(Ship Ship)
        {
            var ship = _db.Ships.SingleOrDefault(s => s.Name == Ship.Name);
            if (ship is null)
            {
                return new Ship() { Id = Ship.Id, Name = Ship.Name };
            }

            throw new KeyNotFoundException("Ship:" + Ship + "already exists.");
        }

        public bool End(User User)
        {
            throw new NotImplementedException();
        }

        public bool Hit(NavyBattlePiece Piece)
        {
            throw new NotImplementedException();
        }

        public bool Login(User User)
        {
            throw new NotImplementedException();
        }

        public bool ReadyPlayer(UserGame UserGame)
        {
            throw new NotImplementedException();
        }

        public bool Register(User User)
        {
            throw new NotImplementedException();
        }

        public bool RemoveGame(Game Game)
        {
            throw new NotImplementedException();
        }

        public bool StartGame(UserGame UserGame)
        {
            throw new NotImplementedException();
        }

        public bool Turn(User User)
        {
            throw new NotImplementedException();
        }
    }
}
