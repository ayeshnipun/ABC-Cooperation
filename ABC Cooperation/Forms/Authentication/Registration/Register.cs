using ABC_Cooperation.Forms.Authentication.Login;
using System;
using System.Data.SqlClient;
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

        private SqlConnection con = new SqlConnection(@"Data Source=AYESHN-LT\SQLEXPRESS;Initial Catalog=ems;Integrated Security=True");

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
            dtp_DateOfBirth.Format = DateTimePickerFormat.Custom;
            dtp_DateOfBirth.CustomFormat = "yyyy/MM/dd";
            dtp_DateOfBirth.Value = DateTime.Today;
            rb_Female.Checked = false;
            rb_Male.Checked = true;
            rtb_Address.Clear();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string firstName = txt_FirstName.Text;
                string lastName = txt_LastName.Text;
                dtp_DateOfBirth.Format = DateTimePickerFormat.Custom;
                dtp_DateOfBirth.CustomFormat = "yyyy/MM/dd";
                string gender;
                if (rb_Male.Checked)
                {
                    gender = "Male";
                }
                else { gender = "Female"; }

                string email = txt_Email.Text;
                string address = rtb_Address.Text;
                int mobilePhone = int.Parse(txt_MobliePhone.Text);
                int homePhone = int.Parse(txt_HomePhone.Text);
                string department = txt_DepartmentName.Text;
                string designation = txt_Designation.Text;
                string employeeType = txt_EmployeeType.Text;

                string insertQuery = "INSERT INTO Employee VALUES ('" + firstName + "','" + lastName + "','" + dtp_DateOfBirth.Text + "','" + gender + "','" + address + "','" + email + "','" + mobilePhone + "','" + homePhone + "','" + department + "','" + designation + "','" + employeeType + "')";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Record added successfully!", "Registered employee!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string selectEmployeesQuery = "SELECT * FROM Employee";

                SqlCommand fetchIdscmd = new SqlCommand(selectEmployeesQuery, con);
                SqlDataReader reader = fetchIdscmd.ExecuteReader();

                cmb_EmployeeNo.Items.Clear();
                cmb_EmployeeNo.Items.Add("New Register");
                while (reader.Read())
                {
                    cmb_EmployeeNo.Items.Add(reader[0].ToString());
                }
            }
            catch (SqlException sqlEx)
            {
                string msg = "Insert error";
                msg += sqlEx.Message;

                MessageBox.Show("Something went wrong! " + msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void llb_Exit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var res = MessageBox.Show("Are you sure, Do you really want to exit....?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string selectEmployeesQuery = "SELECT * FROM Employee";

                SqlCommand cmd = new SqlCommand(selectEmployeesQuery, con);
                SqlDataReader reader = cmd.ExecuteReader();

                cmb_EmployeeNo.Items.Add("New Register");
                while (reader.Read())
                {
                    cmb_EmployeeNo.Items.Add(reader[0].ToString());
                }
            }
            finally
            {
                con.Close();
                //btn_Register.Enabled = false;
                //btn_Update.Enabled = false;
                //btn_Delete.Enabled = false;

                dtp_DateOfBirth.Format = DateTimePickerFormat.Custom;
                dtp_DateOfBirth.CustomFormat = "yyyy/MM/dd";
                dtp_DateOfBirth.Value = DateTime.Today;
            }
        }

        private void cmb_EmployeeNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empNo = cmb_EmployeeNo.Text;

            if (empNo != "New Register")
            {
                try
                {
                    con.Open();

                    string selectEmployeeQuery = "SELECT * FROM Employee WHERE empNo = " + empNo;
                    SqlCommand cmd = new SqlCommand(selectEmployeeQuery, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        txt_FirstName.Text = reader[1].ToString();
                        txt_LastName.Text = reader[2].ToString();
                        dtp_DateOfBirth.Format = DateTimePickerFormat.Custom;
                        dtp_DateOfBirth.CustomFormat = "yyyy/MM/dd";
                        dtp_DateOfBirth.Text = reader[3].ToString();

                        if (reader[4].ToString() == "Male")
                        {
                            rb_Male.Checked = true;
                            rb_Female.Checked = false;
                        }
                        else
                        {
                            rb_Male.Checked = false; rb_Female.Checked = true;
                        }

                        rtb_Address.Text = reader[5].ToString();
                        txt_Email.Text = reader[6].ToString();
                        txt_MobliePhone.Text = reader[7].ToString();
                        txt_HomePhone.Text = reader[8].ToString();
                        txt_DepartmentName.Text = reader[9].ToString();
                        txt_Designation.Text = reader[10].ToString();
                        txt_EmployeeType.Text = reader[11].ToString();
                    }
                }
                finally { con.Close(); btn_Register.Enabled = false; btn_Update.Enabled = true; btn_Delete.Enabled = true; }
            }
            else
            {
                txt_DepartmentName.Clear();
                txt_Designation.Clear();
                txt_Email.Clear();
                txt_EmployeeType.Clear();
                txt_FirstName.Clear();
                txt_LastName.Clear();
                txt_MobliePhone.Clear();
                txt_HomePhone.Clear();
                dtp_DateOfBirth.Format = DateTimePickerFormat.Custom;
                dtp_DateOfBirth.CustomFormat = "yyyy/MM/dd";
                dtp_DateOfBirth.Value = DateTime.Today;
                rb_Female.Checked = false;
                rb_Male.Checked = true;
                rtb_Address.Clear();

                btn_Register.Enabled = true; btn_Update.Enabled = false; btn_Delete.Enabled = false;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure, Do you really want to delete this record....?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string empNo = cmb_EmployeeNo.Text;

            if (res == DialogResult.Yes && empNo != "New Register")
            {
                string deleteEmployeeQuery = "DELETE FROM Employee WHERE empNo =" + empNo;

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(deleteEmployeeQuery, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully", "Deleted Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string selectEmployeesQuery = "SELECT * FROM Employee";

                    SqlCommand fetchIdscmd = new SqlCommand(selectEmployeesQuery, con);
                    SqlDataReader reader = fetchIdscmd.ExecuteReader();

                    cmb_EmployeeNo.Items.Clear();
                    cmb_EmployeeNo.Items.Add("New Register");
                    while (reader.Read())
                    {
                        cmb_EmployeeNo.Items.Add(reader[0].ToString());
                    }
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            string empNo = cmb_EmployeeNo.Text;

            if (empNo != "New Register")
            {
                string firstName = txt_FirstName.Text;
                string lastName = txt_LastName.Text;
                dtp_DateOfBirth.Format = DateTimePickerFormat.Custom;
                dtp_DateOfBirth.CustomFormat = "yyyy/MM/dd";
                string gender;
                if (rb_Male.Checked)
                {
                    gender = "Male";
                }
                else { gender = "Female"; }

                string email = txt_Email.Text;
                string address = rtb_Address.Text;
                int mobilePhone = int.Parse(txt_MobliePhone.Text);
                int homePhone = int.Parse(txt_HomePhone.Text);
                string department = txt_DepartmentName.Text;
                string designation = txt_Designation.Text;
                string employeeType = txt_EmployeeType.Text;

                string query = "UPDATE Employee SET firstName = @FirstName, lastName = @LastName, dateOfBirth = @DateOfBirth, gender = @Gender, address = @Address, email = @Email, mobilePhone = @MobilePhone, homePhone = @HomePhone, departmentName = @DepartmentName, designation = @Designation, employeeType = @EmployeeType WHERE empNo = @EmpNo";

                SqlCommand command = new SqlCommand(query, con);

                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@DateOfBirth", dtp_DateOfBirth.Text);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@MobilePhone", mobilePhone);
                command.Parameters.AddWithValue("@HomePhone", homePhone);
                command.Parameters.AddWithValue("@DepartmentName", department);
                command.Parameters.AddWithValue("@Designation", designation);
                command.Parameters.AddWithValue("@EmployeeType", employeeType);
                command.Parameters.AddWithValue("@EmpNo", empNo);

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully", "Updated Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException sqlEx)
                {
                    string msg = "Update error";
                    msg += sqlEx.Message;

                    MessageBox.Show("Something went wrong! " + msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}