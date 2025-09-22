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
	public partial class AdminAddSubject : Form
	{
		public AdminAddSubject()
		{
			InitializeComponent();
		}

		private void AdminAddSubject_Load(object sender, EventArgs e)
		{
		}


		public static class DatabaseManager
		{
			public static DataTable GetInstructorsWithNames()
			{
				DataTable dataTable = new DataTable();
				string sqlQuery = @"
                 SELECT 
                 i.InstructorID, 
                 p.FirstName + ' ' + p.LastName AS FullName
                 FROM 
                 Instructors i
                 JOIN 
                 Profiles p ON i.ProfileID = p.ProfileID;
                 ";

				using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						try
						{
							connection.Open();
							SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
							dataAdapter.Fill(dataTable);
						}
						catch (Exception ex)
						{
							MessageBox.Show("An error occurred: " + ex.Message);
						}
					}
				}
				return dataTable;
			}
		}

		private void picBack_Click(object sender, EventArgs e)
		{
			AdminSubjects adminSubjects = new AdminSubjects();
			adminSubjects.Show();
			this.Hide();
		}

		private void cmbTeacher_Click(object sender, EventArgs e)
		{

			DataTable instructorsData = DatabaseManager.GetInstructorsWithNames();

			cmbTeacher.DataSource = instructorsData;

			cmbTeacher.DisplayMember = "FullName";

			cmbTeacher.ValueMember = "InstructorID";

		}
	}
}
