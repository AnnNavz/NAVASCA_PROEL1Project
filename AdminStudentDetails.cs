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
	public partial class AdminStudentDetails : Form
	{
		public AdminStudentDetails()
		{
			InitializeComponent();
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
		}


		private int StudentID;
		private string StudentName;

		string connectionString = Database.ConnectionString;

		public AdminStudentDetails(int studentID, string studentName) : this()
		{
			StudentID = studentID;
			StudentName = studentName;

			this.Text = $"Details - Student ID: {StudentID}, {StudentName}";


			LoadSemester();
			LoadSubjectsEnrolled();
			LoadProgram();
			LoadSection();
			LoadEnrollmentDate();
		}

		private void picBack_Click(object sender, EventArgs e)
		{
			AdminStudents students = new AdminStudents();
			students.Show();
			this.Hide();
		}



		private void LoadSemester()
		{
			string sql = @"SELECT
                          CONCAT(s.AcademicYear, ' ', s.TermName) AS FullSemesterName
                          FROM
                          Enrollment e
                          INNER JOIN
                          Semesters s ON e.SemesterID = s.SemesterID
                          WHERE
                          e.StudentID = @studentID_int;";

			txtSemester.Text = GetScalarResultForStudent(sql, "@studentID_int", StudentID);

		}


		private void LoadProgram()
		{
			string sql = @"SELECT
                         p.ProgramName
                         FROM
                         Enrollment e
                         INNER JOIN
                         Programs p ON p.ProgramID = e.ProgramID
                         WHERE
                         e.StudentID = @studentID_int;";

			
			txtProgram.Text = GetScalarResultForStudent(sql, "@studentID_int", StudentID);

		}

		private void LoadSection()
		{
			string sql = @"SELECT
                          Section
                          FROM
                          Enrollment 
                          WHERE
                          StudentID = @studentID_int;";

			txtSection.Text = GetScalarResultForStudent(sql, "@studentID_int", StudentID); 

		}


		private void LoadEnrollmentDate()
		{
			string sql = @"SELECT
                          DateRecorded
                          FROM
                          Enrollment 
                          WHERE
                          StudentID = @studentID_int;";

			txtEnrollDate.Text = GetScalarResultForStudent(sql, "@studentID_int", StudentID); 

		}

		private string GetScalarResultForStudent(string sqlQuery, string parameterName, int studentIdValue)
		{
			string result = string.Empty;

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
				{
					cmd.Parameters.AddWithValue(parameterName, studentIdValue);

					conn.Open();
					object scalarResult = cmd.ExecuteScalar();

					if (scalarResult != null)
					{
						result = scalarResult.ToString();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Database error: {ex.Message}",
								"Data Retrieval Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}

			return result;
		}

		private void LoadSubjectsEnrolled()
		{
			string sqlQuery = "SELECT c.CourseCode, c.CourseName, " +
					"p.FirstName, p.LastName, e.Grade, e.StudentID " +
					"FROM EnrollSubject AS e " +
					"INNER JOIN Courses AS c ON c.CourseID = e.CourseID " +
					"INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
					"INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
					"WHERE e.StudentID = @studentid ";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand command = new SqlCommand(sqlQuery, conn);

					command.Parameters.AddWithValue("@studentid", StudentID);

					SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					CoursesData.DataSource = dataTable;
					SetupCoursesDataGridView();
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void SetupCoursesDataGridView()
		{
			DataTable dataTable = CoursesData.DataSource as DataTable;

			if (dataTable == null) return;

			if (!dataTable.Columns.Contains("InstructorName"))
			{
				dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
			}

			CoursesData.AutoGenerateColumns = false;
			CoursesData.Columns.Clear();
			CoursesData.ReadOnly = true;

			CoursesData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "CourseCode",
				HeaderText = "Course Code",
				DataPropertyName = "CourseCode"
			});

			CoursesData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "CourseName",
				HeaderText = "Course Name",
				DataPropertyName = "CourseName"
			});


			CoursesData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "InstructorName",
				HeaderText = "Instructor Name",
				DataPropertyName = "InstructorName"
			});

			CoursesData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "Grade",
				HeaderText = "Grade",
				DataPropertyName = "Grade"
			});

			CoursesData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;



		}
	}
}
