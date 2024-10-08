using Sistema.Entidades;
using System;
using System.Data.SqlClient;
using System.Data;


namespace Sistema.Datos
{
    public class DPersona
    {
        #region Metodo Listar Todos
        public DataTable listar()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("Persona_Listar", sqlCon);
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
        #region Metodo Listar Proveedores
        public DataTable listar_Proveedores()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("Persona_Listar_Proveedores", sqlCon);
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
        #region Metodo Listar Clientes
        public DataTable listar_Clientes()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("Persona_Listar_Clientes", sqlCon);
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
                SqlCommand comando = new SqlCommand("Persona_Buscar", sqlCon);
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
        #region Metodo Buscar Proveedores
        public DataTable Buscar_Proveedores(string valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("Persona_Buscar_Proveedores", sqlCon);
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
        #region Metodo Buscar Cliente
        public DataTable Buscar_Clientes(string valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("Persona_Buscar_Clientes", sqlCon);
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
                SqlCommand Comando = new SqlCommand("persona_Existe", sqlCon);
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
        public string Insertar(Persona objP)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("persona_insertar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@tipo_persona", SqlDbType.VarChar).Value = objP.TipoPersona;
                Comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = objP.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = objP.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = objP.NumeroDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = objP.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = objP.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = objP.Email;
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
        public string Actualizar(Persona objP)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("persona_Actualizar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idpersona", SqlDbType.Int).Value = objP.IdPersona;
                Comando.Parameters.Add("@tipo_persona", SqlDbType.VarChar).Value = objP.TipoPersona;
                Comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = objP.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = objP.TipoDocumento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = objP.NumeroDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = objP.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = objP.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = objP.Email;
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
                SqlCommand Comando = new SqlCommand("persona_Eliminar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idPersona", SqlDbType.Int).Value = id;
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
      

    }
}
