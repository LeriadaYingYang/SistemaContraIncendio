using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemacontraincendios
{

    internal class Program
    {
        static void Main(string[] args)
        {
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
                Console.WriteLine("------------------------------------------------");

                Console.WriteLine("1. Registrar temperatura manual");
                Console.WriteLine("2. Generar temperatura aleatoria");
                Console.WriteLine("3. Reemplazar temperatura en posición");
                Console.WriteLine("4. Eliminar temperatura en posición");
                Console.WriteLine("5. Mostrar historial de temperatura");
                Console.WriteLine("6. Limpiar historial");
                Console.WriteLine("7. Configurar índices de advertencia");
                Console.WriteLine("8. Activar o Desactivar Alarma de Emergencia");
                Console.WriteLine("9. Buscar temperatura");
                Console.WriteLine("10. Salir");
                Console.Write("Seleccione una opción: ");

                string texto = Console.ReadLine();
                int.TryParse(texto, out opcion);

                if (opcion == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Opción no válida. Debe ingresar un número.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:

                        break;
                    default:

                        break;
                }

            } while (opcion != 10);
        }
    }

}
