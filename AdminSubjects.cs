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
	public partial class AdminSubjects : Form
	{
		public AdminSubjects()
		{
			InitializeComponent();
			CoursesData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			CoursesData.ReadOnly = true;
			LoadCourses();
		}

		string connectionString = Database.ConnectionString;


		private void btnAdd_Click(object sender, EventArgs e)
		{
			AdminAddSubject subject = new AdminAddSubject();
			subject.Show();
			this.Hide();
		}

		private void LoadCourses()
		{

			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits, " +
							  "p.FirstName, p.LastName, d.DepartmentName, c.Status " +
							  "FROM Courses AS c " +
							  "INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
							  "INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
							  "INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
							  "WHERE c.Status = 'Active' " +
							  "ORDER BY c.CourseName";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					CoursesData.DataSource = dataTable;

					// Call the shared setup method
					SetupCoursesDataGridView();
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (CoursesData.SelectedRows.Count > 0)
			{
				// Get the selected row
				DataGridViewRow selectedRow = CoursesData.SelectedRows[0];

				// Get the CourseID from the selected row
				// Assuming 'CourseID' is the name of the column that holds the CourseID
				int courseID = Convert.ToInt32(selectedRow.Cells["CourseID"].Value);

				// Confirmation message box
				DialogResult result = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

				if (result == DialogResult.Yes)
				{
					// Call the DeleteCourse method
					DeleteCourse(courseID);

					// Refresh the DataGridView
					LoadCourses();
				}
			}
			else
			{
				MessageBox.Show("Please select a course to delete.", "No Course Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void DeleteCourse(int courseID)
		{

			// SQL command to update the course status to 'Inactive'
			string sqlCommand = "UPDATE Courses SET Status = 'Inactive' WHERE CourseID = @CourseID";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(sqlCommand, conn);
					cmd.Parameters.AddWithValue("@CourseID", courseID);

					int rowsAffected = cmd.ExecuteNonQuery();

				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred during deletion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			string searchTerm = txtSearch.Text.Trim();

			// If search term is empty, reload all active courses
			if (string.IsNullOrEmpty(searchTerm))
			{
				LoadCourses();
				return;
			}

			// Base SQL query with joins
			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits, " +
							  "p.FirstName, p.LastName, d.DepartmentName, c.Status " +
							  "FROM Courses AS c " +
							  "INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
							  "INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
							  "INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
							  "WHERE c.Status = 'Active' AND " + // Filter by 'Active' status
							  "(c.CourseName LIKE @searchTerm OR c.CourseCode LIKE @searchTerm OR p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm OR d.DepartmentName LIKE @searchTerm)";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					// Bind the filtered data to the DataGridView
					CoursesData.DataSource = dataTable;

					// Reapply the column properties
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

		private void SetupCoursesDataGridView()
		{
			CoursesData.AutoGenerateColumns = false;
			CoursesData.Columns.Clear();
			CoursesData.ReadOnly = true;

			// Add columns
			CoursesData.Columns.Add("CourseID", "Course ID");
			CoursesData.Columns.Add("CourseName", "Course Name");
			CoursesData.Columns.Add("CourseCode", "Course Code");
			CoursesData.Columns.Add("Description", "Description");
			CoursesData.Columns.Add("Credits", "Credits");
			CoursesData.Columns.Add("InstructorName", "Instructor Name");
			CoursesData.Columns.Add("DepartmentName", "Department Name");
			CoursesData.Columns.Add("Status", "Status");

			// Create the calculated column for Instructor Name
			DataTable dataTable = (DataTable)CoursesData.DataSource;
			if (dataTable != null && !dataTable.Columns.Contains("InstructorName"))
			{
				dataTable.Columns.Add("InstructorName", typeof(string), "FirstName + ' ' + LastName");
			}

			// Set DataPropertyName for each column
			foreach (DataGridViewColumn col in CoursesData.Columns)
			{
				if (dataTable.Columns.Contains(col.Name))
				{
					col.DataPropertyName = col.Name;
				}
			}

			// Hide the status column
			CoursesData.Columns["Status"].Visible = false;

			

		}

		private void btnApproval_Click(object sender, EventArgs e)
		{
			this.Hide();
			AdminApproval approval = new AdminApproval();
			approval.Show();
		}

		private void btnHome_Click(object sender, EventArgs e)
		{
			this.Hide();
			AdminDashboard dashboard = new AdminDashboard();
			dashboard.Show();
		}

		private void btnStudents_Click(object sender, EventArgs e)
		{
			AdminStudents adminStudents = new AdminStudents();
			adminStudents.Show();
			this.Hide();
		}

		private void btnTeachers_Click(object sender, EventArgs e)
		{
			AdminTeachers adminTeachers = new AdminTeachers();
			adminTeachers.Show();
			this.Hide();
		}

		private void btnSubjects_Click(object sender, EventArgs e)
		{
			this.Show();
		}

		private void btnReports_Click(object sender, EventArgs e)
		{
			AdminReports adminReports = new AdminReports();
			adminReports.Show();
			this.Hide();
		}

		private void btnLogs_Click(object sender, EventArgs e)
		{
			AdminLogs adminLogs = new AdminLogs();
			adminLogs.Show();
			this.Hide();
		}

		private void btnLogout_Click_1(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want log out?", "Pizsity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Login login = new Login();
				login.Show();
				this.Close();
			}
		}

		private void btnUpdateSubmit_Click(object sender, EventArgs e)
		{
			if (CoursesData.SelectedRows.Count == 0)
			{
				MessageBox.Show("Please select a course to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Retrieve CourseID from the selected row
			int courseID = Convert.ToInt32(CoursesData.SelectedRows[0].Cells["CourseID"].Value);

			// Convert Department Name and Instructor Name back to their IDs for the database
			// You will need helper methods (like GetDepartmentID and GetInstructorID) for this.
			string selectedDepartmentName = cmbDepartment.SelectedItem.ToString();
			string selectedInstructorName = cmbInstructor.SelectedItem.ToString();

			int departmentID = GetDepartmentID(selectedDepartmentName);
			int instructorID = GetInstructorID(selectedInstructorName);

			if (departmentID == -1 || instructorID == -1)
			{
				MessageBox.Show("Invalid Department or Instructor selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// 2. Execute the Update Operation
			UpdateCourse(courseID, txtCourseName.Text, txtCourseCode.Text, txtDescription.Text,
						 Convert.ToInt32(txtCredis.Text), instructorID, departmentID);

			// 3. Refresh Data
			LoadCourses();
		}

		private int GetDepartmentID(string departmentName)
		{
			string connectionString = "Data Source=DESKTOP-5QHCE6M; Initial Catalog=NAVASCA_DB; Integrated Security=true";
			int departmentID = -1;
			string sqlQuery = "SELECT DepartmentID FROM Departments WHERE DepartmentName = @DepartmentName";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(sqlQuery, conn);
					cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
					object result = cmd.ExecuteScalar();
					if (result != null)
					{
						departmentID = Convert.ToInt32(result);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while getting DepartmentID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return departmentID;
		}

		private int GetInstructorID(string fullName)
		{
			string connectionString = "Data Source=DESKTOP-5QHCE6M; Initial Catalog=NAVASCA_DB; Integrated Security=true";
			int instructorID = -1;

			// Split the full name into first and last name for the search
			string[] names = fullName.Split(' ');
			string firstName = names[0];
			string lastName = names.Length > 1 ? names[names.Length - 1] : "";

			// SQL to find InstructorID by joining Profiles and Instructors
			string sqlQuery = "SELECT i.InstructorID FROM Instructors AS i " +
							  "INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
							  "WHERE p.FirstName = @FirstName AND p.LastName = @LastName";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(sqlQuery, conn);
					cmd.Parameters.AddWithValue("@FirstName", firstName);
					cmd.Parameters.AddWithValue("@LastName", lastName);

					object result = cmd.ExecuteScalar();
					if (result != null)
					{
						instructorID = Convert.ToInt32(result);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while getting InstructorID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return instructorID;
		}

		private void UpdateCourse(int courseID, string name, string code, string description, int credits, int instructorID, int departmentID)
		{
			string connectionString = "Data Source=DESKTOP-5QHCE6M; Initial Catalog=NAVASCA_DB; Integrated Security=true";

			string sqlQuery = "UPDATE Courses SET " +
							  "CourseName = @CourseName, " +
							  "CourseCode = @CourseCode, " +
							  "Description = @Description, " +
							  "Credits = @Credits, " +
							  "InstructorID = @InstructorID, " +
							  "DepartmentID = @DepartmentID " +
							  "WHERE CourseID = @CourseID";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(sqlQuery, conn);

					cmd.Parameters.AddWithValue("@CourseID", courseID);
					cmd.Parameters.AddWithValue("@CourseName", name);
					cmd.Parameters.AddWithValue("@CourseCode", code);
					cmd.Parameters.AddWithValue("@Description", description);
					cmd.Parameters.AddWithValue("@Credits", credits);
					cmd.Parameters.AddWithValue("@InstructorID", instructorID);
					cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

					int rowsAffected = cmd.ExecuteNonQuery();

					if (rowsAffected > 0)
					{
						MessageBox.Show("Course updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						MessageBox.Show("No course found with the specified ID or no changes were made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred during the course update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
