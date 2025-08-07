using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class TurnoRequest
    {
        [Required]
        public string NombreCliente { get; set; } = null!;
        [Required]
        public string EmailCliente { get; set; } = null!;
        [Required]
        public DateTime FechaHora
        {
            get; set;
        }
        public string? Comentario
        {
            get; set;
        }
    }
}
