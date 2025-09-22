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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

		string connectionString = Database.ConnectionString;

		private int loginAttempts = 0;
        private const int MAX_ATTEMPTS = 3;

       

        private static string HashPassword(string plainPassword)
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


        private void btnRegister_Click(object sender, EventArgs e)
        {
            Form1 register = new Form1();
            register.ShowDialog();
            this.Hide();
        }


        private void pbNoVisible_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            pbNoVisible.Visible = false;
            pbVisible.Visible =true;
        }

        private void pbVisible_Click(object sender, EventArgs e)
        {
            pbVisible.Visible =false;
            pbNoVisible.Visible=true;
            txtPassword.UseSystemPasswordChar = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ForgotEmailValidation forgotEmailValidation = new ForgotEmailValidation();
            forgotEmailValidation.Show();
            this.Hide();
        }

		private void btnSubmit_Click(object sender, EventArgs e)
		{

			errorProvider1.Clear();
			errorProvider2.Clear();


			bool isValid = true;
			string username = txtUsername.Text.Trim();
			string plainPassword = txtPassword.Text.Trim();

			string plainpassword = txtPassword.Text;
			string hashpassword = (HashPassword(plainpassword));

			if (string.IsNullOrWhiteSpace(username))
			{
				errorProvider1.SetError(txtUsername, "Username is required.");
				isValid = false;
			}

			if (string.IsNullOrWhiteSpace(plainPassword))
			{
				errorProvider2.SetError(txtPassword, "Password is required.");
				isValid = false;
			}

			if (isValid)
			{
				
				string PlainPassword = txtPassword.Text; 
				string hashedPassword = HashPassword(plainPassword); 

				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					SqlCommand cmd = new SqlCommand("Login_SP", conn);
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@username", txtUsername.Text);
					cmd.Parameters.AddWithValue("@password", hashedPassword);

					conn.Open();

					SqlDataReader reader = cmd.ExecuteReader();

					if (reader.Read())
					{
						loginAttempts = 0;

						
						int userId = Convert.ToInt32(reader["UserID"]);
						string Username = reader["Username"].ToString();
						string status = reader["Status"].ToString();
						int roleId = Convert.ToInt32(reader["RoleID"]);
						string roleName = reader["RoleName"].ToString();
						string firstName = reader["FirstName"].ToString();
						string lastName = reader["LastName"].ToString();

						if (status == "Pending")
						{
							MessageBox.Show("Your account is pending approval. Please wait for the admin to approve your account.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.Show();
							return;
						}

						if (status == "Inactive")
						{
							MessageBox.Show("Your account is inactive. Please wait for the admin to approve your account.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.Show();
							return;
						}

						
						MessageBox.Show($"Login Successful! Welcome, {roleName} {firstName} {lastName}.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

						
						if (roleId == 1)
						{
							this.Hide();
							AdminDashboard adminDashboard = new AdminDashboard();
							adminDashboard.Show();
						}
						else if (roleId == 2)
						{
							this.Hide();
							TeacherDashboard teacherDashboard = new TeacherDashboard();
							teacherDashboard.Show();
						}
						else if (roleId == 3)
						{
							this.Hide();
							StudentDashboard studentDashboard = new StudentDashboard();
							studentDashboard.Show();
						}
					}
					else
					{
						loginAttempts++;

						if (loginAttempts >= MAX_ATTEMPTS)
						{
							MessageBox.Show("Maximum login attempts exceeded. The application will now close.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
							Application.Exit();
						}
						else
						{
							MessageBox.Show($"Invalid username or password. You have {MAX_ATTEMPTS - loginAttempts} attempts remaining.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
			}

		}

		private void Login_Load(object sender, EventArgs e)
		{
			pbNoVisible.Visible = false;
		}
	}
}
