using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pacientes
{
    public class CrearPacienteRequestDto
    {
        public int IdPaciente { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public string Telefono { get; private set; }

        public string Correo { get; private set; }
        public bool Estado { get; private set; }
    }
}
