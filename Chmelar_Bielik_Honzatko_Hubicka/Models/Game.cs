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
        public int MaxUsers { get; set; }
        //public  GameState { get; set; }
        [ForeignKey("UserId")]
        public User OwnerId { get; set; }
        [ForeignKey("UserId")]
        public User CurrentPlayerId { get; set; }
        public int GameSize { get; set; }
        public ICollection<UserGame> UserGames { get; set; }
        public ICollection<ShipGame> ShipGames { get; set; }
    }
}
