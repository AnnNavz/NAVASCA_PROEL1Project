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
		}



		private void picBack_Click(object sender, EventArgs e)
		{
			AdminSubjects adminSubjects = new AdminSubjects();
			adminSubjects.Show();
			this.Hide();
		}



		private void btnSubmit_Click(object sender, EventArgs e)
		{
			errorProvider1.Clear();
			errorProvider2.Clear();
			errorProvider3.Clear();
			errorProvider4.Clear();
			errorProvider5.Clear();

			string action = "Add Subject";
			string description = "Added a new subject in " + cmbDepartment.Text + "." ;
			string AddName = txtCourseName.Text;
			string Code = txtCode.Text;
			string formattedCode = Code.ToUpper();


			bool requiredFieldsMissing = false;

			if (string.IsNullOrWhiteSpace(txtCourseName.Text)) { errorProvider1.SetError(txtCourseName, "Course name is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbCredits.Text)) { errorProvider2.SetError(cmbCredits, "Credits is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(txtDescription.Text)) { errorProvider3.SetError(txtDescription, "Description is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbDepartment.Text)) { errorProvider4.SetError(cmbDepartment, "Department is required."); requiredFieldsMissing = true; }
			if (string.IsNullOrWhiteSpace(cmbTerm.Text)) { errorProvider5.SetError(cmbTerm, "Term is required."); requiredFieldsMissing = true; }

			if (requiredFieldsMissing)
			{
				return;
			}

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				string courseName = txtCourseName.Text;
				string status = "Active";


				conn.Open();

				SqlCommand Checkcmd = new SqlCommand("SELECT COUNT(*) FROM Courses WHERE CourseCode = @code", conn);
				Checkcmd.Parameters.AddWithValue("@code", txtCode.Text);

				int CodeCount = (int)Checkcmd.ExecuteScalar();

				if (CodeCount > 0)
				{
					MessageBox.Show("This course code is already in use.", "Course Code Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}


				SqlCommand cmd = new SqlCommand("AddSubject_SP", conn);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
				cmd.Parameters.AddWithValue("@CourseCode", formattedCode);
				cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
				cmd.Parameters.AddWithValue("@Credits", cmbCredits.Text);
				cmd.Parameters.AddWithValue("@CourseSem", cmbTerm.Text);
				cmd.Parameters.AddWithValue("@Department", cmbDepartment.Text);
				cmd.Parameters.AddWithValue("@Status", status);
				cmd.Parameters.AddWithValue("@Action", action);
				cmd.Parameters.AddWithValue("@AddDescription", description);
				cmd.Parameters.AddWithValue("@AddName", AddName);


				cmd.ExecuteNonQuery();
				MessageBox.Show("Added Subject Successful!" + "\n CourseCode: " + formattedCode,
								"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

				AdminSubjects adminSubjects = new AdminSubjects();
				adminSubjects.Show();
				this.Hide();

			}





		}

		


	}
	
}
