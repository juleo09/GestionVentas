/*V21: Agregar Stock en el inventario
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace CodigoBase
{
    internal class Program
    {
        static string archivoCsv = "inventario.csv";
        static string archivoBin = "inventario.bin";
        static string archivoCsv2 = "carrito.csv";
        static string archivoCsv3 = "registro.csv";
        static int opcion;
        static void Main(string[] args)
        { 
        
            
            
            do
            {
                Console.Clear();
                Console.WriteLine("\n===== Gestion de Ventas =====");
                Console.WriteLine("1) Administrador");
                Console.WriteLine("2) Usuario");
                Console.WriteLine("0) Salir");
                Console.Write("Ingrese una Opción: ");

                //Validamos directamente la entrada del teclado si el input del usuario es un número
                if(int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:Console.Clear(); Administrador(); break; // Interfaz de administrador
                        case 2:Console.Clear(); Usuario(); break; // Interfaz de Usuario
                        case 0:break;//Sale limpiamente
                        default:
                            // Captura números fuera del rango
                            Console.WriteLine("\nPor favor, ingrese una opción válida."); break;

                    }
                }
                else
                {
                    // Si introduce letras o símbolos, cae aquí directamente
                    Console.WriteLine("\nPor favor, ingrese una opción válida.");
                    opcion = -1;// Asignamos un valor cualquiera diferente de 0 para que el ciclo continúe
                }
                
                if (opcion != 0) Console.ReadKey();// Detiene la pantalla solo si el usuario no eligió salir (0)
            } while (opcion != 0);

            
        }

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
                Console.WriteLine("5) Rellenar Stock de un producto");
                Console.WriteLine("6) Ver total de productos del inventario");
                Console.WriteLine("7) Respaldar binario");
                Console.WriteLine("8) Leer Respaldo Binario");
                Console.WriteLine("9) Ver Registro histórico de Ventas");
                Console.WriteLine("0) Volver al menú principal");
                Console.Write("Seleccione una Opción: ");

                //Validamos directamente la entrada del teclado si el input del usuario es un número
                if (int.TryParse(Console.ReadLine(), out o))
                {
                    switch (o)
                    {
                        case 1: Console.Clear(); MostrarInventario(); break;
                        case 2: Console.Clear(); AgregarInventario(); break;
                        case 3: Console.Clear(); ModificarInventario(); break;
                        case 4: Console.Clear(); EliminarInventario(); break;
                        case 5: Console.Clear(); RellenarStock(); break;
                        case 6: Console.Clear(); Contar(); break;
                        case 7: Console.Clear(); GuardarBinario(); break;
                        case 8: Console.Clear(); LeerBinario(); break;
                        case 9: Console.Clear(); MostrarRegistro(); break;
                        case 0: break;//Sale limpiamente
                        default:
                            // Captura números fuera del rango
                            Console.WriteLine("\nPor favor, ingrese una opción válida."); break;
                    }
                }

                else
                {
                    // Si introduce letras o símbolos, cae aquí directamente
                    Console.WriteLine("\nPor favor, ingrese una opción válida.");
                    o = -1; // Asignamos un valor cualquiera diferente de 0 para que el ciclo continúe
                }

                if (o != 0) Console.ReadKey();// Detiene la pantalla solo si el usuario no eligió salir (0)
            } while (o != 0);
            
        }

        static void Usuario()
        {
            int o;
            do
            {
                Console.Clear();
                Console.WriteLine("\n===== Panel de Usuario =====");
                Console.WriteLine("1) Buscar e incorporar producto al carrito");
                Console.WriteLine("2) Ver catálogo de precios");
                Console.WriteLine("3) Ver carrito y finalizar compra");
                Console.WriteLine("0) Volver al menú principal");
                Console.Write("Seleccione una Opción: ");

                //Validamos directamente la entrada del teclado si el input del usuario es un número
                if (int.TryParse(Console.ReadLine(), out o))
                {
                    switch (o)
                    {
                        case 1: Console.Clear(); BuscarInventario(); break;
                        case 2: Console.Clear(); MostrarCatalogo(); break;
                        case 3: Console.Clear(); MostrarCarrito(); FinalizarCompra(); break;
                        case 0: break;
                        default:
                            // Captura números fuera del rango
                            Console.WriteLine("\nPor favor, ingrese una opción válida."); break;

                    }
                }

                else
                {
                    // Si introduce letras o símbolos, cae aquí directamente
                    Console.WriteLine("\nPor favor, ingrese una opción válida.");
                    o = -1;
                }

                if (o != 0) Console.ReadKey();// Detiene la pantalla solo si el usuario no eligió salir (0)

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
                File.WriteAllLines(archivoCsv, new string[] { "Nombre,Costo,Venta,Stock" });
                return lista;
            }

            string[] lineas = File.ReadAllLines(archivoCsv);

            for (int i = 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i])) continue;
                string[] datos = lineas[i].Split(',');

                if (datos.Length == 4)
                {
                    lista.Add(datos);
                }
            }

            return lista;
        }

        // GUARDAR CSV inventario
        static void GuardarInventario(List<string[]> lista)
        {
            List<string> lineas = new List<string>();
            lineas.Add("Nombre,Costo,Venta,Stock");

            foreach (var i in lista)
            {
                lineas.Add($"{i[0]},{i[1]},{i[2]},{i[3]}");
            }

            File.WriteAllLines(archivoCsv, lineas);
        }

        // MOSTRAR INVENTARIO(mostrar inventario al Administrador)
        static void MostrarInventario()
        {
            var lista = LeerInventario();

            Console.WriteLine("\n--- INVENTARIO COMPLETO (ADMIN) ---");
            Console.WriteLine($"{"Nombre",-42}{"Costo",-12}{"Venta",-12}{"Stock",-10}");
            Console.WriteLine(new string('-', 76));
            foreach (var i in lista)
            {
                Console.WriteLine($"{i[0],-42} {i[1],-11} {i[2],-11}{i[3],-10}");
            }
        }

        // MOSTRAR CATÁLOGO(Para mostrar el catálogo al cliente)
        static void MostrarCatalogo()
        {
            var lista = LeerInventario();

            Console.WriteLine("\n--- CATÁLOGO ---");
            string[] columnas = {"Nombre", "Precio"};
            Console.WriteLine($"{columnas[0],-42}{columnas[1],-12}");
            Console.WriteLine(new string('-', 66));
            foreach (var i in lista)
            {
                Console.WriteLine($"{i[0],-42}{i[2],-11}");
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

            Console.Write("Stock Inicial: ");
            string stock = Console.ReadLine();

            //Validación: Verificamos si alguno de los inputs está vacío o tiene solo espacios
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(costo) ||
                string.IsNullOrWhiteSpace(venta) ||
                string.IsNullOrWhiteSpace(stock))
            {
                Console.WriteLine("\n[ERROR]: Hubo un error al agregar un producto nuevo. Ningún campo puede quedar vacío");
                return;// Cancelamos el flujo y salimos de la función sin guardar nada
            }

            lista.Add(new string[] { nombre, costo, venta, stock });

            GuardarInventario(lista);

            Console.WriteLine("Producto Agregado exitosamente.");
        }

        // MODIFICAR PRODUCTO(modificar algún producto en el inventario)
        static void ModificarInventario()
        {
            var listaInventario = LeerInventario();

            Console.Write("Ingrese el nombre del producto a modificar (búsqueda parcial): ");
            string buscar = Console.ReadLine().ToLower();

            // Lista temporal para almacenar solo los productos que coinciden con la búsqueda
            List<string[]> coincidencias = new List<string[]>();

            foreach (var i in listaInventario)
            {
                if (i[0].ToLower().Contains(buscar))
                {
                    coincidencias.Add(i);
                }
            }

            // Si no se encontraron coincidencias
            if (coincidencias.Count == 0)
            {
                Console.WriteLine("No se encontraron productos con ese criterio.");
                return;
            }

            // Mostramos todas las coincidencias en forma de opciones numeradas
            Console.WriteLine($"\n--- Se encontraron {coincidencias.Count} coincidencias ---");
            for (int index = 0; index < coincidencias.Count; index++)
            {
                var prod = coincidencias[index];
                Console.WriteLine($"{index + 1}) {prod[0],-42} - Costo: ${prod[1],-8} | Venta: ${prod[2],-8} [Stock: {prod[3]}]");
            }

            Console.WriteLine(new string('-', 76));
            Console.Write("¿Cuál número de opción desea modificar?: ");
            int opcionSeleccionada;

            // Validamos que el usuario ingrese un número entero válido y dentro del rango
            if (int.TryParse(Console.ReadLine(), out opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= coincidencias.Count)
            {
                // Obtenemos el producto seleccionado (restamos 1 por el índice base 0)
                var productoElegido = coincidencias[opcionSeleccionada - 1];

                Console.WriteLine($"\nModificando: {productoElegido[0]}");

                Console.Write($"Nuevo precio de costo (Actual: {productoElegido[1]}): ");
                string nuevoCosto = Console.ReadLine();

                Console.Write($"Nuevo precio de venta (Actual: {productoElegido[2]}): ");
                string nuevaVenta = Console.ReadLine();

                // Validación de seguridad: si deja campos vacíos, cancelamos el cambio
                if (string.IsNullOrWhiteSpace(nuevoCosto) || string.IsNullOrWhiteSpace(nuevaVenta))
                {
                    Console.WriteLine("\n[ERROR]: Hubo un error al modificar el producto. Los precios no pueden quedar vacíos.");
                    return;
                }

                // Aplicamos los cambios directamente en el objeto referenciado de la lista original
                productoElegido[1] = nuevoCosto;
                productoElegido[2] = nuevaVenta;

                // Guardamos los cambios en el archivo CSV
                GuardarInventario(listaInventario);
                Console.WriteLine("\nProducto modificado correctamente.");
            }
            else
            {
                Console.WriteLine("\nOpción inválida. Volviendo al panel administrador.");
            }
        }

        //RELLENAR STOCK(Rellenar el stock en el inventario)
        static void RellenarStock()
        {
            var listaInventario = LeerInventario();

            Console.Write("Ingrese el nombre del producto al que desea añadir stock (búsqueda parcial): ");
            string buscar = Console.ReadLine().ToLower();

            // Lista temporal para almacenar solo los productos que coinciden con la búsqueda
            List<string[]> coincidencias = new List<string[]>();

            foreach (var i in listaInventario)
            {
                if (i[0].ToLower().Contains(buscar))
                {
                    coincidencias.Add(i);
                }
            }

            // Si no se encontraron coincidencias
            if (coincidencias.Count == 0)
            {
                Console.WriteLine("No se encontraron productos con ese criterio.");
                return;
            }

            // Mostramos todas las coincidencias en forma de opciones numeradas
            Console.WriteLine($"\n--- Se encontraron {coincidencias.Count} coincidencias ---");
            for (int index = 0; index < coincidencias.Count; index++)
            {
                var prod = coincidencias[index];
                Console.WriteLine($"{index + 1}) {prod[0],-42} - Stock Actual: {prod[3]}");
            }

            Console.WriteLine(new string('-', 60));
            Console.Write("¿Cuál número de opción desea rellenar?: ");
            int opcionSeleccionada;

            // Validamos la opción ingresada
            if (int.TryParse(Console.ReadLine(), out opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= coincidencias.Count)
            {
                // Obtenemos el producto seleccionado
                var productoElegido = coincidencias[opcionSeleccionada - 1];

                Console.WriteLine($"\nSeleccionado: {productoElegido[0]} | Stock Actual: {productoElegido[3]}");
                Console.Write("Cantidad de stock a añadir: ");

                int cantidadAnadir;
                // Validación para asegurar que digite un número válido al rellenar stock
                if (int.TryParse(Console.ReadLine(), out cantidadAnadir) && cantidadAnadir > 0)
                {
                    int stockActual = Convert.ToInt32(productoElegido[3]);
                    productoElegido[3] = (stockActual + cantidadAnadir).ToString();

                    // Guardamos las modificaciones en el archivo CSV
                    GuardarInventario(listaInventario);
                    Console.WriteLine("\nStock actualizado con éxito.");
                }
                else
                {
                    Console.WriteLine("\n[ERROR]: Cantidad de stock inválida. Debe ser un número entero mayor a 0.");
                }
            }
            else
            {
                Console.WriteLine("\nOpción inválida. Volviendo al panel administrador.");
            }
        }

        // ELIMINAR PRODUCTO(Eliminar un producto por completo del inventario)
        static void EliminarInventario()
        {
            var listaInventario = LeerInventario();

            Console.Write("Ingrese el nombre del producto que desea eliminar (búsqueda parcial): ");
            string buscar = Console.ReadLine().ToLower();

            // Lista temporal para almacenar solo los productos que coinciden con la búsqueda
            List<string[]> coincidencias = new List<string[]>();

            foreach (var i in listaInventario)
            {
                if (i[0].ToLower().Contains(buscar))
                {
                    coincidencias.Add(i);
                }
            }

            // Si no se encontraron coincidencias
            if (coincidencias.Count == 0)
            {
                Console.WriteLine("No se encontraron productos con ese criterio.");
                return;
            }

            // Mostramos todas las coincidencias en forma de opciones numeradas
            Console.WriteLine($"\n--- Se encontraron {coincidencias.Count} coincidencias ---");
            for (int index = 0; index < coincidencias.Count; index++)
            {
                var prod = coincidencias[index];
                Console.WriteLine($"{index + 1}) {prod[0],-42} - Costo: ${prod[1],-8} | Stock: {prod[3]}");
            }

            Console.WriteLine(new string('-', 65));
            Console.Write("¿Cuál número de opción desea ELIMINAR por completo?: ");
            int opcionSeleccionada;

            // Validamos la opción ingresada por el administrador
            if (int.TryParse(Console.ReadLine(), out opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= coincidencias.Count)
            {
                // Obtenemos el producto exacto que se seleccionó de la lista de coincidencias
                var productoAEliminar = coincidencias[opcionSeleccionada - 1];

                //Verificar si se desea eliminar el producto del inventario
                Console.WriteLine($"\n¡ADVERTENCIA!: Está a punto de eliminar definitivamente: {productoAEliminar[0]}");
                Console.Write("¿Está completamente seguro? (si/no): ");
                string confirmar = Console.ReadLine().ToLower();

                if (confirmar == "si" || confirmar == "s")
                {
                    // Removemos el producto seleccionado directamente de la lista principal de inventario
                    listaInventario.Remove(productoAEliminar);

                    // Guardamos el estado actualizado en el archivo CSV
                    GuardarInventario(listaInventario);
                    Console.WriteLine("\nProducto eliminado exitosamente del inventario.");
                }
                else
                {
                    Console.WriteLine("\nOperación cancelada. El producto no sufrió modificaciones.");
                }
            }
            else
            {
                Console.WriteLine("\nOpción inválida. Volviendo al panel administrador.");
            }
        }

        // BUSCAR PRODUCTO(Buscar un producto en específico)

        static void BuscarInventario()
        {
            var listaInventario = LeerInventario();

            Console.Write("Buscar nombre del producto: ");
            string buscar = Console.ReadLine().ToLower();

            // Lista temporal para almacenar solo los productos que coinciden con la búsqueda
            List<string[]> coincidencias = new List<string[]>();

            foreach (var i in listaInventario)
            {
                if (i[0].ToLower().Contains(buscar))
                {
                    coincidencias.Add(i);
                }
            }

            // Si no se encontraron coincidencias
            if (coincidencias.Count == 0)
            {
                Console.WriteLine("No se encontraron productos con ese criterio.");
                return; // Salimos de la función
            }

            // Si se encontraron coincidencias, las mostramos todas en forma de opciones numeradas
            Console.WriteLine($"\n--- Se encontraron {coincidencias.Count} coincidencias ---");
            for (int index = 0; index < coincidencias.Count; index++)
            {
                var prod = coincidencias[index];
                int stockDisp = Convert.ToInt32(prod[3]);
                string estadoStock = stockDisp > 0 ? $"Stock: {stockDisp}" : "SIN STOCK";

                Console.WriteLine($"{index + 1}) {prod[0],-42} - Precio: ${prod[2],-8} [{estadoStock}]");
            }

            Console.WriteLine(new string('-', 70));
            Console.Write("¿Quiere agregar alguna de estas opciones al carrito? (si/no): ");
            string respuesta = Console.ReadLine().ToLower();

            if (respuesta == "si" || respuesta == "s")
            {
                Console.Write("¿Cuál número de opción desea llevar?: ");
                int opcionSeleccionada;

                // Validamos que el usuario ingrese un número y que esté dentro del rango de opciones mostradas
                if (int.TryParse(Console.ReadLine(), out opcionSeleccionada) && opcionSeleccionada >= 1 && opcionSeleccionada <= coincidencias.Count)
                {
                    // Obtenemos el producto seleccionado (restamos 1 porque las listas empiezan en el índice 0)
                    var productoElegido = coincidencias[opcionSeleccionada - 1];
                    int stockDisponible = Convert.ToInt32(productoElegido[3]);

                    // Validamos si el producto tiene stock antes de proceder
                    if (stockDisponible <= 0)
                    {
                        Console.WriteLine("\nLo sentimos, este producto no cuenta con stock disponible por el momento.");
                        return;
                    }

                    Console.WriteLine($"\nSeleccionado: {productoElegido[0]}");
                    Console.Write("¿Qué cantidad quiere?: ");

                    //Validación de la cantidad ingresada
                    int cantidad;
                    if (int.TryParse(Console.ReadLine(), out cantidad) && cantidad > 0)
                    {
                        if (cantidad <= stockDisponible)
                        {
                            AgregarCarrito(cantidad, productoElegido[0], Convert.ToDouble(productoElegido[2]));//cantidad,nombre_producto,Precio_venta
                        }
                        else
                        {
                            Console.WriteLine($"\nError: No puedes agregar esa cantidad. El stock máximo disponible es: {stockDisponible}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n[ERROR]: Cantidad inválida. Debe ingresar un número entero mayor a 0.");
                    }
                }
                else
                {
                    Console.WriteLine("\nOpción inválida. Volviendo al menú.");
                }
            }
            else
            {
                // Si pone "no" o cualquier otra cosa, limpia o avisa que no se concretó la búsqueda
                Console.WriteLine("\nBúsqueda finalizada sin agregar productos.");
            }
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
            Console.WriteLine($"{"Cantidad",-12}{"Nombre",-42}{"Precio Unit.",-16}{"Subtotal",-14}");
            Console.WriteLine(new string('-', 84));
            foreach (var i in lista)
            {
                Console.WriteLine($"{i[0],-12}{i[1],-42}${i[2],-15}${i[3],-13}");
                acumulador += Convert.ToDouble(i[3]);
            }
            Console.WriteLine(new string('-', 84));
            Console.WriteLine($"{"", -54}Total a pagar: ${acumulador}\n");
        }

        // AGREGAR CSV Carrito
        static void AgregarCarrito(int cantidad, string nombreProducto, double precioVenta)
        {
            var lista = LeerCarrito();
            double totalProducto = cantidad * precioVenta;

            lista.Add(new string[] { cantidad.ToString(), nombreProducto, precioVenta.ToString(), totalProducto.ToString() });
            GuardarCarrito(lista);

            Console.WriteLine("Producto Agregado al carrito.");
            
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
                var inventario = LeerInventario();//Para ver si hay suficiente stock en el invetario, descontarlo; y así finalizar la compra

                // 1. Validar que siga habiendo stock de todo lo que está en el carrito antes de procesar
                foreach (var itemCarrito in carrito)
                {
                    string nombreProd = itemCarrito[1];
                    int cantComprar = Convert.ToInt32(itemCarrito[0]);

                    foreach (var prodInv in inventario)
                    {
                        if (prodInv[0].Equals(nombreProd, StringComparison.OrdinalIgnoreCase))// if(prodInv[0].ToLower() == nombreProd.ToLower())
                        {
                            int stockActual = Convert.ToInt32(prodInv[3]);
                            if (stockActual < cantComprar)
                            {
                                Console.WriteLine($"Lo sentimos, ya no hay stock suficiente de '{nombreProd}'. Compra cancelada.");
                                return;
                            }
                        }
                    }
                }

                // 2. Descontar el stock en la lista de inventario
                foreach (var itemCarrito in carrito)
                {
                    string nombreProd = itemCarrito[1];
                    int cantComprar = Convert.ToInt32(itemCarrito[0]);

                    foreach (var prodInv in inventario)
                    {
                        if (prodInv[0].Equals(nombreProd, StringComparison.OrdinalIgnoreCase))// if(prodInv[0].ToLower() == nombreProd.ToLower())
                        {
                            int stockActual = Convert.ToInt32(prodInv[3]);
                            prodInv[3] = (stockActual - cantComprar).ToString(); // Reducción del stock
                            break;
                        }
                    }
                }

                // Guardar los cambios del stock en el CSV de inventario
                GuardarInventario(inventario);

                // 3. Trasladar elementos del carrito al registro histórico de ventas
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

                Console.WriteLine("¡Gracias por su compra! ;) Su orden ha sido registrada.");// y el stock se ha actualizado
            }
        }

        //-----------------------------------------------------------------------------------CSV registro
        // LEER CSV Registro
        static List<string[]> LeerRegistro()
        {
            List<string[]> lista = new List<string[]>();

            if (!File.Exists(archivoCsv3))
            {
                File.WriteAllLines(archivoCsv3, new string[] { "NumeroCompra,Cantidad,Nombre,Precio,TotalProducto" });
                return lista;
            }

            string[] lineas = File.ReadAllLines(archivoCsv3);

            for (int i = 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i])) continue;
                string[] datos = lineas[i].Split(',');

                if (datos.Length == 5)
                    lista.Add(datos);
            }

            return lista;
        }
        // GUARDAR CSV Registro
        static void GuardarRegistro(List<string[]> lista)
        {
            List<string> lineas = new List<string>();
            lineas.Add("NumeroCompra,Cantidad,Nombre,Precio,TotalProducto");

            foreach (var i in lista)
            {
                lineas.Add($"{i[0]},{i[1]},{i[2]},{i[3]}, {i[4]}");
            }

            File.WriteAllLines(archivoCsv3, lineas);
        }
        // MOSTRAR CSV Registro
        static void MostrarRegistro()
        {
            var listaRegistro = LeerRegistro();
            var listaInventario = LeerInventario(); // Cargamos el inventario para cruzar los costos

            double acumuladorVentas = 0;
            double acumuladorCostos = 0;

            Console.WriteLine("\n--- REGISTRO HISTÓRICO DE VENTAS (ADMIN) ---");
            Console.WriteLine($"{"ID Compra",-18}{"Cant.",-8}{"Nombre",-42}{"Precio V.",-14}{"Total V.",-14}");
            Console.WriteLine(new string('-', 96));

            foreach (var i in listaRegistro)
            {
                // Extraer datos del registro de ventas
                string nombreProducto = i[2];
                int cantidad = Convert.ToInt32(i[1]);
                double precioVentaUnitario = Convert.ToDouble(i[3]);
                double totalProductoVenta = Convert.ToDouble(i[4]);

                Console.WriteLine($"{i[0],-18}{cantidad,-8}{nombreProducto,-42}${precioVentaUnitario,-13}${totalProductoVenta,-13}");

                acumuladorVentas += totalProductoVenta;

                // Buscar el precio de costo unitario correspondiente en el inventario
                double precioCostoUnitario = 0;
                foreach (var inv in listaInventario)
                {
                    if (inv[0].Equals(nombreProducto, StringComparison.OrdinalIgnoreCase))// if(inv[0].ToLower() == nombreProducto.ToLower())
                    {
                        precioCostoUnitario = Convert.ToDouble(inv[1]);
                        break;
                    }
                }

                // Calcular el costo total de cada venta(Costo Unitario * Cantidad)
                acumuladorCostos += (precioCostoUnitario * cantidad);
            }

            // Ganancia Neta = Ingresos Totales - Costos Totales
            double gananciasTotales = acumuladorVentas - acumuladorCostos;

            Console.WriteLine(new string('-', 96));
            Console.WriteLine($"Total Histórico de Ventas: ${acumuladorVentas}");
            Console.WriteLine($"Ganancias Totales:          ${gananciasTotales}");
        }

        

        // CONTAR(cantidad de productos en el inventario)
        static void Contar()
        {
            var lista = LeerInventario();
            Console.WriteLine("Cantidad total de productos registrados en inventario: " + lista.Count);
        }

        // BINARIO
        static void GuardarBinario()
        {
            var lista = LeerInventario();

            using (BinaryWriter bw = new BinaryWriter(File.Open(archivoBin, FileMode.Create)))
            {
                bw.Write(lista.Count);

                foreach (var i in lista)
                {
                    bw.Write(i[0]); // Nombre
                    bw.Write(i[1]); // COsto
                    bw.Write(i[2]); // Venta
                    bw.Write(i[3]); // Stock
                }
            }

            Console.WriteLine("El inventario se ha respaldado correctamente en formato binario.");
        }

        static void LeerBinario()
        {
            if (!File.Exists(archivoBin))
            {
                Console.WriteLine("No existe ningún archivo de respaldo binario.");
                return;
            }

            using (BinaryReader br = new BinaryReader(File.Open(archivoBin, FileMode.Open)))
            {
                int n = br.ReadInt32();

                Console.WriteLine("\n--- DATOS DESDE ARCHIVO BINARIO ---");

                for (int i = 0; i < n; i++)
                {
                    string nombre = br.ReadString();
                    string costo = br.ReadString();
                    string venta = br.ReadString();
                    string stock = br.ReadString();

                    Console.WriteLine($"{nombre} -> Costo: ${costo} | Venta: ${venta} | Stock: {stock}");
                }
            }
        }

    }
}
