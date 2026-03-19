using Application.DTOs.Pacientes;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<int> AgregarAsync(CrearPacienteRequestDto request)
        {
            var nuevoPaciente = new Paciente(
                0,
                request.Nombre.Trim(),
                request.Apellido.Trim(),
                request.FechaNacimiento,
                request.Telefono.Trim(),
                request.Correo.Trim(),
                true
            );

            return await _pacienteRepository.AgregarAsync(nuevoPaciente);
        }

        public async Task ActualizarAsync(ActualizarPacienteRequestDto request)
        {
            var pacienteActualizado = new Paciente(
                request.IdPaciente,
                request.Nombre.Trim(),
                request.Apellido.Trim(),
                request.FechaNacimiento,
                request.Telefono.Trim(),
                request.Correo.Trim(),
                true
            );

            await _pacienteRepository.ActualizarAsync(pacienteActualizado);
        }

        public async Task<IEnumerable<Paciente>> ListarAsync()
        {
            return await _pacienteRepository.ListarAsync();
        }

        public async Task<Paciente?> ObtenerPorIdAsync(int idPaciente)
        {
            return await _pacienteRepository.ObtenerPorIdAsync(idPaciente);
        }

        public async Task<bool> ExisteAsync(int idPaciente)
        {
            return await _pacienteRepository.ExisteAsync(idPaciente);
        }

        public async Task DesactivarAsync(int idPaciente,bool estado)
        {
            await _pacienteRepository.DesactivarAsync(idPaciente,estado);
        }

        public async Task ActivarAsync(int idPaciente, bool estado)
        {
            await _pacienteRepository.ActivarAsync(idPaciente, estado);
        }

        public Task Eliminar(int idPaciente)
        {
            throw new NotImplementedException();
        }
    }
}