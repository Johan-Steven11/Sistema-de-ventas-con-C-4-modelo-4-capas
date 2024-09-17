using System;
using System.Data;
using System.Data.SqlClient;
using Sistema.Entidades;


namespace Sistema.Datos
{
    public class DCategoria
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
                SqlCommand comando = new SqlCommand("categoria_listar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                sqlCon.Open();
                Resultado= comando.ExecuteReader();
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
                SqlCommand comando = new SqlCommand("categoria_Buscar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@valor",SqlDbType.VarChar).Value= valor ;
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
        #region Metodo Seleccionar
        public DataTable Seleccionar()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand comando = new SqlCommand("categoria_seleccionar", sqlCon);
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
        #region Metodo validar existencia 
        public string Existe(string valor)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_existe", sqlCon);
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
        public string Insertar(Categoria objc) 
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_insertar",sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = objc.Nombre;
                Comando.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = objc.Descripcion;

                sqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro"; 

            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return Rpta;
        }
        #endregion
        #region Metodo Actualizar
        public string Actualizar(Categoria objc) 
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_Actualizar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@ID", SqlDbType.Int).Value = objc.IdCategoria;
                Comando.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = objc.Nombre;
                Comando.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = objc.Descripcion;

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
                SqlCommand Comando = new SqlCommand("categoria_Eliminar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@ID", SqlDbType.Int).Value = id;
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
                SqlCommand Comando = new SqlCommand("categoria_Activar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@ID", SqlDbType.Int).Value = id;
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
                SqlCommand Comando = new SqlCommand("categoria_Desactivar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@ID", SqlDbType.Int).Value = id;
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
    }
}
