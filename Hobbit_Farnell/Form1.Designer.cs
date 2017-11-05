namespace Hobbit_Farnell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.GenerateFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.directoryPath = new System.Windows.Forms.TextBox();
            this.AddFile = new System.Windows.Forms.Button();
            this.NumCount4 = new System.Windows.Forms.NumericUpDown();
            this.NumCount3 = new System.Windows.Forms.NumericUpDown();
            this.NumCount2 = new System.Windows.Forms.NumericUpDown();
            this.NumCount1 = new System.Windows.Forms.NumericUpDown();
            this.NumDpsPerOneSeries = new System.Windows.Forms.NumericUpDown();
            this.lbl2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NumCount4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumCount3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumCount2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumCount1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumDpsPerOneSeries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerateFile
            // 
            this.GenerateFile.Enabled = false;
            this.GenerateFile.Location = new System.Drawing.Point(357, 167);
            this.GenerateFile.Name = "GenerateFile";
            this.GenerateFile.Size = new System.Drawing.Size(112, 23);
            this.GenerateFile.TabIndex = 9;
            this.GenerateFile.Text = "Generate Price List";
            this.GenerateFile.UseVisualStyleBackColor = true;
            this.GenerateFile.Click += new System.EventHandler(this.GenerateFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "set count of series for which you want generate price list";
            // 
            // directoryPath
            // 
            this.directoryPath.Enabled = false;
            this.directoryPath.Location = new System.Drawing.Point(12, 92);
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.Size = new System.Drawing.Size(298, 20);
            this.directoryPath.TabIndex = 15;
            // 
            // AddFile
            // 
            this.AddFile.Location = new System.Drawing.Point(255, 167);
            this.AddFile.Name = "AddFile";
            this.AddFile.Size = new System.Drawing.Size(96, 23);
            this.AddFile.TabIndex = 16;
            this.AddFile.Text = "Add Altium xlsx";
            this.AddFile.UseVisualStyleBackColor = true;
            this.AddFile.Click += new System.EventHandler(this.AddFile_Click);
            // 
            // NumCount4
            // 
            this.NumCount4.Location = new System.Drawing.Point(240, 36);
            this.NumCount4.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumCount4.Name = "NumCount4";
            this.NumCount4.Size = new System.Drawing.Size(69, 20);
            this.NumCount4.TabIndex = 17;
            // 
            // NumCount3
            // 
            this.NumCount3.Location = new System.Drawing.Point(164, 37);
            this.NumCount3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumCount3.Name = "NumCount3";
            this.NumCount3.Size = new System.Drawing.Size(69, 20);
            this.NumCount3.TabIndex = 18;
            // 
            // NumCount2
            // 
            this.NumCount2.Location = new System.Drawing.Point(88, 37);
            this.NumCount2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumCount2.Name = "NumCount2";
            this.NumCount2.Size = new System.Drawing.Size(69, 20);
            this.NumCount2.TabIndex = 19;
            // 
            // NumCount1
            // 
            this.NumCount1.Location = new System.Drawing.Point(12, 35);
            this.NumCount1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumCount1.Name = "NumCount1";
            this.NumCount1.Size = new System.Drawing.Size(69, 20);
            this.NumCount1.TabIndex = 20;
            // 
            // NumDpsPerOneSeries
            // 
            this.NumDpsPerOneSeries.Location = new System.Drawing.Point(16, 170);
            this.NumDpsPerOneSeries.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumDpsPerOneSeries.Name = "NumDpsPerOneSeries";
            this.NumDpsPerOneSeries.Size = new System.Drawing.Size(69, 20);
            this.NumDpsPerOneSeries.TabIndex = 21;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(16, 151);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(126, 13);
            this.lbl2.TabIndex = 22;
            this.lbl2.Text = "Number dps in one series";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(315, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(154, 149);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 202);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.NumDpsPerOneSeries);
            this.Controls.Add(this.NumCount1);
            this.Controls.Add(this.NumCount2);
            this.Controls.Add(this.NumCount3);
            this.Controls.Add(this.NumCount4);
            this.Controls.Add(this.AddFile);
            this.Controls.Add(this.directoryPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GenerateFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Generator price list";
            ((System.ComponentModel.ISupportInitialize)(this.NumCount4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumCount3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumCount2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumCount1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumDpsPerOneSeries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button GenerateFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox directoryPath;
        private System.Windows.Forms.Button AddFile;
        private System.Windows.Forms.NumericUpDown NumCount4;
        private System.Windows.Forms.NumericUpDown NumCount3;
        private System.Windows.Forms.NumericUpDown NumCount2;
        private System.Windows.Forms.NumericUpDown NumCount1;
        private System.Windows.Forms.NumericUpDown NumDpsPerOneSeries;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

