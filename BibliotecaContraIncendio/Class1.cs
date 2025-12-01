using BibliotecaDatosIncendios;
using BibliotecaLimiteTemperatura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaContraIncendios
{
    public class OperacionesAlarma
    {
        public static int[] historialtemperatura = new int[100];
        public static string[] historialFechas = new string[100];
        public static int cantidad = 0;

        public static int limiteAlerta = 47;
        public static int limitePeligro = 80;
    

    public static void Pausa()
        {
            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
        }
        public static void CargarHistorialDesdeArchivo()
        {
            cantidad = GestionArchivoTemperaturas.CargarHistorial(historialtemperatura, historialFechas);
        }

        public static void CargarLimitesDesdeArchivo()
        {
            GestionLimiteTemperatura.CargarLimites(ref limiteAlerta, ref limitePeligro);
        }

        public static string EvaluarEstado(int temperatura)
        {
            if (temperatura >= limitePeligro)
            {
                return "PELIGRO";
            }
            if (temperatura >= limiteAlerta)
            {
                return "ALERTA";
            }

            return "NORMAL";
        }

        public static void RegistrarTemperaturaManual()
        {
            Console.Clear();
            Console.Write("Ingrese la temperatura: ");

            string texto = Console.ReadLine();
            int temperatura;

            int.TryParse(texto, out temperatura);

            if (temperatura == 0 && texto != "0")
            {
                Console.WriteLine();
                Console.WriteLine("Valor no válido, solo números.");
                Pausa();
                return;
            }

            if (cantidad < historialtemperatura.Length)
            {
                historialtemperatura[cantidad] = temperatura;
                historialFechas[cantidad] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                cantidad++;
            }
            else
            {
                Console.WriteLine("El historial está lleno.");
                Pausa();
                return;
            }

            GestionArchivoTemperaturas.GuardarHistorial(historialtemperatura, historialFechas, cantidad);

            string estado = EvaluarEstado(temperatura);

            Console.WriteLine();
            Console.WriteLine("Temperatura registrada correctamente.");
            Console.WriteLine("Valor : " + temperatura + "°C");
            Console.WriteLine("Estado: " + estado);
            Console.WriteLine("Fecha : " + historialFechas[cantidad - 1]);

            Pausa();
        }


    }


}

