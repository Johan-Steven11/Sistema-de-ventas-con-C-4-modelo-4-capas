using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Sistema.Entidades;

namespace Sistema.Datos
{
    public class DUsuarios
    {
        #region Metodo Listar
        public DataTable listar()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("Usuario_Listar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                sqlCon.Open();
                Resultado = comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

        }
        #endregion
        #region Metodo Buscar
        public DataTable Buscar(string valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("Usuario_Buscar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                sqlCon.Open();
                Resultado = comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

        }
        #endregion
        #region Metodo validar existencia 
        public string Existe(string valor)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_existe", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                SqlParameter ParExiste = new SqlParameter();
                ParExiste.ParameterName = "@existe";
                ParExiste.SqlDbType = SqlDbType.Int;
                ParExiste.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(ParExiste);
                sqlCon.Open();
                Comando.ExecuteNonQuery();
                Rpta = ParExiste.Value.ToString();

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return Rpta;
        }
        #endregion
        #region Metodo Insertar
        public string Insertar(Usuario objU)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("Usuario_Insertar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@IdRol", SqlDbType.Int).Value = objU.IdRol;
                Comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = objU.Nombre;
                Comando.Parameters.Add("@Tipo_documento", SqlDbType.VarChar).Value = objU.TipoDocumento;
                Comando.Parameters.Add("@Num_Documento", SqlDbType.VarChar).Value = objU.NumDocumento;
                Comando.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = objU.Direccion;
                Comando.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = objU.Telefono;
                Comando.Parameters.Add("@Email", SqlDbType.VarChar).Value = objU.Email;
                Comando.Parameters.Add("@Clave", SqlDbType.VarChar).Value = objU.Clave;
                sqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro";

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return Rpta;
        }
        #endregion
        #region Metodo Actualizar
        public string Actualizar(Usuario objU)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("Usuario_Actualizar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = objU.IdUsuario;
                Comando.Parameters.Add("@idRol", SqlDbType.Int).Value = objU.IdRol;
                Comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = objU.Nombre;
                Comando.Parameters.Add("@Tipo_Documento", SqlDbType.VarChar).Value = objU.TipoDocumento;
                Comando.Parameters.Add("@Numero_Documento", SqlDbType.VarChar).Value = objU.NumDocumento;
                Comando.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = objU.Direccion;
                Comando.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = objU.Telefono;
                Comando.Parameters.Add("@Email", SqlDbType.VarChar).Value = objU.Email;
                Comando.Parameters.Add("@Clave", SqlDbType.VarChar).Value = objU.Clave;

                sqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Actualizar el registro";

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return Rpta;

        }
        #endregion
        #region Metodo Eliminar
        public string Eliminar(int id)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("Eliminar_Usuario", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = id;
                sqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Eliminar el registro";

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return Rpta;
        }
        #endregion
        #region Metodo Activar
        public string Activar(int id)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("Activar_Usuario", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = id;
                sqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Activar el registro";

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return Rpta;
        }
        #endregion
        #region Metodo Desactivar
        public string Desactivar(int id)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("Desactivar_Usuario", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = id;
                sqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Desactivar el registro";

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return Rpta;

        }
        #endregion
        #region Login
        public DataTable Login(string Email, string Clave)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("usuario_login", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Email;
                comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = Clave;
                sqlCon.Open();
                Resultado = comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;

            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

        }

        #endregion
    }
}
