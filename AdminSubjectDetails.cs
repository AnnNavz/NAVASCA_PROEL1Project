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
							  "FROM EnrollSubjects AS e " +
							  "INNER JOIN Courses AS c ON e.CourseID = c.CourseID " +
							  "INNER JOIN Enrollment AS m ON m.EnrollmentID = e.EnrollmentID " +
							  "INNER JOIN Semesters AS t ON t.SemesterID = m.SemesterID " +
							  "INNER JOIN Sections AS s ON s.SectionID = m.SectionID " +
							  "INNER JOIN Programs AS r ON r.ProgramID = m.ProgramID " +
							  "LEFT JOIN HandleSubjects h ON h.CourseID = c.CourseID " +
							  "LEFT JOIN Instructors AS i ON h.InstructorID = i.InstructorID " +
							  "LEFT JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
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

					if (dataTable != null && !dataTable.Columns.Contains("InstructorName"))
					{
						dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
					}

					if (dataTable != null)
					{
						DataView view = new DataView(dataTable);

						DataTable distinctTable = view.ToTable(true,
							"Semester",
							"ProgramName",
							"SectionName",
							"InstructorName"
						);

						CoursesData.DataSource = distinctTable;
						
					}

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
					 "FROM EnrollSubjects AS e " +
					 "INNER JOIN Courses AS c ON e.CourseID = c.CourseID " +
					 "INNER JOIN Enrollment AS m ON m.EnrollmentID = e.EnrollmentID " +
					 "INNER JOIN Semesters AS t ON t.SemesterID = m.SemesterID " +
					 "INNER JOIN Sections AS s ON s.SectionID = m.SectionID " +
					 "INNER JOIN Programs AS r ON r.ProgramID = m.ProgramID " +
					 "LEFT JOIN HandleSubjects h ON h.CourseID = c.CourseID AND h.SectionID = m.SectionID AND h.SemesterID = m.SemesterID " +
					  "LEFT JOIN Instructors AS i ON h.InstructorID = i.InstructorID " +
					 "LEFT JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
					 "WHERE c.CourseID = @CourseID AND " +
					 "(t.TermName LIKE @searchTerm OR t.AcademicYear LIKE @searchTerm OR r.ProgramName LIKE @searchTerm OR s.SectionName LIKE @searchTerm OR p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm)";

			sqlQuery += " ORDER BY Semester, s.SectionID ";

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

					DataTable distinctTable = new DataTable();

					if (dataTable.Rows.Count > 0)
					{
						if (!dataTable.Columns.Contains("InstructorName"))
						{
							dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
						}

						DataView view = new DataView(dataTable);

						distinctTable = view.ToTable(true,
							"Semester",
							"ProgramName",
							"SectionName",
							"InstructorName"
						);
					}

					CoursesData.DataSource = distinctTable;


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
