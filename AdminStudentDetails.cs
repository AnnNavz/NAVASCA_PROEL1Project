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
		}


		private int StudentID;

		string connectionString = Database.ConnectionString;

		public AdminStudentDetails(int studentID) : this()
		{
			StudentID = studentID;

			this.Text = $"Enrollment - Student ID: {StudentID}";


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
			string getEnrollmentSemester = @"SELECT
                                            CONCAT(s.AcademicYear, ' ', s.TermName) AS FullSemesterName
                                            FROM
                                            Enrollment e
                                            INNER JOIN
                                            Semesters s ON e.SemesterID = s.SemesterID
                                            WHERE
                                            e.StudentID = @studentID_int;"; // Added semicolon

			string semester = string.Empty;
			// 'StudentID' and 'connectionString' are assumed to be defined elsewhere

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getEnrollmentSemester, conn))
					{
						// Add the parameter using the proper name defined in the SQL
						cmd.Parameters.AddWithValue("@studentID_int", StudentID);

						conn.Open();
						object result = cmd.ExecuteScalar();

						if (result != null)
						{
							semester = result.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the Profile ID: " + ex.Message,
								"Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			txtSemester.Text = semester;

		}


		private void LoadProgram()
		{
			string getEnrollmentSemester = @"SELECT
                                            p.ProgramName
                                            FROM
                                            Enrollment e
                                            INNER JOIN
                                            Programs p ON p.ProgramID = e.ProgramID
                                            WHERE
                                            e.StudentID = @studentID_int;";

			string program = string.Empty;
			// 'StudentID' and 'connectionString' are assumed to be defined elsewhere

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getEnrollmentSemester, conn))
					{
						// Add the parameter using the proper name defined in the SQL
						cmd.Parameters.AddWithValue("@studentID_int", StudentID);

						conn.Open();
						object result = cmd.ExecuteScalar();

						if (result != null)
						{
							program = result.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the Profile ID: " + ex.Message,
								"Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			txtProgram.Text = program;

		}

		private void LoadSection()
		{
			string getEnrollmentSemester = @"SELECT
                                            Section
                                            FROM
                                            Enrollment 
                                            WHERE
                                            StudentID = @studentID_int;";

			string section = string.Empty;
			// 'StudentID' and 'connectionString' are assumed to be defined elsewhere

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getEnrollmentSemester, conn))
					{
						// Add the parameter using the proper name defined in the SQL
						cmd.Parameters.AddWithValue("@studentID_int", StudentID);

						conn.Open();
						object result = cmd.ExecuteScalar();

						if (result != null)
						{
							section = result.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the hatdog: " + ex.Message,
								"Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			txtSection.Text = section;

		}


		private void LoadEnrollmentDate()
		{
			string getEnrollmentSemester = @"SELECT
                                            EnrollmentDate
                                            FROM
                                            Students 
                                            WHERE
                                            StudentID = @studentID_int;";

			string section = string.Empty;
			// 'StudentID' and 'connectionString' are assumed to be defined elsewhere

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getEnrollmentSemester, conn))
					{
						// Add the parameter using the proper name defined in the SQL
						cmd.Parameters.AddWithValue("@studentID_int", StudentID);

						conn.Open();
						object result = cmd.ExecuteScalar();

						if (result != null)
						{
							section = result.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the hatdog: " + ex.Message,
								"Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			txtEnrollDate.Text = section;

		}

		private void LoadSubjectsEnrolled()
		{
			string sqlQuery = "SELECT c.CourseName, " +
					"p.FirstName, p.LastName, e.StudentID " +
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

					// 2. Add the parameter and its value
					// (You'll need to know the correct data type for StudentID)
					command.Parameters.AddWithValue("@studentid", StudentID); // Use the variable holding the StudentID

					// 3. Initialize the SqlDataAdapter with the SqlCommand
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

			// Check if the DataTable is present and valid
			if (dataTable == null) return;

			// --- 1. Prepare the DataTable (Add the calculated column) ---
			// The expression creates a new column by concatenating the names
			if (!dataTable.Columns.Contains("InstructorName"))
			{
				// 'FirstName' and 'LastName' are present from the SQL query
				dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
			}

			// --- 2. Configure the DataGridView ---
			CoursesData.AutoGenerateColumns = false;
			CoursesData.Columns.Clear();
			CoursesData.ReadOnly = true;

			// Add the Course Name column and map it to the 'CourseName' column in the DataTable
			CoursesData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "CourseName",
				HeaderText = "Course Name",
				DataPropertyName = "CourseName" // Maps to the SQL column
			});

			// Add the Instructor Name column and map it to the calculated 'InstructorName' column
			CoursesData.Columns.Add(new DataGridViewTextBoxColumn()
			{
				Name = "InstructorName",
				HeaderText = "Instructor Name",
				DataPropertyName = "InstructorName" // Maps to the calculated DataTable column
			});

			CoursesData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;



		}
	}
}
