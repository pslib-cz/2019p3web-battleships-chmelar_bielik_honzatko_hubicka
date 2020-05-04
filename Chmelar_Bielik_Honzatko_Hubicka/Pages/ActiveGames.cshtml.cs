using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Chmelar_Bielik_Honzatko_Hubicka.Services;
using Chmelar_Bielik_Honzatko_Hubicka.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Chmelar_Bielik_Honzatko_Hubicka.Pages
{
    public class ActiveGamesModel : PageModel
    {
        IGameManipulator _gameManipulator;
        public ActiveGamesModel(IGameManipulator gameManipulator)
        {
            _gameManipulator = gameManipulator;
            MyGameLists = new List<GameOnList>();
            JoinGameLists = new List<GameOnList>();
        }
        public Guid gameId { get; set; }
        public List<Game> Games { get; set; }
        public List<User> Users { get; set; }
        public List<GameOnList> MyGameLists { get; set; }
        public List<GameOnList> JoinGameLists { get; set; }
        public void OnGet()
        {
            Games = new List<Game>();
            Games = _gameManipulator.MyGamesList();
            Games = new List<Game>();
            Games = _gameManipulator.JoinGamesList();
            Users = new List<User>();
            Users = _gameManipulator.UsersList();
        }
    }
}