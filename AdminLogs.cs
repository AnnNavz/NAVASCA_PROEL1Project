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

			string sqlQuery = "SELECT LogID, Name, Action, Description, Date, " +
							  "CONVERT(VARCHAR(8), Time, 100) AS Time " +
							  "FROM Logs " +
							  "ORDER BY " +
							  "LogID DESC";

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

			string sqlQuery = "SELECT LogID, Name, Action, Description, Date, " +
							  "CONVERT(VARCHAR(8), Time, 100) AS Time " +
							  "FROM Logs ";

			if (int.TryParse(searchTerm, out int numericSearchTerm))
			{
				sqlQuery += "WHERE LogID = @searchTerm";
			}
			else if (DateTime.TryParse(searchTerm, out DateTime dateValue))
			{
				sqlQuery += "WHERE Date = @searchTerm";
			}
			else
			{
				sqlQuery += "WHERE Name LIKE @searchTerm OR Action LIKE @searchTerm OR Description LIKE @searchTerm ";
			}

			sqlQuery += " ORDER BY LogID DESC";

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
