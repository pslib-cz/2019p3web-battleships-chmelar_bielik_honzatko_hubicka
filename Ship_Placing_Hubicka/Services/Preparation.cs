using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chmelar_Bielik_Honzatko_Hubicka.Models; 


namespace Ship_Placing_Hubicka.Services
{
    public class Preparation
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public void add(int position)
        {
            int x = position / 10;
            int y = position % 10;
            _db.NavyBattlePieces.Add(new NavyBattlePiece());
        }
    }
}
