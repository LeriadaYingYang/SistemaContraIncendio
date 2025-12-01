using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BibliotecaDatosIncendios
{
    public class GestionArchivoTemperaturas
    {
        private static string rutaArchivo = "temperaturas.txt";

        public static void GuardarHistorial(int[] historialtemperatura, string[] historialFechas, int cantidad)
        {
            string[] lineas = new string[cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                string fecha = historialFechas[i];
                if (fecha == null)
                {
                    fecha = "";
                }

                lineas[i] = historialtemperatura[i].ToString() + ";" + fecha;
            }

            File.WriteAllLines(rutaArchivo, lineas);
        }

        public static int CargarHistorial(int[] historialtemperatura, string[] historialFechas)
        {
            int cantidad = 0;

            if (!File.Exists(rutaArchivo))
            {
                return 0;
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);

            for (int i = 0; i < lineas.Length && i < historialtemperatura.Length; i++)
            {
                string linea = lineas[i];
                string[] partes = linea.Split(';');

                string textoTemp = partes[0];
                int valor;
                int.TryParse(textoTemp, out valor);

                if (valor == 0 && textoTemp != "0")
                {
                    continue;
                }

                historialtemperatura[cantidad] = valor;

                if (partes.Length >= 2)
                {
                    historialFechas[cantidad] = partes[1];
                }
                else
                {
                    historialFechas[cantidad] = "";
                }

                cantidad++;
            }

            return cantidad;
        }
    }
}