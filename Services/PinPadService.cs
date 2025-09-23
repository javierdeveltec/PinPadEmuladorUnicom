using PinPadEmuladorUnicom.Models;
using PinPadEmuladorUnicom.SdkWrapper;
using System.Threading.Tasks;

namespace PinPadEmuladorUnicom.Services
{
    public class PinPadService
    {
        private readonly IPinPadSdk _sdk;

        public PinPadService(IPinPadSdk sdk)
        {
            _sdk = sdk;
        }

        public Task InitializeAsync(string connection, int port, int timeoutSeconds)
            => _sdk.InitializeAsync(connection, port, timeoutSeconds);

        public Task<TransaccionResponse> VentaAsync(VentaRequest req)
            => _sdk.VentaAsync(req);

        public Task<TransaccionResponse> DevolucionAsync(string idVentaOriginal, decimal monto)
            => _sdk.DevolucionAsync(idVentaOriginal, monto);

        public Task<TransaccionResponse> CancelacionVentaAsync(string referenciaFinanciera)
            => _sdk.CancelacionVentaAsync(referenciaFinanciera);

        public Task<TransaccionResponse> ConsultaPuntosAsync(string tarjeta)
            => _sdk.ConsultaPuntosAsync(tarjeta);

        public Task CargarLlavesAsync() => _sdk.CargarLlavesAsync();

        public Task<TransaccionResponse[]> ReversosPendientesAsync() => _sdk.ReversosPendientesAsync();
    }
}
