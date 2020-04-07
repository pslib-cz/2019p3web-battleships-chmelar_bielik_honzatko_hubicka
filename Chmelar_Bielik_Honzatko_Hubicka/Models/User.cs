using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class User : IdentityUser
    {
        public ICollection<Game> GamesPlay { get; set; } //CurrentPlayer v Game
        public ICollection<Game> GamesOwner { get; set; } //Owner v Game
        public ICollection<Game> GamesPlayer { get; set; } //Player v Game
        public ICollection<NavyBattlePiece> Pieces { get; set; }
    }
}
