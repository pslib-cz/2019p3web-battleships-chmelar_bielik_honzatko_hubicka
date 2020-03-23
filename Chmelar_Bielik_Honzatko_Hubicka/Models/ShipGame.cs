using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class ShipGame
    {
        public int Id { get; set; }
        [ForeignKey("GameId")]
        public Game GameId { get; set; }
        [ForeignKey("ShipId")]
        public Ship ShipId { get; set; }
    }
}
