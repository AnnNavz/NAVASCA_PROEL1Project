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
							  "c.CourseSem, d.DepartmentName, c.Status " +
							  "FROM Courses AS c " +
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

					CoursesData.AutoGenerateColumns = false;
					CoursesData.Columns.Clear();
					CoursesData.ReadOnly = true;

					CoursesData.Columns.Add("CourseID", "Course ID");
					CoursesData.Columns.Add("CourseName", "Course Name");
					CoursesData.Columns.Add("CourseCode", "Course Code");
					CoursesData.Columns.Add("Description", "Description");
					CoursesData.Columns.Add("Credits", "Credits");
					CoursesData.Columns.Add("CourseSem", "Term");
					CoursesData.Columns.Add("DepartmentName", "Department Name");
					CoursesData.Columns.Add("Status", "Status");

					DataGridViewButtonColumn detailsButton = new DataGridViewButtonColumn();


					detailsButton.HeaderText = $"                                     ";
					detailsButton.Name = "Details";
					detailsButton.Text = "Details";
					detailsButton.UseColumnTextForButtonValue = true;

					CoursesData.Columns.Add(detailsButton);


					foreach (DataGridViewColumn col in CoursesData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}

					CoursesData.Columns["Status"].Visible = false;


					CoursesData.DataSource = dataTable;
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
							  "c.CourseSem, d.DepartmentName, c.Status " +
							  "FROM Courses AS c " +
							  "INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
							  "WHERE c.Status = 'Active' AND " +
							  "(c.CourseID LIKE @searchTerm OR c.CourseName LIKE @searchTerm OR c.CourseCode LIKE @searchTerm OR c.Description LIKE @searchTerm OR c.CourseSem LIKE @searchTerm OR d.DepartmentName LIKE @searchTerm)";

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

			int DepartmentID = 0;

			if (cmbDepartment.SelectedIndex == 0)
			{
				DepartmentID = 1;
			}
			if (cmbDepartment.SelectedIndex == 1)
			{
				DepartmentID = 2;
			}
			if (cmbDepartment.SelectedIndex == 2)
			{
				DepartmentID = 3;
			}
			if (cmbDepartment.SelectedIndex == 2)
			{
				DepartmentID = 4;
			}

			string sqlQuery = "UPDATE Courses SET " +
							  "CourseName = @CourseName, " +
							  "CourseCode = @CourseCode, " +
							  "Credits = @Credits, " +
							  "Description = @Description, " +
							  "DepartmentID = @DepartmentID, " +
							  "CourseSem = @Term " +
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
					cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);
					cmd.Parameters.AddWithValue("@Term", cmbTerm.Text);

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
				string Term = row.Cells["CourseSem"].Value.ToString();

				txtCourseName.Text = courseName;
				txtCourseCode.Text = courseCode;
				cmbCredits.Text = credits; 
				txtDescription.Text = description;
				cmbTerm.Text = Term;
				cmbDepartment.Text = department;
				
			}

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			pnlUpdate.Visible=false;
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

		private void CoursesData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			object CourseID = CoursesData.Rows[e.RowIndex].Cells["CourseID"].Value;
			object CourseName = CoursesData.Rows[e.RowIndex].Cells["CourseName"].Value;

			if (CourseID is DBNull || CourseID == null)
			{
				MessageBox.Show("Please select a teacher.", "Teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			int selectedCourseID = Convert.ToInt32(CourseID);

			if (selectedCourseID == 0)
			{
				MessageBox.Show("Please select a teacher.", "Teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			string selectedCourseName = (CourseName.ToString());

			string columnName = CoursesData.Columns[e.ColumnIndex].Name;

			if (columnName == "Details")
			{
				OpenDetails(selectedCourseID, selectedCourseName);
				this.Hide();
			}
		}

		private void OpenDetails(int courseID, string CourseName)
		{
			AdminSubjectDetails details = new AdminSubjectDetails(courseID, CourseName);
			details.Show();
		}
	}
}
