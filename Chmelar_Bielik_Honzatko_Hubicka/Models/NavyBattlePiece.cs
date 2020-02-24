using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class NavyBattlePiece
    {
        public int Id { get; set; }
        public int ShipTypeId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool IsHidden { get; set; }
    }
}
