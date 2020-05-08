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
    public class GameonModel : PageModel
    {
        readonly GameLogic _gl;
        
        public string Color { get; set; } //Color of the cell.
        public List<NavyBattlePiece> Pieces { get; set; }

        public GameonModel(GameLogic gl)
        {
            Color = "unknown";
            _gl = gl;
        }

        public void OnGet()
        {
            Pieces = _gl.GetBattlefield("GameKey");
        }

        public void OnPostMessage(string text) //For messages.
        {
            /*var piece = _db.NavyBattlePieces.SingleOrDefault(nbp => nbp.GameId == GameId);

            if (piece is null)
            {
                text = "No piece in game or not existing game.";
            }

            if (Piece02.State == BattlePieceState.Hitted_Ship)
            {
                text = "You hitted a ship.";
            }

            else if (Piece02.State == BattlePieceState.Hitted_Water)
            {
                text = "You missed. You hitted a water.";
            }

            else
            {
                throw new NotImplementedException();
            }*/

            TempData.AddMessage("messagebox", TempDataExtension.MessageType.success, text);
        }
    }
}