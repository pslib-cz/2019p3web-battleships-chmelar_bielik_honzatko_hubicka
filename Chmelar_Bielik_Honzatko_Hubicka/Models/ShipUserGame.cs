using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class ShipUserGame
    {
        public int Id { get; set; }
        public string GameId { get; set; }
        public int ShipId { get; set; }
        public string UserId { get; set; }
    }
}
