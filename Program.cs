﻿using Distribuidora.Helpers;
using System;
using System.Windows.Forms;

namespace Distribuidora
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //PDFHelper.ImprimirDetalleDeVenta();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
        }
    }
}
