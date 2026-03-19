using Application.DTOs.Pacientes;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPacienteService
    {
         Task<IEnumerable<Paciente>> ListarAsync();

        Task<Paciente?> ObtenerPorIdAsync(int idPaciente);
        Task<int> AgregarAsync(CrearPacienteRequestDto paciente);
        Task ActualizarAsync(ActualizarPacienteRequestDto paciente);

        Task<bool> ExisteAsync(int idPaciente);
        
        Task DesactivarAsync(int idPaciente,bool Estado);
        Task ActivarAsync(int idPaciente, bool Estado);
        Task Eliminar(int idPaciente);
    }
}
