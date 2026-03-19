using Domain.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Paciente
    {
        public int IdPaciente { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public DateTime FechaNacimiento { get; private set; }
        public string Telefono { get; private set; }

        public string Correo { get; private set; }
        public bool Estado { get; private set; }

        public Paciente(int idPaciente, string nombre, string apellido, DateTime fechaNacimiento, string telefono, string correo, bool estado)
        {
           
            if (string.IsNullOrWhiteSpace(Nombre))
                throw new DomainException("El nombre del paciente es obligatorio");
            if (string.IsNullOrWhiteSpace(Apellido))
                throw new DomainException("El apellido del paciente es obligatorio");
            if (string.IsNullOrWhiteSpace(Telefono))
                throw new DomainException("El teléfono del paciente es obligatorio");
            if (string.IsNullOrWhiteSpace(Correo))
                throw new DomainException("El correo del paciente es obligatorio");

            IdPaciente = idPaciente;
            Nombre = nombre.Trim();
            Apellido = apellido.Trim();
            FechaNacimiento = fechaNacimiento;
            Telefono = telefono.Trim();
            Correo = correo.Trim();
            Estado = estado;
        }
    }
}
