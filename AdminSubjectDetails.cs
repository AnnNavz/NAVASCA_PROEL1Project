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
	public partial class AdminSubjectDetails : Form
	{
		public AdminSubjectDetails()
		{
			InitializeComponent();
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
		}

		private int CourseID;
		private string CourseName;
		string connectionString = Database.ConnectionString;
		public AdminSubjectDetails(int courseID, string courseName) : this()
		{
			CourseID = courseID;
			CourseName = courseName;

			this.Text = $"Details - Course ID: {CourseID}, {CourseName}";
			LoadCourses();
		}

		private void LoadCourses()
		{
			string sqlQuery = "SELECT t.TermName + ' ' + t.AcademicYear AS Semester, r.ProgramName, s.SectionName, " +
							  "p.FirstName, p.LastName " +
							  "FROM HandleSubjects AS h " +
							  "INNER JOIN Courses AS c ON h.CourseID = c.CourseID " +
							  "INNER JOIN Semesters AS t ON t.SemesterID = h.SemesterID " +
							  "INNER JOIN Sections AS s ON s.SectionID = h.SectionID " +
							  "INNER JOIN Programs AS r ON r.ProgramID = s.ProgramID " +
							  "INNER JOIN Instructors AS i ON h.InstructorID = i.InstructorID " +
							  "INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
							  "WHERE c.CourseID = @CourseID " +
							  "ORDER BY t.SemesterID, s.SectionID";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					dataAdapter.SelectCommand.Parameters.AddWithValue("@CourseID", CourseID);

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);


					CoursesData.AutoGenerateColumns = false;
					CoursesData.Columns.Clear();
					CoursesData.ReadOnly = true;

					if (dataTable != null && !dataTable.Columns.Contains("InstructorName"))
					{
						dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
					}


					CoursesData.Columns.Add("Semester", "Semester");
					CoursesData.Columns.Add("ProgramName", "Program Name");
					CoursesData.Columns.Add("SectionName", "Section");
					CoursesData.Columns.Add("InstructorName", "Teacher");


					foreach (DataGridViewColumn col in CoursesData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}



					CoursesData.DataSource = dataTable;

				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void picBack_Click(object sender, EventArgs e)
		{
			AdminSubjects subjects = new AdminSubjects();
			subjects.Show();
			this.Hide();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{

			string searchTerm = txtSearch.Text.Trim();

			if (string.IsNullOrEmpty(searchTerm))
			{
				LoadCourses();
				return;
			}

			string sqlQuery = "SELECT t.TermName + ' ' + t.AcademicYear AS Semester, r.ProgramName, s.SectionName, " +
							  "p.FirstName, p.LastName " +
							  "FROM HandleSubjects AS h " +
							  "INNER JOIN Courses AS c ON h.CourseID = c.CourseID " +
							  "INNER JOIN Semesters AS t ON t.SemesterID = h.SemesterID " +
							  "INNER JOIN Sections AS s ON s.SectionID = h.SectionID " +
							  "INNER JOIN Programs AS r ON r.ProgramID = s.ProgramID " +
							  "INNER JOIN Instructors AS i ON h.InstructorID = i.InstructorID " +
							  "INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
							  "WHERE c.CourseID = @CourseID AND " +
			                  "(t.TermName LIKE @searchTerm OR t.AcademicYear LIKE @searchTerm OR r.ProgramName LIKE @searchTerm OR s.SectionName LIKE @searchTerm OR p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm)";

			sqlQuery += " ORDER BY t.SemesterID, s.SectionID";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					dataAdapter.SelectCommand.Parameters.AddWithValue("@CourseID", CourseID);
					dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					CoursesData.AutoGenerateColumns = false;
					CoursesData.Columns.Clear();
					CoursesData.ReadOnly = true;

					if (dataTable != null && !dataTable.Columns.Contains("InstructorName"))
					{
						dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
					}


					CoursesData.Columns.Add("Semester", "Semester");
					CoursesData.Columns.Add("ProgramName", "Program Name");
					CoursesData.Columns.Add("SectionName", "Section");
					CoursesData.Columns.Add("InstructorName", "Teacher");


					foreach (DataGridViewColumn col in CoursesData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}

					CoursesData.DataSource = dataTable;


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
