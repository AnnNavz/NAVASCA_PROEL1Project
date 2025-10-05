using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAVASCA_PROEL1Project
{
	public partial class AdminEnrollment : Form
	{
		public AdminEnrollment()
		{
			InitializeComponent();
		}

		private int StudentID;

		public AdminEnrollment(int studentID) : this()
		{
			StudentID = studentID;

			this.Text = $"Enrollment - Student ID: {StudentID}";
		}
	}
}
