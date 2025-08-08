using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public static class TurnoProfile
    {
        public static Turno ToTurnoEntity(TurnoRequest request)
        {
            return new Turno
            {
                NombreCliente = request.NombreCliente,
                EmailCliente = request.EmailCliente,
                FechaHora = request.FechaHora,
                Comentario = request.Comentario,
                Estado = Domain.Enum.EstadoTurno.Pendiente // asumiendo que siempre se inicializa en pendiente
            };
        }

        public static TurnoResponse ToTurnoResponse(Turno turno)
        {
            return new TurnoResponse
            {
                Id = turno.Id,
                NombreCliente = turno.NombreCliente,
                EmailCliente = turno.EmailCliente,
                FechaHora = turno.FechaHora,
                Comentario = turno.Comentario,
                Estado = turno.Estado
            };
        }

        public static Turno ToUpdateTurno(Turno existingTurno, TurnoRequest request)
        {
            existingTurno.NombreCliente = request.NombreCliente;
            existingTurno.EmailCliente = request.EmailCliente;
            existingTurno.FechaHora = request.FechaHora;
            existingTurno.Comentario = request.Comentario;
            return existingTurno;
        }
    }
}
