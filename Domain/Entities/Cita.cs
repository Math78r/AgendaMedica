
using Domain.Execptions;

namespace Domain.Entities;

public class Cita
{
    public int IdCita { get; private set; }
    public int IdMedico { get; private set; }
    public int IdPaciente { get; private set; }

    public DateOnly FechaCita { get; private set; }
    public TimeOnly HoraInicio { get; private set; }
    public TimeOnly HoraFin { get; private set; }

    public string Motivo { get; private set; }
    public int IdEstadoCita { get; private set; }

    public string? MotivoCancelacion { get; private set; }
    public DateTime? FechaCancelacion { get; private set; }

    public DateTime FechaCreacion { get; private set; }
    public bool Activo { get; private set; }

    public Cita(
     int idMedico,
     int idPaciente,
     DateOnly fechaCita,
     TimeOnly horaInicio,
     TimeOnly horaFin,
     string motivo)
    {
        if (idMedico <= 0)
            throw new DomainException("El médico es requerido.");

        if (idPaciente <= 0)
            throw new DomainException("El paciente es requerido.");

        if (string.IsNullOrWhiteSpace(motivo))
            throw new DomainException("El motivo es requerido.");

        if (horaFin <= horaInicio)
            throw new DomainException("La hora fin debe ser mayor a la hora inicio.");

        IdMedico = idMedico;
        IdPaciente = idPaciente;
        FechaCita = fechaCita;
        HoraInicio = horaInicio;
        HoraFin = horaFin;
        Motivo = motivo.Trim();
        IdEstadoCita = 1; // Pendiente, por ejemplo
        FechaCreacion = DateTime.Now;
        Activo = true;
    }

    public void Confirmar(int idEstadoConfirmada)
    {
        ValidarActiva();

        IdEstadoCita = idEstadoConfirmada;
    }

    public void Cancelar(string motivoCancelacion, int idEstadoCancelada)
    {
        ValidarActiva();

        if (string.IsNullOrWhiteSpace(motivoCancelacion))
            throw new DomainException("El motivo de cancelación es requerido.");

        MotivoCancelacion = motivoCancelacion.Trim();
        FechaCancelacion = DateTime.Now;
        IdEstadoCita = idEstadoCancelada;
    }

    public void Reprogramar(
        DateOnly nuevaFecha,
        TimeOnly nuevaHoraInicio,
        TimeOnly nuevaHoraFin)
    {
        ValidarActiva();

        if (nuevaHoraFin <= nuevaHoraInicio)
            throw new DomainException("La nueva hora fin debe ser mayor a la nueva hora inicio.");

        FechaCita = nuevaFecha;
        HoraInicio = nuevaHoraInicio;
        HoraFin = nuevaHoraFin;
    }

    public void Desactivar()
    {
        Activo = false;
    }

    private void ValidarActiva()
    {
        if (!Activo)
            throw new DomainException("La cita está inactiva.");
    }
}