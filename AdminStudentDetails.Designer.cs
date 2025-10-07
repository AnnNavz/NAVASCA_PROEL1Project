namespace NAVASCA_PROEL1Project
{
	partial class AdminStudentDetails
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminStudentDetails));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label5 = new System.Windows.Forms.Label();
			this.picBack = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSemester = new Guna.UI2.WinForms.Guna2TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtProgram = new Guna.UI2.WinForms.Guna2TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtSection = new Guna.UI2.WinForms.Guna2TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtEnrollDate = new Guna.UI2.WinForms.Guna2TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.CoursesData = new Guna.UI2.WinForms.Guna2DataGridView();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
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
			this.label5.TabIndex = 97;
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
			this.label6.Location = new System.Drawing.Point(297, 149);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(390, 17);
			this.label6.TabIndex = 108;
			this.label6.Text = "View here to see enrollment details of the selected student.";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(370, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(245, 38);
			this.label3.TabIndex = 107;
			this.label3.Text = "Student Details";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label1.Location = new System.Drawing.Point(74, 251);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 20);
			this.label1.TabIndex = 110;
			this.label1.Text = "Semester:";
			// 
			// txtSemester
			// 
			this.txtSemester.BackColor = System.Drawing.Color.Transparent;
			this.txtSemester.BorderRadius = 16;
			this.txtSemester.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSemester.DefaultText = "";
			this.txtSemester.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtSemester.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtSemester.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSemester.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSemester.Enabled = false;
			this.txtSemester.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSemester.Font = new System.Drawing.Font("Century Gothic", 9.75F);
			this.txtSemester.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.txtSemester.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSemester.Location = new System.Drawing.Point(70, 277);
			this.txtSemester.Name = "txtSemester";
			this.txtSemester.PlaceholderText = "";
			this.txtSemester.ReadOnly = true;
			this.txtSemester.SelectedText = "";
			this.txtSemester.Size = new System.Drawing.Size(413, 36);
			this.txtSemester.TabIndex = 109;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label2.Location = new System.Drawing.Point(74, 322);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 20);
			this.label2.TabIndex = 112;
			this.label2.Text = "Program:";
			// 
			// txtProgram
			// 
			this.txtProgram.BackColor = System.Drawing.Color.Transparent;
			this.txtProgram.BorderRadius = 16;
			this.txtProgram.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtProgram.DefaultText = "";
			this.txtProgram.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtProgram.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtProgram.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtProgram.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtProgram.Enabled = false;
			this.txtProgram.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtProgram.Font = new System.Drawing.Font("Century Gothic", 9.75F);
			this.txtProgram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.txtProgram.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtProgram.Location = new System.Drawing.Point(70, 348);
			this.txtProgram.Name = "txtProgram";
			this.txtProgram.PlaceholderText = "";
			this.txtProgram.ReadOnly = true;
			this.txtProgram.SelectedText = "";
			this.txtProgram.Size = new System.Drawing.Size(413, 36);
			this.txtProgram.TabIndex = 111;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label4.Location = new System.Drawing.Point(73, 206);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(240, 25);
			this.label4.TabIndex = 113;
			this.label4.Text = "Enrollment Information";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label7.Location = new System.Drawing.Point(78, 398);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(67, 20);
			this.label7.TabIndex = 115;
			this.label7.Text = "Section:";
			// 
			// txtSection
			// 
			this.txtSection.BackColor = System.Drawing.Color.Transparent;
			this.txtSection.BorderRadius = 16;
			this.txtSection.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSection.DefaultText = "";
			this.txtSection.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtSection.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtSection.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSection.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtSection.Enabled = false;
			this.txtSection.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSection.Font = new System.Drawing.Font("Century Gothic", 9.75F);
			this.txtSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.txtSection.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtSection.Location = new System.Drawing.Point(74, 424);
			this.txtSection.Name = "txtSection";
			this.txtSection.PlaceholderText = "";
			this.txtSection.ReadOnly = true;
			this.txtSection.SelectedText = "";
			this.txtSection.Size = new System.Drawing.Size(409, 36);
			this.txtSection.TabIndex = 114;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label8.Location = new System.Drawing.Point(78, 476);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(111, 20);
			this.label8.TabIndex = 117;
			this.label8.Text = "Enrolled Date:";
			// 
			// txtEnrollDate
			// 
			this.txtEnrollDate.BackColor = System.Drawing.Color.Transparent;
			this.txtEnrollDate.BorderRadius = 16;
			this.txtEnrollDate.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEnrollDate.DefaultText = "";
			this.txtEnrollDate.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txtEnrollDate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txtEnrollDate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtEnrollDate.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txtEnrollDate.Enabled = false;
			this.txtEnrollDate.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtEnrollDate.Font = new System.Drawing.Font("Century Gothic", 9.75F);
			this.txtEnrollDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.txtEnrollDate.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txtEnrollDate.Location = new System.Drawing.Point(74, 502);
			this.txtEnrollDate.Name = "txtEnrollDate";
			this.txtEnrollDate.PlaceholderText = "";
			this.txtEnrollDate.ReadOnly = true;
			this.txtEnrollDate.SelectedText = "";
			this.txtEnrollDate.Size = new System.Drawing.Size(251, 36);
			this.txtEnrollDate.TabIndex = 116;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(177)))), ((int)(((byte)(189)))));
			this.label9.Location = new System.Drawing.Point(528, 206);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(183, 25);
			this.label9.TabIndex = 118;
			this.label9.Text = "Subjects Enrolled";
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
			this.CoursesData.Location = new System.Drawing.Point(533, 251);
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
			this.CoursesData.Size = new System.Drawing.Size(381, 287);
			this.CoursesData.TabIndex = 119;
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
			// btnAdd
			// 
			this.btnAdd.Animated = true;
			this.btnAdd.BackColor = System.Drawing.Color.Transparent;
			this.btnAdd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(126)))), ((int)(((byte)(61)))));
			this.btnAdd.BorderRadius = 18;
			this.btnAdd.BorderThickness = 1;
			this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAdd.FillColor = System.Drawing.Color.Orange;
			this.btnAdd.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
			this.btnAdd.ForeColor = System.Drawing.Color.White;
			this.btnAdd.Location = new System.Drawing.Point(649, 559);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(147, 39);
			this.btnAdd.TabIndex = 120;
			this.btnAdd.Text = "Add Subject";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// AdminStudentDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(984, 641);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.CoursesData);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.txtEnrollDate);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtSection);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtProgram);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSemester);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.picBack);
			this.Name = "AdminStudentDetails";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AdminStudentDetails";
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
		private System.Windows.Forms.Label label1;
		private Guna.UI2.WinForms.Guna2TextBox txtSemester;
		private System.Windows.Forms.Label label2;
		private Guna.UI2.WinForms.Guna2TextBox txtProgram;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private Guna.UI2.WinForms.Guna2TextBox txtSection;
		private System.Windows.Forms.Label label8;
		private Guna.UI2.WinForms.Guna2TextBox txtEnrollDate;
		private System.Windows.Forms.Label label9;
		private Guna.UI2.WinForms.Guna2DataGridView CoursesData;
		private Guna.UI2.WinForms.Guna2Button btnAdd;
	}
}