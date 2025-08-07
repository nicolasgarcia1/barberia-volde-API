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

        public async Task<TurnoResponse> GetTurnoByIdAsync(int id)
        {
            var turno = await _turnoRepository.GetTurnoByIdAsync(id);
            if (turno == null)
            {
                throw new KeyNotFoundException("Turno no encontrado.");
            }
            return TurnoProfile.ToTurnoResponse(turno);
        }

        public async Task CrearTurnoAsync(TurnoRequest request)
        {
            var turno = TurnoProfile.ToTurno(request);
            await _turnoRepository.CreateTurnoAsync(turno);
            return turno;
        }

        public async Task ActualizarTurnoAsync(int id, TurnoRequest request)
        {
            var turno = await _turnoRepository.GetTurnoByIdAsync(id);
            if (turno == null)
            {
                throw new KeyNotFoundException("Turno no encontrado.");
            }
            var newTurno = TurnoProfile.UpdateTurnoFromRequest(turno, request);
            await _turnoRepository.UpdateTurnoAsync(turno);
            return newTurno;
        }
    }
}
