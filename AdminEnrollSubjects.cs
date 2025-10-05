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
	public partial class AdminEnrollSubjects : Form
	{
		public AdminEnrollSubjects()
		{
			InitializeComponent();
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			LoadCourses();
		}


		private int StudentID;
		string connectionString = Database.ConnectionString;

		public AdminEnrollSubjects(int studentID) : this()
		{
			StudentID = studentID;

			this.Text = $"Enrollment - Student ID: {StudentID}";
		}

		private void picBack_Click(object sender, EventArgs e)
		{
			AdminStudents students = new AdminStudents();
			students.Show();
			this.Hide();
		}

		private void LoadCourses()
		{
			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits, " +
					"p.FirstName, p.LastName, c.Status " +
					"FROM Courses AS c " +
					"INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
					"INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
					"WHERE c.Status = 'Active' " +
					"AND c.DepartmentID IN ( " +
					  "SELECT prog.DepartmentID " +
					  "FROM Programs AS prog " +
					  "INNER JOIN Enrollment AS e ON prog.ProgramID = e.ProgramID " +
					  "WHERE e.StudentID = @StudentID " +
					") " +
					"ORDER BY c.CourseID DESC";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					// CRITICAL ADDITION: Add the StudentID parameter to the data adapter's select command
					dataAdapter.SelectCommand.Parameters.AddWithValue("@StudentID", StudentID);

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
			CoursesData.AutoGenerateColumns = false;
			CoursesData.Columns.Clear();
			CoursesData.ReadOnly = true;

			CoursesData.Columns.Add("CourseID", "Course ID");
			CoursesData.Columns.Add("CourseName", "Course Name");
			CoursesData.Columns.Add("CourseCode", "Course Code");
			CoursesData.Columns.Add("Description", "Description");
			CoursesData.Columns.Add("Credits", "Credits");
			CoursesData.Columns.Add("InstructorName", "Instructor Name");


			DataGridViewButtonColumn enrollmentButtonColumn = new DataGridViewButtonColumn();

			enrollmentButtonColumn.HeaderText = "Enrollment";
			enrollmentButtonColumn.Name = "Enrollment";
			enrollmentButtonColumn.Text = "Enroll";
			enrollmentButtonColumn.UseColumnTextForButtonValue = true;

			CoursesData.Columns.Add(enrollmentButtonColumn);

			DataTable dataTable = (DataTable)CoursesData.DataSource;
			if (dataTable != null && !dataTable.Columns.Contains("InstructorName"))
			{
				dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
			}

			foreach (DataGridViewColumn col in CoursesData.Columns)
			{
				if (dataTable.Columns.Contains(col.Name))
				{
					col.DataPropertyName = col.Name;
				}
			}


			CoursesData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;



		}

		private void CoursesData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			object courseIDObject = CoursesData.Rows[e.RowIndex].Cells["CourseID"].Value;
			object courseNameObject = CoursesData.Rows[e.RowIndex].Cells["CourseName"].Value;



			if (courseIDObject == null)
			{
				MessageBox.Show("CourseID not found.", "Error");
				return;
			}

			int selectedCourseID = Convert.ToInt32(courseIDObject);
			string selectedCourseName = (courseNameObject.ToString());


			string getEnrollmentIDQuery = "SELECT EnrollmentID FROM Enrollment WHERE StudentID = @studentID_int";
			int enrollmentID = 0;

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getEnrollmentIDQuery, conn))
					{
						cmd.Parameters.AddWithValue("@studentID_int", StudentID);
						conn.Open();
						object result = cmd.ExecuteScalar();
						if (result != null)
						{
							enrollmentID = Convert.ToInt32(result);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the Profile ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string columnName = CoursesData.Columns[e.ColumnIndex].Name;

			if (columnName == "Enrollment")
			{

				if (IsStudentEnrolled(StudentID, selectedCourseID))
				{
					MessageBox.Show("This student is already enrolled in this subject.", "Enrollment Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (MessageBox.Show("Do you want to enroll this student in this subject " + selectedCourseName + "?", "Confirm Enroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{

					using (SqlConnection conn = new SqlConnection(connectionString))
					{
						conn.Open();


						SqlCommand cmd = new SqlCommand("EnrollSubject_SP", conn);
						cmd.CommandType = CommandType.StoredProcedure;


						cmd.Parameters.AddWithValue("@StudentID", StudentID);
						cmd.Parameters.AddWithValue("@CourseID", selectedCourseID);
						cmd.Parameters.AddWithValue("@EnrollmentID", enrollmentID);
						cmd.Parameters.AddWithValue("@Grade", "0.0");


						cmd.ExecuteNonQuery();
						MessageBox.Show("Enrolled Subject Successful!" + "\n Subject: " + selectedCourseName,
										"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

						AdminStudents students = new AdminStudents();
						students.Show();
						this.Hide();

					}
				}
			}
		}


		private bool IsStudentEnrolled(int currentStudentID , int currentCourseID)
		{

			string sqlQuery = "SELECT COUNT(*) FROM EnrollSubject WHERE StudentID = @studentID AND CourseID = @courseID";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
				{
					cmd.Parameters.AddWithValue("@studentID", currentStudentID);
					cmd.Parameters.AddWithValue("@courseID", currentCourseID);
					conn.Open();
					int count = (int)cmd.ExecuteScalar();
					return count > 0;
				}
			}
		}
	}
}
