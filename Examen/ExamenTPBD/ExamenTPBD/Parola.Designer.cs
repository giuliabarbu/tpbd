namespace ExamenTPBD
{
    partial class Parola
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
            this.labelParola = new System.Windows.Forms.Label();
            this.parolaTextBox = new System.Windows.Forms.TextBox();
            this.parolaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelParola
            // 
            this.labelParola.AutoSize = true;
            this.labelParola.Location = new System.Drawing.Point(23, 133);
            this.labelParola.Name = "labelParola";
            this.labelParola.Size = new System.Drawing.Size(206, 13);
            this.labelParola.TabIndex = 0;
            this.labelParola.Text = "Introduceti parola pentru a putea modifica:";
            // 
            // parolaTextBox
            // 
            this.parolaTextBox.Location = new System.Drawing.Point(275, 133);
            this.parolaTextBox.Name = "parolaTextBox";
            this.parolaTextBox.PasswordChar = '*';
            this.parolaTextBox.Size = new System.Drawing.Size(164, 20);
            this.parolaTextBox.TabIndex = 1;
            // 
            // parolaButton
            // 
            this.parolaButton.Location = new System.Drawing.Point(145, 196);
            this.parolaButton.Name = "parolaButton";
            this.parolaButton.Size = new System.Drawing.Size(193, 27);
            this.parolaButton.TabIndex = 2;
            this.parolaButton.Text = "OK";
            this.parolaButton.UseVisualStyleBackColor = true;
            this.parolaButton.Click += new System.EventHandler(this.parolaButton_Click);
            // 
            // Parola
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 404);
            this.Controls.Add(this.parolaButton);
            this.Controls.Add(this.parolaTextBox);
            this.Controls.Add(this.labelParola);
            this.Name = "Parola";
            this.Text = "Parola";
            this.Load += new System.EventHandler(this.Parola_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelParola;
        private System.Windows.Forms.TextBox parolaTextBox;
        private System.Windows.Forms.Button parolaButton;
    }
}