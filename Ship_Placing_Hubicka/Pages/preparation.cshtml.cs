using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ship_Placing_Hubicka.Pages
{
    public class preparationModel : PageModel
    {
        public void OnGet()
        {

        }
        public void OnPostClick(int position)
        {
            if (position == 1)
            {

            }
            OnGet();
        }


    }
}
