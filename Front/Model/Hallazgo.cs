using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Model
{
    public class Hallazgo
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public int idauditoria { get; set; }
        public string titulo { get; set; }
        public int idtipo { get; set; }
        public string tipo { get; set; }
        public int idseveridad { get; set; }
        public string severidad { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
    }
}
