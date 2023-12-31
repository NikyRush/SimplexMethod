namespace SimplexMethod
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_Choose = new System.Windows.Forms.Panel();
            this.txt_CountLimited = new System.Windows.Forms.TextBox();
            this.txt_CountVariable = new System.Windows.Forms.TextBox();
            this.lbl_Limitation = new System.Windows.Forms.Label();
            this.lbl_Variable = new System.Windows.Forms.Label();
            this.pnl_Button = new System.Windows.Forms.Panel();
            this.btn_Solve = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.tblLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnl_LimitedFunction = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnl_TargetFunction = new System.Windows.Forms.Panel();
            this.pnl_Choose.SuspendLayout();
            this.pnl_Button.SuspendLayout();
            this.tblLP_Main.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Choose
            // 
            this.pnl_Choose.Controls.Add(this.txt_CountLimited);
            this.pnl_Choose.Controls.Add(this.txt_CountVariable);
            this.pnl_Choose.Controls.Add(this.lbl_Limitation);
            this.pnl_Choose.Controls.Add(this.lbl_Variable);
            this.pnl_Choose.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Choose.Location = new System.Drawing.Point(0, 0);
            this.pnl_Choose.Name = "pnl_Choose";
            this.pnl_Choose.Size = new System.Drawing.Size(769, 89);
            this.pnl_Choose.TabIndex = 0;
            // 
            // txt_CountLimited
            // 
            this.txt_CountLimited.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_CountLimited.Location = new System.Drawing.Point(463, 46);
            this.txt_CountLimited.MaxLength = 1;
            this.txt_CountLimited.Name = "txt_CountLimited";
            this.txt_CountLimited.ShortcutsEnabled = false;
            this.txt_CountLimited.Size = new System.Drawing.Size(45, 27);
            this.txt_CountLimited.TabIndex = 2;
            this.txt_CountLimited.Text = "3";
            this.txt_CountLimited.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_CountLimited.TextChanged += new System.EventHandler(this.txt_CountLimited_TextChanged);
            this.txt_CountLimited.Enter += new System.EventHandler(this.SelectText_Enter);
            this.txt_CountLimited.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PressOnlyDigit);
            // 
            // txt_CountVariable
            // 
            this.txt_CountVariable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_CountVariable.Location = new System.Drawing.Point(463, 12);
            this.txt_CountVariable.MaxLength = 1;
            this.txt_CountVariable.Name = "txt_CountVariable";
            this.txt_CountVariable.ShortcutsEnabled = false;
            this.txt_CountVariable.Size = new System.Drawing.Size(45, 27);
            this.txt_CountVariable.TabIndex = 1;
            this.txt_CountVariable.Text = "3";
            this.txt_CountVariable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_CountVariable.TextChanged += new System.EventHandler(this.txt_CountVariable_TextChanged);
            this.txt_CountVariable.Enter += new System.EventHandler(this.SelectText_Enter);
            this.txt_CountVariable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PressOnlyDigit);
            // 
            // lbl_Limitation
            // 
            this.lbl_Limitation.AutoSize = true;
            this.lbl_Limitation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Limitation.Location = new System.Drawing.Point(258, 49);
            this.lbl_Limitation.Name = "lbl_Limitation";
            this.lbl_Limitation.Size = new System.Drawing.Size(194, 20);
            this.lbl_Limitation.TabIndex = 1;
            this.lbl_Limitation.Text = "Количество ограничений: ";
            // 
            // lbl_Variable
            // 
            this.lbl_Variable.AutoSize = true;
            this.lbl_Variable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Variable.Location = new System.Drawing.Point(258, 15);
            this.lbl_Variable.Name = "lbl_Variable";
            this.lbl_Variable.Size = new System.Drawing.Size(190, 20);
            this.lbl_Variable.TabIndex = 0;
            this.lbl_Variable.Text = "Количество переменных: ";
            // 
            // pnl_Button
            // 
            this.pnl_Button.Controls.Add(this.btn_Solve);
            this.pnl_Button.Controls.Add(this.btn_Clear);
            this.pnl_Button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Button.Location = new System.Drawing.Point(0, 387);
            this.pnl_Button.Name = "pnl_Button";
            this.pnl_Button.Size = new System.Drawing.Size(769, 63);
            this.pnl_Button.TabIndex = 1;
            // 
            // btn_Solve
            // 
            this.btn_Solve.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Solve.Location = new System.Drawing.Point(433, 13);
            this.btn_Solve.Name = "btn_Solve";
            this.btn_Solve.Size = new System.Drawing.Size(106, 38);
            this.btn_Solve.TabIndex = 98;
            this.btn_Solve.Text = "Решить";
            this.btn_Solve.UseVisualStyleBackColor = true;
            this.btn_Solve.Click += new System.EventHandler(this.btn_Solve_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Clear.Location = new System.Drawing.Point(240, 13);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(106, 38);
            this.btn_Clear.TabIndex = 99;
            this.btn_Clear.Text = "Очистить";
            this.btn_Clear.UseVisualStyleBackColor = true;
            // 
            // tblLP_Main
            // 
            this.tblLP_Main.ColumnCount = 1;
            this.tblLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLP_Main.Controls.Add(this.groupBox2, 0, 1);
            this.tblLP_Main.Controls.Add(this.groupBox1, 0, 0);
            this.tblLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLP_Main.Location = new System.Drawing.Point(0, 89);
            this.tblLP_Main.Name = "tblLP_Main";
            this.tblLP_Main.RowCount = 2;
            this.tblLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.89262F));
            this.tblLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.10738F));
            this.tblLP_Main.Size = new System.Drawing.Size(769, 298);
            this.tblLP_Main.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnl_LimitedFunction);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(3, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(763, 191);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ограничения";
            // 
            // pnl_LimitedFunction
            // 
            this.pnl_LimitedFunction.AutoScroll = true;
            this.pnl_LimitedFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LimitedFunction.Location = new System.Drawing.Point(3, 23);
            this.pnl_LimitedFunction.Name = "pnl_LimitedFunction";
            this.pnl_LimitedFunction.Size = new System.Drawing.Size(757, 165);
            this.pnl_LimitedFunction.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnl_TargetFunction);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Целевая функция";
            // 
            // pnl_TargetFunction
            // 
            this.pnl_TargetFunction.AutoScroll = true;
            this.pnl_TargetFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TargetFunction.Location = new System.Drawing.Point(3, 23);
            this.pnl_TargetFunction.Name = "pnl_TargetFunction";
            this.pnl_TargetFunction.Size = new System.Drawing.Size(757, 69);
            this.pnl_TargetFunction.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 450);
            this.Controls.Add(this.tblLP_Main);
            this.Controls.Add(this.pnl_Button);
            this.Controls.Add(this.pnl_Choose);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Симплекс-метод";
            this.pnl_Choose.ResumeLayout(false);
            this.pnl_Choose.PerformLayout();
            this.pnl_Button.ResumeLayout(false);
            this.tblLP_Main.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Choose;
        private System.Windows.Forms.Panel pnl_Button;
        private System.Windows.Forms.TextBox txt_CountLimited;
        private System.Windows.Forms.TextBox txt_CountVariable;
        private System.Windows.Forms.Label lbl_Limitation;
        private System.Windows.Forms.Label lbl_Variable;
        private System.Windows.Forms.Button btn_Solve;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.TableLayoutPanel tblLP_Main;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnl_LimitedFunction;
        private System.Windows.Forms.Panel pnl_TargetFunction;
    }
}

