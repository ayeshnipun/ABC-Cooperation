using ABC_Cooperation.Forms.Authentication.Registration;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ABC_Cooperation.Forms.Authentication.Login
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=AYESHN-LT\SQLEXPRESS;Initial Catalog=abc-coop-ems;Integrated Security=True");

        private void btn_Login_Click(object sender, EventArgs e)
        {
            con.Open();

            string username = txt_Username.Text;
            string password = txt_Password.Text;

            string querySelect = "SELECT * FROM Login WHERE username ='" + username + "' AND password = '" + password + "'";

            SqlCommand cmd = new SqlCommand(querySelect, con);
            SqlDataReader row = cmd.ExecuteReader();

            if (row.HasRows)
            {
                this.Hide();
                var register = new Register();
                register.Show();
            }
            else
            {
                MessageBox.Show("Invalid login credentials, please check the username and password, and try again !", "Invalid login details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure, Do you really want to exit....?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Username.Clear();
            txt_Password.Clear();
        }
    }
}