﻿using Chmelar_Bielik_Honzatko_Hubicka.Models;
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

        public Game AddGame(Game Game)
        {
            var game = _db.Games.SingleOrDefault(g => g.GameId == Game.GameId);
            if (game is null)
            {
                return new Game() { GameId = Game.GameId, Player1Id = Game.Player1Id, Player2Id = Game.Player2Id, CurrentPlayerId = Game.CurrentPlayerId };
            }

            throw new KeyNotFoundException("Game:" + Game + "already exists.");
        }

        public User AddPlayer(User User)
        {
            var player = _db.Users.SingleOrDefault(u => u.UserName == User.UserName);
            if (player is null)
            {
                return new User() { Id = User.Id, UserName = User.UserName, Password = User.Password };
            }

            throw new KeyNotFoundException("Player:" + User + "already exists.");
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
