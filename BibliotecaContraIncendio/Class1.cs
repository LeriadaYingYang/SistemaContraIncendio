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

            Console.WriteLine("ESTE ES EL HISTORIAL ACTUAL DE LA BASE DE TEXTO:");
            Console.WriteLine();

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine((i + 1) + ". " +historialtemperatura[i] + "°C" +"  -  " + historialFechas[i]);
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

        public static void EliminarTemperatura()
        {
            Console.Clear();

            if (cantidad == 0)
            {
                Console.WriteLine("No hay datos en el historial.");
                Pausa();
                return;
            }

            Console.WriteLine("ESTE ES EL HISTORIAL ACTUAL DE LA BASE DE TEXTO:");
            Console.WriteLine();

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine((i + 1) + ". " + historialtemperatura[i] + "°C" + "  -  " + historialFechas[i]);
            }

            Console.WriteLine();
            Console.Write("Ingrese la posición que desea eliminar (1 a " + cantidad + "): ");

            string textoPosicion = Console.ReadLine();
            int posicion;
            int.TryParse(textoPosicion, out posicion);

            if (posicion <= 0 || posicion > cantidad)
            {
                Console.WriteLine("Posición inválida.");
                Pausa();
                return;
            }

            int indice = posicion - 1;

            for (int i = indice; i < cantidad - 1; i++)
            {
                historialtemperatura[i] = historialtemperatura[i + 1];
                historialFechas[i] = historialFechas[i + 1];
            }

            cantidad--;

            GestionArchivoTemperaturas.GuardarHistorial(historialtemperatura, historialFechas, cantidad);

            Console.WriteLine();
            Console.WriteLine("Temperatura eliminada correctamente.");

            Pausa();
        }

        public static void MostrarHistorial()
        {
            Console.Clear();

            if (cantidad == 0)
            {
                Console.WriteLine("No hay temperaturas registradas.");
                Pausa();
                return;
            }

            Console.WriteLine("ESTE ES EL HISTORIAL DE TEMPERATURAS DE LA BASE DE TEXTO");
            Console.WriteLine();
            Console.WriteLine("1. Ver por fecha (más reciente primero)");
            Console.WriteLine("2. Ordenar de menor a mayor (por temperatura)");
            Console.WriteLine("3. Ordenar de mayor a menor (por temperatura)");
            Console.Write("Seleccione una opción: ");

            string textoOpcion = Console.ReadLine();
            int opcionOrden;
            int.TryParse(textoOpcion, out opcionOrden);

            switch (opcionOrden)
            {
                case 2:
                    {
                        int auxTemperatura;
                        string auxFecha;
                        int limite = cantidad;

                        for (int i = 0; i < limite - 1; i++)
                        {
                            for (int j = 0; j < limite - 1 - i; j++)
                            {
                                if (historialtemperatura[j] > historialtemperatura[j + 1])
                                {
                                    auxTemperatura = historialtemperatura[j];
                                    historialtemperatura[j] = historialtemperatura[j + 1];
                                    historialtemperatura[j + 1] = auxTemperatura;

                                    auxFecha = historialFechas[j];
                                    historialFechas[j] = historialFechas[j + 1];
                                    historialFechas[j + 1] = auxFecha;
                                }
                            }
                        }
                    }
                    break;

                case 3:
                    {
                        int auxTemperatura;
                        string auxFecha;
                        int limite = cantidad;

                        for (int i = 0; i < limite - 1; i++)
                        {
                            for (int j = 0; j < limite - 1 - i; j++)
                            {
                                if (historialtemperatura[j] < historialtemperatura[j + 1])
                                {
                                    auxTemperatura = historialtemperatura[j];
                                    historialtemperatura[j] = historialtemperatura[j + 1];
                                    historialtemperatura[j + 1] = auxTemperatura;

                                    auxFecha = historialFechas[j];
                                    historialFechas[j] = historialFechas[j + 1];
                                    historialFechas[j + 1] = auxFecha;
                                }
                            }
                        }
                    }
                    break;

                default:
                    {
                        int limite = cantidad;
                        int auxTemperatura;
                        string auxFecha;

                        for (int i = 0; i < limite - 1; i++)
                        {
                            for (int j = 0; j < limite - 1 - i; j++)
                            {
                                DateTime fecha1;
                                DateTime fecha2;

                                DateTime.TryParse(historialFechas[j], out fecha1);
                                DateTime.TryParse(historialFechas[j + 1], out fecha2);

                                if (fecha1 < fecha2)
                                {
                                    auxTemperatura = historialtemperatura[j];
                                    historialtemperatura[j] = historialtemperatura[j + 1];
                                    historialtemperatura[j + 1] = auxTemperatura;

                                    auxFecha = historialFechas[j];
                                    historialFechas[j] = historialFechas[j + 1];
                                    historialFechas[j + 1] = auxFecha;
                                }
                            }
                        }
                    }
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Listado de temperaturas:");
            Console.WriteLine();

            for (int i = 0; i < cantidad; i++)
            {
                int temperatura = historialtemperatura[i];
                string estado = EvaluarEstado(temperatura);
                string fecha = historialFechas[i];

                Console.WriteLine((i + 1) + ". " +temperatura + "°C - " +estado + " - " +fecha);
            }

            Pausa();
        }

        public static void LimpiarHistorial()
        {
            Console.Clear();

            if (cantidad == 0)
            {
                Console.WriteLine("El historial ya está vacío.");
                Pausa();
                return;
            }

            Console.Write("¿Está seguro de limpiar todo el historial? (si/no): ");
            string respuesta = Console.ReadLine();

            if (respuesta.ToLower() == "si")
            {
                cantidad = 0;

                GestionArchivoTemperaturas.GuardarHistorial(historialtemperatura, historialFechas, cantidad);

                Console.WriteLine();
                Console.WriteLine("Historial limpiado correctamente.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Operación cancelada.");
            }

            Pausa();
        }

        public static void ConfigurarIndices()
        {
            Console.Clear();

            Console.WriteLine("Límite actual de ALERTA : " + limiteAlerta + "°C");
            Console.WriteLine("Límite actual de PELIGRO: " + limitePeligro + "°C");
            Console.WriteLine();

            Console.Write("Ingrese nuevo límite de ALERTA: ");
            string textoAlerta = Console.ReadLine();
            int nuevoLimiteAlerta;
            int.TryParse(textoAlerta, out nuevoLimiteAlerta);

            if (nuevoLimiteAlerta == 0 && textoAlerta != "0")
            {
                Console.WriteLine("Valor no válido.");
                Pausa();
                return;
            }

            Console.Write("Ingrese nuevo límite de PELIGRO: ");
            string textoPeligro = Console.ReadLine();
            int nuevoLimitePeligro;
            int.TryParse(textoPeligro, out nuevoLimitePeligro);

            if (nuevoLimitePeligro == 0 && textoPeligro != "0")
            {
                Console.WriteLine("Valor no válido.");
                Pausa();
                return;
            }

            limiteAlerta = nuevoLimiteAlerta;
            limitePeligro = nuevoLimitePeligro;

            GestionLimiteTemperatura.GuardarLimites(limiteAlerta, limitePeligro);

            Console.WriteLine();
            Console.WriteLine("Índices actualizados y guardados en limites.txt.");
            Pausa();
        }

        public static void BuscarTemperatura()
        {
            Console.Clear();

            if (cantidad == 0)
            {
                Console.WriteLine("No hay temperaturas registradas.");
                Pausa();
                return;
            }

            Console.WriteLine("SE MUESTRA EL HISOTORIAL DE LA BASE DE TEXTO DONDE BUSCA TEMPERATURA");
            Console.WriteLine("1. Buscar exacta");
            Console.WriteLine("2. Buscar por rango");
            Console.WriteLine("3. Buscar por estado");
            Console.Write("Seleccione una opción: ");

            string textoOpcion = Console.ReadLine();
            int opcionBusqueda;
            int.TryParse(textoOpcion, out opcionBusqueda);

            switch (opcionBusqueda)
            {
                case 1:
                    BuscarPorTemperaturaExacta();
                    break;
                case 2:
                    BuscarPorRango();
                    break;
                case 3:
                    BuscarPorEstado();
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Pausa();
                    break;
            }
        }

        public static void BuscarPorTemperaturaExacta()
        {
            Console.Clear();
            Console.Write("Ingrese la temperatura: ");

            string textoTemperatura = Console.ReadLine();
            int temperaturaBuscada;
            int.TryParse(textoTemperatura, out temperaturaBuscada);

            if (temperaturaBuscada == 0 && textoTemperatura != "0")
            {
                Console.WriteLine("Valor no válido.");
                Pausa();
                return;
            }

            bool encontrado = false;

            Console.WriteLine();
            for (int i = 0; i < cantidad; i++)
            {
                if (historialtemperatura[i] == temperaturaBuscada)
                {
                    Console.WriteLine("Posición " + (i + 1) +": " + historialtemperatura[i] + "°C" +" - " + historialFechas[i]);
                    encontrado = true;
                }
            }

            if (encontrado == false)
            {
                Console.WriteLine("No se encontraron temperaturas con ese valor.");
            }

            Pausa();
        }

        public static void BuscarPorRango()
        {
            Console.Clear();
            Console.Write("Ingrese límite inferior: ");
            string textoMinimo = Console.ReadLine();
            int minimo;
            int.TryParse(textoMinimo, out minimo);

            if (minimo == 0 && textoMinimo != "0")
            {
                Console.WriteLine("Valor no válido.");
                Pausa();
                return;
            }

            Console.Write("Ingrese límite superior: ");
            string textoMaximo = Console.ReadLine();
            int maximo;
            int.TryParse(textoMaximo, out maximo);

            if (maximo == 0 && textoMaximo != "0")
            {
                Console.WriteLine("Valor no válido.");
                Pausa();
                return;
            }

            if (maximo < minimo)
            {
                Console.WriteLine("Error: el máximo no puede ser menor que el mínimo.");
                Pausa();
                return;
            }

            bool encontrado = false;

            Console.WriteLine();
            for (int i = 0; i < cantidad; i++)
            {
                int temperatura = historialtemperatura[i];
                if (temperatura >= minimo && temperatura <= maximo)
                {
                    Console.WriteLine(
                        "Posición " + (i + 1) +": " + temperatura + "°C" +" - " + historialFechas[i]);
                    encontrado = true;
                }
            }

            if (encontrado == false)
            {
                Console.WriteLine("No hay temperaturas en ese rango.");
            }

            Pausa();
        }

        public static void BuscarPorEstado()
        {
            Console.Clear();
            Console.WriteLine("Estados: NORMAL, ALERTA, PELIGRO");
            Console.Write("Ingrese estado: ");

            string estadoBuscado = Console.ReadLine().ToUpper();

            if (estadoBuscado != "NORMAL" && estadoBuscado != "ALERTA" && estadoBuscado != "PELIGRO")
            {
                Console.WriteLine("Estado inválido.");
                Pausa();
                return;
            }

            bool encontrado = false;

            Console.WriteLine();
            for (int i = 0; i < cantidad; i++)
            {
                string estado = EvaluarEstado(historialtemperatura[i]);

                if (estado == estadoBuscado)
                {
                    Console.WriteLine("Posición " + (i + 1) +": " + historialtemperatura[i] + "°C" +" - " + historialFechas[i]);
                    encontrado = true;
                }
            }

            if (encontrado == false)
            {
                Console.WriteLine("No hay temperaturas con ese estado.");
            }

            Pausa();
        }

    }
}

