namespace NAVASCA_PROEL1Project
{
	partial class AdminStudentAddSubject
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminStudentAddSubject));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label5 = new System.Windows.Forms.Label();
			this.picBack = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.CoursesData = new Guna.UI2.WinForms.Guna2DataGridView();
			this.btnSubmit = new Guna.UI2.WinForms.Guna2Button();
			((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CoursesData)).BeginInit();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(80, 35);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(133, 19);
			this.label5.TabIndex = 99;
			this.label5.Text = "Back to Students";
			// 
			// picBack
			// 
			this.picBack.BackColor = System.Drawing.Color.Transparent;
			this.picBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBack.BackgroundImage")));
			this.picBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.picBack.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picBack.Location = new System.Drawing.Point(12, 12);
			this.picBack.Name = "picBack";
			this.picBack.Size = new System.Drawing.Size(62, 60);
			this.picBack.TabIndex = 98;
			this.picBack.TabStop = false;
			this.picBack.Click += new System.EventHandler(this.picBack_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.DimGray;
			this.label6.Location = new System.Drawing.Point(277, 147);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(231, 17);
			this.label6.TabIndex = 110;
			this.label6.Text = "View here to see subjects to enroll.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(282, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(221, 38);
			this.label3.TabIndex = 109;
			this.label3.Text = "Add Subjects";
			// 
			// CoursesData
			// 
			this.CoursesData.AllowUserToResizeRows = false;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.CoursesData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
			this.CoursesData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.CoursesData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(234)))), ((int)(((byte)(243)))));
			this.CoursesData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(150)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(150)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.CoursesData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.CoursesData.ColumnHeadersHeight = 35;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.CoursesData.DefaultCellStyle = dataGridViewCellStyle7;
			this.CoursesData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(157)))), ((int)(((byte)(189)))));
			this.CoursesData.Location = new System.Drawing.Point(123, 192);
			this.CoursesData.Name = "CoursesData";
			this.CoursesData.ReadOnly = true;
			this.CoursesData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.CoursesData.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.CoursesData.RowHeadersVisible = false;
			this.CoursesData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.CoursesData.RowTemplate.Height = 30;
			this.CoursesData.Size = new System.Drawing.Size(539, 305);
			this.CoursesData.TabIndex = 116;
			this.CoursesData.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Blue;
			this.CoursesData.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
			this.CoursesData.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CoursesData.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.CoursesData.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			this.CoursesData.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.CoursesData.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(234)))), ((int)(((byte)(243)))));
			this.CoursesData.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(157)))), ((int)(((byte)(189)))));
			this.CoursesData.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(150)))), ((int)(((byte)(232)))));
			this.CoursesData.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.CoursesData.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CoursesData.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
			this.CoursesData.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.CoursesData.ThemeStyle.HeaderStyle.Height = 35;
			this.CoursesData.ThemeStyle.ReadOnly = true;
			this.CoursesData.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
			this.CoursesData.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.CoursesData.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CoursesData.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.CoursesData.ThemeStyle.RowsStyle.Height = 30;
			this.CoursesData.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			this.CoursesData.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			// 
			// btnSubmit
			// 
			this.btnSubmit.Animated = true;
			this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
			this.btnSubmit.BorderRadius = 14;
			this.btnSubmit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnSubmit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnSubmit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnSubmit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnSubmit.FillColor = System.Drawing.Color.CornflowerBlue;
			this.btnSubmit.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold);
			this.btnSubmit.ForeColor = System.Drawing.Color.White;
			this.btnSubmit.HoverState.FillColor = System.Drawing.Color.RoyalBlue;
			this.btnSubmit.Location = new System.Drawing.Point(285, 531);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(214, 45);
			this.btnSubmit.TabIndex = 117;
			this.btnSubmit.Text = "Submit";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// AdminStudentAddSubject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(784, 611);
			this.Controls.Add(this.btnSubmit);
			this.Controls.Add(this.CoursesData);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.picBack);
			this.Name = "AdminStudentAddSubject";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AdminStudentAddSubject";
			((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CoursesData)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox picBack;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private Guna.UI2.WinForms.Guna2DataGridView CoursesData;
		private Guna.UI2.WinForms.Guna2Button btnSubmit;
	}
}