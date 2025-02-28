using Logica.Services.Alerta;
using Logica.Services.Combo;
using Logica.Services.Excel;
using Logica.Services.Producto;
using Logica.Services.Rubro;
using Logica.Services.Stock;
using Logica.Services.Validacion;
using Logica.Services.Venta;
using Microsoft.Extensions.DependencyInjection;
using Persistencia.DAOs.Alerta;
using Persistencia.DAOs.Combo;
using Persistencia.DAOs.Producto;
using Persistencia.DAOs.Rubro;
using Persistencia.DAOs.Stock;
using Persistencia.DAOs.Venta;
using Persistencia.Helpers.DataBase;
using Persistencia.Helpers.Excel;
using Presentacion.Forms;
using Presentacion.Forms.Factory.Producto;
using Presentacion.Forms.Factory.ReporteInfoProductos;
using Presentacion.Forms.Factory.ReporteStock;
using Presentacion.Forms.Factory.ReporteVenta;
using Presentacion.Forms.Observer;
using Presentacion.Forms.Producto;
using Presentacion.Forms.Stock;
using Presentacion.Forms.Venta;
using System;
using System.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        // Registrar servicios
        services.AddTransient<IAlertaService, AlertaService>();
        services.AddTransient<IComboService, ComboService>();
        services.AddTransient<IExcelService, ExcelService>();
        services.AddTransient<IProductoService, ProductoService>();
        services.AddTransient<IRubroService, RubroService>();
        services.AddTransient<IStockService, StockService>();
        services.AddTransient<IValidacionService, ValidacionService>();
        services.AddTransient<IVentaService, VentaService>();

        // Observer
        services.AddTransient<IPublisherAlerta, PublisherAlerta>();

        //Factory
        services.AddTransient<IProductoFormFactory, ProductoFormFactory>();
        services.AddTransient<IReporteInfoProductosFormFactory, ReporteInfoProductosFormFactory>();
        services.AddTransient<IReporteStockFormFactory, ReporteStockFormFactory>();
        services.AddTransient<IReporteVentaFormFactory, ReporteVentaFormFactory>();

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

        // DAO
        services.AddTransient<IDAOAlerta, DAOAlerta>();
        services.AddTransient<IDAOCombo, DAOCombo>();
        services.AddTransient<IDAOProducto, DAOProducto>();
        services.AddTransient<IDAORubro, DAORubro>();
        services.AddTransient<IDAOStock, DAOStock>();
        services.AddTransient<IDAOVenta, DAOVenta>();

        // Helper
        services.AddScoped<IDataBaseHelper>(provider => new DataBaseHelper(connectionString));
        services.AddTransient<IExcelHelper, ExcelHelper>();

        // Devuelve el proveedor configurado
        return services.BuildServiceProvider();
    }
}