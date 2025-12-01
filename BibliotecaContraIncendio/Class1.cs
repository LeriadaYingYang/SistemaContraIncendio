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

        public static void GenerarTemperaturaAleatoria()
        {
            Console.Clear();

            Random rnd = new Random();
            int temperatura = rnd.Next(10, 200);

            if (cantidad < historialtemperatura.Length)
            {
                historialtemperatura[cantidad] = temperatura;
                historialFechas[cantidad] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                cantidad++;
            }

            GestionArchivoTemperaturas.GuardarHistorial(historialtemperatura, historialFechas, cantidad);

            string estado = EvaluarEstado(temperatura);

            Console.WriteLine("Temperatura generada automáticamente:");
            Console.WriteLine("Valor : " + temperatura + "°C");
            Console.WriteLine("Estado: " + estado);
            Console.WriteLine("Fecha : " + historialFechas[cantidad - 1]);

            Pausa();
        }

        public static void ReemplazarTemperatura()
        {
            Console.Clear();

            if (cantidad == 0)
            {
                Console.WriteLine("No hay datos en el historial.");
                Pausa();
                return;
            }

            Console.WriteLine("=== HISTORIAL ACTUAL ===");
            Console.WriteLine();

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine(
                    (i + 1) + ". " +
                    historialtemperatura[i] + "°C" +
                    "  -  " + historialFechas[i]
                );
            }

            Console.WriteLine();
            Console.Write("Ingrese la posición que desea reemplazar (1 a " + cantidad + "): ");

            string textoPosicion = Console.ReadLine();
            int posicion;
            int.TryParse(textoPosicion, out posicion);

            if (posicion <= 0 || posicion > cantidad)
            {
                Console.WriteLine();
                Console.WriteLine("Posición inválida.");
                Pausa();
                return;
            }

            Console.Write("Ingrese la nueva temperatura: ");

            string textoTemperatura = Console.ReadLine();
            int nuevaTemperatura;
            int.TryParse(textoTemperatura, out nuevaTemperatura);

            if (nuevaTemperatura == 0 && textoTemperatura != "0")
            {
                Console.WriteLine();
                Console.WriteLine("Valor no válido.");
                Pausa();
                return;
            }

            int indice = posicion - 1;
            historialtemperatura[indice] = nuevaTemperatura;
            historialFechas[indice] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            GestionArchivoTemperaturas.GuardarHistorial(historialtemperatura, historialFechas, cantidad);

            string estado = EvaluarEstado(nuevaTemperatura);

            Console.WriteLine();
            Console.WriteLine("Temperatura reemplazada correctamente.");
            Console.WriteLine("Nueva temperatura: " + nuevaTemperatura + "°C");
            Console.WriteLine("Estado          : " + estado);
            Console.WriteLine("Fecha           : " + historialFechas[indice]);

            Pausa();
        }




    }
}

