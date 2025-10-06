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
	public partial class AdminEnrollSubjects : Form
	{
		public AdminEnrollSubjects()
		{
			InitializeComponent();
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			
		}


		private int StudentID;
		private string StudentName;
		string connectionString = Database.ConnectionString;

		public AdminEnrollSubjects(int studentID, string studentName) : this()
		{
			StudentID = studentID;
			StudentName = studentName;

			this.Text = $"EnrollSubjects - Student ID: {StudentID}, {StudentName}";
			LoadCourses();
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
					"p.FirstName, p.LastName, e.StudentID " +
					"FROM Enrollment AS e " +
					"INNER JOIN Programs AS r ON r.ProgramID = e.ProgramID " +
					"INNER JOIN Departments AS d ON r.DepartmentID = d.DepartmentID " + 
					"INNER JOIN Courses AS c ON c.DepartmentID = d.DepartmentID " +
					"INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
					"INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
					"WHERE e.StudentID = @studentid ";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

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


			string action = "Enroll Subject";
			string description = $"Enrolled a student in {selectedCourseName}";
			string name = StudentName;


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
						cmd.Parameters.AddWithValue("@Action", action);
						cmd.Parameters.AddWithValue("@Description", description);
						cmd.Parameters.AddWithValue("@AddName", name);


						cmd.ExecuteNonQuery();
						MessageBox.Show("Enrolled Subject Successful!" + "\n Subject: " + selectedCourseName,
										"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


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

		private void btnSearch_Click(object sender, EventArgs e)
		{
			string searchTerm = txtSearch.Text.Trim();

			if (string.IsNullOrEmpty(searchTerm))
			{
				LoadCourses();
				return;
			}

			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits, " +
					"p.FirstName, p.LastName, e.StudentID " +
					"FROM Enrollment AS e " +
					"INNER JOIN Programs AS r ON r.ProgramID = e.ProgramID " +
					"INNER JOIN Departments AS d ON r.DepartmentID = d.DepartmentID " +
					"INNER JOIN Courses AS c ON c.DepartmentID = d.DepartmentID " +
					"INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
					"INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
					"WHERE e.StudentID = @studentid AND " +
			        "(c.CourseID LIKE @searchTerm OR c.CourseName LIKE @searchTerm OR c.CourseCode LIKE @searchTerm OR p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm OR c.Description LIKE @searchTerm)";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					dataAdapter.SelectCommand.Parameters.AddWithValue("@StudentID", StudentID);
					dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					CoursesData.DataSource = dataTable;

					SetupCoursesDataGridView();

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
