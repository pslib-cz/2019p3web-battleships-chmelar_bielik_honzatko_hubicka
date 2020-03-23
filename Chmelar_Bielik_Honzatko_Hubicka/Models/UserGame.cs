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
         
        public enum PlayerState
        {
            PreperingForGame = 0,
            InGame = 1,
            LFGame = 2
        }
    }
}
