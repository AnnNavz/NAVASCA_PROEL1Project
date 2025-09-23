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
	public partial class AdminStudents : Form
	{
		public AdminStudents()
		{
			InitializeComponent();
			LoadData();
			this.StudentData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StudentData_CellContentClick);
			StudentData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			StudentData.ReadOnly = true;


		}

		string connectionString = Database.ConnectionString;

		private void LoadData()
		{
			string connectionString = "Data Source=DESKTOP-5QHCE6M; Initial Catalog=NAVASCA_DB; Integrated Security=true";

			// SQL query to count students with 'Active' status
			string sqlQuery_TotalCount = "SELECT COUNT(p.ProfileID) " +
										  "FROM Profiles AS p " +
										  "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
										  "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
										  "WHERE r.RoleName = 'Student' AND p.Status = 'Active'";

			// SQL query to load all student data for the DataGridView, sorted by status
			string sqlQuery_LoadData = "SELECT p.ProfileID, p.FirstName, p.LastName, p.Age, p.Gender, p.Phone, p.Address, p.Email, ISNULL(p.Status, 'Unknown') AS Status " +
									   "FROM Profiles AS p " +
									   "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
									   "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
									   "WHERE r.RoleName IN ('Student') AND p.Status <> 'Inactive' " + // Exclude inactive users
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
					int activeStudentCount = (int)countCmd.ExecuteScalar();
					lblTotal.Text = activeStudentCount.ToString();

					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery_LoadData, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					StudentData.AutoGenerateColumns = false;
					StudentData.Columns.Clear();
					StudentData.ReadOnly = true;

					StudentData.Columns.Add("ProfileID", "Profile ID");
					StudentData.Columns.Add("FirstName", "First Name");
					StudentData.Columns.Add("LastName", "Last Name");
					StudentData.Columns.Add("Age", "Age");
					StudentData.Columns.Add("Gender", "Gender");
					StudentData.Columns.Add("Phone", "Phone");
					StudentData.Columns.Add("Address", "Address");
					StudentData.Columns.Add("Email", "Email");
					StudentData.Columns.Add("Status", "Status");

					DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
					btnColumn.Name = "StatusActionButton";
					btnColumn.HeaderText = "Change Status";
					btnColumn.Text = "Approve";
					btnColumn.UseColumnTextForButtonValue = true;
					StudentData.Columns.Insert(9, btnColumn);

					foreach (DataGridViewColumn col in StudentData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}
					StudentData.DataSource = dataTable;
					StudentData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnLogout_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want log out?", "Pizsity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Login login = new Login();
				login.Show();
				this.Close();
			}
		}

		private void btnHome_Click(object sender, EventArgs e)
		{
			AdminDashboard adminDashboard = new AdminDashboard();
			adminDashboard.Show();
			this.Hide();
		}

		private void btnStudents_Click(object sender, EventArgs e)
		{
			this.Show();
		}

		private void btnTeachers_Click(object sender, EventArgs e)
		{
			AdminTeachers adminTeachers = new AdminTeachers();
			adminTeachers.Show();
			this.Hide();
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

		private void StudentData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && StudentData.Columns[e.ColumnIndex].Name == "StatusActionButton")
			{
				DataGridViewRow row = StudentData.Rows[e.RowIndex];
				string profileId = row.Cells["ProfileID"].Value.ToString();
				string currentStatus = row.Cells["Status"].Value.ToString();
				string newStatus = string.Empty;

				if (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
				{
					DialogResult result = MessageBox.Show($"The student is pending. Do you want to activate them?", "Approve Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
					DialogResult result = MessageBox.Show($"The student is inactive. Do you want to re-activate them?", "Re-activate Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
				if (StudentData.SelectedRows.Count > 0)
				{
					DataGridViewRow selectedRow = StudentData.SelectedRows[0];

					string profileId = selectedRow.Cells["ProfileID"].Value.ToString();

					string currentStatus = string.Empty;
					if (selectedRow.Cells["Status"].Value != null)
					{
						currentStatus = selectedRow.Cells["Status"].Value.ToString();
					}

					if (currentStatus.Equals("Inactive", StringComparison.OrdinalIgnoreCase))
					{
						MessageBox.Show("This student is already inactive.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return; 
					}

					DialogResult confirmResult = MessageBox.Show($"Are you sure you want to deactivate Student {profileId}?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if (confirmResult == DialogResult.Yes)
					{
						string newStatus = "Inactive";
						UpdateUserStatus(profileId, newStatus);

						string logDescription = $"Deactivated a student";
						AddLogEntry(Convert.ToInt32(profileId), "Delete Student", logDescription);
					}
				}
				else
				{
					MessageBox.Show("Please select a student to deactivate.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
							MessageBox.Show($"Student {profileId} has been set to '{newStatus}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
					StudentData.DataSource = dataTable;

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

		private void btnAdd_Click(object sender, EventArgs e)
		{
			AdminAddStudent addStudent = new AdminAddStudent();
			addStudent.Show();
			this.Hide();
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
				MessageBox.Show("Please select a student to update.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

				string originalEmail = StudentData.SelectedRows[0].Cells["Email"].Value.ToString();

				
				if (newEmail != originalEmail)
				{
					if (IsEmailTaken(newEmail, selectedProfileId))
					{
						MessageBox.Show("This email address is already in use by another user.", "Email Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}

				
				string sqlQuery = "UPDATE Profiles SET " +
								  "FirstName = @firstName, " +
								  "LastName = @lastName, " +
								  "Age = @age, " +
								  "Gender = @gender, " +
								  "Phone = @phone, " +
								  "Address = @address, " +
								  "Email = @email " +
								  "WHERE ProfileID = @profileId";

				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
					{
						cmd.Parameters.AddWithValue("@firstName", firstName);
						cmd.Parameters.AddWithValue("@lastName", lastName);
						cmd.Parameters.AddWithValue("@age", age);
						cmd.Parameters.AddWithValue("@gender", gender);
						cmd.Parameters.AddWithValue("@phone", phone);
						cmd.Parameters.AddWithValue("@address", address);
						cmd.Parameters.AddWithValue("@email", newEmail);
						cmd.Parameters.AddWithValue("@profileId", selectedProfileId);

						conn.Open();
						int rowsAffected = cmd.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							MessageBox.Show("Student profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
							LoadData();
							pnlUpdate.Visible = false;

							string logDescription = $"Updated a student";
							AddLogEntry(Convert.ToInt32(selectedProfileId), "Update Student", logDescription);
						}
						else
						{
							MessageBox.Show("No records were updated. Profile not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
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

		private void StudentData_CellClick(object sender, DataGridViewCellEventArgs e)
		{

			if (e.RowIndex >= 0)
			{
				DataGridViewRow row = StudentData.Rows[e.RowIndex];

				selectedProfileId = row.Cells["ProfileID"].Value.ToString();

				string firstName = row.Cells["FirstName"].Value.ToString();
				string lastName = row.Cells["LastName"].Value.ToString();
				string age = row.Cells["Age"].Value.ToString();
				string gender = row.Cells["Gender"].Value.ToString();
				string phone = row.Cells["Phone"].Value.ToString();
				string address = row.Cells["Address"].Value.ToString();
				string email = row.Cells["Email"].Value.ToString();

				txtFirstname.Text = firstName;
				txtLastname.Text = lastName;
				txtAge.Text = age;
				txtPhone.Text = phone;
				txtAddress.Text = address;
				txtEmail.Text = email;

				cmbGender.Text = gender;
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
	}
}
