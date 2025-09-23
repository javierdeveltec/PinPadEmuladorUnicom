using PinPadEmuladorUnicom.Models;
using PinPadEmuladorUnicom.SdkWrapper;
using PinPadEmuladorUnicom.Services;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PinPadEmuladorUnicom
{
    public partial class MainWindow : Window
    {
        private PinPadService _service;

        public MainWindow()
        {
            InitializeComponent();

            // Por defecto usamos el Mock SDK (no requiere hardware)
            var mock = new MockPinPadSdk();
            _service = new PinPadService(mock);
            Log("MockPinPadSdk cargado. Puedes inicializar o empezar a probar.");
        }

        private void Log(string text)
        {
            lstLog.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} - {text}");
        }

        private void SetDetail(object obj)
        {            
            txtDetalle.Text = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        private async void btnInit_Click(object sender, RoutedEventArgs e)
        {
            btnInit.IsEnabled = false;
            try
            {
                var conn = txtConexion.Text.Trim();
                if (!int.TryParse(txtPuerto.Text, out var port)) port = 1087;
                if (!int.TryParse(txtTimeout.Text, out var to)) to = 40;

                Log($"Inicializando PinPad: {conn}:{port} (timeout {to}s) ...");
                await _service.InitializeAsync(conn, port, to);
                Log("Inicializado OK.");
            }
            catch (Exception ex)
            {
                Log($"ERROR inicializar: {ex.Message}");
            }
            finally
            {
                btnInit.IsEnabled = true;
            }
        }

        private async void btnCargarLlaves_Click(object sender, RoutedEventArgs e)
        {
            btnCargarLlaves.IsEnabled = false;
            try
            {
                Log("Cargando llaves...");
                await _service.CargarLlavesAsync();
                Log("Llaves cargadas correctamente.");
            }
            catch (Exception ex)
            {
                Log($"ERROR cargar llaves: {ex.Message}");
            }
            finally
            {
                btnCargarLlaves.IsEnabled = true;
            }
        }

        private async void btnVenta_Click(object sender, RoutedEventArgs e)
        {
            btnVenta.IsEnabled = false;
            try
            {
                var req = new VentaRequest
                {
                    IdVenta = txtIdVenta.Text.Trim(),
                    Moneda = "MXN",
                    Tarjeta = txtTarjeta.Text.Trim()
                };
                if (decimal.TryParse(txtMonto.Text.Trim(), out var m)) req.Monto = m;

                Log($"Enviando Venta {req.IdVenta} - {req.Monto:C}");
                var resp = await _service.VentaAsync(req);
                Log($"Venta resp: {resp.CodigoRespuesta} - {resp.Leyenda ?? resp.Autorizacion}");
                SetDetail(resp);
            }
            catch (Exception ex)
            {
                Log($"ERROR venta: {ex.Message}");
            }
            finally
            {
                btnVenta.IsEnabled = true;
            }
        }

        private async void btnDevolucion_Click(object sender, RoutedEventArgs e)
        {
            btnDevolucion.IsEnabled = false;
            try
            {
                var id = txtRefVenta.Text.Trim();
                decimal monto = 0;
                if (!decimal.TryParse(txtMonto.Text.Trim(), out monto)) monto = 0;
                Log($"Enviando Devolución sobre {id} - {monto:C}");
                var resp = await _service.DevolucionAsync(id, monto);
                Log($"Devolución resp: {resp.CodigoRespuesta} - {resp.Leyenda}");
                SetDetail(resp);
            }
            catch (Exception ex)
            {
                Log($"ERROR devolucion: {ex.Message}");
            }
            finally
            {
                btnDevolucion.IsEnabled = true;
            }
        }

        private async void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.IsEnabled = false;
            try
            {
                var refFin = txtRefVenta.Text.Trim();
                Log($"Cancelando venta {refFin}...");
                var resp = await _service.CancelacionVentaAsync(refFin);
                Log($"Cancelación resp: {resp.CodigoRespuesta} - {resp.Leyenda}");
                SetDetail(resp);
            }
            catch (Exception ex)
            {
                Log($"ERROR cancelar: {ex.Message}");
            }
            finally
            {
                btnCancel.IsEnabled = true;
            }
        }

        private async void btnConsultaPuntos_Click(object sender, RoutedEventArgs e)
        {
            btnConsultaPuntos.IsEnabled = false;
            try
            {
                var tarjeta = txtTarjetaConsulta.Text.Trim();
                Log($"Consultando puntos tarjeta {tarjeta}...");
                var resp = await _service.ConsultaPuntosAsync(tarjeta);
                Log($"Consulta resp: {resp.CodigoRespuesta} - {resp.Leyenda}");
                SetDetail(resp);
            }
            catch (Exception ex)
            {
                Log($"ERROR consulta puntos: {ex.Message}");
            }
            finally
            {
                btnConsultaPuntos.IsEnabled = true;
            }
        }

        private async void btnReversos_Click(object sender, RoutedEventArgs e)
        {
            btnReversos.IsEnabled = false;
            try
            {
                Log("Obteniendo reversos pendientes...");
                var reversos = await _service.ReversosPendientesAsync();
                Log($"Reversos encontrados: {reversos?.Length ?? 0}");
                SetDetail(reversos);
            }
            catch (Exception ex)
            {
                Log($"ERROR reversos: {ex.Message}");
            }
            finally
            {
                btnReversos.IsEnabled = true;
            }
        }
    }
}
