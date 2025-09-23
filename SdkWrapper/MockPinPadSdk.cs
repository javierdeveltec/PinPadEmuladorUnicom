using PinPadEmuladorUnicom.Models;
using PinPadEmuladorUnicom.SdkWrapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PinPadEmuladorUnicom.SdkWrapper
{
    public class MockPinPadSdk : IPinPadSdk
    {
        private Random _rnd = new Random();

        public Task InitializeAsync(string pinpadConnection, int pinpadPort, int timeoutSeconds)
        {
            // Simula inicialización
            return Task.CompletedTask;
        }

        public Task CargarLlavesAsync()
        {
            // Simula carga de llaves
            return Task.Delay(300);
        }

        public Task<TransaccionResponse[]> ReversosPendientesAsync()
        {
            var items = Enumerable.Range(1, 3).Select(i => new TransaccionResponse
            {
                Exito = true,
                CodigoRespuesta = "99",
                Autorizacion = "",
                Leyenda = "Reverso simulado",
                Datos = new { Id = $"REV{i:000}", Monto = _rnd.Next(50, 500) },
                Fecha = DateTime.UtcNow.AddMinutes(-_rnd.Next(1, 720))
            }).ToArray();

            return Task.FromResult(items);
        }

        private string GenAuth() => _rnd.Next(100000, 999999).ToString();
        private string GenARQC() => $"ARQC{_rnd.Next(100000, 999999)}";

        public Task<TransaccionResponse> VentaAsync(VentaRequest request)
        {
            var t = new TransaccionResponse
            {
                Exito = true,
                CodigoRespuesta = "00",
                Autorizacion = GenAuth(),
                ARQC = GenARQC(),
                AID = "A0000000041010",
                TrackI = $"B{(request.Tarjeta ?? "4111111111111111")}^MOCK/TEST^25010100000000000000",
                TrackII = $"{(request.Tarjeta ?? "4111111111111111")}=25010100000000000000",
                Leyenda = "Autorizado",
                NumeroTarjeta = request.Tarjeta,
                ModoLectura = "05", // suponemos chip
                Datos = request
            };
            return Task.FromResult(t);
        }

        public Task<TransaccionResponse> DevolucionAsync(string idVentaOriginal, decimal monto)
        {
            var t = new TransaccionResponse
            {
                Exito = true,
                CodigoRespuesta = "00",
                Autorizacion = GenAuth(),
                Leyenda = "Devolución autorizada",
                Datos = new { IdVentaOriginal = idVentaOriginal, Monto = monto }
            };
            return Task.FromResult(t);
        }

        public Task<TransaccionResponse> CancelacionVentaAsync(string referenciaFinanciera)
        {
            var t = new TransaccionResponse
            {
                Exito = true,
                CodigoRespuesta = "00",
                Autorizacion = GenAuth(),
                Leyenda = $"Cancelación de venta {referenciaFinanciera} autorizada",
                Datos = new { ReferenciaFinanciera = referenciaFinanciera }
            };
            return Task.FromResult(t);
        }

        public Task<TransaccionResponse> ConsultaPuntosAsync(string tarjeta)
        {
            var t = new TransaccionResponse
            {
                Exito = true,
                CodigoRespuesta = "00",
                Leyenda = "Consulta de puntos exitosa",
                Datos = new { Tarjeta = tarjeta, Puntos = _rnd.Next(0, 10000) }
            };
            return Task.FromResult(t);
        }
    }
}
