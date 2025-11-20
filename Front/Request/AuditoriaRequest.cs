using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Request
{
    public class AuditoriaRequest
    {
        public int idarea { get; set; }
        public string titulo { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime? fechafin { get; set; }
        public int idresponsable { get; set; }
        public int idestado { get; set; }
    }

}
