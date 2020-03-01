using System;
using System.Collections.Generic;
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
        //public ? PlayerState { get; set;}
    }
}
