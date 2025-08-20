using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;

class Program
{
    static BTree ArbolB = new BTree(3); // Orden del árbol B
    static List<Proveedor> listaLineal = new List<Proveedor>();
    static string rutaArchivo = Path.Combine(Environment.GetFolderPath
        (Environment.SpecialFolder.Desktop), "arbolB.json");
    public static void Main(string[] args)
    {
        CargarArbol();
        Menu();
    }
    public static void Menu()
    {
        while (true)
        {
<<<<<<< Updated upstream
            Console.WriteLine("\n == Menu Principal == ");
=======
            Console.WriteLine("\n == Menu Principal de MultiServicios == ");
>>>>>>> Stashed changes
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
                Comparativa();
                break;
            case "5":
                GuardarArbol();
                Console.WriteLine("Gracias por usar el programa!!!");
                Console.WriteLine("Proyecto realizado por:");
<<<<<<< Updated upstream
                Console.WriteLine("Andrés Alejandro Mazariegos López - 1535724");
                Console.WriteLine("Mario André Velazco Gonzales - 1546124");
                Console.WriteLine("Edgar Eduardo Rodas López - 1629924");
=======
                Console.WriteLine("Andres Mazariegos - 1535724");
>>>>>>> Stashed changes
                return;
            default:
                break;
        }
    }
    public static void RegistroProfesional()
    {
        Console.WriteLine("Se le pediran sus datos personales a continuanción");
        int ID  = GenerarID();
        Console.Write($"ID: {ID} ");
        Console.Write("Nombre: ");
        string Nombre = Console.ReadLine();
        Console.Write("Servicio: ");
        string Servicio = Console.ReadLine();
        Console.Write("Calificación (1-5): ");
        int Calificacion = int.Parse(Console.ReadLine());

        Proveedor p = new Proveedor(ID, Nombre, Servicio, Calificacion);
        ArbolB.Insertar(p);
        listaLineal.Add(p);
    }

    public static void BusquedaPorServicio()
    {
        Console.Write("Servicio a buscar: ");
        string servicio = Console.ReadLine();
        var encontrados = ArbolB.BuscarPorServicio(servicio);
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
        ArbolB.MostrarOrdenado();
    }
    public static void Comparativa()
    {
        Console.Write("Servicio a buscar: ");
        string servicio = Console.ReadLine();

        var inicioLineal = DateTime.Now;
        var resLineal = listaLineal.FindAll(p => p.Servicio.Equals(servicio, StringComparison.OrdinalIgnoreCase));
        var tiempoLineal = (DateTime.Now - inicioLineal).TotalMilliseconds;

        var inicioB = DateTime.Now;
        var resB = ArbolB.BuscarPorServicio(servicio);
        var tiempoB = (DateTime.Now - inicioB).TotalMilliseconds;

        Console.WriteLine($"Lineal: {resLineal.Count} resultados en {tiempoLineal} ms");
        Console.WriteLine($"Árbol B: {resB.Count} resultados en {tiempoB} ms");
    }
    public static void GuardarArbol()
    {
        string json = JsonSerializer.Serialize(listaLineal);
        File.WriteAllText(rutaArchivo, json);
    }

    public static void CargarArbol()
    {
        if (!File.Exists(rutaArchivo))
            return;

        string json = File.ReadAllText(rutaArchivo);
        listaLineal = JsonSerializer.Deserialize<List<Proveedor>>(json);

        ArbolB = new BTree(3);
        foreach (var p in listaLineal)
        {
            ArbolB.Insertar(p);
        }
    }
    public static void PresioneTeclaSalir()
    {
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.Clear();
    }
    public static int GenerarID()
    {
        Random rnd = new Random();
        int id;
        do
        {
            id = rnd.Next(1, 100000);
        } while (listaLineal.Any(p => p.ID == id));
        return id;
    }
}
