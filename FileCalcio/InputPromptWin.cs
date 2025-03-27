using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCalcio
{
    public partial class InputPromptWin : Form
    {
        public string InputTextVal { get; set; }

        public InputPromptWin()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            InputTextVal = txt_testoRicerca.Text;
            DialogueResult = DialogueResult.OK;
            Close();
        }

        private void btn_Annulla_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
