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
using System.Windows.Forms.DataVisualization.Charting;

namespace NAVASCA_PROEL1Project
{
	public partial class AdminDashboard : Form
	{
		public AdminDashboard()
		{
			InitializeComponent();
			LoadStudentChart();
			LoadTeacherChart();
			LoadCount();

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

		private void btnHome_Click(object sender, EventArgs e)
		{
			this.Show();
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

		private void LoadStudentChart()
		{
			string sqlQuery = "SELECT Status, COUNT(*) AS TotalCount " +
					  "FROM Profiles " +
					  "WHERE ProfileID IN (SELECT ProfileID FROM Users WHERE RoleID = (SELECT RoleID FROM Roles WHERE RoleName = 'Student')) " +
					  "GROUP BY Status";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					chartStudentStatus.Series.Clear();
					chartStudentStatus.ChartAreas.Clear();

					chartStudentStatus.ChartAreas.Add(new ChartArea("MainChart"));

					Series series = new Series("StudentStatus");
					series.ChartType = SeriesChartType.Column;
					series.IsValueShownAsLabel = true;

					foreach (DataRow row in dataTable.Rows)
					{
						string status = row["Status"].ToString();
						int count = Convert.ToInt32(row["TotalCount"]);
						series.Points.AddXY(status, count);
					}

					chartStudentStatus.Series.Add(series);

					chartStudentStatus.Titles.Clear();
					chartStudentStatus.Titles.Add("Student");
					chartStudentStatus.ChartAreas["MainChart"].AxisX.Title = "Status";
					chartStudentStatus.ChartAreas["MainChart"].AxisY.Title = "Number of Students";
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading the chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}




		private void LoadTeacherChart()
		{
			string sqlQuery = "SELECT Status, COUNT(*) AS TotalCount " +
					  "FROM Profiles " +
					  "WHERE ProfileID IN (SELECT ProfileID FROM Users WHERE RoleID = (SELECT RoleID FROM Roles WHERE RoleName = 'Instructor')) " +
					  "GROUP BY Status";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					chartTeacher.Series.Clear();
					chartTeacher.ChartAreas.Clear();

					chartTeacher.ChartAreas.Add(new ChartArea("MainChart"));

					Series series = new Series("TeacherStatus");
					series.ChartType = SeriesChartType.Column;
					series.IsValueShownAsLabel = true;

					foreach (DataRow row in dataTable.Rows)
					{
						string status = row["Status"].ToString();
						int count = Convert.ToInt32(row["TotalCount"]);
						series.Points.AddXY(status, count);
					}

					chartTeacher.Series.Add(series);

					chartTeacher.Titles.Clear();
					chartTeacher.Titles.Add("Teacher");
					chartTeacher.ChartAreas["MainChart"].AxisX.Title = "Status";
					chartTeacher.ChartAreas["MainChart"].AxisY.Title = "Number of Teacher";
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading the chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void LoadCount()
		{

			string sqlQuery_TotalStudentCount = "SELECT COUNT(p.ProfileID) " +
										  "FROM Profiles AS p " +
										  "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
										  "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
										  "WHERE r.RoleName = 'Student'";

			string sqlQuery_TotalTeacherCount = "SELECT COUNT(p.ProfileID) " +
										  "FROM Profiles AS p " +
										  "INNER JOIN Users AS u ON p.ProfileID = u.ProfileID " +
										  "INNER JOIN Roles AS r ON u.RoleID = r.RoleID " +
										  "WHERE r.RoleName = 'Instructor'";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{

				conn.Open();

				SqlCommand countCmd = new SqlCommand(sqlQuery_TotalStudentCount, conn);
				int StudentCount = (int)countCmd.ExecuteScalar();
				lblStudentTotal.Text = StudentCount.ToString();

				SqlCommand countCMD = new SqlCommand(sqlQuery_TotalTeacherCount, conn);
				int TeacherCount = (int)countCMD.ExecuteScalar();
				lblTeacherTotal.Text = TeacherCount.ToString();
			}
		}

	}
}
