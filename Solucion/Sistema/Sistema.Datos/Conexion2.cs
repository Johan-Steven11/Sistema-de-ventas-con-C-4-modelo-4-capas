using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class Conexion2
    {


        private static Conexion2 con = null;

        private Conexion2()
        {
         
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Data Source=PC-JSALAZAR;Initial Catalog=dbsistema;Integrated Security=True";

            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        public static Conexion2 getIntancia()
        {
            if (con == null)
            {
                con = new Conexion2();
            }
            return con;
        }
    }
}
