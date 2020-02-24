using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class ShipPiece
    {
        public int Id { get; set; }
        public int ShipId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int IsMargin { get; set; }
    }
}
