using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Rfc
    {
        protected string CadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        //Cargar los datos a la BD
        public void CrearRegistro(E_Rfc objRfc)
        {
            //Objeto de la clase SQLConnection
            SqlConnection conn = new SqlConnection(CadenaConexion);
            //Abrimos la conexion
            conn.Open();
            //Objeto de la clase SQLCommand y SP
            SqlCommand comando = new SqlCommand("crear_rfc", conn);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", objRfc.Nombre.ToUpper());
            comando.Parameters.AddWithValue("@apePat", objRfc.ApePat.ToUpper());
            if (objRfc.ApeMat==null)
            {
                objRfc.ApeMat = "";
            }
            comando.Parameters.AddWithValue("@apeMat", objRfc.ApeMat.ToUpper());
            comando.Parameters.AddWithValue("@rfc", GenerarRfc(objRfc).ToUpper());

            comando.Parameters.AddWithValue("@fechaNacimiento", objRfc.FechaNacimiento);
            
            comando.ExecuteNonQuery();
            conn.Close();
        }
        //Metodo para crear y validar el RFC
        public string GenerarRfc(E_Rfc objRfc)
        {
            string[] malaPalabra = { "BUEI","CACA","CAGA","CAKA","COGE","COJE","COJO","FETO","JOTO","KACO",
                                     "KAGO","KOJO","KULO","MAMO","MEAS","MION","MULA","PEDO","PUTA","QULO",
                                     "BUEY","CACO","CAGO","CAKO","COJA","COJI","CULO","GUEY","KACA","KAGA",
                                     "KOGE","KAKA","MAME","MEAR","MEON","MOCO","PEDA","PENE","PUTO","RATA" };
            string nombre = objRfc.Nombre.ToString().ToUpper();
            if (nombre.Contains(" "))
            {
                string[] nombreSeparado = nombre.Split(' ');
                if ((nombreSeparado[0] == "MARIA") ||
                    (nombreSeparado[0] == "MA.") ||
                    (nombreSeparado[0] == "MA") ||
                    (nombreSeparado[0] == "JOSE") ||
                    (nombreSeparado[0] == "J.") ||
                    (nombreSeparado[0] == "J"))
                {
                    nombre = nombreSeparado[1].Substring(0, 1);

                }
                else
                {
                    nombre = nombreSeparado[0].Substring(0, 1);
                }
            }
            else
            {
                nombre = nombre.Substring(0, 1);
            }


            string apePat = objRfc.ApePat.ToString().ToUpper();
            string letApePat1 = apePat.Substring(0, 1);
            string letApePat2 = apePat.Substring(1, 1);
            if (letApePat1 == "Ñ")
            {
                letApePat1 = "X";
            }
            if        ((letApePat2 != "A") &&
                       (letApePat2 != "E") &&
                       (letApePat2 != "I") &&
                       (letApePat2 != "O") &&
                       (letApePat2 != "U"))
            {
                letApePat2 = "X";
                apePat = letApePat1 + letApePat2;
            }
            else
            {
                apePat = letApePat1 + letApePat2;
            }

            string apeMat = objRfc.ApeMat;
            if (apeMat == null)
            {
                apeMat = "";
            }
            apeMat=apeMat.ToUpper().ToString();
            string letApeMat = "";
            if (apeMat=="")
            {
                letApeMat = "X";
            }
            else
            {
                letApeMat = apeMat.Substring(0, 1);
                if (letApeMat.Equals("Ñ"))
                {
                    letApeMat = letApeMat.Replace('Ñ', 'X');
                }
            }
            string fechaNacimiento = objRfc.FechaNacimiento.ToString("yy/MM/dd");
            string fechaYear = fechaNacimiento.Substring (0,2);
            string fechaMes = fechaNacimiento.Substring(3,2);
            string fechaDia = fechaNacimiento.Substring(6,2);
            string cadenaFechaNacimiento = fechaYear + fechaMes + fechaDia;
            string cadenaTexto = apePat + letApeMat + nombre;
            foreach (string palabras in malaPalabra)
            {
                if (cadenaTexto == palabras)
                {
                    cadenaTexto = apePat + letApeMat + "X";
                }
            }
            string rfcCompleto = cadenaTexto + cadenaFechaNacimiento;
            return rfcCompleto;
        }

        //Metodo para mostrar todos los registros
        public List<E_Rfc> MostrarTodos()
        {
            //Crear lista vacia
            List<E_Rfc> lista = new List<E_Rfc> ();
            //Objeto de la clase SQLConnection
            SqlConnection conn = new SqlConnection(CadenaConexion);
            //Abrimos la conexion
            conn.Open();
            //Objeto SQLCommand y SP
            SqlCommand comando = new SqlCommand("mostrar_todos", conn);
            comando.CommandType = CommandType.StoredProcedure;
            //Objeto SQLDataReader
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                E_Rfc objRfc = new E_Rfc();
                objRfc.IdRfc = Convert.ToInt32(reader["idRfc"]);
                objRfc.Nombre = reader["nombre"].ToString();
                objRfc.ApePat = reader["apePat"].ToString();
                objRfc.ApeMat = reader["apeMat"].ToString();
                objRfc.NombreCompleto = objRfc.Nombre +" " + objRfc.ApePat + " " + objRfc.ApeMat;
                objRfc.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                objRfc.Rfc = reader["rfc"].ToString();
                lista.Add(objRfc);
            }
            conn.Close();
            return lista;
        }

        //Metodo para eliminar
        public void Eliminar(int IdRfc)
        {
            //Objeto SQLConnection
            SqlConnection conn = new SqlConnection(CadenaConexion);
            //Abrir conexion
            conn.Open();
            //SQLCommand y SP
            SqlCommand comando = new SqlCommand("eliminarRfc_por_id",conn);
            comando.Parameters.AddWithValue("@id", IdRfc);
            comando.CommandType=CommandType.StoredProcedure;
            comando.ExecuteNonQuery();
            conn.Close();
        }

        //Metodo para obtener por ID
        public E_Rfc ObtenerPorId(int IdRfc)
        {
            E_Rfc objRfc = new E_Rfc();
            //Objeto SQLConnection
            SqlConnection conn = new SqlConnection(CadenaConexion);
            //Abrir conexion
            conn.Open();
            //SQLCommand y SP
            SqlCommand comando = new SqlCommand("obtenerRfc_por_id",conn);
            comando.Parameters.AddWithValue("@id", IdRfc);
            comando.CommandType = CommandType.StoredProcedure;
            //ObjetoSQLDataReader
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                objRfc.IdRfc = Convert.ToInt32(reader["idRfc"]);
                objRfc.Nombre = reader["nombre"].ToString();
                objRfc.ApePat = reader["apePat"].ToString();
                objRfc.ApeMat = reader["apeMat"].ToString();
                objRfc.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
            }
            conn.Close();
            return objRfc;
        }

        //Metodo para actualizar un registro
        public void EditarRfc(E_Rfc objRfc,int IdRfc)
        {
            //Objeto SLQConnectrion
            SqlConnection conn = new SqlConnection(CadenaConexion);
            //Abrir conexion
            conn.Open();
            //Objeto SQLCommand y SP
            SqlCommand comando = new SqlCommand("editarRfc", conn);
            comando.CommandType=CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", objRfc.Nombre.ToUpper());
            comando.Parameters.AddWithValue("@apePat", objRfc.ApePat.ToUpper());
            if (objRfc.ApeMat == null)
            {
                objRfc.ApeMat = "";
            }
            comando.Parameters.AddWithValue("@apeMat", objRfc.ApeMat.ToUpper());
            comando.Parameters.AddWithValue("@fechaNacimiento", objRfc.FechaNacimiento);
            comando.Parameters.AddWithValue("@rfc", GenerarRfc(objRfc).ToUpper());
            comando.Parameters.AddWithValue("@id", IdRfc);
            comando.ExecuteNonQuery();
            conn.Close();
        }

        //Metodo para buscar
        public List<E_Rfc> Buscar(string BuscadorRfc)
        {
            //Lista Vacioa
            List<E_Rfc> lista = new List<E_Rfc>();

            //Objeto SQLConnection
            SqlConnection conn = new SqlConnection(CadenaConexion);
            //Abri la conexion
            conn.Open();
            //SQLCommand y SP
            SqlCommand comando = new SqlCommand("buscar_por_letra",conn);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@buscador_letra", BuscadorRfc);
            //SQLDataReader
            SqlDataReader reader = comando.ExecuteReader();
            //Reader para guardar los datos
            while(reader.Read())
            {
                //Objeto E_Rfc
                E_Rfc objRfc = new E_Rfc();
                objRfc.IdRfc = Convert.ToInt32(reader["idRfc"]);
                objRfc.Nombre = reader["nombre"].ToString();
                objRfc.ApePat = reader["apePat"].ToString();
                objRfc.ApeMat = reader["apeMat"].ToString();
                objRfc.NombreCompleto = objRfc.Nombre + " " + objRfc.ApePat + " " + objRfc.ApeMat;
                objRfc.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                objRfc.Rfc = reader["rfc"].ToString();
                lista.Add(objRfc);
            }
            conn.Close();
            return lista;
        }

    }
}
