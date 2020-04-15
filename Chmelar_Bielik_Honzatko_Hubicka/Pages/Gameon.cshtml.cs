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
        public Color Color { get; set; } //Color of the cell.

        public GameonModel()
        {
            Piece = new NavyBattlePiece();
            Color = new Color();
        }

        public void OnGet()
        {

        }

        public void OnPostMessage(string text) //For messages.
        {
            TempData.AddMessage("messagebox", TempDataExtension.MessageType.success, text);
        }
    }
}