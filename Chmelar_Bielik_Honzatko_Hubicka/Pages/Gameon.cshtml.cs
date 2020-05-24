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
        public GameDeskModel GameDesk { get; set; }
        public string Text { get; set; }

        public GameonModel(GameLogic gl)
        {
            Color = "unknown";
            _gl = gl;
        }

        public void OnGet()
        {

            Pieces = _gl.GetBattlefield();

            foreach (var p in Pieces)
            {
                if (p.State == BattlePieceState.Hitted_Ship)
                {
                    Color = "hittedship";
                    Text = "You hitted a ship.";
                }

                else if (p.State == BattlePieceState.Hitted_Water)
                {
                    Color = "hittedwater";
                    Text = "You hitted a water.";
                }

                else
                {
                    Color = "unknown";
                }
            }

            GameDesk.Pieces = Pieces;
            GameDesk.Color = Color;
        }

        public void OnGetHit(int pieceId)
        {
            _gl.Hit(pieceId);
        }

        public void OnPost(string text)
        {
            text = Text;

            TempData.AddMessage("messagebox", TempDataExtension.MessageType.success, text);
        }
    }
}