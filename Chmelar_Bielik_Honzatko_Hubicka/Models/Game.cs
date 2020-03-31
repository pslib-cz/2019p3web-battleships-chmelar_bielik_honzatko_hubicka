using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class Game
    {
        [Key]
        public Guid GameId { get; set; }
        public string OwnerId { get; set; }
        public string PlayerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; } //OWNER
        [ForeignKey("PlayerId")]
        public User Player { get; set; }
        public GameState Gamestate { get; set; }
        public PlayerState OwnerState { get; set; }
        public PlayerState PlayerState { get; set; }
        public int Player1Board { get; set; }
        public int Player2Board { get; set; }
        public ICollection<Game> GamePieces { get; set; }
        public int CurrentPlayerId { get; set; }
        public User CurrentPlayer { get; set; }
    }
}
