namespace NAVASCA_PROEL1Project
{
    partial class ForgotEmailValidation
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotEmailValidation));
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.picBack = new System.Windows.Forms.PictureBox();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.btnConfirm = new Guna.UI2.WinForms.Guna2Button();
			this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
			((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.DimGray;
			this.label6.Location = new System.Drawing.Point(275, 229);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(255, 34);
			this.label6.TabIndex = 27;
			this.label6.Text = "Before you can continue, you need to \r\nconfirm your email address. ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(248, 182);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(309, 38);
			this.label3.TabIndex = 26;
			this.label3.Text = "Email Confirmation";
			// 
			// picBack
			// 
			this.picBack.BackColor = System.Drawing.Color.Transparent;
			this.picBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBack.BackgroundImage")));
			this.picBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.picBack.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picBack.Location = new System.Drawing.Point(12, 12);
			this.picBack.Name = "picBack";
			this.picBack.Size = new System.Drawing.Size(62, 67);
			this.picBack.TabIndex = 31;
			this.picBack.TabStop = false;
			this.picBack.Click += new System.EventHandler(this.picBack_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(80, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(109, 19);
			this.label2.TabIndex = 32;
			this.label2.Text = "Back to login";
			// 
			// btnConfirm
			// 
			this.btnConfirm.Animated = true;
			this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
			this.btnConfirm.BorderRadius = 14;
			this.btnConfirm.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnConfirm.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnConfirm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnConfirm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnConfirm.FillColor = System.Drawing.Color.CornflowerBlue;
			this.btnConfirm.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold);
			this.btnConfirm.ForeColor = System.Drawing.Color.White;
			this.btnConfirm.HoverState.FillColor = System.Drawing.Color.RoyalBlue;
			this.btnConfirm.Location = new System.Drawing.Point(293, 415);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(214, 45);
			this.btnConfirm.TabIndex = 47;
			this.btnConfirm.Text = "Confirm";
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
			// 
			// txtEmail
			// 
			this.txtEmail.BackColor = System.Drawing.Color.Transparent;
			this.txtEmail.BorderRadius = 14;
			this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmail.DefaultText = "";
			this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtEmail.Font = new System.Drawing.Font("Century Gothic", 11.25F);
			this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtEmail.Location = new System.Drawing.Point(255, 323);
			this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.PlaceholderText = "Enter your Email Address";
			this.txtEmail.SelectedText = "";
			this.txtEmail.Size = new System.Drawing.Size(302, 39);
			this.txtEmail.TabIndex = 48;
			// 
			// ForgotEmailValidation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(784, 611);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.picBack);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Name = "ForgotEmailValidation";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ForgotEmailValidation";
			((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picBack;
        private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Label label2;
		private Guna.UI2.WinForms.Guna2Button btnConfirm;
		private Guna.UI2.WinForms.Guna2TextBox txtEmail;
	}
}