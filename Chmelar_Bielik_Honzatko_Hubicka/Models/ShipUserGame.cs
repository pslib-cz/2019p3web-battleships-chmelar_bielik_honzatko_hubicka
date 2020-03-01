using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class ShipUserGame
    {
        public int Id { get; set; }
        [ForeignKey("GameId")]
        public Game GameId { get; set; }
        [ForeignKey("ShipId")]
        public Ship ShipId { get; set; }
        [ForeignKey("User")]
        public User UserId { get; set; }
    }
}
