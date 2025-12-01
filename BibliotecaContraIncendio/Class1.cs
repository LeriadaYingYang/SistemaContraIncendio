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



    }


}

