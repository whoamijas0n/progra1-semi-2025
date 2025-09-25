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

        public Claseconexion() { //Construtor, inicializador de atributos
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True";
            objConexion.ConnectionString = cadenaConexion;
            objConexion.Open();//Abrir la conexion a la DB.
        } 
       public DataSet obtenerDatosAlumnos()
        {
            objDs.Clear();//limpiar el dataset
            objComandos.Connection = objConexion;//establecer la conexion para ejecutar los comandos.

            objAdapatador.SelectCommand = objComandos;//Establcer el comando de selecion.
            objComandos.CommandText = "SELECT * FROM alumnos";
            objAdapatador.Fill(objDs, "alumnos");//Tomando datos desde un dataset

            return objDs;
        }
       
        public string administrarDatosAlumnos(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO alumnos(codigo,nombre,direccion,telefono) VALUES (@codigo, @nombre, @direccion, @telefono)";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE alumnos SET codigo=@codigo, nombre=@nombre, direccion=@direccion, telefono=@telefono WHERE IdAlumnos=@IdAlumnos";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM alumnos WHERE IdAlumnos=@IdAlumnos";
            }
            return ejecutarSQL(sql, datos);
        
        }
        private String ejecutarSQL(String sql, String[] datos)
        {
            try
            {
                objComandos.Connection = objConexion;
                objComandos.CommandText = sql;

                objComandos.Parameters.Clear();
                objComandos.Parameters.AddWithValue("@IdAlumnos", datos[0]);
                objComandos.Parameters.AddWithValue("@codigo", datos[1]);
                objComandos.Parameters.AddWithValue("@nombre", datos[2]);
                objComandos.Parameters.AddWithValue("@direccion", datos[3]);
                objComandos.Parameters.AddWithValue("@telefono", datos[4]);

                return objComandos.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public DataSet obtenerDatosDocentes()
        {
            objDs.Clear();//limpiar el dataset
            objComandos.Connection = objConexion;//establecer la conexion para ejecutar los comandos.

            objAdapatador.SelectCommand = objComandos;//Establcer el comando de selecion.
            objComandos.CommandText = "SELECT * FROM docentes";
            objAdapatador.Fill(objDs, "docentes");//Tomando datos desde un dataset

            return objDs;
        }
        public string administrarDatosDocentes(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO docentes(codigo,nombre,direccion,telefono) VALUES (@codigo, @nombre, @direccion, @telefono)";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE docentes SET codigo=@codigo, nombre=@nombre, direccion=@direccion, telefono=@telefono WHERE IdDocentes=@IdDocentes";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM docentes WHERE IdDocentes=@IdDocentes";
            }
            return ejecutarSQLDocente(sql, datos);

        }
        private String ejecutarSQLDocente(String sql, String[] datos)
        {
            try
            {
                objComandos.Connection = objConexion;
                objComandos.CommandText = sql;

                objComandos.Parameters.Clear();
                objComandos.Parameters.AddWithValue("@IdDocentes", datos[0]);
                objComandos.Parameters.AddWithValue("@codigo", datos[1]);
                objComandos.Parameters.AddWithValue("@nombre", datos[2]);
                objComandos.Parameters.AddWithValue("@direccion", datos[3]);
                objComandos.Parameters.AddWithValue("@telefono", datos[4]);

                return objComandos.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
