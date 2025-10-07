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

			TeachersData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
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
							  "t.TermName + ' ' + t.AcademicYear AS Semester, " + // Combined Semester
							  "s.SectionName " +
	                          "FROM HandleSubjects AS e " +
	                          "INNER JOIN Semesters AS t ON t.SemesterID = e.SemesterID " + // Corrected Join Condition
	                          "INNER JOIN Courses AS c ON c.CourseID = e.CourseID " +
	                          "INNER JOIN Sections AS s ON s.SectionID = e.SectionID " +
							  "WHERE e.InstructorID = @teacherID;";

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

					if (dataTable == null) return;


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
						Name = "Semester",
						HeaderText = "Semester",
						DataPropertyName = "Semester"
					});


					TeachersData.Columns.Add(new DataGridViewTextBoxColumn()
					{
						Name = "SectionName",
						HeaderText = "Sections",
						DataPropertyName = "SectionName"
					});


					TeachersData.DataSource = dataTable;
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
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
							  "t.TermName + ' ' + t.AcademicYear AS Semester, " +
							  "s.SectionName " +
							  "FROM HandleSubjects AS e " +
							  "INNER JOIN Semesters AS t ON t.SemesterID = e.SemesterID " +
							  "INNER JOIN Courses AS c ON c.CourseID = e.CourseID " +
							  "INNER JOIN Sections AS s ON s.SectionID = e.SectionID " +
							  "WHERE e.InstructorID = @teacherID AND " +
							  "(c.CourseName LIKE @searchTerm OR c.CourseCode LIKE @searchTerm OR t.TermName LIKE @searchTerm OR t.AcademicYear LIKE @searchTerm OR s.SectionName LIKE @searchTerm)";

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
