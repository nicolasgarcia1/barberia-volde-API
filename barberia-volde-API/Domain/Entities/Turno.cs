using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string EmailCliente { get; set; } = null!;
        public DateTime FechaHora
        {
            get; set;
        }
        public string? Comentario
        {
            get; set;
        }
        public EstadoTurno Estado { get; set; }
    }
}
