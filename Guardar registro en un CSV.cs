//En C# Crear una Aplicacion que permita almacenar los datos de las mascotas que estan en una veterinaria la misma de debe:

//Tener un Menu.
//Guardar en un CSV los datos de las Mascotas
//Se debe almacenar: Nombre,Raza,Fecha Denacimiento,Genero.


using System;
using System.Collections.Generic;
using System.IO;

public class Mascota
{
    public string Nombre { get; set; }
    public string Raza { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public char Genero { get; set; }

    public Mascota(string nombre, string raza, DateTime fechaNacimiento, char genero)
    {
        Nombre = nombre;
        Raza = raza;
        FechaNacimiento = fechaNacimiento;
        Genero = genero;
    }

    public override string ToString()
    {
        return $"Nombre: {Nombre}, Raza: {Raza}, Fecha de Nacimiento: {FechaNacimiento.ToShortDateString()}, Género: {Genero}";
    }
}

public class VeterinariaApp
{
    private List<Mascota> mascotas;
    private string archivoCSV = "mascotas.csv";

    public VeterinariaApp()
    {
        mascotas = new List<Mascota>();
        CargarDatosDesdeCSV();
    }

    public void MostrarMenu()
    {
        while (true)
        {
            Console.WriteLine("------ Menú ------");
            Console.WriteLine("1. Agregar mascota");
            Console.WriteLine("2. Mostrar mascotas");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("Ingrese el nombre de la mascota: ");
                string nombre = Console.ReadLine();
                Console.Write("Ingrese la raza de la mascota: ");
                string raza = Console.ReadLine();
                Console.Write("Ingrese la fecha de nacimiento de la mascota (dd/mm/aaaa): ");
                DateTime fechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                Console.Write("Ingrese el género de la mascota (M/F): ");
                char genero = char.Parse(Console.ReadLine());

                Mascota mascota = new Mascota(nombre, raza, fechaNacimiento, genero);
                mascotas.Add(mascota);
                Console.WriteLine("Mascota agregada correctamente.\n");
            }
            else if (opcion == "2")
            {
                MostrarMascotas();
            }
            else if (opcion == "3")
            {
                GuardarDatosEnCSV();
                Console.WriteLine("Saliendo del programa...");
                break;
            }
            else
            {
                Console.WriteLine("Opción inválida. Intente nuevamente.\n");
            }
        }
    }

    private void MostrarMascotas()
    {
        if (mascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.\n");
            return;
        }

        Console.WriteLine("------ Mascotas Registradas ------");
        foreach (var mascota in mascotas)
        {
            Console.WriteLine(mascota.ToString());
        }
        Console.WriteLine();
    }

    private void CargarDatosDesdeCSV()
    {
        if (File.Exists(archivoCSV))
        {
            using (StreamReader reader = new StreamReader(archivoCSV))
            {
                while (!reader.EndOfStream)
                {
                    string[] datos = reader.ReadLine().Split(',');
                    string nombre = datos[0];
                    string raza = datos[1];
                    DateTime fechaNacimiento = DateTime.ParseExact(datos[2], "dd/MM/yyyy", null);
                    char genero = char.Parse(datos[3]);
                    Mascota mascota = new Mascota(nombre, raza, fechaNacimiento, genero);
                    mascotas.Add(mascota);
                }
            }
        }
    }

    private void GuardarDatosEnCSV()
    {
        using (StreamWriter writer = new StreamWriter(archivoCSV))
        {
            foreach (var mascota in mascotas)
            {
                writer.WriteLine($"{mascota.Nombre},{mascota.Raza},{mascota.FechaNacimiento.ToString("dd/MM/yyyy")},{mascota.Genero}");
            }
        }
        Console.WriteLine("Datos guardados en el archivo CSV.\n");
    }
}

public class Program
{
    static void Main(string[] args)
    {
        VeterinariaApp app = new VeterinariaApp();
        app.MostrarMenu();
    }
}
