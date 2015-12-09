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
    public partial class NAlumno : Form
    {
        DataBaseManager dataBase;
        Form1 actualizar;

        public NAlumno()
        {
            InitializeComponent();
            dataBase = new DataBaseManager();
            actualizar = new Form1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != ""))
            {
                string nombre = textBox1.Text;
                string apellidop = textBox2.Text;
                string apellidom = textBox3.Text;
                dataBase.InsertarAlumno(nombre, apellidop, apellidom);
                Close();
            }
            else
            {
                MessageBox.Show("Llenar campos");
            }
        }
    }
}
