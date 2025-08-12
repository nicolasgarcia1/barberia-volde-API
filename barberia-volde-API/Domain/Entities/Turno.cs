using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string EmailCliente { get; set; } = null!;
        [JsonConverter(typeof(DateTimeJsonConverter))]

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

    // Conversor personalizado
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}
