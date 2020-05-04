using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chmelar_Bielik_Honzatko_Hubicka.Controllers
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
