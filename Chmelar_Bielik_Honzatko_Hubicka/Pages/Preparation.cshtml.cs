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

        }

        public IActionResult OnGetGame()
        {
            _gm.StartGame();

            return RedirectToPage("./Gameon");
        }
    }
}