using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimplexMethod
{
    public partial class DataForm : Form
    {
        public MainForm mainForm;
        private int indexFormNow;
        private int x;
        private int y;
        public DataForm(MainForm form, int index)
        {
            InitializeComponent();
            mainForm = form;
            indexFormNow = index;
            GenerateSimplexTable();
        }

        private void GenerateSimplexTable()
        {
            x = 17;
            y = 17;
            GenerateTextBox("C", true);
            foreach(float coefficient in mainForm.targetFunction.GetRefListCoefficient)
                GenerateTextBox(coefficient.ToString(), false);
            GenerateTextBox("0", false);
            x = 17;
            y += 28;

            //Второй ряд
            GenerateTextBox("базис", true);
            int countAllCoefficient = mainForm.countVariable + mainForm.countNewCoefficientAsBasis;
            for (int index = 0; index < countAllCoefficient; index++)
                GenerateTextBox("x" + (index + 1).ToString(), true);
            GenerateTextBox("b", true);
            x = 17;
            y += 28;
            
            //Остальные ряды до дельт
            foreach(LimitedFunction function in mainForm.listLimitedFunction)
            {
                if (function.Basis == 0)
                    GenerateTextBox("?", true);
                else
                    GenerateTextBox("x" + function.Basis.ToString(), true);
                foreach(float coefficicent in function.GetRefListCoefficient)
                    GenerateTextBox(coefficicent.ToString(), false);
                GenerateTextBox(function.FreeCoefficient.ToString(), false);
                x = 17;
                y += 28;
            }

            if (mainForm.listDelta.Count != 0)
            {
                GenerateTextBox("d", true);
                foreach(float delta in mainForm.listDelta)
                    GenerateTextBox(delta.ToString(), false);
            }
        }

        private TextBox GenerateTextBox(string stringText, bool bold)
        {
            TextBox text = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(70, 27),
                MaxLength = 6,
                Margin = new System.Windows.Forms.Padding(0),
                TextAlign = HorizontalAlignment.Center,
                Text =  stringText,
                ShortcutsEnabled = false, 
                ReadOnly = true
            };
            x += 72;
            if(bold)
                text.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            panel2.Controls.Add(text);
            return text;
        }

        private void DataForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.ClearDateFromFunction();
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (indexFormNow == 0)
                mainForm.ClearDateFromFunction();
            else
            {
                this.Visible = false;
                mainForm.listForm[indexFormNow - 1].Visible = true;
            }
        }
    }
}
