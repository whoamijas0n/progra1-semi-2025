using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //esta libreria que me permite trabajar con bases de datos
using System.Data.SqlClient; //esta liberia me permite trabajar con sql server

namespace MiPrimerProyectoC_
{
    internal class Claseconexion
    {
        //Definir los miembros de la clase atributos y metodos.
        SqlConnection objConexion = new SqlConnection();//Conectarse a la DB.
        SqlCommand objComandos = new SqlCommand(); // ejecutar una consulta en la DB.
        SqlDataAdapter objAdapatador = new SqlDataAdapter(); //un puente entre la DB y la aplicacion.
        DataSet objDs = new DataSet(); //Es una represantcion de la arquitectura de la DB en memoria.

        public Conexion() { //Construtor, inicializador de atributos
            string cadenaConexion= "";
            objConexion.ConnectionString = cadenaConexion;
            objConexion.Open();//Abrir la conexion a la DB.
        }
       public DataSet obtenerDatos()
        {
            objDs.Clear();//limpiar el dataset
            objComandos.Connection = objConexion;//establecer la conexion para ejecutar los comandos.

            objAdapatador.SelectCommand = objComandos;//Establcer el comando de selecion.
            objComandos.CommandText = "SELECT * FROM alumnos";
            objAdapatador.Fill(objDs, "alumnos");//Tomando datos desde un dataset

            return objDs;
        }
    }
}
