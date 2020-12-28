
namespace PainelDeSenhas
{
    partial class FormChamaSenha
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
            this.lbSenha = new System.Windows.Forms.Label();
            this.lbSenhaNum = new System.Windows.Forms.Label();
            this.lbLocal = new System.Windows.Forms.Label();
            this.lbLocalNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbSenha
            // 
            this.lbSenha.Font = new System.Drawing.Font("Arial", 90F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSenha.ForeColor = System.Drawing.Color.Gold;
            this.lbSenha.Location = new System.Drawing.Point(-2, 29);
            this.lbSenha.Name = "lbSenha";
            this.lbSenha.Size = new System.Drawing.Size(1355, 124);
            this.lbSenha.TabIndex = 0;
            this.lbSenha.Text = "SENHA";
            this.lbSenha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSenhaNum
            // 
            this.lbSenhaNum.Font = new System.Drawing.Font("Arial", 120F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSenhaNum.ForeColor = System.Drawing.Color.White;
            this.lbSenhaNum.Location = new System.Drawing.Point(-1, 172);
            this.lbSenhaNum.Name = "lbSenhaNum";
            this.lbSenhaNum.Size = new System.Drawing.Size(1354, 168);
            this.lbSenhaNum.TabIndex = 1;
            this.lbSenhaNum.Text = "515";
            this.lbSenhaNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLocal
            // 
            this.lbLocal.Font = new System.Drawing.Font("Arial", 90F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLocal.ForeColor = System.Drawing.Color.Gold;
            this.lbLocal.Location = new System.Drawing.Point(-1, 407);
            this.lbLocal.Name = "lbLocal";
            this.lbLocal.Size = new System.Drawing.Size(1369, 130);
            this.lbLocal.TabIndex = 2;
            this.lbLocal.Text = "MESA";
            this.lbLocal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLocalNum
            // 
            this.lbLocalNum.Font = new System.Drawing.Font("Arial", 120F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLocalNum.ForeColor = System.Drawing.Color.White;
            this.lbLocalNum.Location = new System.Drawing.Point(-2, 548);
            this.lbLocalNum.Name = "lbLocalNum";
            this.lbLocalNum.Size = new System.Drawing.Size(1370, 171);
            this.lbLocalNum.TabIndex = 3;
            this.lbLocalNum.Text = "1";
            this.lbLocalNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormChamaSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1369, 733);
            this.Controls.Add(this.lbLocal);
            this.Controls.Add(this.lbLocalNum);
            this.Controls.Add(this.lbSenha);
            this.Controls.Add(this.lbSenhaNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChamaSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChamaSenha_FormClosed);
            this.Load += new System.EventHandler(this.FormChamaSenha_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbSenha;
        private System.Windows.Forms.Label lbSenhaNum;
        private System.Windows.Forms.Label lbLocal;
        private System.Windows.Forms.Label lbLocalNum;
    }
}