using Guna.UI2.WinForms.Suite;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace NAVASCA_PROEL1Project
{
	public partial class AdminEnrollment : Form
	{
		public AdminEnrollment()
		{
			InitializeComponent();
			DateRecorded = DateTime.Now;
			
		}

		private int StudentID;
		private string StudentName;
		string connectionString = Database.ConnectionString;
		private DateTime DateRecorded;

		public AdminEnrollment(int studentID, string studentName) : this()
		{
			StudentID = studentID;
			StudentName = studentName;

			this.Text = $"Enrollment - Student ID: {StudentID}, {StudentName}";
		}

		private void picBack_Click(object sender, EventArgs e)
		{
			AdminStudents students = new AdminStudents();
			students.Show();
			this.Hide();
		}

		
		private void btnSubmit_Click(object sender, EventArgs e)
		{

			errorProvider1.Clear();
			errorProvider2.Clear();
			errorProvider3.Clear();

			string action = "Enroll Student";
			string description = $"Enrolled a student in {cmbProgram.Text} (Section {cmbSection.Text}, {cmbSemester.Text})";
			string name = StudentName;


			bool requiredFieldsMissing = false;

			if (string.IsNullOrWhiteSpace(cmbProgram.Text)) { errorProvider1.SetError(cmbProgram, "Program is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbSection.Text)) { errorProvider2.SetError(cmbSection, "Section is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbSemester.Text)) { errorProvider3.SetError(cmbSemester, "Semester is required."); requiredFieldsMissing = true; }

			if (requiredFieldsMissing)
			{
				return;
			}


			string Semester = string.Empty;

			if (cmbSemester.SelectedIndex == 0)
			{
				Semester = "Second Semester";
			}
			else if (cmbSemester.SelectedIndex == 1)
			{
				Semester = "First Semester";
			}
			else
			{
				throw new InvalidOperationException("Please select a valid semester.");

			}




			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();


				SqlCommand cmd = new SqlCommand("Enrollment_SP", conn);
				cmd.CommandType = CommandType.StoredProcedure;


				cmd.Parameters.AddWithValue("@StudentID", StudentID);
				cmd.Parameters.AddWithValue("@Semester", Semester);
				cmd.Parameters.AddWithValue("@Program", cmbProgram.Text);
				cmd.Parameters.AddWithValue("@Section", cmbSection.Text);
				cmd.Parameters.AddWithValue("@DateRecorded", DateRecorded);
				cmd.Parameters.AddWithValue("@Action", action);
				cmd.Parameters.AddWithValue("@Description", description);
				cmd.Parameters.AddWithValue("@AddName", name);


				cmd.ExecuteNonQuery();
				MessageBox.Show("Enrolled Student Successful!" + "\n Student ID: " + StudentID,
								"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

				AdminStudents students = new AdminStudents();
				students.Show();
				this.Hide();

			}
		}

		private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbProgram.SelectedValue != null && cmbProgram.SelectedValue != DBNull.Value)
			{
				try
				{
					if (cmbProgram.SelectedValue is int selectedProgramID)
					{
						DataTable sectionsData = GetSectionsByProgram(selectedProgramID);

						ClearSectionComboBox(cmbSection);
						cmbSection.DataSource = sectionsData;
						cmbSection.DisplayMember = "SectionName";
						cmbSection.ValueMember = "SectionID";
						cmbSection.SelectedIndex = -1;
					}
					else
					{
						ClearSectionComboBox(cmbSection);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading sections: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				ClearSectionComboBox(cmbSection);
			}
		}

		private void AdminEnrollment_Load(object sender, EventArgs e)
		{
			FillProgramComboBox(cmbProgram);
			ClearSectionComboBox(cmbSection);
		}

		private void ClearSectionComboBox(ComboBox cmbSection)
		{
			cmbSection.DataSource = null;
			cmbSection.Items.Clear();
			cmbSection.Text = "";
		}

		private void FillProgramComboBox(ComboBox cmbProgram)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					string query = "SELECT ProgramID, ProgramName FROM Programs ORDER BY ProgramName";

					SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
					DataTable dt = new DataTable();
					adapter.Fill(dt);

					cmbProgram.DataSource = null;

					cmbProgram.DataSource = dt;
					cmbProgram.DisplayMember = "ProgramName";
					cmbProgram.ValueMember = "ProgramID";

					cmbProgram.SelectedIndex = -1; 
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading Programs: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private DataTable GetSectionsByProgram(int programID)
		{
			DataTable dataTable = new DataTable();

			string sqlQuery = @"
            SELECT 
                s.SectionID, s.SectionName 
            FROM 
                Sections s 
            WHERE 
                s.ProgramID = @programID
            ORDER BY
                s.SectionName";

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						command.Parameters.AddWithValue("@programID", programID);
						connection.Open();

						SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
						dataAdapter.Fill(dataTable);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Database Error fetching sections: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return dataTable;
		}

	}
}
