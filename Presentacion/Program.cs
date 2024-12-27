using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Presentacion
{
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
    }
}