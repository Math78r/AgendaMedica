using Application.DTOs.Pacientes;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Models;
using Infrastructure.Persistence.Dapper;

using System.Data;
public class PacienteRepository : IPacienteRepository
{
    private readonly DapperContext _context;

    public PacienteRepository(DapperContext context)
    {
        _context = context;
    }

    public Task ActivarAsync(int idPaciente, bool Estado)
    {
        throw new NotImplementedException();
    }

    public Task ActualizarAsync(Paciente paciente)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AgregarAsync(Paciente paciente)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nombre", paciente.Nombre);
        parameters.Add("@Apellido", paciente.Apellido);
        parameters.Add("@FechaNacimiento", paciente.FechaNacimiento);
        parameters.Add("@Telefono", paciente.NumTelefono);
        parameters.Add("@Correo", paciente.Correo);

        using var connection = _context.CreateConnection();

        var result = await connection.QueryFirstAsync<SpAgregarPacienteResult>(
            "sp_Pacientes_Agregar",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        if (result.ERROR)
            throw new Exception(result.MENSAJE);

        return result.NuevoId ?? 0;
    }

    public Task DesactivarAsync(int idPaciente)
    {
        throw new NotImplementedException();
    }

    public Task DesactivarAsync(int idPaciente, bool Estado)
    {
        throw new NotImplementedException();
    }

    public Task Eliminar(int idPaciente)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExisteAsync(int idPaciente)
    {
        throw new NotImplementedException();
    }

   
    public Task<Paciente?> ObtenerPorIdAsync(int idPaciente)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Paciente>> ListarAsync()
    {
        using var connection = _context.CreateConnection();

        var pacientes = await connection.QueryAsync<Paciente>(
            "sp_Pacientes_Consultar",
            commandType: CommandType.StoredProcedure
        );

        return pacientes;
    }
}