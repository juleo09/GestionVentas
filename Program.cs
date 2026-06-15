/* V3: Agregar función GuardarInventario() para guardar cambios en el inventario al archivo CSV
*/
using System;
using System.Collections.Generic;
using System.IO;

namespace CodigoBase
{
    internal class Program
    {
        //Declaración de variables globales para los archivos y la opción del menú
        static string archivoCsv = "inventario.csv";
        static string archivoBin = "inventario.bin";
        static string archivoCsv2 = "carrito.csv";
        static string archivoCsv3 = "registro.csv";
        static int opcion;
        static void Main(string[] args)
        { 
        
            
            // Menú principal del programa
            do
            {
                try// Manejo de excepciones para la entrada del menú principal
                {
                    Console.Clear();
                    Console.WriteLine("\n===== Gestion de Ventas =====");
                    Console.WriteLine("1) Administrador");
                    Console.WriteLine("2) Usuario");
                    Console.WriteLine("0) Salir");
                    Console.Write("Ingrese Opción: ");
                    opcion = Convert.ToInt32(Console.ReadLine());


                    switch (opcion)
                    {
                        case 1:// Interfaz de administrador
                            Console.Clear();
                            Administrador(); break;


                        case 2:// Interfaz de Usuario
                            Console.Clear();
                            Usuario(); break;

                    }
                    Console.ReadKey();
                }
                catch(FormatException)
                {
                    Console.WriteLine("Por favor, ingrese una opción válida.");
                    Console.ReadKey();
                }
                
            } while (opcion != 0);

            
        }

        // Función para la interfaz de administrador, con opciones para gestionar el inventario y ver el registro de ventas
        static void Administrador()
        {
            int o;
            do
            {                
                Console.Clear();
                Console.WriteLine("\n===== Panel Administrador =====");
                Console.WriteLine("1) Ver Base de datos (CSV)");
                Console.WriteLine("2) Agregar Producto");
                Console.WriteLine("3) Modificar Producto");
                Console.WriteLine("4) Eliminar Producto");
                Console.WriteLine("5) Ver total de productos del inventario");
                Console.WriteLine("6) Respaldar binario");
                Console.WriteLine("7) Leer Respaldo Binario");
                Console.WriteLine("8) Ver Registro histórico de Ventas");
                Console.WriteLine("0) Volver al menú principal");
                Console.Write("Seleccione una Opción: ");
                o = Convert.ToInt32(Console.ReadLine());
                
                switch (o)
                {
                    case 1:
                        Console.Clear();
                        MostrarInventario();
                        break;
                    case 2:
                        Console.Clear();
                        AgregarInventario();
                        break;
                    case 3:
                        Console.Clear();
                        ModificarInventario();
                        break;
                    case 4:
                        Console.Clear();
                        EliminarInventario();
                        break;
                    case 5:
                        Console.Clear();
                        Contar();
                        break;
                    case 6:
                        Console.Clear();
                        GuardarBinario();
                        break;
                    case 7:
                        Console.Clear();
                        LeerBinario();
                        break;
                    case 8:
                        Console.Clear();
                        MostrarRegistro();
                        break;

                }
                if(o != 0) Console.ReadKey();
            } while (o != 0);
            
        }

        // Función para la interfaz de usuario, con opciones para buscar productos, ver el catálogo, gestionar el carrito y finalizar compras
        static void Usuario()
        {
            int o;
            do
            {
                Console.Clear();
                Console.WriteLine("\n===== Panel de Usuario =====");
                Console.WriteLine("1) Buscar e incorporar al carrito");
                Console.WriteLine("2) Ver catálogo de precios");
                Console.WriteLine("3) Ver carrito y finalizar compra");
                Console.WriteLine("0) Volver al menú principal");
                Console.Write("Seleccione una Opción: ");
                o = Convert.ToInt32(Console.ReadLine());
                switch (o)
                {
                    case 1:
                        Console.Clear();
                        BuscarInventario();
                        break;
                    case 2:
                        Console.Clear();
                        MostrarCatalogo();
                        break;
                    case 3:
                        Console.Clear();
                        MostrarCarrito();
                        FinalizarCompra();                                           
                        break;

                }
                if (o != 0) Console.ReadKey();

            } while (o != 0);

        }
        //-----------------------------------------------------------------------------------CSV inventario
        // LEER CSV inventario
        static List<string[]> LeerInventario()
        {
            List<string[]> lista = new List<string[]>();

            if (!File.Exists(archivoCsv))
            {
                // Crear archivo con cabecera si no existe para evitar caídas
                File.WriteAllLines(archivoCsv, new string[] { "Nombre,Costo,Venta" });
                return lista;
            }

            string[] lineas = File.ReadAllLines(archivoCsv);

            for (int i = 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i])) continue;
                string[] datos = lineas[i].Split(',');

                if (datos.Length == 3)
                    lista.Add(datos);
            }

            return lista;
        }

        // GUARDAR CSV inventario
        static void GuardarInventario(List<string[]> lista)
        {
            List<string> lineas = new List<string>();
            lineas.Add("Nombre,Costo,Venta");

            foreach (var i in lista)
            {
                lineas.Add($"{i[0]},{i[1]},{i[2]}");
            }

            File.WriteAllLines(archivoCsv, lineas);
        }
        
    }
}