namespace Select_And_Translate
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxOnOff = new System.Windows.Forms.PictureBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.radioManuelMode = new System.Windows.Forms.RadioButton();
            this.radioPreDefinedSelection = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fromLanguage = new System.Windows.Forms.ComboBox();
            this.toLanguage = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOnOff)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOnOff
            // 
            this.pictureBoxOnOff.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxOnOff.BackgroundImage")));
            this.pictureBoxOnOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxOnOff.Location = new System.Drawing.Point(2, 12);
            this.pictureBoxOnOff.Name = "pictureBoxOnOff";
            this.pictureBoxOnOff.Size = new System.Drawing.Size(86, 50);
            this.pictureBoxOnOff.TabIndex = 0;
            this.pictureBoxOnOff.TabStop = false;
            this.pictureBoxOnOff.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblMode
            // 
            this.lblMode.ForeColor = System.Drawing.Color.White;
            this.lblMode.Location = new System.Drawing.Point(12, 79);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(40, 23);
            this.lblMode.TabIndex = 1;
            this.lblMode.Text = "Mode:";
            // 
            // radioManuelMode
            // 
            this.radioManuelMode.AutoSize = true;
            this.radioManuelMode.ForeColor = System.Drawing.Color.White;
            this.radioManuelMode.Location = new System.Drawing.Point(58, 68);
            this.radioManuelMode.Name = "radioManuelMode";
            this.radioManuelMode.Size = new System.Drawing.Size(107, 17);
            this.radioManuelMode.TabIndex = 2;
            this.radioManuelMode.TabStop = true;
            this.radioManuelMode.Text = "Manuel Selection";
            this.radioManuelMode.UseVisualStyleBackColor = true;
            this.radioManuelMode.CheckedChanged += new System.EventHandler(this.radioManuelMode_CheckedChanged);
            // 
            // radioPreDefinedSelection
            // 
            this.radioPreDefinedSelection.AutoSize = true;
            this.radioPreDefinedSelection.ForeColor = System.Drawing.Color.White;
            this.radioPreDefinedSelection.Location = new System.Drawing.Point(58, 85);
            this.radioPreDefinedSelection.Name = "radioPreDefinedSelection";
            this.radioPreDefinedSelection.Size = new System.Drawing.Size(123, 17);
            this.radioPreDefinedSelection.TabIndex = 3;
            this.radioPreDefinedSelection.TabStop = true;
            this.radioPreDefinedSelection.Text = "Predefined Selection";
            this.radioPreDefinedSelection.UseVisualStyleBackColor = true;
            this.radioPreDefinedSelection.CheckedChanged += new System.EventHandler(this.radioPreDefinedSelection_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(94, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(104, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "To:";
            // 
            // fromLanguage
            // 
            this.fromLanguage.FormattingEnabled = true;
            this.fromLanguage.Location = new System.Drawing.Point(135, 12);
            this.fromLanguage.Name = "fromLanguage";
            this.fromLanguage.Size = new System.Drawing.Size(87, 21);
            this.fromLanguage.TabIndex = 8;
            // 
            // toLanguage
            // 
            this.toLanguage.FormattingEnabled = true;
            this.toLanguage.Location = new System.Drawing.Point(135, 37);
            this.toLanguage.Name = "toLanguage";
            this.toLanguage.Size = new System.Drawing.Size(87, 21);
            this.toLanguage.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(234, 71);
            this.Controls.Add(this.toLanguage);
            this.Controls.Add(this.fromLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioPreDefinedSelection);
            this.Controls.Add(this.radioManuelMode);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.pictureBoxOnOff);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 110);
            this.MinimumSize = new System.Drawing.Size(250, 110);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Translator";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOnOff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblMode;

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOnOff;
        private System.Windows.Forms.RadioButton radioManuelMode;
        private System.Windows.Forms.RadioButton radioPreDefinedSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox fromLanguage;
        private System.Windows.Forms.ComboBox toLanguage;
    }
}