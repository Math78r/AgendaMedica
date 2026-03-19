using Domain.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Medico
    {
        public int IdMedico { get; private set; }
        public string NumCedula { get; private  set; }
        public string Nombre { get; private  set; }
        public string Apellido { get; private  set; }
        public string IdEspecialidad { get; private set; }  
        
        public decimal Tarifa { get; private set; }
        public bool Estado { get; private set; }

        public Medico(int idMedico, string numCedula, 
                      string nombre, string apellido,
                      string idEspecialidad, decimal tarifa, bool estado)
        {
            if (string.IsNullOrWhiteSpace(NumCedula))
                throw new DomainException("La cédula del médico es obligatoria");

            if (string.IsNullOrWhiteSpace(Nombre))
                throw new DomainException("El nombre del médico es obligatorio");
            if (string.IsNullOrWhiteSpace(Apellido))
                throw new DomainException("La apellido del médico es obligatoria");
            if (string.IsNullOrWhiteSpace(IdEspecialidad))
                throw new DomainException("La especialidad del médico es obligatoria");

            IdMedico = idMedico;
            NumCedula = numCedula.Trim();
            Nombre = nombre.Trim();
            Apellido = apellido.Trim();
            IdEspecialidad = idEspecialidad;
            Tarifa = tarifa;
            Estado = estado;
        }
    }
}
