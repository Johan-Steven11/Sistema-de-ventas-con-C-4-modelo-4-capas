using Sistema.Datos;
using Sistema.Entidades;
using System.Data;


namespace Sistema.Negocio
{
    public class NArticulo
    {
        #region Listar
        public static DataTable Listar()
        {
            DArticulo Datos = new DArticulo();
            return Datos.listar();
        }
        #endregion
        #region Buscar
        public static DataTable Buscar(string valor)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Buscar(valor);
        }
        #endregion
        #region Insertar
        public static string Insertar(int idCategoria,string Codigo,string Nombre, decimal precioVneta, int Stock,string Descripcion, string Imagen)
        {
            DArticulo Datos = new DArticulo();

            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "El articulo ya existe";
            }
            else
            {
                Articulo objA = new Articulo();
                objA.IdCategoria = idCategoria;
                objA.Codigo = Codigo;
                objA.Nombre = Nombre;
                objA.PrecioVenta = precioVneta;
                objA.Stock = Stock;
                objA.Descripcion = Descripcion;
                objA.Image = Imagen;
                return Datos.Insertar(objA);
            }
        }
        #endregion
        #region Actualizar
        public static string Actualizar(int id,int idCategoria, string Codigo, string NombreAnt,string Nombre, decimal precioVneta, int Stock, string Descripcion, string Imagen)
        {

            DArticulo Datos = new DArticulo();
            Articulo objA = new Articulo();
            if (NombreAnt.Equals(Nombre))
            {
                objA.Id = id;
                objA.IdCategoria = idCategoria;
                objA.Codigo = Codigo;
                objA.Nombre = Nombre;
                objA.PrecioVenta = precioVneta;
                objA.Stock = Stock;
                objA.Descripcion = Descripcion;
                objA.Image = Imagen;
                return Datos.Actualizar(objA);

            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "El articulo ya existe";
                }
                else
                {
                    objA.Id = id;
                    objA.IdCategoria = idCategoria;
                    objA.Codigo = Codigo;
                    objA.Nombre = Nombre;
                    objA.PrecioVenta = precioVneta;
                    objA.Stock = Stock;
                    objA.Descripcion = Descripcion;
                    objA.Image = Imagen;
                    return Datos.Actualizar(objA);
                }
            }
        }
        #endregion
        #region Eliminar
        public static string Eliminar(int id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Eliminar(id);
        }
        #endregion
        #region Desactivar
        public static string DesActivar(int id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(id);
        }
        #endregion
        #region Activar
        public static string Activar(int id)
        {
            DArticulo Datos = new DArticulo();
            return Datos.Activar(id);
        }
        #endregion
    }
}
