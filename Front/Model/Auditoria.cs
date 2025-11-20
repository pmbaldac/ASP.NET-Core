using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Model
{
    public class Auditoria
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public int idresponsable { get; set; }
        public string responsable { get; set; }
        public int idarea { get; set; }
        public string area { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime? fechafin { get; set; }
        public int idestado { get; set; }
        public string estado { get; set; }
    }
}
