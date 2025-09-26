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
	public partial class AdminApproval : Form
	{
		public AdminApproval()
		{
			InitializeComponent();
			LoadData();
			ApprovalData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			ApprovalData.ReadOnly = true;
		}


		string connectionString = Database.ConnectionString;

		private void LoadData()
		{
			string sqlQuery_TotalCount = "SELECT COUNT(p.ProfileID) " +
								 "FROM Profiles AS p " +
								 "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
								 "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
								 "WHERE r.RoleName = 'Student' AND p.Status = 'Pending'";

			// SQL query to load all pending student data for the DataGridView
			string sqlQuery_LoadData = "SELECT p.ProfileID, p.FirstName, p.LastName, p.Age, p.Gender, p.Phone, p.Address, p.Email, ISNULL(p.Status, 'Unknown') AS Status " +
									   "FROM Profiles AS p " +
									   "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
									   "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
									   "WHERE r.RoleName IN ('Student', 'Instructor') AND p.Status = 'Pending' " +
									   "ORDER BY p.ProfileID";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();

					SqlCommand countCmd = new SqlCommand(sqlQuery_TotalCount, conn);
					int pendingStudentCount = (int)countCmd.ExecuteScalar();
					lblTotal.Text = pendingStudentCount.ToString();

					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery_LoadData, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					ApprovalData.AutoGenerateColumns = false;
					ApprovalData.Columns.Clear();
					ApprovalData.ReadOnly = true;

					ApprovalData.Columns.Add("ProfileID", "Profile ID");
					ApprovalData.Columns.Add("FirstName", "First Name");
					ApprovalData.Columns.Add("LastName", "Last Name");
					ApprovalData.Columns.Add("Age", "Age");
					ApprovalData.Columns.Add("Gender", "Gender");
					ApprovalData.Columns.Add("Phone", "Phone");
					ApprovalData.Columns.Add("Address", "Address");
					ApprovalData.Columns.Add("Email", "Email");
					ApprovalData.Columns.Add("Status", "Status");

					DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
					btnColumn.Name = "StatusActionButton";
					btnColumn.HeaderText = "Change Status";
					btnColumn.Text = "Approve";
					btnColumn.UseColumnTextForButtonValue = true;
					ApprovalData.Columns.Insert(0, btnColumn);

					foreach (DataGridViewColumn col in ApprovalData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;
						}
					}
					ApprovalData.DataSource = dataTable;

					// Hide the Status column
					ApprovalData.Columns["Status"].Visible = false;

					ApprovalData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnApproval_Click(object sender, EventArgs e)
		{
			this.Show();
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

		private void ApprovalData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && ApprovalData.Columns[e.ColumnIndex].Name == "StatusActionButton")
			{
				DataGridViewRow row = ApprovalData.Rows[e.RowIndex];
				string profileId = row.Cells["ProfileID"].Value.ToString();
				string currentStatus = row.Cells["Status"].Value.ToString();
				string newStatus = string.Empty;

				if (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
				{
					DialogResult result = MessageBox.Show($"Do you want to activate this student?", "Approve Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
				if (ApprovalData.SelectedRows.Count > 0)
				{
					DataGridViewRow selectedRow = ApprovalData.SelectedRows[0];

					string profileId = selectedRow.Cells["ProfileID"].Value.ToString();

					string currentStatus = string.Empty;
					if (selectedRow.Cells["Status"].Value != null)
					{
						currentStatus = selectedRow.Cells["Status"].Value.ToString();
					}


					DialogResult confirmResult = MessageBox.Show($"Are you sure you want to delete this Student {profileId}?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if (confirmResult == DialogResult.Yes)
					{
						string newStatus = "Inactive";
						UpdateUserStatus(profileId, newStatus);

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
	}
}
