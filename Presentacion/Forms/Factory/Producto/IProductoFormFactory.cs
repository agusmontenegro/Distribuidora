namespace Presentacion.Forms.Factory.Producto
{
    public interface IProductoFormFactory
    {
        Forms.Producto.Producto CrearFormularioProducto(string idProduct = null);
    }
}