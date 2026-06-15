/* V13: Agregar función FinalizarCompra() para trasladar elementos del carrito al registro histórico de ventas y vaciar el carrito después de finalizar la compra.
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
        
        // MOSTRAR INVENTARIO(mostrar inventario al Administrador)
        static void MostrarInventario()
        {
            var lista = LeerInventario();

            Console.WriteLine("\n--- INVENTARIO COMPLETO (ADMIN) ---");
            Console.WriteLine($"{"Nombre",-20}{"Costo",-15}{"Venta",-15}");
            Console.WriteLine(new string('-', 50));
            foreach (var i in lista)
            {
                Console.WriteLine($"{i[0],-20} {i[1],-15} {i[2],-15}");
            }
        }

        // MOSTRAR CATÁLOGO(Para mostrar el catálogo al cliente)
        static void MostrarCatalogo()
        {
            var lista = LeerInventario();

            Console.WriteLine("\n--- CATÁLOGO ---");
            string[] columnas = {"Nombre", "Precio"};
            Console.WriteLine($"{columnas[0],-20}{columnas[1],-15}");
            Console.WriteLine(new string('-', 35));
            foreach (var i in lista)
            {
                Console.WriteLine($"{i[0],-20}{i[2],-15}");
            }
        }

        // AGREGAR PRODUCTO(Agregar producto al Inventario)
        static void AgregarInventario()
        {
            var lista = LeerInventario();

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Precio Costo: ");
            string costo = Console.ReadLine();

            Console.Write("Precio Venta: ");
            string venta = Console.ReadLine();

            lista.Add(new string[] { nombre, costo, venta });

            GuardarInventario(lista);

            Console.WriteLine("Producto Agregado exitosamente.");
        }

        // MODIFICAR PRODUCTO(modificar algún producto en el inventario)
        static void ModificarInventario()
        {
            var lista = LeerInventario();

            Console.Write("Ingrese nombre del producto a modificar: ");
            string nombre = Console.ReadLine();

            bool encontrado = false;

            foreach (var i in lista)
            {
                if (i[0].Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write($"Nuevo precio de costo (Actual: {i[1]}):");
                    i[1] = Console.ReadLine();

                    Console.Write($"Nuevo precio de venta (Actual: {i[2]}): ");
                    i[2] = Console.ReadLine();

                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                GuardarInventario(lista);
                Console.WriteLine("Producto modificado correctamente.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        // ELIMINAR PRODUCTO(Eliminar un producto por completo del inventario)
        static void EliminarInventario()
        {
            var lista = LeerInventario();

            Console.Write("Ingrese el nombre del producto que desea eliminar: ");
            string nombreEliminar = Console.ReadLine();

            bool encontrado = false;

            // Recorremos la lista de atrás hacia adelante para evitar errores de índice al eliminar
            for (int i = lista.Count - 1; i >= 0; i--)
            {
                if (lista[i][0].Equals(nombreEliminar, StringComparison.OrdinalIgnoreCase))
                {
                    lista.RemoveAt(i);
                    encontrado = true;
                    break; // Suponiendo que los nombres son únicos, salimos del ciclo
                }
            }

            if (encontrado)
            {
                GuardarInventario(lista);
                Console.WriteLine("Producto eliminado exitosamente del inventario.");
            }
            else
            {
                Console.WriteLine("El producto no fue encontrado.");
            }
        }

        // BUSCAR PRODUCTO(Buscar un producto en específico)
        static void BuscarInventario()
        {
            var lista = LeerInventario();

            Console.Write("Buscar nombre del producto: ");
            string buscar = Console.ReadLine().ToLower();
            bool coincidencia = false;

            foreach (var i in lista)
            {
                if (i[0].ToLower().Contains(buscar))
                {
                    coincidencia = true;
                    Console.WriteLine($"\n[Encontrado]: {i[0]} - Precio: ${i[2]}");
                    string nombreProducto = i[0];
                    double precioVenta = Convert.ToDouble(i[2]);

                    Console.Write("Quiere agregar este producto al carrito?(si/no): ");
                    string respuesta = Console.ReadLine().ToLower();
                    if (respuesta == "si" || respuesta == "s")
                    {
                        Console.Write("\nQue cantidad quiere?: ");
                        int cantidad = Convert.ToInt32(Console.ReadLine());
                        AgregarCarrito(cantidad, nombreProducto, precioVenta);
                        break;
                    }
                        
                }
                

            }
            if (!coincidencia) Console.WriteLine("No se encontraron productos con ese criterio.");
        }

        //-----------------------------------------------------------------------------------CSV carrito
        // LEER CSV Carrito
        static List<string[]> LeerCarrito()
        {
            List<string[]> lista = new List<string[]>();

            if (!File.Exists(archivoCsv2))
            {
                File.WriteAllLines(archivoCsv2, new string[] { "Cantidad,Nombre,Precio,TotalProducto" });
                return lista;
            }

            string[] lineas = File.ReadAllLines(archivoCsv2);

            for (int i = 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i])) continue;
                string[] datos = lineas[i].Split(',');

                if (datos.Length == 4)
                    lista.Add(datos);
            }

            return lista;
        }

        // GUARDAR CSV Carrito
        static void GuardarCarrito(List<string[]> lista)
        {
            List<string> lineas = new List<string>();
            lineas.Add("Cantidad,Nombre,Precio,TotalProducto");

            foreach (var i in lista)
            {
                lineas.Add($"{i[0]},{i[1]},{i[2]},{i[3]}");
            }

            File.WriteAllLines(archivoCsv2, lineas);
        }

        // MOSTRAR CSV Carrito
        static void MostrarCarrito()
        {
            var lista = LeerCarrito();
            double acumulador = 0;

            Console.WriteLine("\n--- CARRITO DE COMPRAS ---");
            Console.WriteLine($"{"Cantidad",-12}{"Nombre",-20}{"Precio Unit.",-15}{"Subtotal",-15}");
            Console.WriteLine(new string('-', 62));
            foreach (var i in lista)
            {
                Console.WriteLine($"{i[0],-12}{i[1],-20}${i[2],-15}${i[3],-15}");
                acumulador += Convert.ToDouble(i[3]);
            }
            Console.WriteLine(new string('-', 62));
            Console.WriteLine($"\t\t\t\tTotal a pagar: ${acumulador}\n");
        }

        //FINALIZAR COMPRA
        static void FinalizarCompra()
        {
            var carrito = LeerCarrito();
            if (carrito.Count == 0)
            {
                Console.WriteLine("El carrito está vacío.");
                return;
            }

            Console.Write("¿Desea finalizar la compra? (si/no): ");
            string finalizar = Console.ReadLine().ToLower();

            if (finalizar == "si" || finalizar == "s")
            {
                // Trasladar elementos del carrito al registro histórico de ventas
                string numeroCompra = DateTime.Now.ToString("yyyyMMddHHmmss"); // Genera un ID único basado en tiempo
                var registro = LeerRegistro();

                foreach (var item in carrito)
                {
                    // Formato Registro: NumeroCompra, Cantidad, Nombre, Precio, TotalProducto
                    registro.Add(new string[] { numeroCompra, item[0], item[1], item[2], item[3] });
                }

                GuardarRegistro(registro);

                // Vaciar Carrito de compras
                File.WriteAllLines(archivoCsv2, new string[] { "Cantidad,Nombre,Precio,TotalProducto" });

                Console.WriteLine("¡Gracias por su compra! ;) Su orden ha sido registrada.");
            }
        }

    }
}