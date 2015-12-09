using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActaEvaluacion
{
    public partial class Form1 : Form
    {
        DataBaseManager dataBase;

        public Form1()
        {
            InitializeComponent();
            dataBase = new DataBaseManager();

            String[] E = dataBase.ObtenerEvaluacion();
            llenarDataGridView();
        }

        public void llenarDataGridView()
        {
            string[][] alumnos;
            int p_pract, p_proy;
            String[] E;

            E = dataBase.ObtenerEvaluacion();
            alumnos = dataBase.ObtenerAlumno();
            int tpracticas = dataBase.ObtenerNumPracticas();
            int tproyectos = dataBase.ObtenerNumProyectos();
            p_pract = Convert.ToInt32(E[0]);
            p_proy = Convert.ToInt32(E[1]);
            dataGridView1.Rows.Clear();

            for (int i = 0; i < alumnos.GetLength(0); i++)
            {
                if (tpracticas == 0)
                    tpracticas = 1;
                if (tproyectos == 0)
                    tproyectos = 1;
          

                double calif_Pract = (dataBase.ObtenerPract_Alum(Convert.ToInt32(alumnos[i][0])) * p_pract) /tpracticas;
                 double calif_Proy = (dataBase.Calif_Proy(Convert.ToInt32(alumnos[i][0])) * p_proy) / (tproyectos * 10);
                 alumnos[i][6] = Convert.ToString(calif_Pract);
                 alumnos[i][7] = Convert.ToString(calif_Proy);
                 alumnos[i][9] = Convert.ToString((calif_Pract + calif_Proy)/10 - dataBase.Puntos_Menos(Convert.ToInt32(alumnos[i][0])));
                 double ptsmenos = dataBase.Puntos_Menos(Convert.ToInt32(alumnos[i][0]));
                 if (ptsmenos != 0)
                 {
                     alumnos[i][8] = Convert.ToString(ptsmenos);
                 }
                    dataGridView1.Rows.Add(alumnos[i]);
                 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NAlumno frm = new NAlumno();
            frm.ShowDialog();
            llenarDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NPractica_NProyectos PP = new NPractica_NProyectos();
            PP.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MPracticas_MProyectos MPP = new MPracticas_MProyectos();
            MPP.Practicas();
            MPP.ShowDialog();
            llenarDataGridView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MPracticas_MProyectos MPP = new MPracticas_MProyectos();
            MPP.Proyectos();
            MPP.Show();
            llenarDataGridView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Evaluacion criterios = new Evaluacion();
            criterios.ShowDialog();
            llenarDataGridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int opc = e.ColumnIndex;
            EPractica_EProyecto EPP = new EPractica_EProyecto();
            int id_Alumno = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            if (opc == 4)
            {
                EPP.llenarDataGridView_Practicas(id_Alumno);
                EPP.EPractica(id_Alumno);
                EPP.ShowDialog();
            }

            if (opc == 5)
            {
                EPP.llenarDataGridView_Proyectos(id_Alumno);
                EPP.EProyecto(id_Alumno);
                EPP.ShowDialog();
            }

            if (opc == 8)
            {
                double pm = dataBase.Puntos_Menos(id_Alumno);

                if (pm >= 10)
                    MessageBox.Show("Puntos Menos Excesivos");
                else
                    dataBase.InsertarP_Menos(id_Alumno);
            }

            if (e.ColumnIndex == 10)
            { 
                String nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                String apellidop = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                String apellidom = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                dataBase.EditarAlumno(id_Alumno, nombre, apellidop, apellidom);
            }

            if (e.ColumnIndex == 11)
            {
                dataBase.EliminarAlumno(id_Alumno);
            }
            llenarDataGridView();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
