﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public class Ship
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShipGame> ShipGames { get; set; }
    }
}
