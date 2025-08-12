using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Enum;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;

        public TurnoService(ITurnoRepository turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }

        public async Task<List<TurnoResponse>> GetAllTurnosAsync()
        {
            var turnos = await _turnoRepository.GetAllTurnosAsync();
            return turnos.Select(t => TurnoProfile.ToTurnoResponse(t)).ToList();
        }

        public async Task<List<TurnoResponse>> GetAllTurnosPendientesAsync()
        {

            var turnos = await _turnoRepository.GetAllTurnosPendientesAsync();
            return turnos.Select(t => TurnoProfile.ToTurnoResponse(t)).ToList();
        }

        public async Task<TurnoResponse> GetTurnoByIdAsync(int id)
        {
            var turno = await _turnoRepository.GetTurnoByIdAsync(id);
            if (turno == null)
            {
                throw new KeyNotFoundException("Turno no encontrado.");
            }
            return TurnoProfile.ToTurnoResponse(turno);
        }

        public async Task<TurnoResponse> CrearTurnoAsync(TurnoRequest request)
        {
            if (request.FechaHora.Minute != 0 || request.FechaHora.Second != 0 || request.FechaHora.Millisecond != 0)
            {
                throw new ArgumentException("La hora del turno debe ser en punto (sin minutos ni segundos).");
            }

            var turno = TurnoProfile.ToTurnoEntity(request);
            bool creado = await _turnoRepository.CrearTurnoAsync(turno);
            if (!creado)
            {
                throw new InvalidOperationException("Ya existe un turno para la fecha y hora especificada.");
            }

            return TurnoProfile.ToTurnoResponse(turno);
        }

        public async Task<TurnoResponse> ActualizarTurnoAsync(int id, TurnoRequest request)
        {
            var turno = await _turnoRepository.GetTurnoByIdAsync(id);
            if (turno == null)
            {
                throw new KeyNotFoundException("Turno no encontrado.");
            }
            var newTurno = TurnoProfile.ToUpdateTurno(turno, request);
            await _turnoRepository.ActualizarTurnoAsync(turno);
            return TurnoProfile.ToTurnoResponse(newTurno);
        }

        public async Task<TurnoResponse> ActualizarEstadoAsync(int id, EstadoTurno estado)
        {
            if (!Enum.IsDefined(typeof(EstadoTurno), estado))
            {
                throw new ArgumentException("Estado de turno inválido.");
            }

            var turno = await _turnoRepository.GetTurnoByIdAsync(id);
            if (turno == null)
            {
                throw new KeyNotFoundException("Turno no encontrado.");
            }

            turno.Estado = estado;
            await _turnoRepository.ActualizarTurnoAsync(turno);
            return TurnoProfile.ToTurnoResponse(turno);
        }
    }
}
