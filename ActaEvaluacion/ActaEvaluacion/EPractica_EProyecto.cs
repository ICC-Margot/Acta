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
    public partial class EPractica_EProyecto : Form
    {
        DataBaseManager dataBase;
        int opc = 0, id_A;

        public EPractica_EProyecto()
        {
            InitializeComponent();
            dataBase = new DataBaseManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void EPractica(int id)
        {
            label1.Text = "Selecciona practica : ";
            label2.Visible = false;
            textBox1.Visible = false;
            opc = 1;
            id_A = id;
            llenarComboBox1(1);
        }

        public void EProyecto(int id)
        {
            label1.Text = "Selecciona proyecto : ";
            label2.Text = "Calificación : ";
            opc = 2;
            id_A = id;
            llenarComboBox1(2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;
            string Nom_P = selectedItem.ToString();

            if (Nom_P == "")
            {
                MessageBox.Show("Llenar campos ");
            }
            else
            {
                if (opc == 1)
                {
                    dataBase.Practica_Entregada(id_A, Nom_P);
                    llenarDataGridView_Practicas(id_A);
                }

                if (opc == 2)
                {
                    string Ca = textBox1.Text;
                    double C = Double.Parse(Ca);
                    if (Ca == "")
                    {
                        MessageBox.Show("Ingresar Calificación");
                    }

                    if ((C >= 0) && (C <= 10))
                    {
                        dataBase.Proyecto_Entregado(id_A, Nom_P, C);
                        llenarDataGridView_Proyectos(id_A);
                    }
                    else
                    {
                        MessageBox.Show("Calificación fuera de rango");
                    }
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void llenarComboBox1(int opc)
        {
            string[] p;
            p = new String[10];
            int i = 0;
            string[] pr_pr;
            pr_pr = new String[10];

            if (opc == 1)
            {
                int npract;
                p = dataBase.Obtener_Practicas();
                npract = dataBase.ObtenerPract_Alum(id_A);
                pr_pr = new String[npract];
                pr_pr = dataBase.Obtener_nomPract(id_A);
                comboBox1.Items.Clear();
            }

            if (opc == 2)
            {
                int nproy;
                p = dataBase.Obtener_Proyectos();
                nproy = dataBase.ObtenerProy_Alum(id_A);
                pr_pr = new String[nproy];
                pr_pr = dataBase.Obtener_nomProy(id_A);
                comboBox1.Items.Clear();
            }

            for (i = 0; i < pr_pr.GetLength(0); i++)
            {
                for (int j = 0; j < p.GetLength(0); j++)
                {
                    if (pr_pr[i] == p[j])
                    {
                        p[j] = " ";
                    }
                }
            }

            for (i = 0; i < p.GetLength(0); i++)
            {
                if (p[i] != " ")
                {
                    comboBox1.Items.Add(p[i]);
                }
            }
        }

        public void llenarDataGridView_Practicas(int id)
        {
            dataGridView1.Visible = false;
            string[] P;
            int pract = dataBase.ObtenerPract_Alum(id);

            P = dataBase.Obtener_nomPract(id);
            dataGridView2.Rows.Clear();

            for (int i = 0; i < pract; i++)
            {
                dataGridView2.Rows.Add(P[i]);
            }
        }

        private void EPractica_EProyecto_Load(object sender, EventArgs e)
        {

        }

        public void llenarDataGridView_Proyectos(int id)
        {
            dataGridView2.Visible = false;
            string[][] P;
            int proy = dataBase.ObtenerProy_Alum(id);

            P = dataBase.Obtener_califnom_Proy(id);
            dataGridView1.Rows.Clear();

            for (int i = 0; i < proy; i++)
            {
                dataGridView1.Rows.Add(P[i]);
            }
        }
    }
}
