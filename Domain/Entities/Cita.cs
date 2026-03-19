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
    public int IdDiaSemana { get; private set; }
    public string Motivo { get; private set; }
    public int IdEstadoC { get; private set; }
    public string? MotivoCancelacion { get; private set; }
    public DateTime? FechaCancelacion { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public bool Estado { get; private set; }

    protected Cita() { }

    public Cita(
        int idMedico,
        int idPaciente,
        DateOnly fechaCita,
        TimeOnly horaInicio,
        TimeOnly horaFin,
        int idDiaSemana,
        string motivo,
        int idEstadoC)
    {
        if (idMedico <= 0) throw new DomainException("El médico es requerido.");
        if (idPaciente <= 0) throw new DomainException("El paciente es requerido.");
        if (string.IsNullOrWhiteSpace(motivo)) throw new DomainException("El motivo es requerido.");
        if (horaFin <= horaInicio) throw new DomainException("La hora fin debe ser mayor a la hora inicio.");

        IdMedico = idMedico;
        IdPaciente = idPaciente;
        FechaCita = fechaCita;
        HoraInicio = horaInicio;
        HoraFin = horaFin;
        IdDiaSemana = idDiaSemana;
        Motivo = motivo;
        IdEstadoC = idEstadoC;
        FechaCreacion = DateTime.Now;
        Estado = true;
    }

    public void Confirmar(int idEstadoConfirmada)
    {
        if (!Estado)
            throw new DomainException("No se puede confirmar una cita inactiva.");

        IdEstadoC = idEstadoConfirmada;
    }

    public void Cancelar(string motivoCancelacion, int idEstadoCancelada)
    {
        if (!Estado)
            throw new DomainException("No se puede cancelar una cita inactiva.");

        if (string.IsNullOrWhiteSpace(motivoCancelacion))
            throw new DomainException("El motivo de cancelación es requerido.");

        MotivoCancelacion = motivoCancelacion;
        FechaCancelacion = DateTime.Now;
        IdEstadoC = idEstadoCancelada;
    }

    public void Reprogramar(DateOnly nuevaFecha, TimeOnly nuevaHoraInicio, TimeOnly nuevaHoraFin, int nuevoDiaSemana)
    {
        if (!Estado)
            throw new DomainException("No se puede reprogramar una cita inactiva.");

        if (nuevaHoraFin <= nuevaHoraInicio)
            throw new DomainException("La nueva hora fin debe ser mayor a la nueva hora inicio.");

        FechaCita = nuevaFecha;
        HoraInicio = nuevaHoraInicio;
        HoraFin = nuevaHoraFin;
        IdDiaSemana = nuevoDiaSemana;
    }

    public void Desactivar()
    {
        Estado = false;
    }
}