using Logica;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estudiantes
{
    public partial class Form1 : Form
    {
        private LEstudiantes estudiante;
        //private Librarys librarys;
        public Form1()
        {
            InitializeComponent();
            //librarys = new Librarys();
            var listTextBox = new List<TextBox>();
            listTextBox.Add(textBoxNid);
            listTextBox.Add(textBoxNombre);
            listTextBox.Add(textBoxApellido);
            listTextBox.Add(textBoxEmail);
            var listLabel = new List<Label>();
            listLabel.Add(labelNid);
            listLabel.Add(labelNombre);
            listLabel.Add(labelApellido);
            listLabel.Add(labelEmail);
            listLabel.Add(labelPaginas);
            object[] objetos = { 
                pictureBoxImage,
                Properties.Resources.Agregar,  //obtener la imagen que va en el picture
                dataGridViewEstudiante
                numericUpDown1
            };

            estudiante  = new LEstudiantes(listTextBox, listLabel, objetos);

            

        }

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            estudiante.uploadImage.CargarImagen(pictureBoxImage);

        }

        private void textBoxNid_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNid.Text.Equals("") )
            {
                labelNid.ForeColor = Color.Red;
            }
            else
            {
                labelNid.ForeColor = Color.Green;
                labelNid.Text = "Nid";

            }
        }

        private void textBoxNid_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.numberKeyPress(e);
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {

            if (textBoxNombre.Text.Equals(""))
            {
                labelNombre.ForeColor = Color.Red;
            }
            else
            {
                labelNombre.ForeColor = Color.Green;
                labelNombre.Text = "Nombre";

            }

        }

        private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.textKeyPress(e);
        }

        private void textBoxApellido_TextChanged(object sender, EventArgs e)
        {
            if (textBoxApellido.Text.Equals(""))
            {
                labelApellido.ForeColor = Color.Red;
            }
            else
            {
                labelApellido.ForeColor = Color.Green;
                labelApellido.Text = "Apellido";

            }
        }

        private void textBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.textBoxEvent.textKeyPress(e);
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

            if (textBoxEmail.Text.Equals(""))
            {
                labelEmail.ForeColor = Color.Red;
            }
            else
            {
                labelEmail.ForeColor = Color.Green;
                labelEmail.Text = "Email";

            }

        }

     


        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            estudiante.Registrar();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {

            estudiante.Eliminar();

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            estudiante.Restablecer();
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            estudiante.SearchEstudiante(textBoxBuscar.Text);
        }

        private void buttonPrimero_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Primero");
        }

        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Anterior");
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Siguiente");
        }

        private void buttonUltimo_Click(object sender, EventArgs e)
        {
            estudiante.Paginador("Ultimo");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            estudiante.Registro_Paginas();
        }

        private void dataGridViewEstudiante_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewEstudiante.Rows.Count !=0 )
            {
                estudiante.GetEstudiante();

            }

        }

        private void dataGridViewEstudiante_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridViewEstudiante.Rows.Count != 0)
            {
                estudiante.GetEstudiante();

            }

        }

        private void dataGridViewEstudiante_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
