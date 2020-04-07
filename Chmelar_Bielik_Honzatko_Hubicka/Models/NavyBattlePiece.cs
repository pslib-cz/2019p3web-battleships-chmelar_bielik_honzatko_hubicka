using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class NavyBattlePiece
    {
        [Key]
        public int Id { get; set; }
        public Guid GameId { get; set; }
        public int BoardId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public BattlePieceState State { get; set; }
    }
}
