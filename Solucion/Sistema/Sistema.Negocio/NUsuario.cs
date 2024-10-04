using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NUsuario
    {
        #region Listar
        public static DataTable Listar()
        {
            DUsuarios Datos = new DUsuarios();
            return Datos.listar();
        }
        #endregion
        #region Buscar
        public static DataTable Buscar(string valor)
        {
            DUsuarios Datos = new DUsuarios();
            return Datos.Buscar(valor);
        }
        #endregion
        #region Insertar
        public static string Insertar(int idrol,string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email, string Clave)
        {
            DUsuarios Datos = new DUsuarios();

            string Existe = Datos.Existe(Email);
            if (Existe.Equals("1"))
            {
                return "El Usuario con este email ya existe";
            }
            else
            {
                Usuario objU = new Usuario();
                objU.IdRol = idrol;
                objU.Nombre = Nombre;
                objU.TipoDocumento = TipoDocumento;
                objU.NumDocumento = NumDocumento;
                objU.Direccion = Direccion;
                objU.Telefono = Telefono;
                objU.Email = Email;
                objU.Clave = Clave;
                return Datos.Insertar(objU);
            }
        }
        #endregion
        #region Actualizar
        public static string Actualizar(int id,int idRol,string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string EmailAnt, string Email, string Clave)
        {

            DUsuarios Datos = new DUsuarios();
            Usuario objU = new Usuario();
            if (EmailAnt.Equals(Email))
            {
                objU.IdUsuario = id;
                objU.IdRol = idRol;
                objU.Nombre = Nombre;
                objU.TipoDocumento=TipoDocumento;
                objU.NumDocumento=NumDocumento;
                objU.Direccion= Direccion;
                objU.Telefono= Telefono;
                objU.Email = Email; 
                objU.Clave= Clave;
                
                return Datos.Actualizar(objU);

            }
            else
            {
                string Existe = Datos.Existe(Email);
                if (Existe.Equals("1"))
                {
                    return "El Usuario con este email ya existe";
                }
                else
                {

                    objU.IdUsuario = id;
                    objU.IdRol = idRol;
                    objU.Nombre = Nombre;
                    objU.TipoDocumento = TipoDocumento;
                    objU.NumDocumento = NumDocumento;
                    objU.Direccion = Direccion;
                    objU.Telefono = Telefono;
                    objU.Email = Email;
                    objU.Clave = Clave;
                    
                    return Datos.Actualizar(objU);
                }
            }
        }
        #endregion
        #region Eliminar
        public static string Eliminar(int id)
        {
            DUsuarios Datos = new DUsuarios();
            return Datos.Eliminar(id);
        }
        #endregion
        #region Desactivar
        public static string DesActivar(int id)
        {
            DUsuarios Datos = new DUsuarios();
            return Datos.Desactivar(id);
        }
        #endregion
        #region Activar
        public static string Activar(int id)
        {
            DUsuarios Datos = new DUsuarios();
            return Datos.Activar(id);
        }
        #endregion
        #region Login
        public static DataTable Login(string Email, string Clave)
        {
            DUsuarios Datos = new DUsuarios();
            return Datos.Login(Email, Clave);
        }
        #endregion
    }
}
