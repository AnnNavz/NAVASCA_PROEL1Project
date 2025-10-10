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
using System.Xml.Linq;

namespace NAVASCA_PROEL1Project
{
	public partial class AdminStudentAddSubject : Form
	{
		public AdminStudentAddSubject()
		{
			InitializeComponent();
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;

		}

		private int StudentID;

		string connectionString = Database.ConnectionString;

		public AdminStudentAddSubject(int studentID) : this()
		{
			StudentID = studentID;
			LoadCourses();
		}

		private void LoadCourses()
		{
			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits " +
				  "FROM Enrollment AS e " +
				  "INNER JOIN Programs AS r ON e.ProgramID = r.ProgramID " +
				  "INNER JOIN Departments AS d ON r.DepartmentID = d.DepartmentID " +
				  "INNER JOIN Courses AS c ON c.DepartmentID = d.DepartmentID " +
				  "INNER JOIN Semesters AS s ON s.SemesterID = e.SemesterID " +
				  "WHERE e.StudentID = @StudentID " +
				  "  AND s.TermName = c.CourseSem " +
				  "  AND NOT EXISTS ( " +
				  "    SELECT 1 " +
				  "    FROM EnrollSubjects AS es " +
				  "    WHERE es.EnrollmentID = e.EnrollmentID " +
				  "      AND es.CourseID = c.CourseID " +
				  "  );";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					dataAdapter.SelectCommand.Parameters.AddWithValue("@StudentID", StudentID);

					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					CoursesData.AutoGenerateColumns = false;
					CoursesData.Columns.Clear();

					CoursesData.ReadOnly = false;

					DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
					checkBoxColumn.HeaderText = "Select Subject";
					checkBoxColumn.Name = "SubjectSelected";
					checkBoxColumn.MinimumWidth = 50;
					checkBoxColumn.TrueValue = true;
					checkBoxColumn.FalseValue = false;

					checkBoxColumn.ReadOnly = false;

					CoursesData.Columns.Add(checkBoxColumn);

					var courseIdCol = new DataGridViewTextBoxColumn();
					courseIdCol.Name = "CourseID";
					courseIdCol.HeaderText = "Course ID";
					CoursesData.Columns.Add(courseIdCol);

					CoursesData.Columns.Add("CourseName", "Course Name");
					CoursesData.Columns.Add("CourseCode", "Course Code");
					CoursesData.Columns.Add("Description", "Description");
					CoursesData.Columns.Add("Credits", "Credits");

					foreach (DataGridViewColumn col in CoursesData.Columns)
					{
						if (dataTable.Columns.Contains(col.Name))
						{
							col.DataPropertyName = col.Name;

							col.ReadOnly = true;
						}
					}

					CoursesData.DataSource = dataTable;

					foreach (DataGridViewRow row in CoursesData.Rows)
					{
						if (!row.IsNewRow)
						{
							row.Cells["SubjectSelected"].Value = false;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An error occurred while loading courses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		private void picBack_Click(object sender, EventArgs e)
		{
			AdminStudents students = new AdminStudents();
			students.Show();
			this.Hide();
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			bool requiredFieldsMissing = false;

			if (!CoursesData.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow && r.Cells["SubjectSelected"].Value != null && (bool)r.Cells["SubjectSelected"].Value))
			{
				MessageBox.Show("No subjects to add.", "Add Subject Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				requiredFieldsMissing = true;
			}

			foreach (DataGridViewRow row in CoursesData.Rows)
			{
				if (row.IsNewRow || row.DataBoundItem == null) continue;

				object selectedValue = row.Cells["SubjectSelected"].Value;
				bool isChecked = selectedValue != null && selectedValue != DBNull.Value && (bool)selectedValue;

				if (isChecked)
				{
					object courseIdValue = row.Cells["CourseID"].Value;

					if (courseIdValue == null || courseIdValue == DBNull.Value)
					{
						MessageBox.Show("A selected subject row is empty. Please uncheck empty or invalid rows.", "Add Subject Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						row.Cells["SubjectSelected"].Value = false;
						requiredFieldsMissing = true;
						break;
					}
				}
			}

			if (requiredFieldsMissing)
			{
				return;
			}

			string getEnrollmentIDQuery = "SELECT EnrollmentID FROM Enrollment WHERE StudentID = @studentID_int";
			int enrollmentID = 0;

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					using (SqlCommand cmd = new SqlCommand(getEnrollmentIDQuery, conn))
					{
						cmd.Parameters.AddWithValue("@studentID_int", StudentID);
						conn.Open();
						object result = cmd.ExecuteScalar();
						if (result != null)
						{
							enrollmentID = Convert.ToInt32(result);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not find the Enrollment ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string enrollSubjectsQuery = "INSERT INTO EnrollSubjects (EnrollmentID, CourseID, Grade) VALUES (@EnrollmentID, @CourseID, @Grade)";

			if (MessageBox.Show("Do you want to add these subjects?", "Add Subject Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{

					try
					{
						conn.Open();

						SqlTransaction transaction = conn.BeginTransaction();

						try
						{


							foreach (DataGridViewRow row in CoursesData.Rows)
							{
								if (row.IsNewRow || row.DataBoundItem == null) continue;

								object selectedSubValue = row.Cells["SubjectSelected"].Value;
								bool isSubjectSelected = selectedSubValue != null && selectedSubValue != DBNull.Value && (bool)selectedSubValue;

								if (isSubjectSelected)
								{
									object courseIdValue = row.Cells["CourseID"].Value;

									int courseId = Convert.ToInt32(courseIdValue);
									decimal grade = 0.00M;
									using (SqlCommand cmdSubject = new SqlCommand(enrollSubjectsQuery, conn, transaction))
									{
										cmdSubject.Parameters.AddWithValue("@EnrollmentID", enrollmentID);
										cmdSubject.Parameters.AddWithValue("@CourseID", courseId);
										cmdSubject.Parameters.AddWithValue("@Grade", grade);

										cmdSubject.ExecuteNonQuery();
									}
								}

								transaction.Commit();

								MessageBox.Show("Add Subjects Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

								
							}
						}
						catch (Exception ex)
						{
							try
							{
								transaction.Rollback();
							}
							catch (Exception exRollback)
							{
								MessageBox.Show("Transaction rollback failed: " + exRollback.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							MessageBox.Show("Enrollment Failed. " +
											"Details: " + ex.Message,
											"Error",
											MessageBoxButtons.OK,
											MessageBoxIcon.Error);
						}

					}
					catch (Exception exConn)
					{
						MessageBox.Show("Database connection error: " + exConn.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

			}
		}
	}
}
