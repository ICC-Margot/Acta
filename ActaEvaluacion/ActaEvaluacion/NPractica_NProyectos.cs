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
    public partial class NPractica_NProyectos : Form
    {
        DataBaseManager dataBase;

        public NPractica_NProyectos()
        {
            InitializeComponent();
            dataBase = new DataBaseManager();
            label2.Visible = false;
            textBox1.Visible = false;
            button2.Visible = false;
        }

        private void EPractica_EProyectos_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;

            if (selectedItem.ToString() == "Practica")
            {
                label2.Text = "Ingresa el Nombre de la Practica: ";
                label2.Visible = true;
                textBox1.Visible = true;
            }

            if (selectedItem.ToString() == "Proyecto")
            {
                label2.Text = "Ingresa el Nombre del Proyecto: ";
                label2.Visible = true;
                textBox1.Visible = true;
            }
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;

            if (selectedItem.ToString() == "Practica")
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Llenar nombre de la practica");
                }
                else
                {
                    string practica = textBox1.Text;
                    dataBase.InsertarPractica(practica);
                    Close();
                }
            }

           if (selectedItem.ToString() == "Proyecto")
           {   
                if (textBox1.Text != "")
                {
                    string proyecto = textBox1.Text;
                    dataBase.InsertarProyecto(proyecto);
                    Close();
                }
                else
                {
                    MessageBox.Show("Llenar nombre del proyecto");
                }     
          }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
