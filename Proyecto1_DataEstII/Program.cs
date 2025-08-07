using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

class Program
{
    ManejoProfesiones profesiones = new ManejoProfesiones();
    public static void Main(string[] args)
    {
        Menu();
    }
    public static void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n Menú \n");
            Console.WriteLine("1.)");
            Console.WriteLine("2.)");
            Console.WriteLine("3.)");
            Console.WriteLine("4.)");
            Console.Write("Que desea hacer: ");
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
                break;
            case "3":
                break;
            case "4":
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
        Console.WriteLine("\n Registro de Profesionales");
        Console.Write("Que desea hacer: ");
        string Nombre = Console.ReadLine();
    }
}
