using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class TextBoxEvent
    {
        public void textKeyPress(KeyPressEventArgs e)
        {
            //condición que solo nos permite ingresar datos de tipo texto
            if (char.IsLetter(e.KeyChar)) { e.Handled = false; }
            //condicion que permite no dar salto de linea cuando se oprime enter
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            //condición que nos permite utilizar la tecla backspace
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //condición que nos permite utilizar la tecla de espacio
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else
            {
                e.Handled = true;
            }
            
        }


        public void numberKeyPress(KeyPressEventArgs e)
        {
            //condición que solo nos permite ingresar datos de tipo texto
            if (char.IsDigit(e.KeyChar)) { e.Handled = false; }
            //condicion que permite no dar salto de linea cuando se oprime enter
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
            //condición que no permite ingresasr campos de tipo texto
            else if (char.IsLetter(e.KeyChar)) { e.Handled = true; }
            //condición que nos permite utilizar la tecla backspace
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //condición que nos permite utilizar la tecla de espacio
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else
            {
                e.Handled = true;
            }
        }

        public bool comprobarFormatoEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }



    }
}
