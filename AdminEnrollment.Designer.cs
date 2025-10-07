namespace NAVASCA_PROEL1Project
{
	partial class AdminEnrollment
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminEnrollment));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			this.picBack = new System.Windows.Forms.PictureBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnSubmit = new Guna.UI2.WinForms.Guna2Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbSemester = new Guna.UI2.WinForms.Guna2ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.cmbProgram = new Guna.UI2.WinForms.Guna2ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbSection = new Guna.UI2.WinForms.Guna2ComboBox();
			this.label = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
			this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.CoursesData = new Guna.UI2.WinForms.Guna2DataGridView();
			this.label9 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CoursesData)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// picBack
			// 
			this.picBack.BackColor = System.Drawing.Color.Transparent;
			this.picBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picBack.BackgroundImage")));
			this.picBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.picBack.Cursor = System.Windows.Forms.Cursors.Hand;
			this.picBack.Location = new System.Drawing.Point(12, 7);
			this.picBack.Name = "picBack";
			this.picBack.Size = new System.Drawing.Size(62, 60);
			this.picBack.TabIndex = 92;
			this.picBack.TabStop = false;
			this.picBack.Click += new System.EventHandler(this.picBack_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(80, 30);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(133, 19);
			this.label5.TabIndex = 93;
			this.label5.Text = "Back to Students";
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
			this.btnSubmit.Location = new System.Drawing.Point(214, 750);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(214, 45);
			this.btnSubmit.TabIndex = 105;
			this.btnSubmit.Text = "Submit";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.DimGray;
			this.label6.Location = new System.Drawing.Point(186, 59);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(271, 17);
			this.label6.TabIndex = 104;
			this.label6.Text = "Complete the form to enroll the student.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(190, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(262, 38);
			this.label3.TabIndex = 103;
			this.label3.Text = "Enrollment Form";
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
			this.cmbSemester.Location = new System.Drawing.Point(82, 130);
			this.cmbSemester.Name = "cmbSemester";
			this.cmbSemester.Size = new System.Drawing.Size(479, 36);
			this.cmbSemester.TabIndex = 110;
			this.cmbSemester.SelectedIndexChanged += new System.EventHandler(this.cmbSemester_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label7.Location = new System.Drawing.Point(84, 107);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(79, 20);
			this.label7.TabIndex = 109;
			this.label7.Text = "Semester:";
			// 
			// cmbProgram
			// 
			this.cmbProgram.BackColor = System.Drawing.Color.Transparent;
			this.cmbProgram.BorderRadius = 16;
			this.cmbProgram.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cmbProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbProgram.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cmbProgram.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.cmbProgram.Font = new System.Drawing.Font("Century Gothic", 9.75F);
			this.cmbProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
			this.cmbProgram.ItemHeight = 30;
			this.cmbProgram.Items.AddRange(new object[] {
            "BSEd-ENG (BACHELOR OF SECONDARY EDUCATION - ENGLISH)",
            "BSEd-MATH (BACHELOR OF SECONDARY EDUCATION - MATH)",
            "BSIT (BACHELOR OF SCIENCE IN INFORMATION TECHNOLOGY)",
            "BSCS (BACHELOR OF SCIENCE IN COMPUTER SCIENCE)",
            "BSBA-FM (BACHELOR OF SCIENCE IN BUSINESS ADMINISTRATION - FINANCIAL MANAGEMENT)",
            "BSBA-MM (BACHELOR OF SCIENCE IN BUSINESS ADMINISTRATION MARJOR IN MARKETING MANAG" +
                "EMENT))",
            "BSN (BACHELOR OF SCIENCE IN NURSING)"});
			this.cmbProgram.Location = new System.Drawing.Point(82, 203);
			this.cmbProgram.Name = "cmbProgram";
			this.cmbProgram.Size = new System.Drawing.Size(479, 36);
			this.cmbProgram.TabIndex = 112;
			this.cmbProgram.SelectedIndexChanged += new System.EventHandler(this.cmbProgram_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label1.Location = new System.Drawing.Point(84, 180);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 20);
			this.label1.TabIndex = 111;
			this.label1.Text = "Program:";
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
			this.cmbSection.Location = new System.Drawing.Point(82, 278);
			this.cmbSection.Name = "cmbSection";
			this.cmbSection.Size = new System.Drawing.Size(479, 36);
			this.cmbSection.TabIndex = 114;
			// 
			// label
			// 
			this.label.AutoSize = true;
			this.label.BackColor = System.Drawing.Color.Transparent;
			this.label.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label.Location = new System.Drawing.Point(84, 255);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(67, 20);
			this.label.TabIndex = 113;
			this.label.Text = "Section:";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// errorProvider2
			// 
			this.errorProvider2.ContainerControl = this;
			// 
			// errorProvider3
			// 
			this.errorProvider3.ContainerControl = this;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.CoursesData);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.cmbSection);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label);
			this.panel1.Controls.Add(this.btnSubmit);
			this.panel1.Controls.Add(this.cmbProgram);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.cmbSemester);
			this.panel1.Location = new System.Drawing.Point(54, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(659, 830);
			this.panel1.TabIndex = 115;
			// 
			// CoursesData
			// 
			this.CoursesData.AllowUserToResizeRows = false;
			dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle17.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.CoursesData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
			this.CoursesData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.CoursesData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(234)))), ((int)(((byte)(243)))));
			this.CoursesData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(150)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(150)))), ((int)(((byte)(232)))));
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.CoursesData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
			this.CoursesData.ColumnHeadersHeight = 35;
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle19.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(201)))), ((int)(((byte)(228)))));
			dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.CoursesData.DefaultCellStyle = dataGridViewCellStyle19;
			this.CoursesData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(157)))), ((int)(((byte)(189)))));
			this.CoursesData.Location = new System.Drawing.Point(82, 396);
			this.CoursesData.Name = "CoursesData";
			this.CoursesData.ReadOnly = true;
			this.CoursesData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.CoursesData.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
			this.CoursesData.RowHeadersVisible = false;
			this.CoursesData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.CoursesData.RowTemplate.Height = 30;
			this.CoursesData.Size = new System.Drawing.Size(479, 304);
			this.CoursesData.TabIndex = 115;
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
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label9.Location = new System.Drawing.Point(264, 362);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(130, 20);
			this.label9.TabIndex = 119;
			this.label9.Text = "Subjects to Enroll";
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Location = new System.Drawing.Point(0, 79);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(784, 533);
			this.panel2.TabIndex = 116;
			// 
			// AdminEnrollment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(784, 611);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.picBack);
			this.Name = "AdminEnrollment";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AdminEnrollment";
			this.Load += new System.EventHandler(this.AdminEnrollment_Load);
			((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CoursesData)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picBack;
		private System.Windows.Forms.Label label5;
		private Guna.UI2.WinForms.Guna2Button btnSubmit;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private Guna.UI2.WinForms.Guna2ComboBox cmbSemester;
		private System.Windows.Forms.Label label7;
		private Guna.UI2.WinForms.Guna2ComboBox cmbProgram;
		private System.Windows.Forms.Label label1;
		private Guna.UI2.WinForms.Guna2ComboBox cmbSection;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ErrorProvider errorProvider2;
		private System.Windows.Forms.ErrorProvider errorProvider3;
		private System.Windows.Forms.Panel panel1;
		private Guna.UI2.WinForms.Guna2DataGridView CoursesData;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Panel panel2;
	}
}