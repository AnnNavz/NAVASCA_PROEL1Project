using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAVASCA_PROEL1Project
{
	public partial class AdminTeachers : Form
	{
		public AdminTeachers()
		{
			InitializeComponent();
			TeachersData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			LoadData();
		}

		string connectionString = Database.ConnectionString;
		private string selectedInstructorId;


		private void btnAdd_Click(object sender, EventArgs e)
		{
			AdminAddTeacher addTeacher = new AdminAddTeacher();
			addTeacher.Show();
			this.Hide();
		}



		private void LoadData()
		{
			string sqlQuery_TotalCount = "SELECT COUNT(p.ProfileID) " +
								  "FROM Profiles AS p " +
								  "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
								  "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
								  "WHERE r.RoleName = 'Instructor' AND p.Status = 'Active'";

			string sqlQuery_LoadData = "SELECT i.InstructorID, p.FirstName, p.LastName, p.Age, p.Gender, p.Phone, p.Address, p.Email, ISNULL(p.Status, 'Unknown') AS Status, d.DepartmentName " +
									   "FROM Profiles AS p " +
									   "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
									   "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
									   "INNER JOIN Instructors AS i ON p.ProfileID = i.ProfileID " +
									   "INNER JOIN Departments AS d ON i.DepartmentID = d.DepartmentID " +
									   "WHERE r.RoleName IN ('Instructor') AND p.Status = 'Active' " +
									   "ORDER BY " +
									   "p.ProfileID DESC";


			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();

					SqlCommand countCmd = new SqlCommand(sqlQuery_TotalCount, conn);
					int activeTeacherCount = (int)countCmd.ExecuteScalar();
					lblTotal.Text = activeTeacherCount.ToString();

					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery_LoadData, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					TeachersData.AutoGenerateColumns = false;
					TeachersData.Columns.Clear();
					TeachersData.ReadOnly = true;

					TeachersData.Columns.Add("InstructorID", "Instructor ID");
					TeachersData.Columns.Add("FirstName", "First Name");
					TeachersData.Columns.Add("LastName", "Last Name");
					TeachersData.Columns.Add("Age", "Age");
					TeachersData.Columns.Add("Gender", "Gender");
					TeachersData.Columns.Add("Phone", "Phone");
					TeachersData.Columns.Add("Address", "Address");
					TeachersData.Columns.Add("Email", "Email");
					TeachersData.Columns.Add("DepartmentName", "Department Name");
					TeachersData.Columns.Add("Status", "Status");

					TeachersData.Columns["Status"].Visible = false;

					DataGridViewButtonColumn manageButtonColumn = new DataGridViewButtonColumn();

					manageButtonColumn.HeaderText = " ";
					manageButtonColumn.Name = "Subjects";
					manageButtonColumn.Text = "Manage Subjects";
					manageButtonColumn.UseColumnTextForButtonValue = true;

					TeachersData.Columns.Add(manageButtonColumn);

					DataGridViewButtonColumn detailsButtonColumn = new DataGridViewButtonColumn();

					detailsButtonColumn.HeaderText = $"                                     ";
					detailsButtonColumn.Name = "Details";
					detailsButtonColumn.Text = "Details";
					detailsButtonColumn.UseColumnTextForButtonValue = true;

					TeachersData.Columns.Add(detailsButtonColumn);




					foreach (DataGridViewColumn col in TeachersData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}

					TeachersData.DataSource = dataTable;
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void btnDelete_Click(object sender, EventArgs e)
		{

			string getProfileIDQuery = "SELECT ProfileID FROM Instructors WHERE InstructorID = @instructorID_int";
			int profileID = 0;

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getProfileIDQuery, conn))
					{
						cmd.Parameters.AddWithValue("@instructorID_int", selectedInstructorId);
						conn.Open();
						object result = cmd.ExecuteScalar();
						if (result != null)
						{
							profileID = Convert.ToInt32(result);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the Profile ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				if (TeachersData.SelectedRows.Count > 0)
				{
					DataGridViewRow selectedRow = TeachersData.SelectedRows[0];


					string currentStatus = string.Empty;
					if (selectedRow.Cells["Status"].Value != null)
					{
						currentStatus = selectedRow.Cells["Status"].Value.ToString();
					}


					DialogResult confirmResult = MessageBox.Show($"Are you sure you want to deactivate this teacher?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if (confirmResult == DialogResult.Yes)
					{
						string newStatus = "Inactive";
						UpdateUserStatus(profileID, newStatus);


						string LogName = txtFirstname.Text + " " + txtLastname.Text;
						string logDescription = $"Deactivated a Teacher.";
						AddLogEntry(LogName, "Delete a Teacher", logDescription);
					}
				}
				else
				{
					MessageBox.Show("Please select a teacher to deactivate.", "No Teacher Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void UpdateUserStatus(int profileId, string newStatus)
		{
			

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					string updateQuery = "UPDATE Profiles SET Status = @newStatus WHERE ProfileID = @profileId";

					using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
					{
						cmd.Parameters.AddWithValue("@newStatus", newStatus);
						cmd.Parameters.AddWithValue("@profileId", profileId);

						int rowsAffected = cmd.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							MessageBox.Show($"Teacher has been set to '{newStatus}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

							LoadData();
						}
						else
						{
							MessageBox.Show("The status could not be updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"An error occurred while updating the database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			string searchTerm = txtSearch.Text.Trim();
			string parameterName = "";
			object parameterValue = null; 

			if (string.IsNullOrEmpty(searchTerm))
			{
				LoadData();
				return;
			}

			string sqlQuery = "SELECT i.InstructorID, p.FirstName, p.LastName, p.Age, p.Gender, p.Phone, p.Address, p.Email, p.Status, d.DepartmentName " +
							  "FROM Profiles AS p " +
							  "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
							  "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
							  "LEFT JOIN Instructors AS i ON p.ProfileID = i.ProfileID " +
							  "LEFT JOIN Departments AS d ON i.DepartmentID = d.DepartmentID " +
							  "WHERE r.RoleName = 'Instructor' AND p.Status = 'Active' AND ";

			if (int.TryParse(searchTerm, out int numericSearchTerm))
			{
				sqlQuery += "(p.ProfileID = @NumericTerm OR p.Age = @NumericTerm)";
				parameterName = "@NumericTerm";
				parameterValue = numericSearchTerm;
			}
			else if (searchTerm.Equals("Male", StringComparison.OrdinalIgnoreCase) || searchTerm.Equals("Female", StringComparison.OrdinalIgnoreCase))
			{
				sqlQuery += "p.Gender = @ExactTerm";
				parameterName = "@ExactTerm";
				parameterValue = searchTerm;
			}
			else
			{
				sqlQuery += "(p.FirstName LIKE @WildcardTerm OR p.LastName LIKE @WildcardTerm OR p.Phone LIKE @WildcardTerm OR p.Address LIKE @WildcardTerm OR p.Email LIKE @WildcardTerm OR p.Status LIKE @WildcardTerm OR d.DepartmentName LIKE @WildcardTerm)";
				parameterName = "@WildcardTerm";
				parameterValue = "%" + searchTerm + "%";
			}

			sqlQuery += " ORDER BY i.InstructorID DESC";


			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					if (parameterName != "")
					{
						dataAdapter.SelectCommand.Parameters.AddWithValue(parameterName, parameterValue);
					}

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					TeachersData.DataSource = dataTable;

					if (dataTable.Rows.Count == 0)
					{
						MessageBox.Show("No users found matching your search criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred during search: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			pnlUpdate.Visible = true;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			pnlUpdate.Visible = false;
		}


		string mailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$";
		string phonePattern = @"^(?:\+63|0)?9\d{9}$";
		string agePattern = @"^(1[0-9]{2}|[1-9]?[0-9])$";

		

		public static bool IsValid(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern);
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{

			errorProvider1.Clear();
			errorProvider2.Clear();
			errorProvider3.Clear();
			errorProvider4.Clear();
			errorProvider5.Clear();
			errorProvider6.Clear();
			errorProvider7.Clear();
			errorProvider8.Clear();


			if (string.IsNullOrEmpty(selectedInstructorId))
			{
				MessageBox.Show("Please select a teacher to update.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			bool requiredFieldsMissing = false;

			if (string.IsNullOrWhiteSpace(txtFirstname.Text)) { errorProvider1.SetError(txtFirstname, "First name is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(txtLastname.Text)) { errorProvider2.SetError(txtLastname, "Last name is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbGender.Text)) { errorProvider3.SetError(cmbGender, "Gender is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(txtAge.Text)) { errorProvider4.SetError(txtAge, "Age is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(txtPhone.Text)) { errorProvider5.SetError(txtPhone, "Phone number is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(txtAddress.Text)) { errorProvider6.SetError(txtAddress, "Address is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(txtEmail.Text)) { errorProvider7.SetError(txtEmail, "Email is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbDepartment.Text)) { errorProvider8.SetError(txtEmail, "Department is required."); requiredFieldsMissing = true; }

			if (requiredFieldsMissing)
			{
				return;
			}

			string getProfileIDQuery = "SELECT ProfileID FROM Instructors WHERE InstructorID = @instructorID_int";
			int profileID = 0;

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getProfileIDQuery, conn))
					{
						cmd.Parameters.AddWithValue("@instructorID_int", selectedInstructorId); 
						conn.Open();
						object result = cmd.ExecuteScalar();
						if (result != null)
						{
							profileID = Convert.ToInt32(result);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the Profile ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				string firstName = txtFirstname.Text;
				string lastName = txtLastname.Text;
				string gender = cmbGender.Text;
				string address = txtAddress.Text;
				string newEmail = txtEmail.Text;
				string age = txtAge.Text;
				string phone = txtPhone.Text;


				bool allValid = true;

				if (!IsValid(newEmail, mailPattern))
				{
					errorProvider1.SetError(txtEmail, "Please enter a valid Email.");
					allValid = false;
				}

				if (!IsValid(phone, phonePattern))
				{
					errorProvider2.SetError(txtPhone, "Please enter a valid Phone number.");
					allValid = false;
				}

				if (!IsValid(age, agePattern))
				{
					errorProvider3.SetError(txtAge, "Age is in invalid format.");
					allValid = false;
				}

				if (!allValid)
				{
					return;

				}

				string originalEmail = TeachersData.SelectedRows[0].Cells["Email"].Value.ToString();


				if (newEmail != originalEmail)
				{
					if (IsEmailTaken(newEmail, profileID))
					{
						MessageBox.Show("This email address is already in use by another user.", "Email Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}

				string selectedDepartmentName = cmbDepartment.SelectedItem.ToString();

				int departmentID = GetDepartmentID(selectedDepartmentName);

				if (departmentID == -1)
				{
					MessageBox.Show("Selected department not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}


				string sqlQuery = "UPDATE Profiles SET " +
								  "FirstName = @FirstName, " +
								  "LastName = @LastName, " +
								  "Age = @Age, " +
								  "Gender = @Gender, " +
								  "Phone = @Phone, " +
								  "Address = @Address, " +
								  "Email = @Email " +
								  "WHERE ProfileID = @profileId; " +
								  "UPDATE Instructors SET DepartmentID = @DepartmentID WHERE ProfileID = @profileId;";

				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					try
					{
						conn.Open();
						SqlCommand cmd = new SqlCommand(sqlQuery, conn);

						cmd.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
						cmd.Parameters.AddWithValue("@LastName", txtLastname.Text);
						cmd.Parameters.AddWithValue("@Age", txtAge.Text);
						cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
						cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
						cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
						cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
						cmd.Parameters.AddWithValue("@profileId",profileID);
						cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

						int rowsAffected = cmd.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							string LogName = txtFirstname.Text + " " +  txtLastname.Text;
							string logDescription = $"Updated a teacher.";
							AddLogEntry(LogName, "Update Teacher", logDescription);

							MessageBox.Show("Teacher updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
							LoadData();
						}
						else
						{
							MessageBox.Show("No records were updated. Profile not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("An error occurred during the update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred during the update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}




		private bool IsEmailTaken(string email, int currentProfileId)
		{
			
			string sqlQuery = "SELECT COUNT(*) FROM Profiles WHERE Email = @email AND ProfileID != @currentProfileId";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
				{
					cmd.Parameters.AddWithValue("@email", email);
					cmd.Parameters.AddWithValue("@currentProfileId", currentProfileId);
					conn.Open();
					int count = (int)cmd.ExecuteScalar();
					return count > 0;
				}
			}
		}

		private int GetDepartmentID(string departmentName)
		{
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

		private void TeachersData_CellClick(object sender, DataGridViewCellEventArgs e)
		{

			if (e.RowIndex >= 0)
			{
				DataGridViewRow row = TeachersData.Rows[e.RowIndex];

				selectedInstructorId = row.Cells["InstructorID"].Value.ToString();

				string firstName = row.Cells["FirstName"].Value.ToString();
				string lastName = row.Cells["LastName"].Value.ToString();
				string age = row.Cells["Age"].Value.ToString();
				string gender = row.Cells["Gender"].Value.ToString();
				string phone = row.Cells["Phone"].Value.ToString();
				string address = row.Cells["Address"].Value.ToString();
				string email = row.Cells["Email"].Value.ToString();
				string department = row.Cells["DepartmentName"].Value.ToString();

				txtFirstname.Text = firstName;
				txtLastname.Text = lastName;
				txtAge.Text = age;
				txtPhone.Text = phone;
				txtAddress.Text = address;
				txtEmail.Text = email;
				cmbGender.Text = gender;
				cmbDepartment.Text = department;
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
			this.Show();
		}

		private void btnSubjects_Click(object sender, EventArgs e)
		{
			AdminSubjects adminSubjects = new AdminSubjects();
			adminSubjects.Show();
			this.Hide();
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

		private void TeachersData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			object teacherIDObject = TeachersData.Rows[e.RowIndex].Cells["InstructorID"].Value;
			object teacherFirstNameObject = TeachersData.Rows[e.RowIndex].Cells["FirstName"].Value;
			object teacherLastNameObject = TeachersData.Rows[e.RowIndex].Cells["LastName"].Value;



			if (teacherIDObject is DBNull || teacherIDObject == null)
			{
				MessageBox.Show("Please select a teacher.", "Teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			int selectedTeacherID = Convert.ToInt32(teacherIDObject);

			if (selectedTeacherID == 0)
			{
				MessageBox.Show("Please select a teacher.", "Teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			string selectedTeacherName = (teacherFirstNameObject.ToString()) + " " + (teacherLastNameObject.ToString());

			string columnName = TeachersData.Columns[e.ColumnIndex].Name;

			if (columnName == "Details")
			{
				OpenDetails(selectedTeacherID, selectedTeacherName);
				this.Hide();
			}
			else if (columnName == "Subjects")
			{
				OpenSubjects(selectedTeacherID, selectedTeacherName);
				this.Hide();
			}


		}

		private void OpenDetails(int profileID, string teacherName)
		{
			AdminTeacherDetails details = new AdminTeacherDetails(profileID, teacherName);
			details.Show();
		}

		private void OpenSubjects(int profileID, string teacherName)
		{
			AdminTeacherSubjects subjects = new AdminTeacherSubjects(profileID, teacherName);
			subjects.Show();
		}



	}
}
