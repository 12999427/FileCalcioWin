namespace FileCalcio
{
    partial class InputPromptWin
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
            txt_testoRicerca = new TextBox();
            btn_OK = new Button();
            btn_Annulla = new Button();
            SuspendLayout();
            // 
            // txt_testoRicerca
            // 
            txt_testoRicerca.Location = new Point(53, 26);
            txt_testoRicerca.Name = "txt_testoRicerca";
            txt_testoRicerca.Size = new Size(225, 23);
            txt_testoRicerca.TabIndex = 0;
            // 
            // btn_OK
            // 
            btn_OK.Location = new Point(228, 91);
            btn_OK.Name = "btn_OK";
            btn_OK.Size = new Size(75, 23);
            btn_OK.TabIndex = 1;
            btn_OK.Text = "OK";
            btn_OK.UseVisualStyleBackColor = true;
            btn_OK.Click += btn_OK_Click;
            // 
            // btn_Annulla
            // 
            btn_Annulla.Location = new Point(42, 91);
            btn_Annulla.Name = "btn_Annulla";
            btn_Annulla.Size = new Size(75, 23);
            btn_Annulla.TabIndex = 2;
            btn_Annulla.Text = "Annulla";
            btn_Annulla.UseVisualStyleBackColor = true;
            btn_Annulla.Click += btn_Annulla_Click;
            // 
            // InputPromptWin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 127);
            Controls.Add(btn_Annulla);
            Controls.Add(btn_OK);
            Controls.Add(txt_testoRicerca);
            Name = "InputPromptWin";
            Text = "InputPromptWin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_testoRicerca;
        private Button btn_OK;
        private Button btn_Annulla;
    }
}