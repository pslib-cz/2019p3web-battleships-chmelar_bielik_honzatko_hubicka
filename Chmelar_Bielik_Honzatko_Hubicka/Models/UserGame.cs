using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class UserGame
    {
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [ForeignKey("GameId")]
        public Game GameId { get; set; }
        [ForeignKey("ShipUserGame")]
        public ShipUserGame PlayerState { get; set;}
    }
}
