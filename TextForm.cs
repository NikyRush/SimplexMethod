using System;
using System.Windows.Forms;

namespace SimplexMethod
{
    public partial class TextForm : Form
    {
        private MainForm mainForm;
        private int indexFormNow;
        public TextForm(MainForm form, int index)
        {
            InitializeComponent();
            mainForm = form;
            indexFormNow = index;
            textBox1.Text = mainForm.tempText;
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            if (indexFormNow < mainForm.listForm.Count - 1)
            {
                this.Visible = false;
                mainForm.listForm[indexFormNow + 1].Visible = true;
            }
            else
                mainForm.ClearDateFromFunction();
        }

        private void TextForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.ClearDateFromFunction();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if(indexFormNow == 0)
                mainForm.ClearDateFromFunction();
            else
            {
                this.Visible = false;
                mainForm.listForm[indexFormNow - 1].Visible = true;
            }
        }
    }
}
