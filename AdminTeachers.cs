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
			LoadData();
			this.TeachersData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TeachersData_CellContentClick);
			TeachersData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			TeachersData.ReadOnly = true;
		}

		string connectionString = Database.ConnectionString;

		private void btnLogout_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want log out?", "Pizsity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Login login = new Login();
				login.Show();
				this.Close();
			}
		}

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

			// SQL query to load teacher data, including the department name
			string sqlQuery_LoadData = "SELECT p.ProfileID, p.FirstName, p.LastName, p.Age, p.Gender, p.Phone, p.Address, p.Email, ISNULL(p.Status, 'Unknown') AS Status, d.DepartmentName " +
									   "FROM Profiles AS p " +
									   "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
									   "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
									   "INNER JOIN Instructors AS i ON p.ProfileID = i.ProfileID " + // Join with Instructors table
									   "INNER JOIN Departments AS d ON i.DepartmentID = d.DepartmentID " + // Join with Departments table
									   "WHERE r.RoleName IN ('Instructor') AND p.Status <> 'Inactive' " +
									   "ORDER BY " +
									   "CASE p.Status " +
									   "WHEN 'Active' THEN 1 " +
									   "WHEN 'Pending' THEN 2 " +
									   "ELSE 3 END";

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

					// Add the new 'Department Name' column
					TeachersData.Columns.Add("ProfileID", "Profile ID");
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




					foreach (DataGridViewColumn col in TeachersData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}
					TeachersData.DataSource = dataTable;
					TeachersData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void TeachersData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

			if (e.RowIndex >= 0 && TeachersData.Columns[e.ColumnIndex].Name == "StatusActionButton")
			{
				DataGridViewRow row = TeachersData.Rows[e.RowIndex];
				string profileId = row.Cells["ProfileID"].Value.ToString();
				string currentStatus = row.Cells["Status"].Value.ToString();
				string newStatus = string.Empty;

				if (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
				{
					DialogResult result = MessageBox.Show($"The teacher is pending. Do you want to activate them?", "Approve Teacher", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						newStatus = "Active";
					}
					else
					{
						return;
					}
				}
				else if (currentStatus.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
				{
					DialogResult result = MessageBox.Show($"The teacher is inactive. Do you want to re-activate them?", "Re-activate Teacher", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (result == DialogResult.Yes)
					{
						newStatus = "Active";
					}
					else
					{
						return;
					}
				}
				if (!string.IsNullOrEmpty(newStatus))
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
									MessageBox.Show($"Successfully updated status to '{newStatus}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

									LoadData();
								}
								else
								{
									MessageBox.Show("No rows were affected. The update may have failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								}
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show("An error occurred while updating the database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (TeachersData.SelectedRows.Count > 0)
				{
					DataGridViewRow selectedRow = TeachersData.SelectedRows[0];

					string profileId = selectedRow.Cells["ProfileID"].Value.ToString();

					string currentStatus = string.Empty;
					if (selectedRow.Cells["Status"].Value != null)
					{
						currentStatus = selectedRow.Cells["Status"].Value.ToString();
					}

					if (currentStatus.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
					{
						MessageBox.Show("This teacher is already inactive.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}

					DialogResult confirmResult = MessageBox.Show($"Are you sure you want to deactivate Teacher {profileId}?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if (confirmResult == DialogResult.Yes)
					{
						string newStatus = "Inactive";
						UpdateUserStatus(profileId, newStatus);

						string logDescription = $"Deactivated a teacher";
						AddLogEntry(Convert.ToInt32(profileId), "Delete Teacher", logDescription);
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


		private void UpdateUserStatus(string profileId, string newStatus)
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
							MessageBox.Show($"Teacher {profileId} has been set to '{newStatus}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

			if (string.IsNullOrEmpty(searchTerm))
			{
				LoadData();
				return;
			}

			string sqlQuery = "SELECT p.ProfileID, p.FirstName, p.LastName, p.Age, p.Gender, p.Phone, p.Address, p.Email, p.Status " +
							  "FROM Profiles p " +
							  "INNER JOIN Users u ON p.ProfileID = u.ProfileID " +
							  "INNER JOIN Roles r ON u.RoleID = r.RoleID " +
							  "WHERE r.RoleName = 'Student' AND ";

			if (int.TryParse(searchTerm, out int numericSearchTerm))
			{
				sqlQuery += " (p.ProfileID = @searchTerm OR p.Age = @searchTerm)";
			}
			else if (searchTerm.Equals("Male", StringComparison.OrdinalIgnoreCase) || searchTerm.Equals("Female", StringComparison.OrdinalIgnoreCase))
			{
				sqlQuery += " p.Gender = @exactSearchTerm";
			}
			else
			{
				sqlQuery += " (p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm OR p.Phone LIKE @searchTerm OR p.Address LIKE @searchTerm OR p.Email LIKE @searchTerm OR p.Status LIKE @searchTerm)";
			}

			sqlQuery += " ORDER BY " +
						"CASE p.Status " +
						"WHEN 'Active' THEN 1 " +
						"WHEN 'Pending' THEN 2 " +
						"WHEN 'Inactive' THEN 3 " +
						"ELSE 4 END";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					// Add parameters based on the search type
					if (int.TryParse(searchTerm, out int numericSearchTerm2))
					{
						dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", numericSearchTerm2);
					}
					else if (searchTerm.Equals("Male", StringComparison.OrdinalIgnoreCase) || searchTerm.Equals("Female", StringComparison.OrdinalIgnoreCase))
					{
						dataAdapter.SelectCommand.Parameters.AddWithValue("@exactSearchTerm", searchTerm);
					}
					else
					{
						dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
					}

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);
					TeachersData.DataSource = dataTable;

					if (dataTable.Rows.Count == 0)
					{
						MessageBox.Show("No teachers found matching your search criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


		string mailPattern = @"^[\w\.-]+@gmail\.com$";
		string phonePattern = @"^(?:\+63|0)?9\d{9}$";
		string agePattern = @"^(1[0-9]{2}|[1-9]?[0-9])$";

		private string selectedProfileId;

		public static bool IsValid(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern);
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{

			errorProvider1.Clear();
			errorProvider2.Clear();
			errorProvider3.Clear();


			if (string.IsNullOrEmpty(selectedProfileId))
			{
				MessageBox.Show("Please select a teacher to update.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
					if (IsEmailTaken(newEmail, selectedProfileId))
					{
						MessageBox.Show("This email address is already in use by another user.", "Email Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}

				string selectedDepartmentName = cmbDepartment.SelectedItem.ToString();

				// Get the DepartmentID from the department name
				int departmentID = GetDepartmentID(selectedDepartmentName);

				if (departmentID == -1)
				{
					MessageBox.Show("Selected department not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// Assuming you have a variable for the ProfileID of the selected teacher
				int selectedProfileID = (int)TeachersData.SelectedRows[0].Cells["ProfileID"].Value;


				// SQL query to update both the Profiles and Instructors tables
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

						// Add parameters for the Profiles table update
						cmd.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
						cmd.Parameters.AddWithValue("@LastName", txtLastname.Text);
						cmd.Parameters.AddWithValue("@Age", txtAge.Text);
						cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
						cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
						cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
						cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
						cmd.Parameters.AddWithValue("@profileId", selectedProfileID);

						// Add parameter for the Instructors table update
						cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

						int rowsAffected = cmd.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							// Create log description
							string logDescription = $"Updated teacher with ProfileID: {selectedProfileID} and assigned to Department: {selectedDepartmentName}";

							// Assuming AddLogEntry is a pre-existing method
							// You will need to replace `currentAdminID` with the actual ID of the logged-in administrator.
							int currentAdminID = 1;
							AddLogEntry(currentAdminID, "Update Teacher", logDescription);

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




		private bool IsEmailTaken(string email, string currentProfileId)
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

				selectedProfileId = row.Cells["ProfileID"].Value.ToString();

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

		private void AddLogEntry(int profileID, string action, string description)
		{
			
			string sqlQuery = "INSERT INTO Logs (ProfileID, Action, Description) VALUES (@profileId, @action, @description)";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
				{
					cmd.Parameters.AddWithValue("@profileId", profileID);
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
	}
}
