using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenTPBD
{
    public partial class Parola : Form
    {
        string parola;
        bool enableTextBox = false;
        public Parola(string parolaModificare)
        {
            InitializeComponent();
            parola = parolaModificare;
        }
        
        
        private void Parola_Load(object sender, EventArgs e)
        {
            
        }

        private void parolaButton_Click(object sender, EventArgs e)
        {
            if (parola == parolaTextBox.Text)
            {
                MessageBox.Show("Parola introdusa este corecta!");
                //enableTextBox = true;
                this.DialogResult = DialogResult.OK;
                this.Close();

                //this.Close();
            }
            else
            {
                MessageBox.Show("Parola introdusa nu este corecta! Incercati din nou!");
                parolaTextBox.Text = "";
            }
        }

        public bool TheValue
        {
            get { return enableTextBox; }
        }
    }
}
