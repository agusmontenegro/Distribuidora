using Logica.Services.Alerta;
using Logica.Services.Combo;
using Logica.Services.Excel;
using Logica.Services.Producto;
using Logica.Services.Rubro;
using Logica.Services.Stock;
using Logica.Services.Validacion;
using Logica.Services.Venta;
using Microsoft.Extensions.DependencyInjection;
using Presentacion.Forms;
using Presentacion.Forms.Producto;
using Presentacion.Forms.Stock;
using Presentacion.Forms.Venta;
using System;

public static class DependencyInjectionConfig
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Registrar servicios
        services.AddTransient<IAlertaService, AlertaService>();
        services.AddTransient<IComboService, ComboService>();
        services.AddTransient<IExcelService, ExcelService>();
        services.AddTransient<IProductoService, ProductoService>();
        services.AddTransient<IRubroService, RubroService>();
        services.AddTransient<IStockService, StockService>();
        services.AddTransient<IValidacionService, ValidacionService>();
        services.AddTransient<IVentaService, VentaService>();

        // Registrar formularios
        services.AddTransient<Menu>();
        services.AddTransient<BuscarProducto>();
        services.AddTransient<Producto>();
        services.AddTransient<ReporteInfoProductos>();
        services.AddTransient<Venta>();
        services.AddTransient<ReporteVenta>();
        services.AddTransient<Stock>();
        services.AddTransient<ReporteStock>();
        services.AddTransient<Estadistica>();
        services.AddTransient<Alerta>();
        services.AddTransient<Estadistica>();

        // Devuelve el proveedor configurado
        return services.BuildServiceProvider();
    }
}