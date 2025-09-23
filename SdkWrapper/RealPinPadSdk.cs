using PinPadEmuladorUnicom.Models;
using PinPadEmuladorUnicom.SdkWrapper;
using System;
using System.Threading.Tasks;

namespace PinPadEmuladorUnicom.SdkWrapper
{
    // Este archivo es un template: pega aquí las llamadas al SDK real.
    // Ejemplo (basado en el PDF): 
    // Peticion peticion = new Peticion();
    // peticion.SetAfiliacion(...);
    // peticion.SetTerminal(...);
    // peticion.SetOperacion(Operacion.Venta, parametros);
    // Tarjeta tarjeta = peticion.LeerTarjeta();
    // Respuesta respuesta = peticion.Autorizar();
    //
    // Las propiedades y métodos concretos dependen del namespace/nombres que incluya la DLL del banco.
    //
    // Referencias en PDF: Peticion, LeerTarjeta(), Autorizar(), FinalizarLecturaTarjeta(). :contentReference[oaicite:5]{index=5}

    public class RealPinPadSdk : IPinPadSdk
    {
        // TODO: Cambiar los tipos/dll-namespaces por los reales del SDK que te entregue el banco
        // private EGlobal.TotalPosInterfazNet.Interfaz.Interfaz _sdk;

        public Task InitializeAsync(string pinpadConnection, int pinpadPort, int timeoutSeconds)
        {
            // Ejemplo hipotético:
            // Interfaz.Instance.Configuracion.PinpadConexion = pinpadConnection;
            // Interfaz.Instance.Configuracion.PinpadPuerto = pinpadPort.ToString();
            // Interfaz.Instance.Inicializar();
            //
            // Si tu SDK expone Interfaz.Instance.Inicializar() (ver PDF), llama ahí.
            return Task.CompletedTask;
        }

        public Task CargarLlavesAsync()
        {
            // Ejemplo: Interfaz.Instance.CargarLlaves(); o llamar método de Peticion para carga.
            return Task.CompletedTask;
        }

        public Task<TransaccionResponse[]> ReversosPendientesAsync()
        {
            throw new NotImplementedException("Implementar según la DLL real.");
        }

        public Task<TransaccionResponse> VentaAsync(VentaRequest request)
        {
            // Mapea la lógica del PDF a la llamada real:
            // Peticion peticion = new Peticion();
            // peticion.SetAfiliacion(...);
            // peticion.SetTerminal(...);
            // peticion.Operador = ...;
            // peticion.Fecha = DateTime.Now.ToString("yyyyMMddHHmmss");
            // peticion.SetOperacion(Operacion.Venta, parametros);
            // peticion.LeerTarjeta(); // bloquea para leer tarjeta en pinpad
            // Respuesta respuesta = peticion.Autorizar();
            //
            // Convertir Respuesta (SDK) -> TransaccionResponse (tu modelo)
            throw new NotImplementedException("Implementar según la DLL real.");
        }

        public Task<TransaccionResponse> DevolucionAsync(string idVentaOriginal, decimal monto)
        {
            throw new NotImplementedException();
        }

        public Task<TransaccionResponse> CancelacionVentaAsync(string referenciaFinanciera)
        {
            throw new NotImplementedException();
        }

        public Task<TransaccionResponse> ConsultaPuntosAsync(string tarjeta)
        {
            throw new NotImplementedException();
        }
    }
}
