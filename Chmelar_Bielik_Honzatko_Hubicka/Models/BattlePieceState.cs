using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Models
{
    public enum BattlePieceState
    {
        Unknown = 0,
        Ship = 1,
        Water = 2,
        Hitted_Water = 3,
        Hitted_Ship = 4
    }
}
