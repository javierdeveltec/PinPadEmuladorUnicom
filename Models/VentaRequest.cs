using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPadEmuladorUnicom.Models
{
    public class VentaRequest
    {
        public string IdVenta { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; } = "MXN";
        public string Tarjeta { get; set; } // en modo MOTO / demo
    }
}
