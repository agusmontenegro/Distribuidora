using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    //public class Movimiento
    //{
    //    public int PosicionX;
    //    public int PosicionY;
    //    public bool EsPosible;
    //}

    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var serviceProvider = DependencyInjectionConfig.ConfigureServices();
            var menu = serviceProvider.GetRequiredService<Forms.Menu>();
            Application.Run(menu);
        }

        //static void Main()
        //{
        //    int[,] tablero = {
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //        { 0, 0, 0, 0, 0, 0, 0, 0 },
        //    };

        //    int iteracion = 1;
        //    int posicionInicialX = 7;
        //    int posicionInicialY = 7;
        //    tablero[posicionInicialX, posicionInicialY] = 1;
        //    iteracion++;
        //    var tableroCompletado = TableroCompletado(posicionInicialX, posicionInicialY, tablero, iteracion);
        //    if (tableroCompletado)
        //    {
        //        for (int i = 0;i < tablero.GetLength(0);i++)
        //        {
        //            for (int j = 0;j < tablero.GetLength(1);j++)
        //            {
        //                Console.Write(tablero[i, j].ToString().Length == 2 ? tablero[i, j] + " " : tablero[i, j] + "  ");
        //            }
        //            Console.Write("\n");
        //        }
        //        Console.ReadLine();
        //    }
        //}

        //static bool TableroCompletado(int posicionX, int posicionY, int[,] tablero, int iteracion)
        //{
        //    //Encontramos los posibles hijos del nodo actual.
        //    var posiblesMovimientos = PosiblesMovimientos(posicionX, posicionY, tablero);

        //    //Llegamos al final de la solucion, devolvemos true finalizando
        //    if (iteracion == 65)
        //        return true;

        //    //Utilizamos Warnsdorff para ordenar los hijos del nodo actual de mas probable a menos probable.
        //    posiblesMovimientos.Sort((a, b) => PosiblesMovimientos(a.PosicionX, a.PosicionY, tablero).Count.CompareTo(PosiblesMovimientos(b.PosicionX, b.PosicionY, tablero).Count));

        //    //Si no encontramos mas hijos en el nodo actual significa que camino del arbol no es posible.
        //    if (posiblesMovimientos.Count == 0)
        //        return false;

        //    foreach (var posibleMovimiento in posiblesMovimientos)
        //    {
        //        //Marcamos el casillero del tablero
        //        tablero[posibleMovimiento.PosicionX, posibleMovimiento.PosicionY] = iteracion;

        //        //Hacemos recursividad e invocamos pasandole el hijo del nodo actual
        //        if (TableroCompletado(posibleMovimiento.PosicionX, posibleMovimiento.PosicionY, tablero, iteracion + 1))
        //        {
        //            //Llegamos al final de la solucion, devolvemos true finalizando
        //            return true;
        //        }
        //        else
        //        {
        //            //Borramos el paso en falso del tablero
        //            tablero[posibleMovimiento.PosicionX, posibleMovimiento.PosicionY] = 0;
        //        }
        //    }
        //    return false;
        //}

        //static List<Movimiento> PosiblesMovimientos(int x, int y, int[,] tablero)
        //{
        //    var posiblesMovimientos = new List<Movimiento>();
        //    if (x + 1 <= 7 && y + 2 <= 7)
        //    {
        //        if (tablero[x + 1, y + 2] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x + 1,
        //                PosicionY = y + 2,
        //                EsPosible = true
        //            });
        //        }
        //    }

        //    if (x + 1 <= 7 && y - 2 >= 0)
        //    {
        //        if (tablero[x + 1, y - 2] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x + 1,
        //                PosicionY = y - 2,
        //                EsPosible = true
        //            });
        //        }
        //    }
        //    if (x - 1 >= 0 && y - 2 >= 0)
        //    {
        //        if (tablero[x - 1, y - 2] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x - 1,
        //                PosicionY = y - 2,
        //                EsPosible = true
        //            });
        //        }
        //    }
        //    if (x - 1 >= 0 && y + 2 <= 7)
        //    {
        //        if (tablero[x - 1, y + 2] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x - 1,
        //                PosicionY = y + 2,
        //                EsPosible = true
        //            });
        //        }
        //    }
        //    if (x + 2 <= 7 && y + 1 <= 7)
        //    {
        //        if (tablero[x + 2, y + 1] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x + 2,
        //                PosicionY = y + 1,
        //                EsPosible = true
        //            });
        //        }
        //    }
        //    if (x + 2 <= 7 && y - 1 >= 0)
        //    {
        //        if (tablero[x + 2, y - 1] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x + 2,
        //                PosicionY = y - 1,
        //                EsPosible = true
        //            });
        //        }
        //    }
        //    if (x - 2 >= 0 && y - 1 >= 0)
        //    {
        //        if (tablero[x - 2, y - 1] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x - 2,
        //                PosicionY = y - 1,
        //                EsPosible = true
        //            });
        //        }
        //    }
        //    if (x - 2 >= 0 && y + 1 <= 7)
        //    {
        //        if (tablero[x - 2, y + 1] == 0)
        //        {
        //            posiblesMovimientos.Add(new Movimiento
        //            {
        //                PosicionX = x - 2,
        //                PosicionY = y + 1,
        //                EsPosible = true
        //            });
        //        }
        //    }

        //    return posiblesMovimientos;
        //}
    }
}