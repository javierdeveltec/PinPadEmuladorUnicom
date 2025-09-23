using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPadEmuladorUnicom.Models
{
    public class TransaccionResponse
    {
        public bool Exito { get; set; }
        public string CodigoRespuesta { get; set; }  // e.g. "00"
        public string Autorizacion { get; set; }     // 6 dígitos
        public string ARQC { get; set; }
        public string AID { get; set; }
        public string TrackI { get; set; }
        public string TrackII { get; set; }
        public string Leyenda { get; set; }          // mensaje del emisor
        public string NumeroTarjeta { get; set; }
        public string ModoLectura { get; set; }      // 05 Chip, 01 Digitada, 90 Banda...
        public object Datos { get; set; }            // payload original (request)
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
