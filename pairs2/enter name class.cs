using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using System.Drawing;

namespace pairs2
{
    class enter_name_class
    {
        private string boxtitle;
        private string message;

        public string Boxtitle { get { return boxtitle; } set { boxtitle = value; } }
        // allows set a value to boxtitle, and use boxtitle
        public string Message { get { return message; } set { message = value; } }
        // allows set a value to message, and use message


        public string EnterNamePopUp()
        {
            Form mainform = new Form();
            Label text = new Label();
            Button savebutton = new Button();
            TextBox popup = new TextBox();

            mainform.Text = boxtitle;
            // this is creating the title of my pop up
            text.Text = message;
            // this creates a message on my pop up
            savebutton.Text = "ok";
            // what will come up on my button

            savebutton.DialogResult = DialogResult.OK;
            // type of button

            text.SetBounds(15, 25, 500, 15);
            // bounds of label
            popup.SetBounds(100, 50, 400, 15);
            // sets bound of my textbix
            savebutton.SetBounds(200, 80, 68, 20);
            // sets bounds of my button

            mainform.ClientSize = new Size(400, 110);
            // sets size of the form 
            mainform.Controls.AddRange(new Control[] { text, savebutton, popup });
            // adds all my things to the form
            mainform.ClientSize = new Size(Math.Max(300, text.Right + 10), mainform.ClientSize.Height);
            // fits the size of the form around the label
            mainform.StartPosition = FormStartPosition.CenterScreen;
            // opens form up in the middle of the screen

            mainform.MinimizeBox = false;
            // no minimize box
            mainform.MaximizeBox = false;
            // keeps it at set size
            mainform.AcceptButton = savebutton;
            // allows the button to work/actually do something

            text.AutoSize = true;
            savebutton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            // puts button in right bottom corner of form

            if (mainform.ShowDialog() == DialogResult.OK)
            {
                return popup.Text;
                // saving the input
            }

            else
            {
                return "n/a";
            }




            
           
        }
    }

}
