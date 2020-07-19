namespace MIPS_Emulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.codeTXT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mips_reg_DGV = new System.Windows.Forms.DataGridView();
            this.registers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reg_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pip_reg_DGV = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.PCTXT = new System.Windows.Forms.TextBox();
            this.InitializeBTN = new System.Windows.Forms.Button();
            this.runCycleBTN = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mips_reg_DGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pip_reg_DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // codeTXT
            // 
            this.codeTXT.Location = new System.Drawing.Point(24, 61);
            this.codeTXT.Multiline = true;
            this.codeTXT.Name = "codeTXT";
            this.codeTXT.Size = new System.Drawing.Size(362, 395);
            this.codeTXT.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "User code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(431, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "MIPS Register";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(760, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Pipeline Registers";
            // 
            // mips_reg_DGV
            // 
            this.mips_reg_DGV.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.mips_reg_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mips_reg_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.registers,
            this.reg_value});
            this.mips_reg_DGV.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.mips_reg_DGV.Location = new System.Drawing.Point(434, 61);
            this.mips_reg_DGV.Name = "mips_reg_DGV";
            this.mips_reg_DGV.RowTemplate.Height = 26;
            this.mips_reg_DGV.Size = new System.Drawing.Size(284, 395);
            this.mips_reg_DGV.TabIndex = 4;
            // 
            // registers
            // 
            this.registers.HeaderText = "Registers";
            this.registers.Name = "registers";
            this.registers.Width = 120;
            // 
            // reg_value
            // 
            this.reg_value.HeaderText = "Value";
            this.reg_value.Name = "reg_value";
            this.reg_value.Width = 120;
            // 
            // pip_reg_DGV
            // 
            this.pip_reg_DGV.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.pip_reg_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pip_reg_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.pip_reg_DGV.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.pip_reg_DGV.Location = new System.Drawing.Point(763, 61);
            this.pip_reg_DGV.Name = "pip_reg_DGV";
            this.pip_reg_DGV.RowTemplate.Height = 26;
            this.pip_reg_DGV.Size = new System.Drawing.Size(513, 395);
            this.pip_reg_DGV.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 499);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "PC";
            // 
            // PCTXT
            // 
            this.PCTXT.Location = new System.Drawing.Point(55, 499);
            this.PCTXT.Name = "PCTXT";
            this.PCTXT.Size = new System.Drawing.Size(149, 24);
            this.PCTXT.TabIndex = 7;
            this.PCTXT.Text = "1000";
            // 
            // InitializeBTN
            // 
            this.InitializeBTN.Location = new System.Drawing.Point(223, 482);
            this.InitializeBTN.Name = "InitializeBTN";
            this.InitializeBTN.Size = new System.Drawing.Size(163, 56);
            this.InitializeBTN.TabIndex = 8;
            this.InitializeBTN.Text = "Initialize";
            this.InitializeBTN.UseVisualStyleBackColor = true;
            this.InitializeBTN.Click += new System.EventHandler(this.InitializeBTN_Click);
            // 
            // runCycleBTN
            // 
            this.runCycleBTN.Location = new System.Drawing.Point(434, 482);
            this.runCycleBTN.Name = "runCycleBTN";
            this.runCycleBTN.Size = new System.Drawing.Size(163, 56);
            this.runCycleBTN.TabIndex = 9;
            this.runCycleBTN.Text = "Run 1 Cycle";
            this.runCycleBTN.UseVisualStyleBackColor = true;
            this.runCycleBTN.Click += new System.EventHandler(this.runCycleBTN_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Registers";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 327670000;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 350;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 563);
            this.Controls.Add(this.runCycleBTN);
            this.Controls.Add(this.InitializeBTN);
            this.Controls.Add(this.PCTXT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pip_reg_DGV);
            this.Controls.Add(this.mips_reg_DGV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.codeTXT);
            this.Name = "Form1";
            this.Text = "MIPS Emulator";
            ((System.ComponentModel.ISupportInitialize)(this.mips_reg_DGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pip_reg_DGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox codeTXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView mips_reg_DGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn registers;
        private System.Windows.Forms.DataGridViewTextBoxColumn reg_value;
        private System.Windows.Forms.DataGridView pip_reg_DGV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PCTXT;
        private System.Windows.Forms.Button InitializeBTN;
        private System.Windows.Forms.Button runCycleBTN;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}

