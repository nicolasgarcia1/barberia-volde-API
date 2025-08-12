using Application.Models.Request;
using Application.Models.Response;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITurnoService
    {
        Task<List<TurnoResponse>> GetAllTurnosAsync();
        Task<List<TurnoResponse>> GetAllTurnosPendientesAsync();
        Task<TurnoResponse> GetTurnoByIdAsync(int id);
        Task<TurnoResponse> CrearTurnoAsync(TurnoRequest request);
        Task<TurnoResponse> ActualizarTurnoAsync(int id, TurnoRequest request);
        Task<TurnoResponse> ActualizarEstadoAsync(int id, EstadoTurno estado);
    }
}
