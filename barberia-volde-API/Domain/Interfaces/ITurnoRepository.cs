
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITurnoRepository
    {
        Task<List<Turno>> GetAllTurnosAsync();
        Task<List<Turno>> GetAllTurnosPendientesAsync();
        Task<Turno?> GetTurnoByIdAsync(int id);
        Task CrearTurnoAsync(Turno turno);
        Task ActualizarTurnoAsync(Turno turno);
        Task ActualizarEstadoAsync(Turno turno);

    }
}
