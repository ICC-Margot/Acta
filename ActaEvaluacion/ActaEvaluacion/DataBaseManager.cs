using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ActaEvaluacion
{
    class DataBaseManager
    {
        MySqlConnection conexion;

        public DataBaseManager()
        {
            string datosConexion = " Database=acta; Data Source=localhost; User id=root; Password="; // le estamos diciendo como nos vamos a conectar
            conexion = new MySqlConnection(datosConexion); //Inicializamos el objeto 
        }

        public string[][] ObtenerAlumno()
        {
            string[][] alumno;
            int numAlumnos = ObtenerNumAlumnos();
            string query = "SELECT * FROM alumno";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            alumno = new string[numAlumnos][];
            int i = 0;

            while (reader.Read())
            {
                alumno[i] = new string[20];
                alumno[i][0] = "" + reader.GetString(0);
                alumno[i][1] = reader.GetString(1);
                alumno[i][2] = reader.GetString(2);
                alumno[i][3] = reader.GetString(3);
                alumno[i][4] = "Practica";
                alumno[i][5] = "Proyecto";
                alumno[i][8] = "-1";
                alumno[i][10] = "Editar";
                alumno[i][11] = "Eliminar";
                i++;
            }
            reader.Close();
            conexion.Close();
            return alumno;
        }

        public int ObtenerNumAlumnos()
        {
            int numAlumno = 0;
            string query = "SELECT count(*) FROM alumno";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            numAlumno = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return numAlumno;
        }

        public void InsertarAlumno(string nombre, string apellidop, string apellidom)
        {
            string query = "INSERT INTO alumno VALUES (NULL , ?Nombre , ?ApellidoP, ?ApellidoM)";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?Nombre", MySqlDbType.VarChar, 80).Value = nombre;
            command.Parameters.Add("?ApellidoP", MySqlDbType.VarChar, 80).Value = apellidop;
            command.Parameters.Add("?ApellidoM", MySqlDbType.VarChar, 80).Value = apellidom;

            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void EditarAlumno(int id, string nombre, string apellidop, string apellidom)
        {
            string query = "UPDATE alumno SET Nombre='" + nombre + "',ApellidoP='" + apellidop + "',ApellidoM='" + apellidom + "' WHERE id_Alumno=" + id;
            //MessageBox.Show(query);
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?id_Alumno", MySqlDbType.Int32, 80).Value = id;
            command.Parameters.Add("?Nombre", MySqlDbType.VarChar, 50).Value = nombre;
            command.Parameters.Add("?ApellidoP", MySqlDbType.VarChar, 50).Value = apellidop;
            command.Parameters.Add("?ApellidoM", MySqlDbType.VarChar, 50).Value = apellidom;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void EliminarAlumno(int id)
        {
            string query = "DELETE FROM alumno WHERE id_Alumno='" + id + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?id_Alumno", MySqlDbType.Int32, 80).Value = id;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public int ObtenerNumPracticas()
        {
            int numP = 0;
            string query = "SELECT count(*) FROM practicas";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            numP = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return numP;
        }

        public string[][] ObtenerPracticas()
        {
            string[][] p;
            int numP = ObtenerNumPracticas();
            string query = "SELECT * FROM practicas";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            p = new string[numP][];
            int i = 0;
            while (reader.Read())
            {

                p[i] = new string[10];
                p[i][0] = "" + reader.GetString(0);
                p[i][1] = reader.GetString(1);
                p[i][2] = "Editar";
                p[i][3] = "Eliminar";
                i++;
            }
            reader.Close();
            conexion.Close();
            return p;
        }

        public string[] Obtener_Practicas()
        {
            string[] p;
            int numP = ObtenerNumPracticas();
            string query = "SELECT Nombre_Practica FROM practicas";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            p = new string[numP];
            int i = 0;
            while (reader.Read())
            {
                p[i] = reader.GetString(0);
                i++;
            }
            reader.Close();
            conexion.Close();
            return p;
        }

        public void InsertarPractica(string practica)
        {
            string query = "INSERT INTO practicas VALUES (NULL , ?Nombre_Practica)";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?Nombre_Practica", MySqlDbType.VarChar, 80).Value = practica;

            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        private int Obtener_idPract(string Nom_Practica)
        {
            int id = 0;
            string query = "SELECT id_Practica FROM practicas WHERE Nombre_Practica= '" + Nom_Practica + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            id = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return id;
        }

        public void Practica_Entregada(int id_Alumno, string Nom_Practica)
        {
            int id_Practica = Obtener_idPract(Nom_Practica);
            string query = "INSERT INTO alum_pract VALUES (?id_Alumno , ?id_Practica)";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?id_Alumno", MySqlDbType.Int32, 80).Value = id_Alumno;
            command.Parameters.Add("?id_Practica", MySqlDbType.Int32, 80).Value = id_Practica;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void Modificar_Practica(int id, string nombre_practica)
        {
            string query = "UPDATE practicas SET Nombre_Practica='" + nombre_practica + "' WHERE id_Practica='" + id + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?id_Practica", MySqlDbType.Int32, 80).Value = id;
            command.Parameters.Add("?Nombre_Practica", MySqlDbType.VarChar, 80).Value = nombre_practica;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void Eliminar_Practica(int id_practica)
        {
            string query = "DELETE FROM practicas WHERE id_Practica='" + id_practica + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?id_Practica", MySqlDbType.Int32).Value = id_practica;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public int ObtenerNumProyectos()
        {
            int numP = 0;
            string query = "SELECT count(*) FROM proyecto";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            numP = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return numP;
        }

        public string[][] ObtenerProyectos()
        {
            string[][] p;
            int numP = ObtenerNumProyectos();
            string query = "SELECT * FROM proyecto";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            p = new string[numP][];
            int i = 0;
            while (reader.Read())
            {
                p[i] = new string[40];
                p[i][0] = "" + reader.GetString(0);
                p[i][1] = reader.GetString(1);
                p[i][2] = "Editar";
                p[i][3] = "Eliminar";
                i++;
            }
            reader.Close();
            conexion.Close();
            return p;
        }

        public string[] Obtener_Proyectos()
        {
            string[] p;
            int numP = ObtenerNumProyectos();
            string query = "SELECT Nombre_Proyecto FROM proyecto";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            p = new string[numP];
            int i = 0;
            while (reader.Read())
            {
                p[i] = reader.GetString(0);
                i++;
            }
            reader.Close();
            conexion.Close();
            return p;
        }

        public void InsertarProyecto(string proyecto)
        {
            string query =" INSERT INTO proyecto (id_Proyecto, Nombre_Proyecto) VALUES(NULL,'"+proyecto+"')";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?Nombre_Proyecto", MySqlDbType.VarChar, 80).Value = proyecto;

            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        private int Obtener_idProy(string Nom_Proyecto)
        {
            int id = 0;
            string query = "SELECT id_Proyecto FROM proyecto WHERE Nombre_Proyecto= '" + Nom_Proyecto + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            id = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return id;
        }

        public void Proyecto_Entregado(int id_Alumno, string Nom_Proyecto, double Calif)
        {
            int id_Proyecto = Obtener_idProy(Nom_Proyecto);
            string query = "INSERT INTO alum_proy(id_Alumno, id_Proyecto, Calificacion) VALUES ('"+id_Alumno+"','"+id_Proyecto+"','"+Calif+"')";
            MySqlCommand command = new MySqlCommand(query);
            //MessageBox.Show(query);
            command.Parameters.Add("?id_Alumno", MySqlDbType.Int32, 80).Value = id_Alumno;
            command.Parameters.Add("?id_Proyecto", MySqlDbType.Int32, 80).Value = id_Proyecto;
            command.Parameters.Add("?Calificacion", MySqlDbType.Double).Value = Calif;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void Modificar_Proyecto(int id_Proyecto, string nombre_proyecto)
        {
            string query = "Update proyecto SET Nombre_Proyecto='" + nombre_proyecto + "' WHERE id_Proyecto='" + id_Proyecto + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?id_Proyecto", MySqlDbType.Int32, 80).Value = id_Proyecto;
            command.Parameters.Add("?Nombre_Proyecto", MySqlDbType.VarChar, 80).Value = nombre_proyecto;

            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void Eliminar_Proyecto(int id_proyecto)
        {
            string query = "DELETE FROM proyecto WHERE id_Proyecto='" + id_proyecto + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?id_Proyecto", MySqlDbType.Int32).Value = id_proyecto;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public int ObtenerPract_Alum(int id)
        {
            int numA_P = 0;
            string query = "SELECT count(*) FROM alum_pract WHERE id_Alumno='" + id + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            numA_P = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return numA_P;
        }

        public int ObtenerProy_Alum(int id)
        {
            int numA_P = 0;
            string query = "SELECT count(*) FROM alum_proy WHERE id_Alumno='" + id + "'";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            numA_P = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return numA_P;
        }

        public double Calif_Proy(int id)
        {
            string query = "SELECT Calificacion FROM alum_proy WHERE id_Alumno='" + id + "'";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            int i = 0;
            double c = 0;
            while (reader.Read())
            {
                c = c + reader.GetDouble(0);
                i++;
            }
            reader.Close();
            conexion.Close();
            return c;
        }

        public void InsertarP_Menos(int id_Alumno)
        {
            string query = "INSERT INTO puntosmenos(id_Alumno, Menos) VALUES ('" + id_Alumno + "',0.1)";
            MySqlCommand command = new MySqlCommand(query);
            //MessageBox.Show(query);
            command.Parameters.Add("?id_Alumno", MySqlDbType.Int32, 80).Value = id_Alumno;
            command.Parameters.Add("?Menos", MySqlDbType.Int32).Value = 0.1;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public double Puntos_Menos(int id)
        {
            string query = "SELECT Menos FROM puntosmenos WHERE id_Alumno='" + id + "'";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            int i = 0;
            double m = 0;
            while (reader.Read())
            {
                m = m + reader.GetDouble(0);
                i++;
            }
            reader.Close();
            conexion.Close();
            return m;
        }

        public void Modificar_Evaluacion(int P_Pract, int P_Proy)
        {
            string query = "UPDATE criterios SET P_Practicas='" + P_Pract + "',P_Proyectos='" + P_Proy + "' WHERE id_E=0";
            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.Add("?P_Practicas", MySqlDbType.Int32, 2).Value = P_Pract;
            command.Parameters.Add("?P_Proyectos", MySqlDbType.Int32, 2).Value = P_Proy;
            command.Connection = conexion;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public string[] ObtenerEvaluacion()
        {
            string[] E;
            E = new string[2];
            string query = "SELECT P_Practicas FROM criterios WHERE id_E=0";
            string query2 = "SELECT P_Proyectos FROM criterios WHERE id_E=0";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            E[0] = Convert.ToString(reader.GetInt32(0));
            reader.Close();
            command.Connection.Close();

            command = new MySqlCommand(query2);
            command.Connection = conexion;
            command.Connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            E[1] = Convert.ToString(reader.GetInt32(0));
            reader.Close();
            command.Connection.Close();
            return E;
        }

        public int ObtenerP_Proyectos()
        {
            int P = 0;
            string query = "SELECT P_Proyectos FROM criterios WHERE id_E=0";
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = conexion;
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            P = reader.GetInt32(0);
            reader.Close();
            command.Connection.Close();
            return P;
        }

        public string[] Obtener_nomProy(int id)
        {
            string[]p;
            int numP = ObtenerProy_Alum(id);
            string query = "SELECT Nombre_Proyecto FROM proyecto INNER JOIN alum_proy ON proyecto.id_Proyecto=alum_proy.id_Proyecto WHERE alum_proy.id_Alumno='" + id + "'";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            p = new string[numP];
            int i = 0;
            while (reader.Read())
            {
                p[i] = reader.GetString(0);
                i++;
            }
            reader.Close();
            conexion.Close();
            return p;
        }

        public string[] Obtener_nomPract(int id)
        {
            string[] p;
            int numP = ObtenerPract_Alum(id);
            string query = "SELECT Nombre_Practica FROM practicas INNER JOIN alum_pract ON practicas.id_Practica=alum_pract.id_Practica WHERE alum_pract.id_Alumno='"+id+"'";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            p = new string[numP];
            int i = 0;
            while (reader.Read())
            {
                p[i] = reader.GetString(0);
                i++;
            }
            reader.Close();
            conexion.Close();
            return p;
        }

        public string[][] Obtener_califnom_Proy(int id)
        {
            string[][] p;
            int numP = ObtenerPract_Alum(id);
            string query = "SELECT Nombre_Proyecto, Calificacion FROM proyecto INNER JOIN alum_proy ON proyecto.id_Proyecto=alum_proy.id_Proyecto WHERE alum_proy.id_Alumno='"+id+"'";
            MySqlCommand command = new MySqlCommand(query);
            MySqlDataReader reader;
            command.Connection = conexion;
            conexion.Open();
            reader = command.ExecuteReader();
            p = new string[numP][];
            int i = 0;
            while (reader.Read())
            {
                p[i] = new string[2];
                p[i][0] = reader.GetString(0);
                p[i][1] = reader.GetString(1);
                i++;
            }
            reader.Close();
            conexion.Close();
            return p;
        }

    }
}
