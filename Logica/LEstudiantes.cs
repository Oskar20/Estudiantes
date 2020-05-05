using Data;
using LinqToDB;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
    public class LEstudiantes: Librarys 

    {
        private List<TextBox> listTextBox;
        private List<Label> listLabel;
        private PictureBox image;
        private Bitmap _imagBitmap;
        private DataGridView _dataGridView;
        private NumericUpDown _numericUpDown;
        private Paginador<Estudiante> _paginador;
        private string _accion = "insert";
        //private Librarys librarys;

        public LEstudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objetos)
        {
            
            this.listTextBox = listTextBox;
            this.listLabel = listLabel;
            //librarys = new Librarys();
            image = (PictureBox)objetos[0];
            _imagBitmap = (Bitmap)objetos[1];
            _dataGridView = (DataGridView)objetos[2];
            _numericUpDown = (NumericUpDown)objetos[3];
            Restablecer();
        }

        public void Registrar()
        {
            if (listTextBox[0].Text.Equals(""))
            {
                listLabel[0].Text = "Nid - El campo es requerido";
                listLabel[0].ForeColor = Color.Red;
                listTextBox[0].Focus();
            }
            else
            {
                if (listTextBox[1].Text.Equals(""))
                {
                    listLabel[1].Text = "Nombre - El campo es requerido";
                    listLabel[1].ForeColor = Color.Red;
                    listTextBox[1].Focus();
                }
                else
                {
                    if (listTextBox[2].Text.Equals(""))
                    {
                        listLabel[2].Text = "Apellido - El campo es requerido";
                        listLabel[2].ForeColor = Color.Red;
                        listTextBox[2].Focus();
                    }
                    else
                    {
                        if (listTextBox[3].Text.Equals(""))
                        {
                            listLabel[3].Text = "Email - El campo es requerido";
                            listLabel[3].ForeColor = Color.Red;
                            listTextBox[3].Focus();
                        }
                        else
                        {
                            if (textBoxEvent.comprobarFormatoEmail(listTextBox[3].Text))
                            {
                                
                                var user = _Estudiante.Where(u => u.Email.Equals(listTextBox[3].Text) ).ToList();
                                if (user.Count == 0)
                                {
                                    Save();
                                }
                                else
                                {
                                    if (user[0].Id.Equals(_idEstudiante))
                                    {
                                        Save();

                                    }
                                    else
                                    {


                                    }
                                    listLabel[3].Text = "Email ya registrado";
                                    listLabel[3].ForeColor = Color.Red;
                                    listLabel[3].Focus();
                                }
                                
                               
                            }
                            else
                            {
                                listLabel[3].Text = "Email - Email no valido";
                                listLabel[3].ForeColor = Color.Red;
                                listTextBox[3].Focus();
                            }
                        }
                    }
                }


            }

        }

        private void Save()
        {

            BeginTransactionAsync();
            try
            {
                var imageArray = uploadImage.ImageToByte(image.Image);
                var db1 = new Conexion();
                switch (_accion)
                {
                    case "insert":
                        //var estudianteIndo = new Estudiante()
                        //{
                        //    Nid = listTextBox[0].Text,
                        //    Nombre = listTextBox[1].Text,
                        //    Apellido = listTextBox[2].Text,
                        //    Email = listTextBox[3].Text,
                        //};
                        //db1.InsertarEstudiante(estudianteIndo);
                        _Estudiante.Value(e => e.Nid, listTextBox[0].Text)
                                    .Value(e => e.Nombre, listTextBox[1].Text)
                                    .Value(e => e.Apellido, listTextBox[2].Text)
                                    .Value(e => e.Email, listTextBox[3].Text)
                                    .Value(e => e.Image, imageArray)
                                    .Insert();
                       
                        //var db = new Conexion();
                        //db.Insert(new Estudiante()
                        //{
                        //    Nid = listTextBox[0].Text,
                        //    Nombre = listTextBox[1].Text,
                        //    Apellido = listTextBox[2].Text,
                        //    Email = listTextBox[3].Text,
                        //}) ;

                        break;
                    case "update":
                        _Estudiante.Where(u => u.Id.Equals(_idEstudiante))
                                   .Set(e => e.Nombre, listTextBox[1].Text)
                                   .Set(e => e.Apellido, listTextBox[2].Text)
                                   .Set(e => e.Email, listTextBox[3].Text)
                                   .Set(e => e.Image, imageArray)
                                   .Update();
                        break;

                }


                CommitTransaction();
                Restablecer();



            }
            catch (Exception)
            {

                RollbackTransaction();
            }

        }

        private int _reg_por_pagina = 2, _num_pagina = 1;

        public void SearchEstudiante(string campo)
        {
            List<Estudiante> query = new List<Estudiante>();
            int inicio = (_num_pagina - 1) * _reg_por_pagina;
            if (campo.Equals(""))
            {
                query = _Estudiante.ToList();

            }
            else
            {
                query = _Estudiante.Where(c => c.Nid.StartsWith(campo) || c.Nombre.StartsWith(campo) || c.Apellido.StartsWith(campo)).ToList();
            }
            if (0 < query.Count)
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.Id,
                    c.Nid,
                    c.Nombre,
                    c.Apellido,
                    c.Email,
                    c.Image,

                }).Skip(inicio).Take(_reg_por_pagina).ToList();
                _dataGridView.Columns[0].Visible = false;
                _dataGridView.Columns[5].Visible = false;
                _dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                _dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            }
            else
            {
                _dataGridView.DataSource = query.Select(c => new
                {
                    c.Id,
                    c.Nid,
                    c.Nombre,
                    c.Apellido,
                    c.Email,
                }).ToList();
                _dataGridView.Columns[0].Visible = false;

            }

        }
       

        private int _idEstudiante = 0;
        public void GetEstudiante()
        {
            _accion = "update";
            _idEstudiante = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
            listTextBox[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
            listTextBox[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
            listTextBox[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
            listTextBox[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);
            try
            {
                byte[] arrayImage = (byte[])_dataGridView.CurrentRow.Cells[5].Value;
                image.Image = uploadImage.byteArrayToImage(arrayImage);
            }
            catch (Exception)
            {

                image.Image = _imagBitmap;
            }
        }

        private List<Estudiante> listEstudiante;
        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero";
                    _num_pagina = _paginador.primero();
                    break;
                case "Anterior";
                    _num_pagina = _paginador.anterior();
                    break;
                case "Siguiente";
                    _num_pagina = _paginador.siguiente();
                    break;
                case "Ultimo";
                    _num_pagina = _paginador.ultimo();
                    break;
            }
            SearchEstudiante("");
        }

        public void Registro_Paginas()
        {
            _num_pagina = 1;
            _reg_por_pagina = (int)_numericUpDown.Value;
            var list = _Estudiante.ToList();
            if (0 < list.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabel[4], _reg_por_pagina);
                SearchEstudiante("");
            }
        }

        public void Eliminar()
        {
            if (_idEstudiante.Equals(0))
            {
                MessageBox.Show("Seleccione un estudiante");
            }
            else
            {
                if (MessageBox.Show("Estas seguro que desea eliminar el estudiante?", "Eliminar Estudiante",MessageBoxButtons.YesNo)== DialogResult.Yes)
                {
                    _Estudiante.Where(c => c.Equals(_idEstudiante)).Delete();
                    Restablecer();
                }
            }


        }


        public void Restablecer()
        {
            _accion = "insert";
            _num_pagina = 1;
            _idEstudiante = 0;
            image.Image = _imagBitmap;
            listLabel[0].Text = "Nid";
            listLabel[1].Text = "Nombre";
            listLabel[2].Text = "Apellido";
            listLabel[3].Text = "Email";
            listLabel[0].ForeColor = Color.LightSlateGray;
            listLabel[1].ForeColor = Color.LightSlateGray;
            listLabel[2].ForeColor = Color.LightSlateGray;
            listLabel[3].ForeColor = Color.LightSlateGray;
            listTextBox[0].Text = "";
            listTextBox[1].Text = "";
            listTextBox[2].Text = "";
            listTextBox[3].Text = "";
            listEstudiante = _Estudiante.ToList();
            if (0 < listEstudiante.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listLabel[4], _reg_por_pagina);
            }
            SearchEstudiante("");

        }



    }
}
