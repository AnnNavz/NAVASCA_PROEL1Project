namespace NAVASCA_PROEL1Project
{
	partial class ForgotConfirmPassword
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotConfirmPassword));
			this.picBack = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtNew = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtConfirm = new Guna.UI2.WinForms.Guna2TextBox();
			this.btnConfirm = new Guna.UI2.WinForms.Guna2Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
			this.SuspendLayout();
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
			this.picBack.TabIndex = 32;
			this.picBack.TabStop = false;
			this.picBack.Click += new System.EventHandler(this.picBack_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.DimGray;
			this.label6.Location = new System.Drawing.Point(248, 214);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(303, 34);
			this.label6.TabIndex = 34;
			this.label6.Text = "To change your password, you must first \r\nconfirm your current one for security p" +
    "urposes.\r\n";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(262, 166);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(266, 38);
			this.label3.TabIndex = 33;
			this.label3.Text = "Forgot Password";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(80, 37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(217, 19);
			this.label5.TabIndex = 42;
			this.label5.Text = "Back to Email Confirmation";
			// 
			// txtNew
			// 
			this.txtNew.BackColor = System.Drawing.Color.Transparent;
			this.txtNew.BorderRadius = 10;
			this.txtNew.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtNew.DefaultText = "";
			this.txtNew.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtNew.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtNew.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtNew.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtNew.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtNew.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtNew.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtNew.Location = new System.Drawing.Point(258, 294);
			this.txtNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtNew.Name = "txtNew";
			this.txtNew.PlaceholderText = "New Password";
			this.txtNew.SelectedText = "";
			this.txtNew.Size = new System.Drawing.Size(278, 33);
			this.txtNew.TabIndex = 44;
			// 
			// txtConfirm
			// 
			this.txtConfirm.BackColor = System.Drawing.Color.Transparent;
			this.txtConfirm.BorderRadius = 10;
			this.txtConfirm.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtConfirm.DefaultText = "";
			this.txtConfirm.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtConfirm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtConfirm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtConfirm.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtConfirm.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtConfirm.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtConfirm.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtConfirm.Location = new System.Drawing.Point(258, 338);
			this.txtConfirm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtConfirm.Name = "txtConfirm";
			this.txtConfirm.PlaceholderText = "Confirm Password";
			this.txtConfirm.SelectedText = "";
			this.txtConfirm.Size = new System.Drawing.Size(278, 33);
			this.txtConfirm.TabIndex = 45;
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
			this.btnConfirm.Location = new System.Drawing.Point(289, 442);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(214, 45);
			this.btnConfirm.TabIndex = 46;
			this.btnConfirm.Text = "Confirm";
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click_1);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// errorProvider2
			// 
			this.errorProvider2.ContainerControl = this;
			// 
			// ForgotConfirmPassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(784, 611);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.txtConfirm);
			this.Controls.Add(this.txtNew);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.picBack);
			this.Name = "ForgotConfirmPassword";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ForgotConfirmPassword";
			((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picBack;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private Guna.UI2.WinForms.Guna2TextBox txtNew;
		private Guna.UI2.WinForms.Guna2TextBox txtConfirm;
		private Guna.UI2.WinForms.Guna2Button btnConfirm;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ErrorProvider errorProvider2;
	}
}