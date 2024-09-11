using Sistema.Entidades;
using System;
using System.Data.SqlClient;
using System.Data;

namespace Sistema.Datos
{
    public class DArticulo
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
                SqlCommand comando = new SqlCommand("articulo_listar", sqlCon);
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
                SqlCommand comando = new SqlCommand("articulo_buscar", sqlCon);
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
                SqlCommand Comando = new SqlCommand("articulo_existe", sqlCon);
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
        public string Insertar(Articulo objc)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("articulo_insertar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idcategoria",SqlDbType.Int).Value = objc.IdCategoria;
                Comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = objc.Codigo;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objc.Nombre;
                Comando.Parameters.Add("@precio_venta", SqlDbType.Decimal).Value = objc.PrecioVenta;
                Comando.Parameters.Add("@stock", SqlDbType.Int).Value = objc.Stock;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = objc.Descripcion;
                Comando.Parameters.Add("@imagen", SqlDbType.VarChar).Value = objc.Image;

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
        public string Actualizar(Articulo objc)
        {
            string Rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.getIntancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("articulo_actualizar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = objc.Id;
                Comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = objc.IdCategoria;
                Comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = objc.Codigo;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objc.Nombre;
                Comando.Parameters.Add("@precio_venta", SqlDbType.Decimal).Value = objc.PrecioVenta;
                Comando.Parameters.Add("@stock", SqlDbType.Int).Value = objc.Stock;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = objc.Descripcion;
                Comando.Parameters.Add("@imagen", SqlDbType.VarChar).Value = objc.Image;

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
                SqlCommand Comando = new SqlCommand("articulo_eliminar", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = id;
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
                SqlCommand Comando = new SqlCommand("activar_articulo", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = id;
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
                SqlCommand Comando = new SqlCommand("desactivar_articulo", sqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idarticulo", SqlDbType.Int).Value = id;
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
