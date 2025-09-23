using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinPadEmuladorUnicom.Models;

namespace PinPadEmuladorUnicom.SdkWrapper
{
    public interface IPinPadSdk
    {
        Task InitializeAsync(string pinpadConnection, int pinpadPort, int timeoutSeconds);
        Task<TransaccionResponse> VentaAsync(VentaRequest request);
        Task<TransaccionResponse> DevolucionAsync(string idVentaOriginal, decimal monto);
        Task<TransaccionResponse> CancelacionVentaAsync(string referenciaFinanciera);
        Task<TransaccionResponse> ConsultaPuntosAsync(string tarjeta);
        Task CargarLlavesAsync();
        Task<TransaccionResponse[]> ReversosPendientesAsync();
    }
}
