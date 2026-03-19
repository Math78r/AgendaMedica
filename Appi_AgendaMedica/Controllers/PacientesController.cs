using Application.DTOs.Pacientes;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appi_AgendaMedica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var pacientes = await _pacienteService.ListarAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var paciente = await _pacienteService.ObtenerPorIdAsync(id);

            if (paciente is null)
                return NotFound(new { mensaje = "Paciente no encontrado." });

            return Ok(paciente);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] CrearPacienteRequestDto request)
        {
            var nuevoId = await _pacienteService.AgregarAsync(request);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = nuevoId },
                new
                {
                    mensaje = "Paciente agregado correctamente.",
                    idPaciente = nuevoId
                });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarPacienteRequestDto request)
        {
            if (id != request.IdPaciente)
            {
                return BadRequest(new { mensaje = "El id de la ruta no coincide con el id del cuerpo." });
            }

            await _pacienteService.ActualizarAsync(request);
            return Ok(new { mensaje = "Paciente actualizado correctamente." });
        }

        [HttpPatch("{id:int}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _pacienteService.DesactivarAsync(id, false);
            return Ok(new { mensaje = "Paciente desactivado correctamente." });
        }

        [HttpPatch("{id:int}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            await _pacienteService.ActivarAsync(id, true);
            return Ok(new { mensaje = "Paciente activado correctamente." });
        }
    }
}
