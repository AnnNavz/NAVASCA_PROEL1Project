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
	public partial class AdminTeacherDetails : Form
	{
		public AdminTeacherDetails()
		{
			InitializeComponent();
		}


		private int TeacherID;
		private string TeacherName;
		string connectionString = Database.ConnectionString;

		public AdminTeacherDetails(int teacherID, string teacherName) : this()
		{
			TeacherID = teacherID;
			TeacherName = teacherName;

			this.Text = $"Details - Student ID: {TeacherID}, {TeacherName}";


			LoadSubjectsEnrolled();

		}
		private void picBack_Click(object sender, EventArgs e)
		{
			AdminTeachers adminTeachers = new AdminTeachers();
			adminTeachers.Show();
			this.Hide();
		}

		private void LoadSubjectsEnrolled()
		{
			string sqlQuery = "SELECT c.CourseCode, c.CourseName, " +
					"p.FirstName, p.LastName, e.Grade, c.InstructorID " +
					"FROM EnrollSubject AS e " +
					"INNER JOIN Courses AS c ON c.CourseID = e.CourseID " +
					"INNER JOIN Enrollment AS m ON m.EnrollmentID = e.EnrollmentID " +
					"INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
					"INNER JOIN Students AS s ON s.StudentID = e.StudentID " +
					"INNER JOIN Profiles AS p ON s.ProfileID = p.ProfileID " +
					"WHERE i.InstructorID = @teacherID ";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand command = new SqlCommand(sqlQuery, conn);

					command.Parameters.AddWithValue("@teacherID", TeacherID);

					SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					TeachersData.DataSource = dataTable;
					SetupTeachersDataGridView();
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void SetupTeachersDataGridView()
		{
			DataTable dataTable = TeachersData.DataSource as DataTable;

			if (dataTable == null) return;

			if (!dataTable.Columns.Contains("StudentName"))
			{
				dataTable.Columns.Add("StudentName", typeof(string), "FirstName + ' ' + LastName");
			}

			TeachersData.AutoGenerateColumns = false;
			TeachersData.Columns.Clear();
			TeachersData.ReadOnly = true;

			TeachersData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "CourseCode",
				HeaderText = "Course Code",
				DataPropertyName = "CourseCode"
			});

			TeachersData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "CourseName",
				HeaderText = "Course Name",
				DataPropertyName = "CourseName"
			});


			TeachersData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "StudentName",
				HeaderText = "Student Name",
				DataPropertyName = "StudentName"
			});

			TeachersData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "Grade",
				HeaderText = "Grade",
				DataPropertyName = "Grade"
			});

			



		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			string searchTerm = txtSearch.Text.Trim();

			if (string.IsNullOrEmpty(searchTerm))
			{
			    LoadSubjectsEnrolled();
				return;
			}

			string sqlQuery = "SELECT c.CourseCode, c.CourseName, " +
					"p.FirstName, p.LastName, e.Grade, c.InstructorID " +
					"FROM EnrollSubject AS e " +
					"INNER JOIN Courses AS c ON c.CourseID = e.CourseID " +
					"INNER JOIN Enrollment AS m ON m.EnrollmentID = e.EnrollmentID " +
					"INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
					"INNER JOIN Students AS s ON s.StudentID = e.StudentID " +
					"INNER JOIN Profiles AS p ON s.ProfileID = p.ProfileID " +
					"WHERE i.InstructorID = @teacherID AND " +
			        "(c.CourseName LIKE @searchTerm OR c.CourseCode LIKE @searchTerm OR p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm OR e.Grade LIKE @searchTerm)";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					dataAdapter.SelectCommand.Parameters.AddWithValue("@teacherID", TeacherID);
					dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					TeachersData.DataSource = dataTable;

					SetupTeachersDataGridView();

					if (dataTable.Rows.Count == 0)
					{
						MessageBox.Show("No courses found matching your search criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred during search: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
