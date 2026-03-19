using Application.DTOs.Pacientes;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> ListarAsync();
        Task<Paciente?> ObtenerPorIdAsync(int idPaciente);
        Task<int> AgregarAsync(Paciente paciente);
        Task ActualizarAsync(Paciente paciente);

        Task<bool> ExisteAsync(int idPaciente);

        Task DesactivarAsync(int idPaciente, bool Estado);
        Task ActivarAsync(int idPaciente, bool Estado);
        Task Eliminar(int idPaciente);
    }
}