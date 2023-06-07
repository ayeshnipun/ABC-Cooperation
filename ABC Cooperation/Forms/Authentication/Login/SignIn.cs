using ABC_Cooperation.Forms.Authentication.Registration;
using System;
using System.Windows.Forms;

namespace ABC_Cooperation.Forms.Authentication.Login
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_Username.Text;
            string password = txt_Password.Text;

            var register = new Register();
            register.Show();
            this.Hide();
            //if (username == "admin" && password == "admin")
            //{
            //}
            //else
            //{
            //    MessageBox.Show("Invalid username or password. Please try again.");
            //}
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Username.Clear();
            txt_Password.Clear();
        }
    }
}