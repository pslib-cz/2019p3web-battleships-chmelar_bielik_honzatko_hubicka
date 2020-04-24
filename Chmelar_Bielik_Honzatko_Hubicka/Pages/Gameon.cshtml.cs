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
        readonly GameManipulator _gm;
        readonly GameSessionStorage<Guid> _gss;
        readonly ApplicationDbContext _db;

        public NavyBattlePiece Piece { get; set; } //For selecting of piece to hit.
        public NavyBattlePiece Piece02 { get; set; } //For selecting of piece to hit.
        public Color Color { get; set; } //Color of the cell.
        public Guid GameId { get; set; }

        public GameonModel()
        {
            Piece = new NavyBattlePiece();
            Color = new Color();
        }

        public void OnGet()
        {
            var piece = _db.NavyBattlePieces.SingleOrDefault(nbp => nbp.GameId == GameId);

            _gss.SetGameId(piece.GameId);
            _gss.LoadGame();

            if (piece != null)
            {
                Piece = piece;
            }

            if (Piece02.State == BattlePieceState.Hitted_Water)
            {
                Color = Color.Aqua;
            }

            else if (Piece02.State == BattlePieceState.Hitted_Ship)
            {
                Color = Color.Black;
            }

            else
            {
                Color = Color.White;
            }
        }

        public void OnGetPosition(int posX, int posY) 
        {
            Piece02.PosX = posX;
            Piece02.PosY = posY;

            if (Piece02 != null)
            {
                _gm.Hit(Piece02);
            }
        }

        public void OnPostMessage(string text) //For messages.
        {
            var piece = _db.NavyBattlePieces.SingleOrDefault(nbp => nbp.GameId == GameId);

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
            }

            TempData.AddMessage("messagebox", TempDataExtension.MessageType.success, text);
        }
    }
}