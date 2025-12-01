using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BibliotecaLimiteTemperatura
{
    public class GestionLimiteTemperatura
    {
        private static string rutaLimites = "limites.txt";

        public static void GuardarLimites(int limiteAlerta, int limitePeligro)
        {
            string[] lineas = new string[2];
            lineas[0] = "ALERTA=" + limiteAlerta;
            lineas[1] = "PELIGRO=" + limitePeligro;
            File.WriteAllLines(rutaLimites, lineas);
        }

        public static void CargarLimites(ref int limiteAlerta, ref int limitePeligro)
        {
            if (!File.Exists(rutaLimites))
            {
                return;
            }

            string[] lineas = File.ReadAllLines(rutaLimites);

            for (int i = 0; i < lineas.Length; i++)
            {
                string linea = lineas[i];

                if (linea.StartsWith("ALERTA="))
                {
                    string texto = linea.Substring("ALERTA=".Length);
                    int valor;
                    int.TryParse(texto, out valor);

                    if (!(valor == 0 && texto != "0"))
                    {
                        limiteAlerta = valor;
                    }
                }
                else if (linea.StartsWith("PELIGRO="))
                {
                    string texto = linea.Substring("PELIGRO=".Length);
                    int valor;
                    int.TryParse(texto, out valor);

                    if (!(valor == 0 && texto != "0"))
                    {
                        limitePeligro = valor;
                    }
                }
            }
        }
    }
}