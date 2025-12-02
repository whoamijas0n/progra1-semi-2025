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
        public SqlConnection objConexion = new SqlConnection();//Conectarse a la DB.
        public SqlCommand objComandos = new SqlCommand(); // ejecutar una consulta en la DB.
        public SqlDataAdapter objAdapatador = new SqlDataAdapter(); //un puente entre la DB y la aplicacion.
        public DataSet objDs = new DataSet(); //Es una represantcion de la arquitectura de la DB en memoria.

        public Claseconexion() { //Construtor, inicializador de atributos
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True";
            objConexion.ConnectionString = cadenaConexion;
            objConexion.Open();//Abrir la conexion a la DB.
        } 
       public DataSet obtenerDatos()
        {
            objDs.Clear();//limpiar el dataset
            objComandos.Connection = objConexion;//establecer la conexion para ejecutar los comandos.

            objAdapatador.SelectCommand = objComandos;//Establcer el comando de selecion.
            objComandos.CommandText = "SELECT * FROM usuarios";
            objAdapatador.Fill(objDs, "usuarios");//Tomando datos desde un dataset

            return objDs;
        }
       
        public string Mantenimiento_usuarios(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO usuarios(usuario,clave,nombre,direccion,telefono) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "', '" + datos[5] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE usurio SET usuario='" + datos[1] + "', clave='" + datos[2] + "', nombre='" + datos[3] + "' direccion='" + datos[4] + "', telefono='" + datos[5] + "' WHERE IdUsuario='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM usuarios WHERE IdUsuario='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        
        }

        public String ejecutarSQL(String sql)
        {
            try
            {
                objComandos.Connection = objConexion;
                objComandos.CommandText = sql;
                return objComandos.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
      
}
