using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

class Program
{
    static BTree arbol = new BTree(3); // Orden del árbol B
    static List<Proveedor> listaLineal = new List<Proveedor>();
    ManejoProfesiones profesiones = new ManejoProfesiones();
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
            Console.WriteLine("\n == Menu Principal de UBER == ");
            Console.WriteLine("Eliga la opcion que desea realizar");
            Console.WriteLine("1. Registrar proveedor");
            Console.WriteLine("2. Buscar por servicio");
            Console.WriteLine("3. Mostrar proveedores ordenados");
            Console.WriteLine("4. Comparar búsqueda lineal vs. Árbol B");
            Console.WriteLine("5. Salir");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();
            ManejoMEnu(opcion);

        }
    }
    public static void ManejoMEnu(string opcion)
    {

        switch (opcion)
        {
            case "1":
                RegistroProfesional();
                break;
            case "2":
                BusquedaPorServicio();
                break;
            case "3":
                MostrarPorvvedores();
                break;
            case "4":

                break;
            case "5":
                Console.WriteLine("Gracias por usar el programa!!!");
                Console.WriteLine("Proyecto realizado por:");
                Console.WriteLine("");
                return;
            default:
                break;
        }
    }
    public static void RegistroProfesional()
    {
        Console.Write("ID: ");
        int ID = int.Parse(Console.ReadLine());
        Console.Write("Nombre: ");
        string Nombre = Console.ReadLine();
        Console.Write("Servicio: ");
        string Servicio = Console.ReadLine();
        Console.Write("Calificación (1-5): ");
        int Calificacion = int.Parse(Console.ReadLine());

        Proveedor p = new Proveedor(ID, Nombre, Servicio, Calificacion);
        arbol.Insertar(p);
        listaLineal.Add(p);
    }

    public static void BusquedaPorServicio()
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

    public static void MostrarPorvvedores()
    {
        Console.WriteLine("Se mostrara a todos los proveedores en orden de mayor a menor calificación");
        arbol.MostrarOrdenado();
    }
    public static void Comparativa()
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
}
