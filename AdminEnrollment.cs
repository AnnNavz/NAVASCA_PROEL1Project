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
			CoursesData.CellBorderStyle = DataGridViewCellBorderStyle.Single;

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

			//if (IsStudentEnrolled(StudentID))
			//{
			//	btnSubmit.Enabled = false;
			//}
			//else if(IsStudentNotEnrolled(StudentID)) 
			//{
			//	btnUpdate.Enabled = false;
			//}
		}

		private void LoadCourses(string semester, int programID)
		{
			string sqlQuery = "SELECT c.CourseID, c.CourseName, c.CourseCode, c.Description, c.Credits " +
					"FROM Courses AS c " +
					"INNER JOIN Departments AS d ON c.DepartmentID = d.DepartmentID " +
					"INNER JOIN Programs AS r ON r.DepartmentID = d.DepartmentID " +
					"WHERE r.ProgramID = @programID AND c.CourseSem = @semester";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();
					SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);

					dataAdapter.SelectCommand.Parameters.AddWithValue("@StudentID", StudentID);
					dataAdapter.SelectCommand.Parameters.AddWithValue("@semester", semester);
					dataAdapter.SelectCommand.Parameters.AddWithValue("@programID", programID);

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

							// 💡 CRITICAL CHANGE: Make all DATA columns ReadOnly = TRUE
							// We only want the checkbox column to be editable.
							col.ReadOnly = true;
						}
					}

					// You can also make the entire DataGridView ReadOnly = true, and ONLY set the Checkbox column to ReadOnly = false.
					// CoursesData.ReadOnly = true; // Set DGV to read-only
					// CoursesData.Columns["SubjectSelected"].ReadOnly = false; // Override for the checkbox

					CoursesData.DataSource = dataTable;

					// 💡 IMPORTANT: Ensure the default value for the checkbox column is FALSE
					// This initializes all rows' checkboxes to unchecked.
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

		//private bool IsStudentNotEnrolled(int currentStudentID)
		//{

		//	string sqlQuery = "SELECT COUNT(*) FROM Enrollment WHERE StudentID = @studentID;";

		//	using (SqlConnection conn = new SqlConnection(connectionString))
		//	{
		//		using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
		//		{
		//			cmd.Parameters.AddWithValue("@studentID", currentStudentID);
		//			conn.Open();
		//			int count = (int)cmd.ExecuteScalar();
		//			return count == 0;
		//		}
		//	}
		//}

		//private bool IsStudentEnrolled(int currentStudentID)
		//{

		//	string sqlQuery = "SELECT COUNT(*) FROM Enrollment WHERE StudentID = @studentID";

		//	using (SqlConnection conn = new SqlConnection(connectionString))
		//	{
		//		using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
		//		{
		//			cmd.Parameters.AddWithValue("@studentID", currentStudentID);
		//			conn.Open();
		//			int count = (int)cmd.ExecuteScalar();
		//			return count > 0;
		//		}
		//	}
		//}

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

			if (!CoursesData.Rows.Cast<DataGridViewRow>().Any(r => !r.IsNewRow && r.Cells["SubjectSelected"].Value != null && (bool)r.Cells["SubjectSelected"].Value))
			{
				MessageBox.Show("Please select at least one subject to enroll.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
						MessageBox.Show("A selected subject row is empty. Please uncheck empty or invalid rows.", "Enrollment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

			string Semester = (cmbSemester.SelectedIndex == 0) ? "Second Semester" :
							  (cmbSemester.SelectedIndex == 1) ? "First Semester" :
							   throw new InvalidOperationException("Please select a valid semester.");


			int newEnrollmentId = 0;
			string enrollSubjectsQuery = "INSERT INTO EnrollSubjects (EnrollmentID, CourseID, Grade) VALUES (@EnrollmentID, @CourseID, @Grade)";

			

				if (MessageBox.Show("Do you want to enrolled this student?" + "\n" +
				                "\nSemester: " + cmbSemester.Text + 
				                "\nProgram: " + cmbProgram.Text +
								"\nSection: " + cmbSection.Text, "Enrollment Confirm" , MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
			    {

				using (SqlConnection conn = new SqlConnection(connectionString))
				{

					try
					{
						conn.Open();

						SqlTransaction transaction = conn.BeginTransaction();

						try
						{

							SqlCommand cmdEnrollment = new SqlCommand("Enrollment_SP", conn, transaction);
							cmdEnrollment.CommandType = CommandType.StoredProcedure;

							cmdEnrollment.Parameters.AddWithValue("@StudentID", StudentID);
							cmdEnrollment.Parameters.AddWithValue("@Semester", Semester);
							cmdEnrollment.Parameters.AddWithValue("@Program", cmbProgram.Text);
							cmdEnrollment.Parameters.AddWithValue("@Section", cmbSection.Text);
							cmdEnrollment.Parameters.AddWithValue("@DateRecorded", DateRecorded);
							cmdEnrollment.Parameters.AddWithValue("@Action", action);
							cmdEnrollment.Parameters.AddWithValue("@Description", description);
							cmdEnrollment.Parameters.AddWithValue("@AddName", name);

							SqlParameter outputParam = cmdEnrollment.Parameters.Add("@NewEnrollmentID", SqlDbType.Int);
							outputParam.Direction = ParameterDirection.Output;

							cmdEnrollment.ExecuteNonQuery();

							newEnrollmentId = (int)outputParam.Value;

							if (newEnrollmentId <= 0)
							{
								throw new InvalidOperationException("Enrollment failed or returned an invalid Enrollment ID.");
							}

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
										cmdSubject.Parameters.AddWithValue("@EnrollmentID", newEnrollmentId);
										cmdSubject.Parameters.AddWithValue("@CourseID", courseId);
										cmdSubject.Parameters.AddWithValue("@Grade", grade);

										cmdSubject.ExecuteNonQuery();
									}
								}

								transaction.Commit();

								MessageBox.Show("Enrolled Student Successful!" + "\n Student: " + StudentName,
											"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

								AdminStudents students = new AdminStudents();
								students.Show();
								this.Hide();
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

						string SemesterID = string.Empty;

						if (cmbSemester.SelectedIndex == 0)
						{
							SemesterID = "Second Semester";
							LoadCourses(SemesterID, selectedProgramID);
						}
						if (cmbSemester.SelectedIndex == 1)
						{
							SemesterID = "First Semester";
							LoadCourses(SemesterID, selectedProgramID);
						}


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

		//private const string LoadSemester_SQL = @"SELECT
  //                        CONCAT(s.TermName, ' ', s.AcademicYear) AS FullSemesterName
  //                        FROM
  //                        Enrollment e
  //                        INNER JOIN
  //                        Semesters s ON e.SemesterID = s.SemesterID
  //                        WHERE
  //                        e.StudentID = @studentID_int;";
		//private const string LoadProgram_SQL = @"SELECT p.ProgramName FROM Enrollment e INNER JOIN Programs p ON p.ProgramID = e.ProgramID WHERE e.StudentID = @studentID_int;";
		//private const string LoadSection_SQL = @"SELECT s.SectionName FROM Enrollment e INNER JOIN Sections s ON s.SectionId = e.SectionID WHERE e.StudentID = @studentID_int;";

		private void AdminEnrollment_Load(object sender, EventArgs e)
		{
			FillProgramComboBox(cmbProgram);
			ClearSectionComboBox(cmbSection);

			//if (IsStudentEnrolled(StudentID))
			//{
			//	string enrolledSemester = GetScalarResultForStudent(LoadSemester_SQL, "@studentID_int", StudentID);
			//	string enrolledProgram = GetScalarResultForStudent(LoadProgram_SQL, "@studentID_int", StudentID);
			//	string enrolledSection = GetScalarResultForStudent(LoadSection_SQL, "@studentID_int", StudentID);

			//	if (!string.IsNullOrEmpty(enrolledProgram))
			//	{
			//		cmbProgram.SelectedIndex = cmbProgram.FindStringExact(enrolledProgram);

			//		if (cmbProgram.SelectedValue != null)
			//		{
			//			LoadSectionsAndSelectEnrolled(enrolledSection);
			//		}
			//	}

			//	cmbSemester.Text = enrolledSemester;
			//}
		}

		//private void LoadSectionsAndSelectEnrolled(string enrolledSectionName)
		//{
		//	if (cmbProgram.SelectedValue is int selectedProgramID)
		//	{
		//		DataTable sectionsData = GetSectionsByProgram(selectedProgramID);

		//		ClearSectionComboBox(cmbSection);
		//		cmbSection.DataSource = sectionsData;
		//		cmbSection.DisplayMember = "SectionName";
		//		cmbSection.ValueMember = "SectionID";

		//		if (!string.IsNullOrEmpty(enrolledSectionName))
		//		{
		//			cmbSection.SelectedIndex = cmbSection.FindStringExact(enrolledSectionName);
		//		}
		//		else
		//		{
		//			cmbSection.SelectedIndex = -1;
		//		}
		//	}
		//	else
		//	{
		//		ClearSectionComboBox(cmbSection);
		//	}
		//}

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
					string query = "SELECT ProgramID, ProgramName FROM Programs ORDER BY ProgramID";

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
                s.SectionID";

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

		private void cmbSemester_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbProgram.SelectedValue is int selectedProgramID)
			{
				string SemesterID = string.Empty;

				if (cmbSemester.SelectedIndex == 0)
				{
					SemesterID = "Second Semester";
					LoadCourses(SemesterID, selectedProgramID);
				}
				if (cmbSemester.SelectedIndex == 1)
				{
					SemesterID = "First Semester";
					LoadCourses(SemesterID, selectedProgramID);
				}


			}

		}


		//private string GetScalarResultForStudent(string sqlQuery, string parameterName, int studentIdValue)
		//{
		//	string result = string.Empty;

		//	try
		//	{
		//		using (SqlConnection conn = new SqlConnection(connectionString))
		//		using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
		//		{
		//			cmd.Parameters.AddWithValue(parameterName, studentIdValue);

		//			conn.Open();
		//			object scalarResult = cmd.ExecuteScalar();

		//			if (scalarResult != null)
		//			{
		//				result = scalarResult.ToString();
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show($"Database error: {ex.Message}",
		//						"Data Retrieval Error",
		//						MessageBoxButtons.OK,
		//						MessageBoxIcon.Error);
		//	}

		//	return result;
		//}

		//private void btnUpdate_Click(object sender, EventArgs e)
		//{
		//	errorProvider1.Clear();
		//	errorProvider2.Clear();
		//	errorProvider3.Clear();

		//	bool requiredFieldsMissing = false;

		//	if (string.IsNullOrWhiteSpace(cmbProgram.Text)) { errorProvider1.SetError(cmbProgram, "Program is required."); requiredFieldsMissing = true; }
		//	if (string.IsNullOrWhiteSpace(cmbSection.Text)) { errorProvider2.SetError(cmbSection, "Section is required."); requiredFieldsMissing = true; }
		//	if (string.IsNullOrWhiteSpace(cmbSemester.Text)) { errorProvider3.SetError(cmbSemester, "Semester is required."); requiredFieldsMissing = true; }

		//	if (cmbProgram.SelectedValue == null || cmbProgram.SelectedValue == DBNull.Value) { errorProvider1.SetError(cmbProgram, "Please select a program from the list."); requiredFieldsMissing = true; }
		//	if (cmbSection.SelectedValue == null || cmbSection.SelectedValue == DBNull.Value) { errorProvider2.SetError(cmbSection, "Please select a section from the list."); requiredFieldsMissing = true; }


		//	if (requiredFieldsMissing)
		//	{
		//		MessageBox.Show("Please ensure a valid selection is made for Program and Section.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//		return;
		//	}

		//	int programId = Convert.ToInt32(cmbProgram.SelectedValue);
		//	int sectionId = Convert.ToInt32(cmbSection.SelectedValue);
		//	string semesterName = cmbSemester.Text;
		//	int semesterId = GetSemesterIDByName(semesterName);

		//	if (semesterId == -1)
		//	{
		//		MessageBox.Show($"Could not find a valid ID for the selected semester: {semesterName}. Please check your selection.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//		return;
		//	}


		//	string sqlUpdate = @"
		//              UPDATE Enrollment
		//              SET 
		//                  SemesterID = @SemesterID,
		//                  ProgramID = @ProgramID,
		//                  SectionID = @SectionID,
		//                  DateRecorded = GETDATE() 
		//              WHERE StudentID = @StudentID;";

		//	try
		//	{
		//		using (SqlConnection conn = new SqlConnection(connectionString))
		//		{
		//			conn.Open();

		//			SqlCommand cmd = new SqlCommand(sqlUpdate, conn);

		//			// 6. Add Parameters
		//			cmd.Parameters.AddWithValue("@StudentID", StudentID);
		//			cmd.Parameters.AddWithValue("@SemesterID", semesterId);
		//			cmd.Parameters.AddWithValue("@ProgramID", programId);
		//			cmd.Parameters.AddWithValue("@SectionID", sectionId);

		//			// 7. Execute the update
		//			int rowsAffected = cmd.ExecuteNonQuery();

		//			if (rowsAffected > 0)
		//			{

		//				MessageBox.Show($"Enrollment Updated Successfully for Student: {StudentName}",
		//								"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

		//				AdminStudents students = new AdminStudents();
		//				students.Show();
		//				this.Hide();
		//			}
		//			else
		//			{
		//				MessageBox.Show("No records were updated. Enrollment data may be the same or the student was not found.",
		//								"Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show("An error occurred during the update process: " + ex.Message,
		//						"Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//}

		//private int GetSemesterIDByName(string semesterName)
		//{
		//	// Assuming Semesters table has TermName and AcademicYear columns 
		//	// and the semesterName is in the CONCAT(TermName, ' ', AcademicYear) format
		//	string sqlQuery = @"
		//              SELECT SemesterID FROM Semesters 
		//              WHERE CONCAT(TermName, ' ', AcademicYear) = @SemesterName;";

		//	try
		//	{
		//		using (SqlConnection conn = new SqlConnection(connectionString))
		//		using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
		//		{
		//			cmd.Parameters.AddWithValue("@SemesterName", semesterName);
		//			conn.Open();
		//			object result = cmd.ExecuteScalar();

		//			if (result != null && result != DBNull.Value)
		//			{
		//				return Convert.ToInt32(result);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		// In a real application, you should log this error:
		//		Console.WriteLine($"Error retrieving Semester ID: {ex.Message}");
		//	}

		//	return -1; // Indicate failure
		//}
	}
}
