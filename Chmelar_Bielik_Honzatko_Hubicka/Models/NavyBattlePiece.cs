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
        [ForeignKey("BoardId")]
        public int BoardId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public BattlePieceState State { get; set; }
        public ICollection<Game> GamePieces { get; set; }
        public enum BattlePieceState
        {
            Ship = 0,
            Water = 1,
            Hit = 2
        }
    }
}
