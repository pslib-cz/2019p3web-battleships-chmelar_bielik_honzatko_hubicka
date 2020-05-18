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
        private string _gameKey;
        
        public string Color { get; set; } //Color of the cell.
        public List<NavyBattlePiece> Pieces { get; set; }
        public GameDeskModel GameDesk { get; set; }

        public GameonModel(GameLogic gl)
        {
            Color = "unknown";
            _gl = gl;
        }

        public void OnGet()
        {
            if (Request.QueryString["val"] != null)
                _gameKey = Request.QueryString["val"];

            Pieces = _gl.GetBattlefield(_gameKey);

            foreach (var p in Pieces)
            {
                if (p.State == BattlePieceState.Hitted_Ship)
                {
                    Color = "hittedship";
                }

                else if (p.State == BattlePieceState.Hitted_Water)
                {
                    Color = "hittedwater";
                }

                else
                {
                    Color = "unknown";
                }
            }

            GameDesk.Pieces = Pieces;
            GameDesk.Color = Color;
        }

        public void OnPost(string text, int pieceId)
        {
            text = _gl.Hit(_gameKey, pieceId);

            TempData.AddMessage("messagebox", TempDataExtension.MessageType.success, text);
        }
    }
}