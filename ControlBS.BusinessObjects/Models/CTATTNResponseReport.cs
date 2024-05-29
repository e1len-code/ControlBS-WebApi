using System.Buffers.Text;

namespace ControlBS.BusinessObjects.Models
{
    public class CTATTNResponseReport
    {
        public string? NOMBRES_Y_APELLIDOS { get; set; }
        public string? DNI { get; set; }
        public string? OBSERVACION { get; set; }
        public DateOnly FECHA { get; set; }
        public TimeOnly? HORA_INGRESO { get; set; }
        public TimeOnly? HORA_ALMUERZO { get; set; }
        public TimeOnly? HORA_ALMUERZO_FINAL { get; set; }
        public TimeOnly? HORA_SALIDA { get; set; }
    }
}