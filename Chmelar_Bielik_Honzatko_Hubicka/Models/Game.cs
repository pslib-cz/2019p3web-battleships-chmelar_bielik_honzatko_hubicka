using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class Game
    {
        public Guid GameId { get; set; }
        public int MaxUsers { get; set; }
        //public  GameState { get; set; }
        public string OwnerId { get; set; }
        public string CurrentPlayerId { get; set; }
        public int GameSize { get; set; }
    }
}
