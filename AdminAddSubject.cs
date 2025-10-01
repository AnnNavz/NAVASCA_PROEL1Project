using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
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

		string connectionString = Database.ConnectionString;


		private void AdminAddSubject_Load(object sender, EventArgs e)
		{

			DataTable departmentsData = DatabaseManager.GetDepartments();
			cmbDepartment.DataSource = departmentsData;
			cmbDepartment.DisplayMember = "DepartmentName";
			cmbDepartment.ValueMember = "DepartmentID";
		}


		public static class DatabaseManager
		{
			public static DataTable GetDepartments()
			{
				DataTable dataTable = new DataTable();
				string sqlQuery = "SELECT DepartmentID, DepartmentName FROM Departments";
				using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						connection.Open();
						SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
						dataAdapter.Fill(dataTable);
					}
				}
				return dataTable;
			}


			public static DataTable GetInstructorsByDepartment(int departmentID)
			{
				DataTable dataTable = new DataTable();
				string sqlQuery = @"
                                  SELECT 
                                  i.InstructorID, 
                                  p.FirstName + ' ' + p.LastName AS FullName
                                  FROM 
                                  Instructors i
                                  INNER JOIN 
                                  Profiles p ON i.ProfileID = p.ProfileID
                                  WHERE 
                                  i.DepartmentID = @DepartmentID
                                  AND
                                  p.Status = 'Active';
                                  ";

				using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, connection))
					{
						// Add the parameter to the command to prevent SQL injection
						command.Parameters.AddWithValue("@DepartmentID", departmentID);
						connection.Open();
						SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
						dataAdapter.Fill(dataTable);
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


		private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbDepartment.SelectedValue != null && cmbDepartment.SelectedValue.ToString() != "")
			{
				try
				{
					int selectedDepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);

					DataTable instructorsData = DatabaseManager.GetInstructorsByDepartment(selectedDepartmentID);

					cmbTeacher.DataSource = instructorsData;
					cmbTeacher.DisplayMember = "FullName";
					cmbTeacher.ValueMember = "InstructorID";
				}
				catch (InvalidCastException ex)
				{
					Console.WriteLine("InvalidCastException: " + ex.Message);
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred: " + ex.Message);
				}
			}
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			errorProvider1.Clear();
			errorProvider2.Clear();
			errorProvider3.Clear();
			errorProvider4.Clear();
			errorProvider5.Clear();

			string action = "Add Subject";
			string description = "Added a new subject";
			string AddName = txtCourseName.Text;


			bool requiredFieldsMissing = false;

			if (string.IsNullOrWhiteSpace(txtCourseName.Text)) { errorProvider1.SetError(txtCourseName, "Course name is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbCredits.Text)) { errorProvider2.SetError(cmbCredits, "Credits is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(txtDescription.Text)) { errorProvider3.SetError(txtDescription, "Description is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbDepartment.Text)) { errorProvider4.SetError(cmbDepartment, "Department is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbTeacher.Text)) { errorProvider5.SetError(cmbTeacher, "Teacher is required."); requiredFieldsMissing = true; }

			if (requiredFieldsMissing)
			{
				return;
			}

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string courseName = txtCourseName.Text;
				string generatedCode = CourseCodeGenerator.GenerateCode(courseName);
				string status = "Active";


				conn.Open();


				SqlCommand cmd = new SqlCommand("AddSubject_SP", conn);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
				cmd.Parameters.AddWithValue("@CourseCode", generatedCode);
				cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
				cmd.Parameters.AddWithValue("@Credits", cmbCredits.Text);
				cmd.Parameters.AddWithValue("@Teacher", cmbTeacher.Text);
				cmd.Parameters.AddWithValue("@Department", cmbDepartment.Text);
				cmd.Parameters.AddWithValue("@Status", status);
				cmd.Parameters.AddWithValue("@Action", action);
				cmd.Parameters.AddWithValue("@AddDescription", description);
				cmd.Parameters.AddWithValue("@AddName", AddName);


				cmd.ExecuteNonQuery();
				MessageBox.Show("Added Subject Successful!" + "\n CourseCode: " + generatedCode,
								"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

				AdminSubjects adminSubjects = new AdminSubjects();
				adminSubjects.Show();
				this.Hide();

			}





		}

		public static class CourseCodeGenerator
		{
			public static string GenerateCode(string courseName)
			{
				string cleanedName = Regex.Replace(courseName, "[^a-zA-Z0-9 ]", "");
				string[] words = cleanedName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				StringBuilder courseCode = new StringBuilder();

				foreach (string word in words)
				{
					string prefix = word.Length >= 3 ? word.Substring(0, 3) : word;
					courseCode.Append(prefix.ToUpper());
				}

				return courseCode.ToString();
			}



		}
	}
	
}
