using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Chmelar_Bielik_Honzatko_Hubicka.Helpers;
using Chmelar_Bielik_Honzatko_Hubicka.Models;
using Chmelar_Bielik_Honzatko_Hubicka.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chmelar_Bielik_Honzatko_Hubicka
{
    public class PreparationModel : PageModel
    {
        readonly GameManipulator _gm;
        private string _gameKey;
        public string Text { get; set; }

        public PreparationModel(GameManipulator gm)
        {
            _gm = gm;
        }

        public void OnPostMessage()
        {
            TempData.AddMessage("messagebox", TempDataExtension.MessageType.success, Text);
        }
        public void OnGet()
        {
            _gameKey = _gm.StartGame();
        }

        public void OnGet(bool generate)
        {
            if (generate == true)
            {
                _gm.GeneratorPieces(_gameKey);
                Text = "Your field was generated.";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IActionResult OnPostActive()
        {
            return RedirectToPage("./ActiveGames");
        }

        public IActionResult OnPostGame()
        {
            return RedirectToPage("./Gameon");
        }

        protected void btnRedirect(object sender, EventArgs e)
        {
            Response.Redirect($"Gameon.cshtml.cs?val={ _gameKey }");
        }
    }
}