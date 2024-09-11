using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NCategoria
    {
        
        public static DataTable Listar() 
        {
            DCategoria Datos = new DCategoria();
            return Datos.listar();
        }

        public static DataTable Buscar(string valor)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Buscar(valor);
        }

        public static string Insertar(string Nombre, string Descripcion) 
        {
            DCategoria Datos = new DCategoria();

            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                Categoria objC = new Categoria();
                objC.Nombre = Nombre;
                objC.Descripcion = Descripcion;
                return Datos.Insertar(objC);
            }
        }

        public static string Actualizar(int id, string NombreAnt, string Nombre, string Descripcion) 
        {

            DCategoria Datos = new DCategoria();
            Categoria objC = new Categoria();
            if (NombreAnt.Equals(Nombre))
            {
                objC.IdCategoria = id;
                objC.Nombre = Nombre;
                objC.Descripcion = Descripcion;
                return Datos.Actualizar(objC);

            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "La categoria ya existe";
                }
                else
                {

                    objC.IdCategoria = id;
                    objC.Nombre = Nombre;
                    objC.Descripcion = Descripcion;
                    return Datos.Actualizar(objC);
                }
            }
        }

        public static string Eliminar(int id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Eliminar(id);
        }
        public static string DesActivar(int id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Desactivar(id);
        }
        public static string Activar(int id)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Activar(id);
        }

    }
}
