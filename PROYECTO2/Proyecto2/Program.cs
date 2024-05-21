using System;
using System.Collections.Generic;
using System.Globalization;


// Programa realizado para el Laboratorio de pensamiento computacional; elaborado, debuggeado e implementado por Johan Eduardo Escobar
// año: 2024
class Program
{
    static void Main(string[] args)
    {
        // Variables para la información de la cuenta
        string tipoCuenta = "", nombre = "", dpi = "", direccion = "", telefono = "";
        decimal saldo = 2500.00m;
        int duplicarSaldoContador = 0;

        // Listado para transacciones 
        List<string> transacciones = new List<string>();
        // Listado para el mantenimiento de las cuentas de terceros
        List<CuentaTercero> cuentasTercero = new List<CuentaTercero>();

        // Solicitar información de la cuenta
        Console.WriteLine("Ingrese la información de la cuenta bancaria:");
        tipoCuenta = SolicitarDato("Tipo de cuenta (monetaria quetzales, monetaria dólares, ahorro quetzales, ahorro dólares): ", ValidarTipoCuenta);
        nombre = SolicitarDato("Nombre: ", ValidarNombre);
        dpi = SolicitarDato("DPI (5 caracteres): ", ValidarDpi);
        direccion = SolicitarDato("Dirección: ", ValidarTexto);
        telefono = SolicitarDato("Teléfono: ", ValidarTelefono);

        bool ejecutar = true;
        while (ejecutar)
        {
            Console.WriteLine("\nMenú de opciones:");
            Console.WriteLine("1. Ver información de la cuenta");
            Console.WriteLine("2. Comprar producto financiero");
            Console.WriteLine("3. Vender producto financiero");
            Console.WriteLine("4. Abonar a cuenta");
            Console.WriteLine("5. Simular paso del tiempo");
            Console.WriteLine("6. Mantenimiento de cuentas de terceros");
            Console.WriteLine("7. Realizar transferencias a otras cuentas");
            Console.WriteLine("8. Pago de servicios");
            Console.WriteLine("9. Imprimir informe de transacciones");
            Console.WriteLine("10. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    VerInformacionCuenta(tipoCuenta, nombre, dpi, direccion, telefono, saldo, transacciones);
                    break;
                case "2":
                    saldo = ComprarProductoFinanciero(saldo, transacciones);
                    break;
                case "3":
                    saldo = VenderProductoFinanciero(saldo, transacciones);
                    break;
                case "4":
                    saldo = AbonarCuenta(saldo, ref duplicarSaldoContador, transacciones);
                    break;
                case "5":
                    saldo = SimularPasoTiempo(saldo, transacciones);
                    break;
                case "6":
                    MantenimientoCuentasTerceros(cuentasTercero);
                    break;
                case "7":
                    saldo = RealizarTransferencias(saldo, cuentasTercero, transacciones);
                    break;
                case "8":
                    saldo = PagoServicios(saldo, transacciones);
                    break;
                case "9":
                    ImprimirInformeTransacciones(transacciones);
                    break;
                case "10":
                    ejecutar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    static string SolicitarDato(string mensaje, Func<string, bool> validar)
    {
        string dato;
        do
        {
            Console.Write(mensaje);
            dato = Console.ReadLine();
        } while (!validar(dato));
        return dato;
    }

    static bool ValidarTipoCuenta(string tipoCuenta)
    {
        string[] tiposValidos = { "monetaria quetzales", "monetaria dólares", "ahorro quetzales", "ahorro dólares" };
        if (Array.Exists(tiposValidos, tipo => tipo.Equals(tipoCuenta.ToLower())))
            return true;
        else
        {
            Console.WriteLine("Tipo de cuenta no válido.");
            return false;
        }
    }

    static bool ValidarNombre(string nombre)
    {
        foreach (char c in nombre)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                Console.WriteLine("El nombre solo puede contener caracteres alfabéticos y espacios.");
                return false;
            }
        }
        return true;
    }

    static bool ValidarDpi(string dpi)
    {
        if (dpi.Length == 5 && int.TryParse(dpi, out _))
            return true;
        else
        {
            Console.WriteLine("El DPI debe ser numérico y tener exactamente 5 caracteres.");
            return false;
        }
    }

    static bool ValidarTexto(string texto)
    {
        return !string.IsNullOrWhiteSpace(texto);
    }

    static bool ValidarTelefono(string telefono)
    {
        foreach (char c in telefono)
        {
            if (!char.IsDigit(c))
            {
                Console.WriteLine("El teléfono solo puede contener caracteres numéricos.");
                return false;
            }
        }
        return true;
    }

    static void VerInformacionCuenta(string tipoCuenta, string nombre, string dpi, string direccion, string telefono, decimal saldo, List<string> transacciones)
    {
        Console.WriteLine("\nInformación de la cuenta:");
        Console.WriteLine($"Tipo de cuenta: {tipoCuenta}");
        Console.WriteLine($"Nombre: {nombre}");
        Console.WriteLine($"DPI: {dpi}");
        Console.WriteLine($"Dirección: {direccion}");
        Console.WriteLine($"Teléfono: {telefono}");
        Console.WriteLine($"Saldo: Q{saldo:F2}");
        Console.WriteLine("Transacciones:");
        foreach (var transaccion in transacciones)
        {
            Console.WriteLine(transaccion);
        }
    }

    static decimal ComprarProductoFinanciero(decimal saldo, List<string> transacciones)
    {
        saldo *= 0.90m;
        string transaccion = $"[Débito] {DateTime.Now}: Compra de producto financiero. Saldo actual: Q{saldo:F2}";
        transacciones.Add(transaccion);
        Console.WriteLine(transaccion);
        return saldo;
    }

    static decimal VenderProductoFinanciero(decimal saldo, List<string> transacciones)
    {
        if (saldo > 500.00m)
        {
            saldo *= 1.11m;
            string transaccion = $"[Crédito] {DateTime.Now}: Venta de producto financiero. Saldo actual: Q{saldo:F2}";
            transacciones.Add(transaccion);
            Console.WriteLine(transaccion);
        }
        else
        {
            string transaccion = $"[Aviso] {DateTime.Now}: No se puede realizar la venta. Saldo insuficiente. Saldo actual: Q{saldo:F2}";
            transacciones.Add(transaccion);
            Console.WriteLine(transaccion);
        }
        return saldo;
    }

    static decimal AbonarCuenta(decimal saldo, ref int duplicarSaldoContador, List<string> transacciones)
    {
        if (saldo > 500.00m && duplicarSaldoContador < 2)
        {
            saldo *= 2;
            duplicarSaldoContador++;
            string transaccion = $"[Crédito] {DateTime.Now}: Abono a cuenta. Saldo actual: Q{saldo:F2}";
            transacciones.Add(transaccion);
            Console.WriteLine(transaccion);
        }
        else
        {
            Console.WriteLine("No se puede abonar a la cuenta. Límite de duplicación alcanzado o saldo insuficiente.");
        }
        return saldo;
    }

    static decimal SimularPasoTiempo(decimal saldo, List<string> transacciones)
    {
        Console.WriteLine("Seleccione el periodo de capitalización (1: Una vez al mes, 2: Dos veces al mes): ");
        string opcion = Console.ReadLine();
        int vecesMes = (opcion == "1") ? 1 : 2;
        decimal tasaInteres = 0.02m;
        saldo += saldo * tasaInteres * vecesMes / 12;
        string transaccion = $"[Crédito] {DateTime.Now}: Simulación de tiempo. Saldo actual: Q{saldo:F2}";
        transacciones.Add(transaccion);
        Console.WriteLine(transaccion);
        return saldo;
    }

    static void MantenimientoCuentasTerceros(List<CuentaTercero> cuentasTercero)
    {
        Console.WriteLine("Seleccione una opción (1: Crear, 2: Eliminar, 3: Actualizar): ");
        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                CrearCuentaTercero(cuentasTercero);
                break;
            case "2":
                EliminarCuentaTercero(cuentasTercero);
                break;
            case "3":
                ActualizarCuentaTercero(cuentasTercero);
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    static void CrearCuentaTercero(List<CuentaTercero> cuentasTercero)
    {
        string nombre = SolicitarDato("Nombre del cuentahabiente: ", ValidarNombre);
        string numeroCuenta = SolicitarDato("Número de cuenta: ", ValidarTexto);
        string banco = SolicitarDato("Nombre del banco: ", ValidarTexto);
        string montoTransferir = SolicitarDato("Monto a transferir: ", ValidarTexto);
        string moneda = SolicitarDato("Moneda (quetzales/dólares): ", ValidarTipoCuenta);

        int id = cuentasTercero.Count + 1;
        cuentasTercero.Add(new CuentaTercero { Id = id, Nombre = nombre, NumeroCuenta = numeroCuenta, Banco = banco, MontoTransferir = montoTransferir, Moneda = moneda });
        Console.WriteLine("Cuenta de tercero creada exitosamente.");
    }

    static void EliminarCuentaTercero(List<CuentaTercero> cuentasTercero)
    {
        int id = int.Parse(SolicitarDato("ID de la cuenta a eliminar: ", ValidarTexto));
        var cuenta = cuentasTercero.Find(c => c.Id == id);
        if (cuenta != null)
        {
            cuentasTercero.Remove(cuenta);
            Console.WriteLine("Cuenta de tercero eliminada exitosamente.");
        }
        else
        {
            Console.WriteLine("Cuenta no encontrada.");
        }
    }

    static void ActualizarCuentaTercero(List<CuentaTercero> cuentasTercero)
    {
        int id = int.Parse(SolicitarDato("ID de la cuenta a actualizar: ", ValidarTexto));
        var cuenta = cuentasTercero.Find(c => c.Id == id);
        if (cuenta != null)
        {
            string nombre = SolicitarDato("Nombre del cuentahabiente: ", ValidarNombre);
            string numeroCuenta = SolicitarDato("Número de cuenta: ", ValidarTexto);
            string banco = SolicitarDato("Nombre del banco: ", ValidarTexto);
            string montoTransferir = SolicitarDato("Monto a transferir: ", ValidarTexto);
            string moneda = SolicitarDato("Moneda (quetzales/dólares): ", ValidarTipoCuenta);

            cuenta.Nombre = nombre;
            cuenta.NumeroCuenta = numeroCuenta;
            cuenta.Banco = banco;
            cuenta.MontoTransferir = montoTransferir;
            cuenta.Moneda = moneda;

            Console.WriteLine("Cuenta de tercero actualizada exitosamente.");
        }
        else
        {
            Console.WriteLine("Cuenta no encontrada.");
        }
    }

    static decimal RealizarTransferencias(decimal saldo, List<CuentaTercero> cuentasTercero, List<string> transacciones)
    {
        Console.WriteLine("Seleccione una cuenta para transferir:");
        foreach (var cuenta in cuentasTercero)
        {
            Console.WriteLine($"ID: {cuenta.Id}, Nombre: {cuenta.Nombre}, Banco: {cuenta.Banco}, Monto: {cuenta.MontoTransferir}, Moneda: {cuenta.Moneda}");
        }

        int id = int.Parse(SolicitarDato("ID de la cuenta a transferir: ", ValidarTexto));
        var cuentaTercero = cuentasTercero.Find(c => c.Id == id);
        if (cuentaTercero != null)
        {
            decimal monto = decimal.Parse(SolicitarDato("Monto a transferir (200 o 2000): ", ValidarTexto));
            if (monto == 200 || monto == 2000)
            {
                if (saldo >= monto)
                {
                    saldo -= monto;
                    string transaccion = $"[Débito] {DateTime.Now}: Transferencia a cuenta {cuentaTercero.NumeroCuenta}. Monto: Q{monto:F2}. Saldo actual: Q{saldo:F2}";
                    transacciones.Add(transaccion);
                    Console.WriteLine(transaccion);
                }
                else
                {
                    Console.WriteLine("Saldo insuficiente para realizar la transferencia.");
                }
            }
            else
            {
                Console.WriteLine("Monto no válido. Debe ser 200 o 2000.");
            }
        }
        else
        {
            Console.WriteLine("Cuenta no encontrada.");
        }
        return saldo;
    }

    static decimal PagoServicios(decimal saldo, List<string> transacciones)
    {
        Console.WriteLine("Seleccione el proveedor de servicios (1: Agua, 2: Electricidad, 3: Teléfono): ");
        string proveedor = Console.ReadLine();
        decimal monto = decimal.Parse(SolicitarDato("Monto a pagar: ", ValidarTexto));

        if (saldo >= monto)
        {
            saldo -= monto;
            string transaccion = $"[Débito] {DateTime.Now}: Pago de servicio ({proveedor}). Monto: Q{monto:F2}. Saldo actual: Q{saldo:F2}";
            transacciones.Add(transaccion);
            Console.WriteLine(transaccion);
        }
        else
        {
            Console.WriteLine("Saldo insuficiente para realizar el pago.");
        }
        return saldo;
    }

    static void ImprimirInformeTransacciones(List<string> transacciones)
    {
        Console.WriteLine("Historial de transacciones:");
        foreach (var transaccion in transacciones)
        {
            Console.WriteLine(transaccion);
        }
    }
}

public class CuentaTercero
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string NumeroCuenta { get; set; }
    public string Banco { get; set; }
    public string MontoTransferir { get; set; }
    public string Moneda { get; set; }
}
