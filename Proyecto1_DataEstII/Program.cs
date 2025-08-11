using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

class Program
{
    ArbolB arbol = new ArbolB(3); // Orden del árbol B
    List<Proveedor> listaLineal = new List<Proveedor>();

    public static void Main(string[] args)
    {
        Menu();
    }
    public static void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n == Menu Principal de UBER == ");
            Console.WriteLine("Eliga la opcion que desea realizar");
            Console.WriteLine("1. Registrar proveedor");
            Console.WriteLine("2. Buscar por servicio");
            Console.WriteLine("3. Mostrar proveedores ordenados");
            Console.WriteLine("4. Comparar búsqueda lineal vs. Árbol B");
            Console.WriteLine("5. Salir");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Proveedor p = new Proveedor();
                Console.Write("ID: ");
                p.ID = int.Parse(Console.ReadLine());
                Console.Write("Nombre: ");
                p.Nombre = Console.ReadLine();
                Console.Write("Servicio: ");
                p.Servicio = Console.ReadLine();
                Console.Write("Calificación (1-5): ");
                p.Calificacion = int.Parse(Console.ReadLine());

                arbol.Insertar(p);
                listaLineal.Add(p);
            }
            else if (opcion == "2")
            {
                Console.Write("Servicio a buscar: ");
                string servicio = Console.ReadLine();
                var encontrados = arbol.BuscarPorServicio(servicio);
                if (encontrados.Count > 0)
                {
                    foreach (var prov in encontrados)
                        Console.WriteLine(prov);
                }
                else
                {
                    Console.WriteLine("No se encontraron proveedores.");
                }
            }
            else if (opcion == "3")
            {
                arbol.MostrarOrdenado();
            }
            else if (opcion == "4")
            {
                Console.Write("Servicio a buscar: ");
                string servicio = Console.ReadLine();

                var inicioLineal = DateTime.Now;
                var resLineal = listaLineal.FindAll(p => p.Servicio.Equals(servicio, StringComparison.OrdinalIgnoreCase));
                var tiempoLineal = (DateTime.Now - inicioLineal).TotalMilliseconds;

                var inicioB = DateTime.Now;
                var resB = arbol.BuscarPorServicio(servicio);
                var tiempoB = (DateTime.Now - inicioB).TotalMilliseconds;

                Console.WriteLine($"Lineal: {resLineal.Count} resultados en {tiempoLineal} ms");
                Console.WriteLine($"Árbol B: {resB.Count} resultados en {tiempoB} ms");
            }
            else if (opcion == "5")
            {
                break;
            }
        }
    }
}
