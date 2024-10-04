using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;


namespace Sistema.Negocio
{
    public class NRol
    {
        public static DataTable Listar() 
        {
            DRol Datos = new DRol();
            return Datos.listar();
        }
    }
}
