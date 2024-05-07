using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Rfc
    {
        //Crear Registr
        public void CrearRegistro(E_Rfc objRfc)
        {
            //Creamos el objeto de la capa de datos
            D_Rfc datos = new D_Rfc();
            datos.CrearRegistro(objRfc);
        }
        //Combiar RFC
        public string RegistroCreado(E_Rfc objRfc)
        {
            //Creamos objeto de la capa de datos
            D_Rfc datos = new D_Rfc();
            return datos.GenerarRfc(objRfc);
        }
        //Mostrar todos los datos
        public List<E_Rfc> MostrarTodos()
        {
            //Objeto de la capa de datos
            D_Rfc datos = new D_Rfc();
            return datos.MostrarTodos();
        }
        //Borrar
        public void Eliminar(int IdRfc)
        {
            //Objeto de la capa de datos
            D_Rfc datos = new D_Rfc();
            datos.Eliminar(IdRfc);
        }
        //Buscar por ID
        public E_Rfc ObtenerPorId(int IdRfc)
        {
            //Objeto de la capa de datos
            D_Rfc datos = new D_Rfc();
            return datos.ObtenerPorId(IdRfc); ;
        }
        //Editar datos
        public void EditarRfc(E_Rfc objRfc,int IdRfc)
        {
            //Objeto de la capa de datos
            D_Rfc datos = new D_Rfc();
            datos.EditarRfc(objRfc,IdRfc);
        }

        //Buscador de RFC
        public List<E_Rfc> Buscar(string BuscadorRfc)
        {
            //Objeto de la capa de datos
            D_Rfc datos = new D_Rfc();
            return datos.Buscar(BuscadorRfc);
        }
    }
}
