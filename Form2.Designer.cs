
namespace PainelDeSenhas
{
    partial class FormConf
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtSom = new System.Windows.Forms.TextBox();
            this.NumVol = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.CbVozes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtWebSocketUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnSalvar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumVol)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Arquivo de Som";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TxtSom
            // 
            this.TxtSom.Location = new System.Drawing.Point(12, 24);
            this.TxtSom.Name = "TxtSom";
            this.TxtSom.Size = new System.Drawing.Size(291, 20);
            this.TxtSom.TabIndex = 1;
            // 
            // NumVol
            // 
            this.NumVol.Location = new System.Drawing.Point(12, 76);
            this.NumVol.Name = "NumVol";
            this.NumVol.Size = new System.Drawing.Size(291, 20);
            this.NumVol.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Volume";
            // 
            // CbVozes
            // 
            this.CbVozes.FormattingEnabled = true;
            this.CbVozes.Location = new System.Drawing.Point(12, 128);
            this.CbVozes.Name = "CbVozes";
            this.CbVozes.Size = new System.Drawing.Size(291, 21);
            this.CbVozes.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Voz";
            // 
            // TxtWebSocketUrl
            // 
            this.TxtWebSocketUrl.Location = new System.Drawing.Point(12, 180);
            this.TxtWebSocketUrl.Name = "TxtWebSocketUrl";
            this.TxtWebSocketUrl.Size = new System.Drawing.Size(291, 20);
            this.TxtWebSocketUrl.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "WebSocketIP";
            // 
            // BtnSalvar
            // 
            this.BtnSalvar.Location = new System.Drawing.Point(12, 220);
            this.BtnSalvar.Name = "BtnSalvar";
            this.BtnSalvar.Size = new System.Drawing.Size(79, 23);
            this.BtnSalvar.TabIndex = 9;
            this.BtnSalvar.Text = "Salvar";
            this.BtnSalvar.UseVisualStyleBackColor = true;
            this.BtnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Location = new System.Drawing.Point(123, 220);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(80, 23);
            this.BtnCancelar.TabIndex = 10;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 264);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnSalvar);
            this.Controls.Add(this.TxtWebSocketUrl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CbVozes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NumVol);
            this.Controls.Add(this.TxtSom);
            this.Controls.Add(this.label1);
            this.Name = "FormConf";
            this.Text = "Configuração";
            this.Load += new System.EventHandler(this.Conf_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumVol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtSom;
        private System.Windows.Forms.NumericUpDown NumVol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CbVozes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtWebSocketUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnSalvar;
        private System.Windows.Forms.Button BtnCancelar;
    }
}