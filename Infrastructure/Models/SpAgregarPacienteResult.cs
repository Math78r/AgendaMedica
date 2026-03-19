using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class SpAgregarPacienteResult
    {
        public string MENSAJE { get; set; } = string.Empty;
        public bool ERROR { get; set; }
        public int? NuevoId { get; set; }
    }
}
