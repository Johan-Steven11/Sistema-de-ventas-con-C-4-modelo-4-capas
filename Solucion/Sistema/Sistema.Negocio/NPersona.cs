

using Sistema.Datos;
using Sistema.Entidades;
using System.Data;

namespace Sistema.Negocio
{
    public class NPersona
    {
        #region Listar Todos
        public static DataTable Listar()
        {
            DPersona Datos = new DPersona();
            return Datos.listar();
        }
        #endregion
        #region Listar Proveedor
        public static DataTable ListarProveedor()
        {
            DPersona Datos = new DPersona();
            return Datos.listar_Proveedores();
        }
        #endregion
        #region Listar Cliente
        public static DataTable ListarCliente()
        {
            DPersona Datos = new DPersona();
            return Datos.listar_Clientes();
        }
        #endregion
        #region Buscar
        public static DataTable Buscar(string valor)
        {
            DPersona Datos = new DPersona();
            return Datos.Buscar(valor);
        }
        #endregion
        #region Buscar Proveedores
        public static DataTable BuscarProveedores(string valor) {
            DPersona Datos = new DPersona();
            return Datos.Buscar_Proveedores(valor);
        }
        #endregion
        #region Buscar Clientes
        public static DataTable BuscarCliente(string valor) {
            DPersona Datos = new DPersona();
            return Datos.Buscar_Clientes(valor);
        }
        #endregion
        #region Insertar
        public static string Insertar(string Tipo_persona, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email)
        {
            DPersona Datos = new DPersona();

            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "Esta persona ya existe. ";
            }
            else
            {
                Persona objP = new Persona();
                objP.TipoPersona = Tipo_persona;
                objP.Nombre = Nombre;
                objP.TipoDocumento = TipoDocumento;
                objP.NumeroDocumento = NumDocumento;
                objP.Direccion = Direccion;
                objP.Telefono = Telefono;
                objP.Email = Email;
                return Datos.Insertar(objP);
            }
        }
        #endregion
        #region Actualizar
        public static string Actualizar(int id, string TipoPersona, string NombreAnt,string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email)
        {

            DPersona Datos = new DPersona();
            Persona objP = new Persona();
            if (NombreAnt.Equals(Nombre))
            {
                objP.IdPersona = id;
                objP.TipoPersona = TipoPersona;
                objP.Nombre = Nombre;
                objP.TipoDocumento = TipoDocumento;
                objP.NumeroDocumento = NumDocumento;
                objP.Direccion = Direccion;
                objP.Telefono = Telefono;
                objP.Email = Email;
               

                return Datos.Actualizar(objP);

            }
            else
            {
                string Existe = Datos.Existe(Email);
                if (Existe.Equals("1"))
                {
                    return "Una persona con este nombre ya existe";
                }
                else
                {

                    objP.IdPersona = id;
                    objP.TipoPersona = TipoPersona;
                    objP.Nombre = Nombre;
                    objP.TipoDocumento = TipoDocumento;
                    objP.NumeroDocumento = NumDocumento;
                    objP.Direccion = Direccion;
                    objP.Telefono = Telefono;
                    objP.Email = Email;
                    
                    return Datos.Actualizar(objP);
                }
            }
        }
        #endregion
        #region Eliminar
        public static string Eliminar(int id)
        {
            DPersona Datos = new DPersona();
            return Datos.Eliminar(id);
        }
        #endregion
    }
}
