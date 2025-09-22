using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAVASCA_PROEL1Project
{
	public partial class ForgotConfirmPassword : Form
	{

		public ForgotConfirmPassword()
		{
			InitializeComponent();
		}

		string connectionString = Database.ConnectionString;
		private string email;

		public ForgotConfirmPassword(string userEmail)
		{
			InitializeComponent();
			this.email = userEmail;
		}

		private string HashPassword(string plainPassword)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(plainPassword);
				byte[] hash = sha256.ComputeHash(bytes);
				StringBuilder builder = new StringBuilder();
				foreach (byte b in hash)
				{
					builder.Append(b.ToString("x2"));
				}
				return builder.ToString();
			}
		}


		private void picBack_Click(object sender, EventArgs e)
		{
			ForgotEmailValidation forgotEmailValidation = new ForgotEmailValidation();
			forgotEmailValidation.Show();
			this.Hide();
		}

		private void btnConfirm_Click_1(object sender, EventArgs e)
		{

			string oldPassword = txtOld.Text.Trim();
			string newPassword = txtNew.Text.Trim();
			string confirmPassword = txtConfirm.Text.Trim();

			string hashedOldPassword = HashPassword(oldPassword);
			string hashedNewPassword = HashPassword(newPassword);

			if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
			{
				MessageBox.Show("All fields are required.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (newPassword != confirmPassword)
			{
				MessageBox.Show("New password and confirmation do not match.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();


					string getProfileIdQuery = "SELECT ProfileID FROM Profiles WHERE Email = @Email";
					SqlCommand getProfileIdCmd = new SqlCommand(getProfileIdQuery, connection);
					getProfileIdCmd.Parameters.AddWithValue("@Email", email);
					object profileIdObj = getProfileIdCmd.ExecuteScalar();

					if (profileIdObj == null)
					{
						MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					int profileId = (int)profileIdObj;


					string checkQuery = "SELECT COUNT(*) FROM Users WHERE Password = @OldPassword AND ProfileID = @ProfileID";
					SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
					checkCmd.Parameters.AddWithValue("@OldPassword", hashedOldPassword);
					checkCmd.Parameters.AddWithValue("@ProfileID", profileId);

					int exists = (int)checkCmd.ExecuteScalar();
					if (exists == 0)
					{
						MessageBox.Show("Old password is incorrect.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}


					string updateQuery = "UPDATE Users SET Password = @NewPassword WHERE ProfileID = @ProfileID";
					SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
					updateCmd.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
					updateCmd.Parameters.AddWithValue("@ProfileID", profileId);

					int rows = updateCmd.ExecuteNonQuery();
					if (rows > 0)
					{
						MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
						Login login = new Login();
						login.Show();
						this.Hide();
					}
					else
					{
						MessageBox.Show("Failed to update password. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (SqlException ex)
				{
					MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception ex)
				{
					MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
