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

namespace NAVASCA_PROEL1Project
{
	public partial class AdminTeacherSubjects : Form
	{
		public AdminTeacherSubjects()
		{
			InitializeComponent();
			this.Load += new EventHandler(AdminTeacherSubjects_Load);
			LoadCourses();
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


		private void LoadCourses()
		{
			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits, " +
							  "c.CourseSem, d.DepartmentName, c.Status " +
							  "FROM Courses AS c " +
							  "INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
							  "WHERE c.Status = 'Active' " +
							  "ORDER BY c.CourseID DESC";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();

					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					CoursesData.AutoGenerateColumns = false;
					CoursesData.Columns.Clear();
					CoursesData.ReadOnly = true;

					CoursesData.Columns.Add("CourseID", "Course ID");
					CoursesData.Columns.Add("CourseName", "Course Name");
					CoursesData.Columns.Add("CourseCode", "Course Code");
					CoursesData.Columns.Add("Description", "Description");
					CoursesData.Columns.Add("Credits", "Credits");
					CoursesData.Columns.Add("CourseSem", "Term");
					CoursesData.Columns.Add("DepartmentName", "Department Name");
					CoursesData.Columns.Add("Status", "Status");


					foreach (DataGridViewColumn col in CoursesData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}

					CoursesData.Columns["Status"].Visible = false;


					CoursesData.DataSource = dataTable;
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
						// Parameterize the query with the instructor ID
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
	}
}
