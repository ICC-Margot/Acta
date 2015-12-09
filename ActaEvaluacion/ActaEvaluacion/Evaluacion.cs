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
    public partial class Evaluacion : Form
    {
        DataBaseManager dataBase;
        String[] E;

        public Evaluacion()
        {
            InitializeComponent();
            dataBase = new DataBaseManager();
            E = new String[2];
            E = dataBase.ObtenerEvaluacion();
            textBox1.Text = E[0];
            textBox2.Text = E[1];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") && (textBox2.Text == ""))
                MessageBox.Show("Llenar campos");
            else
            {
                int pract = Convert.ToInt32(textBox1.Text);
                int proy = Convert.ToInt32(textBox2.Text);
                if (((pract + proy) == 100) || ((pract + proy) == 10))
                {
                    if ((pract + proy) == 10)
                    {
                        pract = pract * 10;
                        proy = proy * 10;
                        dataBase.Modificar_Evaluacion(pract, proy);
                        Close();
                    }
                    else
                    {
                        dataBase.Modificar_Evaluacion(pract, proy);
                        Close();
                    }
                }
                else
                    MessageBox.Show("Porcentajes incorrectos");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
