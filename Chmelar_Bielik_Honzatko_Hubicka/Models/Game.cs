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
        [ForeignKey("UserId")]
        public User Player1Id { get; set; } //OWNER
        public User Player2Id { get; set; }
        public GameState Gamestate { get; set; }
        public PlayerState Player1 { get; set; }
        public PlayerState Player2 { get; set; }
        public int Player1Board { get; set; }
        public int Player2Board { get; set; }
        public ICollection<Game> GamePieces { get; set; }
        public ICollection<UserGame> UserGames { get; set; }
        public int CurrentPlayerId { get; set; }
        public enum GameState
        {
            Preparing = 0,
            Fighting = 1,
            End = 2
        }
        public enum PlayerState
        {
            PreperingForGame = 0,
            InGame = 1,
            LFGame = 2
        }
    }
}
