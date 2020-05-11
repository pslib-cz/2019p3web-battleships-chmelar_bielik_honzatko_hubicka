using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Chmelar_Bielik_Honzatko_Hubicka.Services;
using Chmelar_Bielik_Honzatko_Hubicka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Chmelar_Bielik_Honzatko_Hubicka.Pages
{
    [Authorize]
    public class ActiveGamesModel : PageModel
    {
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }


        IGameManipulator _gameManipulator;
        public ActiveGamesModel(IGameManipulator gameManipulator)
        {
            _gameManipulator = gameManipulator;
            MyGameLists = new List<Game>();
            JoinGameLists = new List<Game>();
        }
        public Guid gameId { get; set; }
        public List<Game> MyGames { get; set; }
        public List<Game> OtherGames { get; set; }
        public string UserId { get; set; }
        public List<Game> MyGameLists { get; set; }
        public List<Game> JoinGameLists { get; set; }
        public void OnGet()
        {
            MyGames = new List<Game>();
            MyGames = _gameManipulator.MyGamesList();
            OtherGames = new List<Game>();
            OtherGames = _gameManipulator.JoinGamesList();
            UserId = _gameManipulator.GetUserId();
        }

        public IActionResult OnPostRemoveGame(Guid id)
        {
            bool result = _gameManipulator.RemoveGame(id);
            if (result)
            {
                MessageSuccess = "Hra byla smazána";
            }

            else
            {
                MessageError = "Hru se nepovedlo smazat";
            }
            return RedirectToPage("/ActiveGames");
        }

        public IActionResult OnPostJoinGame(Guid id)
        {
            _gameManipulator.JoinGame("Game", id);
            return RedirectToPage("/Preparation");
        }
    }
}