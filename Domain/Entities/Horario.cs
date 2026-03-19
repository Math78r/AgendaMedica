using Domain.Execptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Horario
    {
        public int IdHorario { get; private set; }
        public int IdMedico { get; private set; }
        public int IdDiaSemana { get; private set; }
        public TimeOnly HoraInicio { get; private set; }
        public TimeOnly HoraFin { get; private set; }
        public bool Estado { get; private set; }

        public Horario(int idHorario, int idMedico, int idDiaSemana, TimeOnly horaInicio, TimeOnly horaFin, bool estado)
        {

            if (HoraInicio > HoraFin)
                throw new DomainException("La hora de fin debe ser mayor a la hora de inicio.");
            if (HoraInicio.ToTimeSpan() == default(TimeSpan))
                throw new DomainException("La hora de inicio es obligatoria.");
            if (HoraFin.ToTimeSpan() == default(TimeSpan))
                throw new DomainException("La hora de fin es obligatoria.");
            if (IdMedico <= 0)
                throw new DomainException("Seleccionar un médico es obligatorio");
            if (IdDiaSemana <= 0)
                throw new DomainException("Seleccionar un día hábil  es obligatorio");
            IdHorario = idHorario;
            IdMedico = idMedico;
            IdDiaSemana = idDiaSemana;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Estado = estado;
        }
        public bool EstaDentroHorario(TimeOnly inicio, TimeOnly fin)
        {
            return inicio >= HoraInicio && fin <= HoraFin;
        }
    }
}
