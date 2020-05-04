using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.ViewModels
{
    public class GameOnList
    {
        [Display(Name = "Game")]
        public string GameState { get; set; }
        [Display(Name = "Player 1")]
        public string Player1 { get; set; }
        [Display(Name = "Player 2")]
        public string Player2 { get; set; }
    }
}
