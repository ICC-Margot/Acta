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
    public partial class MPracticas_MProyectos : Form
    {
        DataBaseManager dataBase;
        int opc=0;
        public MPracticas_MProyectos()
        {
            InitializeComponent();
            dataBase = new DataBaseManager();
        }

        public void Practicas()
        {
            string[][] p;            
            p = dataBase.ObtenerPracticas();
            dataGridView1.Rows.Clear();
            opc = 1;
            for (int i = 0; i < p.GetLength(0); i++)
                 dataGridView1.Rows.Add(p[i]);
        }

        public void Proyectos()
        {
            string[][] p;
            p = dataBase.ObtenerProyectos();
            dataGridView1.Rows.Clear();
            opc = 2;
            for (int i = 0; i < p.GetLength(0); i++)
                 dataGridView1.Rows.Add(p[i]);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                String nombre_P = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                
                if (opc == 1)
                {
                    dataBase.Modificar_Practica(id, nombre_P);
                    
                    Practicas();
                }

                if (opc == 2)
                {
                    dataBase.Modificar_Proyecto(id, nombre_P);
                    Proyectos();
                }
            }

            if (e.ColumnIndex == 3)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                if (opc == 1)
                {
                    dataBase.Eliminar_Practica(id);
                    Practicas();
                }

                if (opc == 2)
                {
                    dataBase.Eliminar_Proyecto(id);
                    Proyectos();
                }
            }
        }
    }
}
