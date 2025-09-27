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
	public partial class AdminLogs : Form
	{
		public AdminLogs()
		{
			InitializeComponent();
			LogsData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
			LoadLogs();
		}

		string connectionString = Database.ConnectionString;

		private void LoadLogs()
		{

			string sqlQuery = "SELECT l.LogID, p.FirstName, p.LastName, l.Action, l.Description, l.Date, " +
							  "CONVERT(VARCHAR(8), l.Time, 100) AS Time " +
							  "FROM Logs l " +
							  "INNER JOIN Profiles p ON l.ProfileID = p.ProfileID " +
							  "INNER JOIN Users u ON p.ProfileID = u.ProfileID " +
							  "INNER JOIN Roles r ON u.RoleID = r.RoleID " +
							  "WHERE r.RoleName IN ('Student', 'Instructor') " + 
							  "ORDER BY " +
							  "l.LogID DESC";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					LogsData.DataSource = dataTable;
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{

			string searchTerm = txtSearch.Text.Trim();

			if (string.IsNullOrEmpty(searchTerm))
			{
				LoadLogs();
				return;
			}

			string sqlQuery = "SELECT l.LogID, p.FirstName, p.LastName, l.Action, l.Description, l.Date, " +
							  "CONVERT(VARCHAR(8), l.Time, 100) AS Time " +
							  "FROM Logs l " +
							  "INNER JOIN Profiles p ON l.ProfileID = p.ProfileID ";

			if (int.TryParse(searchTerm, out int numericSearchTerm))
			{
				sqlQuery += "WHERE l.LogID = @searchTerm";
			}
			else if (DateTime.TryParse(searchTerm, out DateTime dateValue))
			{
				sqlQuery += "WHERE l.Date = @searchTerm";
			}
			else
			{
				sqlQuery += "WHERE p.FirstName LIKE @searchTerm OR p.LastName LIKE @searchTerm OR l.Action LIKE @searchTerm OR l.Description LIKE @searchTerm";
			}

			sqlQuery += " ORDER BY " +
						"CASE l.Action " +
						"WHEN 'Add Student' THEN 1 " +
						"WHEN 'Delete Student' THEN 2 " +
						"WHEN 'Update Student' THEN 3 " +
						"WHEN 'Add Teacher' THEN 4 " +
						"WHEN 'Delete Teacher' THEN 5 " +
						"WHEN 'Update Teacher' THEN 6 " +
						"ELSE 7 END, " +
						"l.Date DESC, l.Time DESC";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					if (int.TryParse(searchTerm, out int numericValue))
					{
						dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", numericValue);
					}
					else if (DateTime.TryParse(searchTerm, out DateTime date))
					{
						dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", date.Date);
					}
					else
					{
						dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
					}

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);
					LogsData.DataSource = dataTable;

					if (dataTable.Rows.Count == 0)
					{
						MessageBox.Show("No logs found matching your search criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			this.Show();
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
	}
}
