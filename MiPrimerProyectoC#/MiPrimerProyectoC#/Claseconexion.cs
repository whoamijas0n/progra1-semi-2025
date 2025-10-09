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

            objAdapatador.SelectCommand = objComandos;//Establcer el comando de selecion.}
            //Alumnos
            objComandos.CommandText = "SELECT * FROM alumnos";
            objAdapatador.Fill(objDs, "alumnos");//Tomando datos desde un dataset
            //Docentes
            objComandos.CommandText = "SELECT * FROM docentes";
            objAdapatador.Fill(objDs, "docentes");//Tomando datos desde un dataset
            //Materias
            objComandos.CommandText = "SELECT * FROM materias";
            objAdapatador.Fill(objDs, "materias");//Tomando datos desde un dataset

            return objDs;
        }
       
        public string administrarDatosAlumnos(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO alumnos(codigo,nombre,direccion,telefono) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE alumnos SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', direccion='" + datos[3] + "', telefono='" + datos[4] + "' WHERE IdAlumno='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM alumnos WHERE IdAlumnos='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        
        }
        
        public string administrarDatosDocentes(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO docentes(codigo,nombre,direccion,telefono) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE docentes SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', direccion='" + datos[3] + "', telefono='" + datos[4] + "' WHERE IdDocentes='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM docentes WHERE IdDocentes='" + datos[0] + "'";
            }
            return ejecutarSQL(sql);
        }

        public string administrarDatosMaterias(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "nuevo")
            {
                sql = "INSERT INTO materias(codigo,nombre,uv) VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "')";
            }
            else if (accion == "modificar")
            {
                sql = "UPDATE materias SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', uv='" + datos[3] + "' WHERE IdMaterias='" + datos[0] + "'";
            }
            else if (accion == "eliminar")
            {
                sql = "DELETE FROM materias WHERE IdMaterias='" + datos[0] + "'";
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
