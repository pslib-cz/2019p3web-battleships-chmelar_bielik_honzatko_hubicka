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
        [ForeignKey("ShipPieceId")]
        public ShipPiece ShipTypeId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool IsHidden { get; set; }
    }
}
