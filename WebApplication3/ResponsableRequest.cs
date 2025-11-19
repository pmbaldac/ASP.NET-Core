using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest
{
    public class ResponsableRequest
    {
        public int idarea { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
    }
}
