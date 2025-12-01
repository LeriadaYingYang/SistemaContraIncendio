using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaContraIncendios;

namespace sistemacontraincendios
{

    internal class Program
    {
        static void Main(string[] args)
        {
            OperacionesAlarma.CargarHistorialDesdeArchivo();
            OperacionesAlarma.CargarLimitesDesdeArchivo();
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("  SISTEMA DE CONSOLA DE ALARMA CONTRA INCENDIOS ");
                Console.WriteLine("-----------------INTEGRANTES--------------------");
                Console.WriteLine("1. Daniel Enrique Jara Alva - N00243181");
                Console.WriteLine("2. Quiroz Cabanillas Franco Yaren - N00500542");
                Console.WriteLine("3. Pablo Diaz Tello  - N00483823");
                Console.WriteLine("4. Pompa Culqui Jhordan Jesús  - N00490707");
                Console.WriteLine($"La fecha y hora es :{ DateTime.Now.AddHours(0)}");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("-------------MENU DE CONSOLA-------------------");
                Console.WriteLine("1. Registrar temperatura manual");
                Console.WriteLine("2. Generar temperatura aleatoria");
                Console.WriteLine("3. Reemplazar temperatura en posición");
                Console.WriteLine("4. Eliminar temperatura en posición");
                Console.WriteLine("5. Mostrar historial de temperatura");
                Console.WriteLine("6. Limpiar historial");
                Console.WriteLine("7. Configurar índices de advertencia");
                Console.WriteLine("8. Buscar temperatura");
                Console.WriteLine("9. Salir");
                Console.Write("Seleccione una opción: ");

                string texto = Console.ReadLine();
                int.TryParse(texto, out opcion);

                if (opcion == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Opción no válida. Debe ingresar un número.");
                    OperacionesAlarma.Pausa();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        OperacionesAlarma.RegistrarTemperaturaManual();
                        break;
                    case 2:
                        OperacionesAlarma.GenerarTemperaturaAleatoria();
                        break;
                    case 3:
                        OperacionesAlarma.ReemplazarTemperatura();
                        break;
                    case 4:
                        OperacionesAlarma.EliminarTemperatura();
                        break;
                    case 5:
                        OperacionesAlarma.MostrarHistorial();
                        break;
                    case 6:
                        OperacionesAlarma.LimpiarHistorial();
                        break;
                    case 7:
                        OperacionesAlarma.ConfigurarIndices();
                        break;
                    case 8:
                        OperacionesAlarma.BuscarTemperatura();
                        break;
                    case 9:
                        Console.WriteLine("Saliendo del sistema de consola");
                        OperacionesAlarma.Pausa();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Selecciono una opción fuera de rango. Intente nuevamente.");
                        OperacionesAlarma.Pausa();

                        break;
                }

            } while (opcion != 9);
        }
    }

}
