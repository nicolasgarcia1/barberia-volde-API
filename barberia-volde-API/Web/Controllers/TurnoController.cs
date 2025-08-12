using Application.Interfaces;
using Application.Models.Request;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;

        public TurnoController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpGet("AllTurnos")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTurnos()
        {
            try
            {
                var turnos = await _turnoService.GetAllTurnosAsync();
                if (turnos == null || !turnos.Any())
                {
                    return NotFound("No se encontraron turnos.");
                }
                return Ok(turnos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("TurnosPendientes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTurnosPendientes()
        {
            try
            {
                var turnosPendientes = await _turnoService.GetAllTurnosPendientesAsync();
                if (!turnosPendientes.Any())
                {
                    return NotFound("No se encontraron turnos pendientes.");
                }
                return Ok(turnosPendientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Turnos/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTurnoById(int id)
        {
            try
            {
                var turno = await _turnoService.GetTurnoByIdAsync(id);
                if (turno == null)
                {
                    return NotFound($"No se encontró el turno con ID {id}.");
                }
                return Ok(turno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CrearTurno")]
        public async Task<IActionResult> CrearTurno([FromBody] TurnoRequest turnoRequest)
        {
            try
            {
                var nuevoTurno = await _turnoService.CrearTurnoAsync(turnoRequest);
                return CreatedAtAction(nameof(GetTurnoById), new { id = nuevoTurno.Id }, nuevoTurno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("ActualizarTurno/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActualizarTurno(int id, [FromBody] TurnoRequest turnoRequest)
        {
            try
            {
                var turnoActualizado = await _turnoService.ActualizarTurnoAsync(id, turnoRequest);
                if (turnoActualizado == null)
                {
                    return NotFound($"No se encontró el turno con ID {id} para actualizar.");
                }
                return Ok(turnoActualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("ActualizarEstado/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EstadoTurno estado)
        {
            try
            {
                var turnoActualizado = await _turnoService.ActualizarEstadoAsync(id, estado);
                if (turnoActualizado == null)
                {
                    return NotFound($"No se encontró el turno con ID {id} para actualizar.");
                }
                return Ok(turnoActualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
