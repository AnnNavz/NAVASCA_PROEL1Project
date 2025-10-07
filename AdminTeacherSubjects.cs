using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NAVASCA_PROEL1Project
{
	public partial class AdminTeacherSubjects : Form
	{
		public AdminTeacherSubjects()
		{
			InitializeComponent();
			this.Load += new EventHandler(AdminTeacherSubjects_Load);
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
		}




		private int TeacherID;
		private string TeacherName;
		string connectionString = Database.ConnectionString;
		public AdminTeacherSubjects(int teacherID, string teacherName) : this()
		{
			TeacherID = teacherID;
			TeacherName = teacherName;

			this.Text = $"Details - Student ID: {TeacherID}, {TeacherName}";

		}
		private void picBack_Click(object sender, EventArgs e)
		{
			AdminTeachers adminTeachers = new AdminTeachers();
			adminTeachers.Show();
			this.Hide();
		}


		private void AdminTeacherSubjects_Load(object sender, EventArgs e)
		{
			int currentInstructorId = TeacherID;
			LoadSectionsForInstructor(cmbSection, currentInstructorId);

		}


		private void LoadCourses(string semester)
		{
			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Credits " +
							  "FROM Courses AS c " +
							  "INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
							  "INNER JOIN Instructors AS i ON i.DepartmentID = d.DepartmentID " +
							  "WHERE c.Status = 'Active' AND i.InstructorID = @InstructorID AND c.CourseSem = @Semester " +
							  "ORDER BY c.CourseID DESC";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();

					using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
					{
						cmd.Parameters.AddWithValue("@InstructorID", TeacherID);
						cmd.Parameters.AddWithValue("@Semester", semester);

						SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
						DataTable dataTable = new DataTable();
						dataAdapter.Fill(dataTable);

						CoursesData.Columns.Clear();
						CoursesData.DataSource = null;

						CoursesData.AutoGenerateColumns = false;
						CoursesData.ReadOnly = true;

						CoursesData.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CourseID", HeaderText = "Course ID", DataPropertyName = "CourseID" });
						CoursesData.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CourseName", HeaderText = "Course Name", DataPropertyName = "CourseName" });
						CoursesData.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CourseCode", HeaderText = "Course Code", DataPropertyName = "CourseCode" });
						CoursesData.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Credits", HeaderText = "Credits", DataPropertyName = "Credits" });
						CoursesData.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Status", HeaderText = "Status", DataPropertyName = "Status", Visible = false });

						DataGridViewButtonColumn handleButtonColumn = new DataGridViewButtonColumn();

						handleButtonColumn.HeaderText = "";
						handleButtonColumn.Name = "HandleSubject";
						handleButtonColumn.Text = "Handle Subject";
						handleButtonColumn.UseColumnTextForButtonValue = true;

						CoursesData.Columns.Add(handleButtonColumn);

						CoursesData.DataSource = dataTable;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		public void LoadSectionsForInstructor(ComboBox cmbSection, int instructorID)
		{
			try
			{
				DataTable sectionsData = GetSectionsByInstructorDepartment(instructorID);


				cmbSection.DataSource = sectionsData;
				cmbSection.DisplayMember = "SectionName";
				cmbSection.ValueMember = "SectionID";
				cmbSection.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading sections by instructor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private DataTable GetSectionsByInstructorDepartment(int instructorID)
		{
			DataTable dataTable = new DataTable();

			string sqlQuery = @"
            SELECT DISTINCT 
                s.SectionID, 
                s.SectionName 
            FROM 
                Sections s
            INNER JOIN 
                Programs p ON s.ProgramID = p.ProgramID
            INNER JOIN 
                Instructors i ON p.DepartmentID = i.DepartmentID
            WHERE 
                i.InstructorID = @InstructorID
            ORDER BY
                s.SectionName";

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						command.Parameters.AddWithValue("@InstructorID", instructorID);
						connection.Open();

						SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
						dataAdapter.Fill(dataTable);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Database Error fetching sections: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return dataTable;
		}

		private void cmbSemester_SelectedIndexChanged(object sender, EventArgs e)
		{
			string Semester = string.Empty;

			if (cmbSemester.SelectedIndex == 0)
			{
				Semester = "Second Semester";
				LoadCourses(Semester);
			}
			if (cmbSemester.SelectedIndex == 1)
			{
				Semester = "First Semester";
				LoadCourses(Semester);
			}
		}

		private void CoursesData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			object courseIDObject = CoursesData.Rows[e.RowIndex].Cells["CourseID"].Value;
			object courseNameObject = CoursesData.Rows[e.RowIndex].Cells["CourseName"].Value;
			object courseCodeObject = CoursesData.Rows[e.RowIndex].Cells["CourseCode"].Value;

			if (courseIDObject is DBNull || courseIDObject == null)
			{
				MessageBox.Show("Please select a course.", "Manage Subject", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			int selectedCourseID = Convert.ToInt32(courseIDObject);

			if (selectedCourseID == 0)
			{
				MessageBox.Show("Please select a course.", "Manage Subject", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			string selectedCourseName = Convert.ToString(courseNameObject);
			string selecteCourseCode = Convert.ToString(courseCodeObject);
			string columnName = CoursesData.Columns[e.ColumnIndex].Name;


			if (columnName == "HandleSubject")
			{
				errorProvider1.Clear();

				bool requiredFieldsMissing = false;

				if (string.IsNullOrWhiteSpace(cmbSection.Text)) { errorProvider1.SetError(cmbSection, "Section is required."); requiredFieldsMissing = true; }

				if (requiredFieldsMissing)
				{
					return;
				}

				if (IsTeacher(TeacherID, selecteCourseCode, cmbSection.Text))
				{
					MessageBox.Show("This teacher already handle this subject in this section.", "Enrollment Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);

					return;
				}


				string Semester = string.Empty;

				if (cmbSemester.SelectedIndex == 0)
				{
					Semester = "Second Semester";
				}
				if (cmbSemester.SelectedIndex == 1)
				{
					Semester = "First Semester";
				}

				if (MessageBox.Show("Do you want this teacher to handle in this subject " + selectedCourseName + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{

					using (SqlConnection conn = new SqlConnection(connectionString))
					{
						conn.Open();


						SqlCommand cmd = new SqlCommand("HandleSubject_SP", conn);
						cmd.CommandType = CommandType.StoredProcedure;


						cmd.Parameters.AddWithValue("@Semester", Semester);
						cmd.Parameters.AddWithValue("@Section", cmbSection.Text);
						cmd.Parameters.AddWithValue("@CourseID", selectedCourseID);
						cmd.Parameters.AddWithValue("@InstructorID", TeacherID);


						cmd.ExecuteNonQuery();
						MessageBox.Show("Managed Subject Successful!" + "\n Subject: " + selectedCourseName,
										"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


					}
				}


			}
		}

		private bool IsTeacher(int currentTeacherID, string currentCourseCode, string currentSection)
		{

			string sqlQuery = "SELECT COUNT(*) " +
				  "FROM HandleSubjects e " +
				  "INNER JOIN Sections s ON e.SectionID = s.SectionID " +
				  "INNER JOIN Courses c ON c.CourseID = e.CourseID " +
				  "WHERE e.InstructorID = @instructorID AND c.CourseCode = @courseCode AND s.SectionName = @sectionName;";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
				{
					cmd.Parameters.AddWithValue("@instructorID", currentTeacherID);
					cmd.Parameters.AddWithValue("@courseCode", currentCourseCode);
					cmd.Parameters.AddWithValue("@sectionName", currentSection);


					conn.Open();
					int count = (int)cmd.ExecuteScalar();
					return count > 0;
				}
			}
		}
	}
}
