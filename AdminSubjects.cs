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
using static NAVASCA_PROEL1Project.AdminAddSubject;

namespace NAVASCA_PROEL1Project
{
	public partial class AdminSubjects : Form
	{
		public AdminSubjects()
		{
			InitializeComponent();
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			LoadCourses();

			DataTable departmentsData = DatabaseManager.GetDepartments();
			cmbDepartment.DataSource = departmentsData;
			cmbDepartment.DisplayMember = "DepartmentName";
			cmbDepartment.ValueMember = "DepartmentID";
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
			string sqlQuery_TotalSubjectCount = "SELECT COUNT(CourseID)" +
												"FROM Courses " +
												"WHERE Status = 'Active'";

			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits, " +
							  "p.FirstName, p.LastName, d.DepartmentName, c.Status " +
							  "FROM Courses AS c " +
							  "INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
							  "INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
							  "INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
							  "WHERE c.Status = 'Active' " +
							  "ORDER BY c.CourseID DESC";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();

					SqlCommand countSubjectcmd = new SqlCommand(sqlQuery_TotalSubjectCount, conn);
					int SubjectCount = (int)countSubjectcmd.ExecuteScalar();
					lblTotalSubjects.Text = SubjectCount.ToString();

					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (CoursesData.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = CoursesData.SelectedRows[0];

				int courseID = Convert.ToInt32(selectedRow.Cells["CourseID"].Value);

				DialogResult result = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

				if (result == DialogResult.Yes)
				{

					string LogName = txtCourseName.Text;
					string logDescription = $"Deleted a subject.";
					AddLogEntry(LogName, "Delete Subject", logDescription);

					DeleteCourse(courseID);
				}
			}
			else
			{
				MessageBox.Show("Please select a course to delete.", "No Course Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void DeleteCourse(int courseID)
		{

			string sqlCommand = "UPDATE Courses SET Status = 'Inactive' WHERE CourseID = @CourseID";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(sqlCommand, conn);
					cmd.Parameters.AddWithValue("@CourseID", courseID);

					int rowsAffected = cmd.ExecuteNonQuery();

					LoadCourses();

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

			if (string.IsNullOrEmpty(searchTerm))
			{
				LoadCourses();
				return;
			}

			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits, " +
							  "p.FirstName, p.LastName, d.DepartmentName, c.Status " +
							  "FROM Courses AS c " +
							  "INNER JOIN Instructors AS i ON c.InstructorID = i.InstructorID " +
							  "INNER JOIN Profiles AS p ON i.ProfileID = p.ProfileID " +
							  "INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
							  "WHERE c.Status = 'Active' AND " +
							  "(c.CourseID LIKE @searchTerm OR c.CourseName LIKE @searchTerm OR c.CourseCode LIKE @searchTerm OR p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm OR c.Description LIKE @searchTerm OR d.DepartmentName LIKE @searchTerm)";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
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
			CoursesData.Columns.Add("DepartmentName", "Department Name");
			CoursesData.Columns.Add("Status", "Status");

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

			CoursesData.Columns["Status"].Visible = false;

			CoursesData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;



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
				MessageBox.Show("Please select a course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			int courseID = Convert.ToInt32(CoursesData.SelectedRows[0].Cells["CourseID"].Value);

			string selectedDepartmentName = cmbDepartment.Text;
			string selectedInstructorName = cmbTeacher.Text;

			if (string.IsNullOrEmpty(selectedDepartmentName) || string.IsNullOrEmpty(selectedInstructorName))
			{
				MessageBox.Show("Please select both a Department and an Instructor.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (cmbDepartment.SelectedValue == null || cmbTeacher.SelectedValue == null)
			{
				MessageBox.Show("Selected Department or Instructor is not a valid item.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
			int instructorID = Convert.ToInt32(cmbTeacher.SelectedValue);

			string sqlQuery = "UPDATE Courses SET " +
							  "CourseName = @CourseName, " +
							  "CourseCode = @CourseCode, " +
							  "Credits = @Credits, " +
							  "Description = @Description, " +
							  "DepartmentID = @DepartmentID, " +
							  "InstructorID = @InstructorID " +
							  "WHERE CourseID = @CourseID;";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(sqlQuery, conn);

					cmd.Parameters.AddWithValue("@CourseID", courseID);
					cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
					cmd.Parameters.AddWithValue("@CourseCode", txtCourseCode.Text);
					cmd.Parameters.AddWithValue("@Credits", cmbCredits.Text);
					cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

					cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
					cmd.Parameters.AddWithValue("@InstructorID", instructorID);

					int rowsAffected = cmd.ExecuteNonQuery();

					if (rowsAffected > 0)
					{


						string LogName = txtCourseName.Text;
						string logDescription = $"Updated a subject.";
						AddLogEntry(LogName, "Update Subject", logDescription);

						MessageBox.Show("Course details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
						LoadCourses();


					}
					else
					{
						MessageBox.Show("Update failed. Course not found or no changes were made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred during the update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void btnUpdate_Click(object sender, EventArgs e)
		{
		   pnlUpdate.Visible = true;
		}

		private string selectedCourseId;
		private void CoursesData_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridViewRow row = CoursesData.Rows[e.RowIndex];

				selectedCourseId = row.Cells["CourseID"].Value.ToString();

				string courseName = row.Cells["CourseName"].Value.ToString();
				string courseCode = row.Cells["CourseCode"].Value.ToString();
				string credits = row.Cells["Credits"].Value.ToString();
				string description = row.Cells["Description"].Value.ToString();
				string department = row.Cells["DepartmentName"].Value.ToString();
				string instructor = row.Cells["InstructorName"].Value.ToString();

				txtCourseName.Text = courseName;
				txtCourseCode.Text = courseCode;
				cmbCredits.Text = credits; 
				txtDescription.Text = description;

				cmbDepartment.SelectedIndexChanged -= cmbDepartment_SelectedIndexChanged;
				cmbDepartment.SelectedIndex = cmbDepartment.FindStringExact(department);
				cmbDepartment.SelectedIndexChanged += cmbDepartment_SelectedIndexChanged;

				if (cmbDepartment.SelectedValue != null)
				{
					int selectedDepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);

					DataTable instructorsData = DatabaseManager.GetInstructorsByDepartment(selectedDepartmentID);
					cmbTeacher.DataSource = instructorsData;
					cmbTeacher.DisplayMember = "FullName";
					cmbTeacher.ValueMember = "InstructorID";
				}

				cmbTeacher.SelectedIndex = cmbTeacher.FindStringExact(instructor);
			}

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			pnlUpdate.Visible=false;
		}

		public static class DatabaseManager
		{
			public static DataTable GetDepartments()
			{
				DataTable dataTable = new DataTable();
				string sqlQuery = "SELECT DepartmentID, DepartmentName FROM Departments";
				using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						connection.Open();
						SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
						dataAdapter.Fill(dataTable);
					}
				}
				return dataTable;
			}


			public static DataTable GetInstructorsByDepartment(int departmentID)
			{
				DataTable dataTable = new DataTable();
				string sqlQuery = @"
                                  SELECT 
                                  i.InstructorID, 
                                  p.FirstName + ' ' + p.LastName AS FullName
                                  FROM 
                                  Instructors i
                                  INNER JOIN 
                                  Profiles p ON i.ProfileID = p.ProfileID
                                  WHERE 
                                  i.DepartmentID = @DepartmentID
                                  AND
                                  p.Status = 'Active';
                                  ";

				using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						command.Parameters.AddWithValue("@DepartmentID", departmentID);
						connection.Open();
						SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
						dataAdapter.Fill(dataTable);
					}
				}
				return dataTable;
			}


		}

		private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
		{

			if (cmbDepartment.SelectedValue != null && cmbDepartment.SelectedValue.ToString() != "")
			{
				try
				{
					int selectedDepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);

					DataTable instructorsData = DatabaseManager.GetInstructorsByDepartment(selectedDepartmentID);

					cmbTeacher.DataSource = instructorsData;
					cmbTeacher.DisplayMember = "FullName";
					cmbTeacher.ValueMember = "InstructorID";
				}
				catch (InvalidCastException ex)
				{
					Console.WriteLine("InvalidCastException: " + ex.Message);
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message);
				}
			}
		}


		private void AddLogEntry(string name, string action, string description)
		{

			string sqlQuery = "INSERT INTO Logs (Name, Action, Description) VALUES (@name, @action, @description)";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
				{
					cmd.Parameters.AddWithValue("@name", name);
					cmd.Parameters.AddWithValue("@action", action);
					cmd.Parameters.AddWithValue("@description", description);

					try
					{
						conn.Open();
						cmd.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error logging action: " + ex.Message);
					}
				}
			}
		}
	}
}
