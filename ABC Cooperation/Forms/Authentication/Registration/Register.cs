using ABC_Cooperation.Forms.Authentication.Login;
using System;
using System.Windows.Forms;

namespace ABC_Cooperation.Forms.Authentication.Registration
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            rb_Male.Checked = true;
            rb_Female.Checked = false;
        }

        private void llb_Logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var signIn = new SignIn();
            signIn.Show();
            this.Close();
        }

        private void btn_Clear_Click(object sender, System.EventArgs e)
        {
            txt_DepartmentName.Clear();
            txt_Designation.Clear();
            txt_Email.Clear();
            txt_EmployeeType.Clear();
            txt_FirstName.Clear();
            txt_LastName.Clear();
            txt_MobliePhone.Clear();
            txt_HomePhone.Clear();
            cmb_EmployeeNo.Items.Clear();
            dtp_DateOfBirth.Value = DateTime.Now;
            rb_Female.Checked = false;
            rb_Male.Checked = true;
            rtb_Address.Clear();
        }
    }
}
