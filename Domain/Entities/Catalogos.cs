using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DiaSemana
    {
        public int IdDs { get; set; }
        public string DescDs { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }

    public class Especialidad
    {
        public int IdEspecialidad { get; set; }
        public string DescEspecialidad { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }

    public class EstadoCita
    {
        public int IdEstadoC { get; set; }
        public string DescEstadoC { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }

}
