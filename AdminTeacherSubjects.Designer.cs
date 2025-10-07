namespace NAVASCA_PROEL1Project
{
	partial class AdminTeacherSubjects
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminTeacherSubjects));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label5 = new System.Windows.Forms.Label();
			this.picBack = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbSemester = new Guna.UI2.WinForms.Guna2ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.cmbSection = new Guna.UI2.WinForms.Guna2ComboBox();
			this.label = new System.Windows.Forms.Label();
			this.CoursesData = new Guna.UI2.WinForms.Guna2DataGridView();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CoursesData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
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
			this.label5.Size = new System.Drawing.Size(139, 19);
			this.label5.TabIndex = 97;
			this.label5.Text = "Back to Teachers";
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
			this.picBack.TabIndex = 96;
			this.picBack.TabStop = false;
			this.picBack.Click += new System.EventHandler(this.picBack_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.DimGray;
			this.label6.Location = new System.Drawing.Point(372, 150);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(241, 17);
			this.label6.TabIndex = 108;
			this.label6.Text = "View here to see subjects to handle.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(349, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(286, 38);
			this.label3.TabIndex = 107;
			this.label3.Text = "Manage Subjects";
			// 
			// cmbSemester
			// 
			this.cmbSemester.BackColor = System.Drawing.Color.Transparent;
			this.cmbSemester.BorderRadius = 16;
			this.cmbSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSemester.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cmbSemester.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cmbSemester.Font = new System.Drawing.Font("Century Gothic", 9.75F);
			this.cmbSemester.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cmbSemester.ItemHeight = 30;
			this.cmbSemester.Items.AddRange(new object[] {
            "Second Semester A.Y. 2025 - 2026",
            "First Semester A.Y. 2026 - 2027"});
			this.cmbSemester.Location = new System.Drawing.Point(79, 241);
			this.cmbSemester.Name = "cmbSemester";
			this.cmbSemester.Size = new System.Drawing.Size(285, 36);
			this.cmbSemester.TabIndex = 112;
			this.cmbSemester.SelectedIndexChanged += new System.EventHandler(this.cmbSemester_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label7.Location = new System.Drawing.Point(81, 218);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(79, 20);
			this.label7.TabIndex = 111;
			this.label7.Text = "Semester:";
			// 
			// cmbSection
			// 
			this.cmbSection.BackColor = System.Drawing.Color.Transparent;
			this.cmbSection.BorderRadius = 16;
			this.cmbSection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSection.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cmbSection.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cmbSection.Font = new System.Drawing.Font("Century Gothic", 9.75F);
			this.cmbSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cmbSection.ItemHeight = 30;
			this.cmbSection.Location = new System.Drawing.Point(79, 313);
			this.cmbSection.Name = "cmbSection";
			this.cmbSection.Size = new System.Drawing.Size(285, 36);
			this.cmbSection.TabIndex = 116;
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.BackColor = System.Drawing.Color.Transparent;
			this.label.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label.Location = new System.Drawing.Point(81, 290);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(67, 20);
			this.label.TabIndex = 115;
			this.label.Text = "Section:";
			// 
			// CoursesData
			// 
			this.CoursesData.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.CoursesData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.CoursesData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.CoursesData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(234)))), ((int)(((byte)(243)))));
			this.CoursesData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(150)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(150)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.CoursesData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.CoursesData.ColumnHeadersHeight = 35;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.CoursesData.DefaultCellStyle = dataGridViewCellStyle3;
			this.CoursesData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(157)))), ((int)(((byte)(189)))));
			this.CoursesData.Location = new System.Drawing.Point(401, 218);
			this.CoursesData.Name = "CoursesData";
			this.CoursesData.ReadOnly = true;
			this.CoursesData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.CoursesData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.CoursesData.RowHeadersVisible = false;
			this.CoursesData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.CoursesData.RowTemplate.Height = 30;
			this.CoursesData.Size = new System.Drawing.Size(504, 347);
			this.CoursesData.TabIndex = 121;
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
			this.CoursesData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CoursesData_CellContentClick);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// errorProvider2
			// 
			this.errorProvider2.ContainerControl = this;
			// 
			// AdminTeacherSubjects
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(984, 641);
			this.Controls.Add(this.CoursesData);
			this.Controls.Add(this.cmbSection);
			this.Controls.Add(this.label);
			this.Controls.Add(this.cmbSemester);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.picBack);
			this.Name = "AdminTeacherSubjects";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AdminTeacherSubjects";
			this.Load += new System.EventHandler(this.AdminTeacherSubjects_Load);
			((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CoursesData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox picBack;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private Guna.UI2.WinForms.Guna2ComboBox cmbSemester;
		private System.Windows.Forms.Label label7;
		private Guna.UI2.WinForms.Guna2ComboBox cmbSection;
		private System.Windows.Forms.Label label;
		private Guna.UI2.WinForms.Guna2DataGridView CoursesData;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ErrorProvider errorProvider2;
	}
}