using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SFMS
{
    public partial class frmHome : Form
    {
        String sname;
        String yname;
        private string staffCode;
        private string studentCode;
        private string feeCode;
       // private string studentCode;
        private string optn;
        private string query;
        public frmHome(string s, string y)
        {
            sname = s;
            yname = y;
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit", "SFMS MESSAGE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void lblMaximaze_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            lblMaximize.Visible = true;
            lblMaximaze.Visible = false;
        }

        private void lblMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            lblMaximaze.Visible = true;
            lblMaximize.Visible = false;
        }
        private void FeeTransactionResult()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                string query = "select * from transaction";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.dgvFeeStructure.Rows.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Transaction_Id"].ToString().Replace(" ", "") != "")
                    {
                        string[] row = new string[] { dataReader["Transaction_Id"].ToString(), dataReader["Transaction_Date"].ToString(), dataReader["Stu_Reg"].ToString(), dataReader["Stu_Name"].ToString(), dataReader["Total_Paid"].ToString(), dataReader["Balance"].ToString() };
                        dgvFeeStructure.Rows.Add(row);
                    }

                }


                connect.CloseConnection();
            }
        }
        private void frmHome_Load(object sender, EventArgs e)
        {
            lblAdmin.Text = sname+" You Login As "+yname;
            lblRole.Text = yname;
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
            timer2.Start();
            timer2.Enabled = true;
            groupBoxDashboard.Visible = true;
            groupBoxFeeTransaction.Visible = false;
            groupBoxStudent.Visible = false;
            gbAccountant.Visible = false;
            groupBoxFeeStatement.Visible = false;
            groupBoxFeeStructure.Visible = false;
            cboStaffNo.Visible = false;
            cboRegNo.Visible = false;
            GetAccountant();
            FeeTransactionResult();
            staffCode = "";
            studentCode = "";
            AssignStaffNo();
            showStaff();
            GetPrograms();
            GetStudent();
            showStudent();
            showStudentFee();
             FeeStructure();
            cboYosFee.Visible = false;
            feeCode = "";
            GetStudentProgram();
           /* if (lblRole.Text == "Accountant")
            {
                btnAcountant.Enabled = false;
                
            }
            else
            {
                btnAcountant.Enabled = true;
            }*/
            

        }
         private void FeeStructure()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                string query = "select * from fee";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.dataGridViewFeeStructure.Rows.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Fee_Id"].ToString().Replace(" ", "") != "")
                    {
                        string[] row = new string[] {dataReader["Yos"].ToString(), dataReader["Semister"].ToString(), dataReader["program"].ToString(), dataReader["Examination"].ToString(), dataReader["Tution"].ToString(), dataReader["Medical"].ToString(), dataReader["Activity"].ToString(), dataReader["amenity"].ToString(), dataReader["Amount"].ToString(), dataReader["Accademic_Year"].ToString()};
                        dataGridViewFeeStructure.Rows.Add(row);
                    }

                }


                connect.CloseConnection();
            }
        }
        public void CleanFeeStructure()
        {
            txtTutionFeeFee.Text = "";
            txtExaminationFee.Text = "";
            txtActivityFee.Text = "";
            txtMedicalFee.Text = "";
            txtAmenityFee.Text = "";
            txtTotalAmountFee.Text = "";
            cboSemesterFee.Text = "";
            cboProgramFee.Text = "";
            txtYos.Text = "";
            cboaccYearFee.Text = "";
        }
        private void AssignStaffNo()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                query = "SELECT * FROM staff ORDER BY Staff_No DESC";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                //this.cboStaffNo.Items.Clear();
                int stffNo;
                if (dataReader.Read())
                {
                    if (dataReader["Staff_No"].ToString().Replace(" ", "") != "")
                    {

                        stffNo = Convert.ToInt32((dataReader["Staff_No"].ToString().Substring(4)));
                        if (stffNo < 9)
                        {
                            txtStaffNo.Text = "EMP/000" + (stffNo + 1);
                        }
                        else if (stffNo >= 9 && stffNo < 99)
                        {
                            txtStaffNo.Text = "EMP/00" + (stffNo + 1);
                        }
                        else if (stffNo >= 99 && stffNo < 999)
                        {
                            txtStaffNo.Text = "EMP/0" + (stffNo + 1);
                        }
                        else
                        {
                            txtStaffNo.Text = "EMP/" + (stffNo + 1);
                        }

                        //MessageBox.Show(txtStaffNo.Text);
                    }
                }
                connect.CloseConnection();



            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int A = rand.Next(0, 255);
            int R = rand.Next(0, 255);
            int G = rand.Next(0, 255);
            int B = rand.Next(0, 255);
            lblAdmin.ForeColor = Color.FromArgb(A, R, G, B);
        }

        private void btnAcountant_Click(object sender, EventArgs e)
        {
            gbAccountant.Visible = true;
            groupBoxStudent.Visible = false;
            groupBoxFeeTransaction.Visible = false;
            groupBoxDashboard.Visible = false;
            groupBoxFeeStatement.Visible = false;
            label2.Text = "Manage Accountant Wizzard";

           /* if (lblRole.Text == "Accountant")
            {
               // gbAccountant.Visible = false;
                MessageBox.Show("You Have No Access To This Module!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(lblRole.Text == "Administrator")
            {
                // groupBoxDashboard.Visible = false;
                gbAccountant.Visible = true;
                label2.Text = "Manage Accountant Wizzard";
                groupBoxStudent.Visible = false;
                groupBoxDashboard.Visible = false;
            }*/
            
            
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            groupBoxDashboard.Visible = true;
            gbAccountant.Visible = false;
            groupBoxFeeTransaction.Visible = false;
            groupBoxStudent.Visible = false;
            groupBoxFeeStatement.Visible = false;
            groupBoxFeeStructure.Visible = false;
            label2.Text = "DashBoard Wizzard";
        }

        private void GetAccountant()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                query = "SELECT * FROM staff ORDER BY Staff_No ASC";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.cboStaffNo.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Staff_No"].ToString().Replace(" ", "") != "")
                    {
                        this.cboStaffNo.Items.Add(dataReader["Staff_No"].ToString());
                    }
                }
                connect.CloseConnection();



            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /* if ((cboStaffNo.Visible == true && Session.userID == cboStaffNo.Text && (Session.prev == "Staff" || Session.prev == "Clerk")) || Session.prev == "Administrator")
             {*/
            long phone;
            string phn = txtPhoneNo.Text.Replace("+", "").Trim().ToString();
            string postalcode = txtPostalCode.Text.Trim().ToString();
            string stName = txtStaffName.Text.Trim().ToString();

            if (txtStaffNo.Text.Replace(" ", "") == "" && cboStaffNo.Visible == false)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStaffNo.Focus();
            }
            else if (cboStaffNo.Text.Replace(" ", "") == "" && cboStaffNo.Visible == true)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboStaffNo.Focus();
            }
            else if (txtPfNo.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPfNo.Focus();
            }
            else if (txtStaffName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStaffName.Focus();
            }
            else if (IsCharacter(txtStaffName.Text.ToString()))
            {
                MessageBox.Show("Invalid Name!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStaffName.Focus();
            }
            else if (txtStaffID.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStaffID.Focus();
            }
            else if (txtPostalAddress.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPostalAddress.Focus();
            }
            else if (txtPostalCode.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPostalCode.Focus();
            }
            else if (!long.TryParse(postalcode, out phone))
            {
                MessageBox.Show("Invalid Postal Code!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPostalCode.Focus();
            }
            else if (txtTown.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTown.Focus();
            }
            else if (txtResidence.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtResidence.Focus();
            }
            else if (txtEmail.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
            }
            else if (!IsValidEmail(txtEmail.Text.Replace(" ", "").ToString()))
            {
                MessageBox.Show("Invalid Email", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
            }
            else if (txtPhoneNo.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhoneNo.Focus();
            }
            else if (txtPhoneNo.Text.Length != 13)
            {
                MessageBox.Show("Invalid Phone Number!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhoneNo.Focus();
            }
            else if (!long.TryParse(phn, out phone))
            {
                MessageBox.Show("Invalid Phone Number!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhoneNo.Focus();
            }
            else if (!txtPhoneNo.Text.Contains("+"))
            {
                MessageBox.Show("Invalid Phone Number!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhoneNo.Focus();
            }
            else if (txtTimeAvailable1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTimeAvailable1.Focus();
            }
            else if (txtLoginName1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLoginName1.Focus();
            }
            else if (txtPassword1.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword1.Focus();
            }
            else if (txtPassword1.Text.Length < 5)
            {
                MessageBox.Show("Weak Password, Put 5 or more characters", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword1.Focus();
            }
            else if (txtConfirmPassword1.Text == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConfirmPassword1.Focus();
            }
            else if (txtPassword1.Text.Replace(" ", "") != txtConfirmPassword1.Text.Replace(" ", ""))
            {
                MessageBox.Show("Password Mismatch!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword1.Text = "";
                txtConfirmPassword1.Text = "";
                txtPassword1.Focus();
            }
            else if (cboPriviledges.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboPriviledges.Focus();
            }
            else
            {
                string msg = "";
                int op;
                int status;
                string gender = "";
                if (chkStatus.CheckState == CheckState.Checked)
                {
                    status = 1;
                }
                else
                {
                    status = 0;
                }
                    /*if (rbtnMale.Checked)
                    {
                        gender = "Male";
                    }
                    else if (rbtnFemale.Checked)
                    {
                        gender = "Female";
                    }*/
                    if (staffCode == "")
                    {
                        if (FindRecord(txtStaffNo.Text.ToString()) == false)
                        {


                            query = "INSERT INTO staff(Staff_No,Pf_No,Staff_Name,Staff_ID,Postal_Address,Postal_Code,Town,Residence,Email_Address,Phone_No,Time_Available,Login_Name,Passsword,Priviledges,Status) VALUES('" + txtStaffNo.Text.ToString() + "', '" + txtPfNo.Text.ToString() + "','" + txtStaffName.Text + "','" + txtStaffID.Text.ToString() + "','" + txtPostalAddress.Text + "','" + txtPostalCode.Text + "','" + txtTown.Text + "','" + txtResidence.Text + "','" + txtEmail.Text + "','" + txtPhoneNo.Text + "','" + txtTimeAvailable1.Text + "','" + txtLoginName1.Text + "','" + txtPassword1.Text + "','" + cboPriviledges.Text + "'," + status + ")";
                            msg = "Records have been successfully saved";
                            op = 0;
                        }
                        else
                        {


                            query = "UPDATE staff SET Staff_No='" + txtStaffNo.Text + "',Pf_No='" + txtPfNo.Text.ToString() + "', Staff_Name= '" + txtStaffName.Text.ToString() + "', Staff_ID='" + txtStaffID.Text + "',Postal_Address='" + txtPostalAddress.Text + "',Postal_Code='" + txtPostalCode.Text + "',Town='" + txtTown.Text + "',Residence='" + txtResidence.Text + "',Email_Address='" + txtEmail.Text + "',Phone_No='" + txtPhoneNo.Text + "',Time_Available='" + txtTimeAvailable1.Text + "',Login_Name='" + txtLoginName1.Text + "',Passsword='" + txtPassword1.Text + "',Priviledges='" + cboPriviledges.Text + "',Status=" + status + " WHERE Staff_No='" + txtStaffNo.Text.ToString() + "'";
                            msg = "Records have been Successfully updated!";
                            op = 1;
                        }
                    }
                    else
                    {
                        query = "UPDATE staff SET Staff_No='" + cboStaffNo.Text + "',Pf_No='" + txtPfNo.Text.ToString() + "', Staff_Name= '" + txtStaffName.Text.ToString() + "', Staff_ID='" + txtStaffID.Text + "',Postal_Address='" + txtPostalAddress.Text + "',Postal_Code='" + txtPostalCode.Text + "',Town='" + txtTown.Text + "',Residence='" + txtResidence.Text + "',Email_Address='" + txtEmail.Text + "',Phone_No='" + txtPhoneNo.Text + "',Time_Available='" + txtTimeAvailable1.Text + "',Login_Name='" + txtLoginName1.Text + "',Passsword='" + txtPassword1.Text + "',Priviledges='" + cboPriviledges.Text + "',Status=" + status + " WHERE Staff_No='" + staffCode + "'";
                        msg = "Records have been Successfully updated!";
                        op = 1;
                    }

                    //open connection
                    connection connect = new connection();
                    connect.CloseConnection();
                    if (connect.OpenConnection() == true)
                    {
                        if (op == 0)
                        {
                            optn = "Are you sure you want to Insert these details?";
                            //optn = MessageBox.Show("Are you sure you want to Insert these details?", "SFMS Message", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        }
                        else
                        {
                            optn = optn = "Are you sure you want to update these details?";
                            //optn = MessageBox.Show("Are you sure you want to update these details?", "SFMS Message", MessageBoxButtons.YesNoMessageBoxIcon.Question);
                        }
                        if (MessageBox.Show(optn, "SFMS Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //create command and assign the query and connection from the constructor
                            MySqlCommand cmd = new MySqlCommand(query, connect.con);

                            //Execute command
                            cmd.ExecuteNonQuery();

                            //close connection


                            clean();
                            MessageBox.Show(msg, "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            clean();
                            MessageBox.Show("Changes not Effected!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        connect.CloseConnection();
                        GetAccountant();
                        //AssignStaffNo();
                        showStaff();

                    }
                }
            }
        
           /* else
            {
                MessageBox.Show("You are not allowed to access this service!\nPlease Contact System Administrator", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
               
        }*/
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsCharacter(string str)
        {
            int x = 0;
            bool res = false;
            for (x = 0; x <= 9; x++)
            {
                if (str.Contains(x.ToString()))
                {
                    res = true;
                }

            }
            return res;

        }

        private void clean()
        {
            txtPfNo.Text = "";
            txtStaffName.Text = "";
            txtStaffID.Text = "";
            txtPostalAddress.Text = "";
            txtPostalCode.Text = "";
            txtTown.Text = "";
            txtResidence.Text = "";
            txtEmail.Text = "";
            txtPhoneNo.Text = "";
            txtTimeAvailable1.Text = "";
            txtLoginName1.Text = "";
            txtPassword1.Text = "";
            txtConfirmPassword1.Text = "";
            cboPriviledges.Text = "Accountant";
            chkStatus.CheckState = 0;
            if (cboStaffNo.Visible)
            {
                cboStaffNo.Text = "";
                cboStaffNo.Focus();
            }
            else
            {
                txtStaffNo.Text = "";
                txtStaffNo.Focus();
            }
        }


        private bool FindStuRecord(string scode)
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {

                query = "SELECT * FROM student WHERE Reg_No='" + scode + "'";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.Read())
                {
                    connect.CloseConnection();
                    return true;
                }
                else
                {
                    connect.CloseConnection();
                    return false;
                }
            }
            else
            {
                return true;
            }



        }

        private bool FindRecord(string scode)
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {

                query = "SELECT * FROM staff WHERE Staff_No='" + scode + "'";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.Read())
                {
                    connect.CloseConnection();
                    return true;
                }
                else
                {
                    connect.CloseConnection();
                    return false;
                }
            }
            else
            {
                return true;
            }



        }


        private void btnAccExit_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            cboStaffNo.Visible = true;
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            clean();
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            if (cboSearchCriteria.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSearchCriteria.Focus();
            }
            else if (txtSearchValue.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearchValue.Focus();
            }
            else
            {
                connection connect = new connection();
                if (connect.OpenConnection() == true)
                {
                    string searchval = "%" + txtSearchValue.Text + "%";
                    if (cboSearchCriteria.Text == "All")
                    {
                        query = "SELECT * FROM staff ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Staff_No")
                    {
                        query = "SELECT * FROM staff WHERE Staff_No LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Staff_Name")
                    {
                        query = "SELECT * FROM staff WHERE Staff_Name LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Postal_Address")
                    {
                        query = "SELECT * FROM staff WHERE Postal_Address LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "ID No")
                    {
                        query = "SELECT * FROM staff WHERE Staff_ID LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Phone No")
                    {
                        query = "SELECT * FROM staff WHERE Phone_No LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Priviledges")
                    {
                        query = "SELECT * FROM staff WHERE Priviledges LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Login_Name")
                    {
                        query = "SELECT * FROM staff WHERE Login_Name LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Town")
                    {
                        query = "SELECT * FROM staff WHERE Town LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Residence")
                    {
                        query = "SELECT * FROM staff WHERE Residence LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else
                    {
                        MessageBox.Show("Invalid Criteria", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboSearchCriteria.Focus();
                    }


                    MySqlCommand cmd = new MySqlCommand(query, connect.con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    this.dataGridView1.Rows.Clear();
                    while (dataReader.Read())
                    {
                        if (dataReader["Staff_No"].ToString().Replace(" ", "") != "")
                        {
                            string[] row = new string[] { dataReader["Staff_No"].ToString(), dataReader["Pf_No"].ToString(), dataReader["Staff_Name"].ToString(), dataReader["Staff_ID"].ToString(), dataReader["Postal_Address"].ToString(), dataReader["Postal_Code"].ToString(), dataReader["Town"].ToString(), dataReader["Residence"].ToString(), dataReader["Email_Address"].ToString(), dataReader["Phone_No"].ToString(), dataReader["Time_Available"].ToString(), dataReader["Login_Name"].ToString(), dataReader["Priviledges"].ToString(), dataReader["Reg_Date"].ToString(), dataReader["Status"].ToString() };
                            dataGridView1.Rows.Add(row);
                        }

                    }


                    connect.CloseConnection();
                }

            }
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            cboSearchCriteria.Text = "All";
            txtSearchValue.Text = "All";
            cboSearchCriteria.Focus();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (cboStaffNo.Visible == false)
            {
                MessageBox.Show("Select Records to Delete", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboStaffNo.Visible = true;
                cboStaffNo.Focus();
            }
            else if (cboStaffNo.Text.Replace(" ", "") == "" && cboStaffNo.Visible == true)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboStaffNo.Focus();
            }
            else if (txtPfNo.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPfNo.Focus();
            }
            else if (txtStaffName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStaffName.Focus();
            }
            else if (txtStaffID.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStaffID.Focus();
            }
            else if (txtPostalAddress.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPostalAddress.Focus();
            }
            else if (txtPostalCode.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPostalCode.Focus();
            }
            else if (txtTown.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTown.Focus();
            }
            else if (txtResidence.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtResidence.Focus();
            }
            else if (txtEmail.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
            }
            else if (txtPhoneNo.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPhoneNo.Focus();
            }
            else if (txtTimeAvailable1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTimeAvailable1.Focus();
            }
            else if (txtLoginName1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLoginName1.Focus();
            }
            else if (txtPassword1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword1.Focus();
            }
            else if (cboPriviledges.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboPriviledges.Focus();
            }
            else
            {
                connection connect = new connection();
                connect.CloseConnection();
                if (connect.OpenConnection() == true)
                {
                    if (MessageBox.Show("Are you sure you want to delete these details?", "SFMS Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        query = "DELETE FROM staff WHERE Staff_No='" + cboStaffNo.Text + "'";
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query, connect.con);

                        //Execute command
                        cmd.ExecuteNonQuery();

                        //close connection

                        clean();

                        MessageBox.Show("Records Successfully Deleted!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        clean();
                        MessageBox.Show("Records Not Deleted!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    connect.CloseConnection();
                    GetAccountant();

                }

            }
        }

        private void showStudent()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.dataGridView2.Rows.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Reg_No"].ToString().Replace(" ", "") != "")
                    {
                        string[] row = new string[] { dataReader["Reg_No"].ToString(), dataReader["Student_Name"].ToString(), dataReader["Program_Code"].ToString(), dataReader["Semester"].ToString(), dataReader["Year_of_Study"].ToString(), dataReader["Postal_Address"].ToString(), dataReader["Postal_Code"].ToString(), dataReader["Town"].ToString(), dataReader["Residence"].ToString(), dataReader["Email_Address"].ToString(), dataReader["Phone_No"].ToString(), dataReader["Status"].ToString()};
                        dataGridView2.Rows.Add(row);
                    }

                }


                connect.CloseConnection();
            }
        }

        private void showStaff()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
               MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.dataGridView1.Rows.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Staff_No"].ToString().Replace(" ", "") != "")
                    {
                        string[] row = new string[] { dataReader["Staff_No"].ToString(), dataReader["Pf_No"].ToString(), dataReader["Staff_Name"].ToString(), dataReader["Staff_ID"].ToString(), dataReader["Postal_Address"].ToString(), dataReader["Postal_Code"].ToString(), dataReader["Town"].ToString(), dataReader["Residence"].ToString(), dataReader["Email_Address"].ToString(), dataReader["Phone_No"].ToString(), dataReader["Time_Available"].ToString(), dataReader["Login_Name"].ToString(), dataReader["Priviledges"].ToString(), dataReader["Reg_Date"].ToString(), dataReader["Status"].ToString() };
                        dataGridView1.Rows.Add(row);
                    }

                }


                connect.CloseConnection();
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.Width = dataGridView1.Width + 100;

            int totalRowHeight = dataGridView1.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            dataGridView1.Height = totalRowHeight;
            this.Height = dataGridView1.Height + 100;
            this.dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = Color.White;

            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);

            this.dataGridView1.Height = 224;
            this.dataGridView1.Width = 1085;
            this.dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.BackgroundColor = Color.White;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboStaffNo.Visible = true;
            cboStaffNo.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            staffCode = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            //gboxSearch.Visible = false;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (cboSearchCriteria.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSearchCriteria.Focus();
            }
            else if (txtSearchValue.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearchValue.Focus();
            }
            else
            {
                connection connect = new connection();
                if (connect.OpenConnection() == true)
                {
                    string searchval = "%" + txtSearchValue.Text + "%";
                    if (cboSearchCriteria.Text == "All")
                    {
                        query = "SELECT * FROM staff ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "PF No")
                    {
                        query = "SELECT * FROM staff WHERE Staff_No LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Name")
                    {
                        query = "SELECT * FROM staff WHERE Staff_Name LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Department")
                    {
                        query = "SELECT * FROM staff WHERE Department_Code LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "ID No")
                    {
                        query = "SELECT * FROM staff WHERE Staff_ID LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else if (cboSearchCriteria.Text == "Phone No")
                    {
                        query = "SELECT * FROM staff WHERE Phone_No LIKE '" + searchval + "' ORDER BY Staff_No ASC";

                    }
                    else
                    {
                        MessageBox.Show("Invalid Criteria", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboSearchCriteria.Focus();
                    }


                    MySqlCommand cmd = new MySqlCommand(query, connect.con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    this.dataGridView1.Rows.Clear();
                    while (dataReader.Read())
                    {
                        if (dataReader["Staff_No"].ToString().Replace(" ", "") != "")
                        {
                            string[] row = new string[] { dataReader["Staff_No"].ToString(), dataReader["Pf_No"].ToString(), dataReader["Staff_Name"].ToString(), dataReader["Staff_ID"].ToString(), dataReader["Postal_Address"].ToString(), dataReader["Postal_Code"].ToString(), dataReader["Town"].ToString(), dataReader["Residence"].ToString(), dataReader["Email_Address"].ToString(), dataReader["Phone_No"].ToString(), dataReader["Time_Available"].ToString(), dataReader["Login_Name"].ToString(), dataReader["Priviledges"].ToString(), dataReader["Reg_Date"].ToString(), dataReader["Status"].ToString() };
                            dataGridView1.Rows.Add(row);
                        }

                    }


                    connect.CloseConnection();
                    PrintDialog printDial = new PrintDialog();
                    printDial.Document = printDocument1;
                    printDial.UseEXDialog = true;
                    if (DialogResult.OK == printDial.ShowDialog())
                    {
                        printDocument1.DocumentName = "Staff Details";
                        printDocument1.Print();
                    }
                }

            }
        }

        private void btnAccNew_Click(object sender, EventArgs e)
        {
            cboStaffNo.Visible = false;
            staffCode = "";
            clean();
            AssignStaffNo();
        }

        private void cboStaffNo_TextChanged(object sender, EventArgs e)
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {

                query = "SELECT * FROM staff WHERE Staff_No='" + cboStaffNo.Text.ToString() + "'";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.Read())
                {
                    cboStaffNo.Text = dataReader["Staff_No"].ToString();
                    txtPfNo.Text = dataReader["Pf_No"].ToString();
                    txtStaffName.Text = dataReader["Staff_Name"].ToString();
                    txtStaffID.Text = dataReader["Staff_ID"].ToString();
                    txtPostalAddress.Text = dataReader["Postal_Address"].ToString();
                    txtPostalCode.Text = dataReader["Postal_Code"].ToString();
                    txtTown.Text = dataReader["Town"].ToString();
                    txtResidence.Text = dataReader["Residence"].ToString();
                    txtEmail.Text = dataReader["Email_Address"].ToString();
                    txtPhoneNo.Text = dataReader["Phone_No"].ToString();
                    txtTimeAvailable1.Text = dataReader["Time_Available"].ToString();
                    txtLoginName1.Text = dataReader["Login_Name"].ToString();
                    txtPassword1.Text = dataReader["Passsword"].ToString();
                    txtConfirmPassword1.Text = dataReader["Passsword"].ToString();
                    cboPriviledges.Text = dataReader["Priviledges"].ToString();
                    if (dataReader["Status"].ToString() == "1")
                    {
                        chkStatus.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        chkStatus.CheckState = CheckState.Unchecked;
                    }
                    staffCode = cboStaffNo.Text;
                }
                connect.CloseConnection();
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            clean();
        }

        private void btnStudentProfile_Click(object sender, EventArgs e)
        {
            groupBoxStudent.Visible = true;
            gbAccountant.Visible = false;
            groupBoxDashboard.Visible = false;
            groupBoxFeeTransaction.Visible = false;
            groupBoxFeeStatement.Visible = false;
            groupBoxFeeStructure.Visible = false;
            label2.Text = "Manage Student Wizzard";
        }

        private void btnStudetSave_Click(object sender, EventArgs e)
        {
            long phone1;
            string phn = txtStuPhoneNo1.Text.Replace("+", "").Trim().ToString();
            string postalcodes = txtStuPostalCode1.Text.Trim().ToString();
            string stName = txtStudentName1.Text.Trim().ToString();
            if (txtRegNo.Text.Replace(" ", "") == "" && cboRegNo.Visible == false)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRegNo.Focus();
            }
            else if (cboRegNo.Text.Replace(" ", "") == "" && cboRegNo.Visible == true)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRegNo.Focus();
            }
            else if (txtStudentName1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStudentName1.Focus();
            }
            else if (IsCharacter(txtStudentName1.Text.ToString()))
            {
                MessageBox.Show("Invalid Name!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStudentName1.Focus();
            }
            else if (cboProgCode1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboProgCode1.Focus();
            }
            else if (cboSemester.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSemester.Focus();
            }
            else if (cboYoS.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboYoS.Focus();
            }
            else if (txtStuPostalAddress1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPostalAddress1.Focus();
            }
            else if (txtStuPostalCode1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPostalCode1.Focus();
            }
            else if (!long.TryParse(postalcodes, out phone1))
            {
                MessageBox.Show("Invalid Postal Code!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPostalCode1.Focus();
            }
            else if (txtStuTown1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuTown1.Focus();
            }
            else if (txtStuResidence.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuResidence.Focus();
            }
            else if (txtStuEmailAddress1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
            }
            else if (!IsValidEmail(txtStuEmailAddress1.Text.Replace(" ", "").ToString()))
            {
                MessageBox.Show("Invalid Email", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuEmailAddress1.Focus();
            }
            else if (txtStuPhoneNo1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPhoneNo1.Focus();
            }
            else if (txtStuPhoneNo1.Text.Length != 13)
            {
                MessageBox.Show("Invalid Phone Number!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPhoneNo1.Focus();
            }
            else if (!long.TryParse(phn, out phone1))
            {
                MessageBox.Show("Invalid Phone Number!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPhoneNo1.Focus();
            }
            else if (!txtStuPhoneNo1.Text.Contains("+"))
            {
                MessageBox.Show("Invalid Phone Number!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPhoneNo1.Focus();
            }
            else
            {

                string msg = "";
                int op;
                int statuss;
                if (chkstatuss.CheckState == CheckState.Checked)
                {
                    statuss = 1;
                }
                else
                {
                    statuss = 0;
                }
                if (studentCode == "")
                {
                    if (FindStuRecord(txtRegNo.Text.ToString()) == false)
                    {


                        query = "INSERT INTO student VALUES('" + txtRegNo.Text.ToString() + "', '" + txtStudentName1.Text.ToString() + "','" + cboProgCode1.Text + "', '" + cboSemester.Text.ToString() + "','" + cboYoS.Text.ToString() + "','" + txtStuPostalAddress1.Text + "','" + txtStuPostalCode1.Text + "','" + txtStuTown1.Text + "','" + txtStuResidence.Text + "','" + txtStuEmailAddress1.Text + "','" + txtStuPhoneNo1.Text + "'," + statuss + ")";
                        msg = "Records have been successfully saved";
                        op = 0;
                    }
                    else
                    {


                        query = "UPDATE student SET Reg_No='" + txtRegNo.Text + "',Student_Name='" + txtStudentName1.Text.ToString() + "', Program_Code= '" + cboProgCode1.Text.ToString() + "', Semester='" + cboSemester.Text.ToString() + "', Year_of_Study='" + cboYoS.Text.ToString() + "', Postal_Address='" + txtPostalAddress.Text + "',Postal_Code='" + txtPostalCode.Text + "',Town='" + txtTown.Text + "',Residence='" + txtResidence.Text + "',Email_Address='" + txtEmail.Text + "',Phone_No='" + txtPhoneNo.Text + "',Status=" + statuss + " WHERE Reg_No='" + txtRegNo.Text.ToString() + "'";
                        msg = "Records have been Successfully updated!";
                        op = 1;
                    }
                }
                else
                {
                    query = "UPDATE student SET Reg_No='" + cboRegNo.Text + "',Student_Name='" + txtStudentName1.Text.ToString() + "', Program_Code= '" + cboProgCode1.Text.ToString() + "', Semester='" + cboSemester.Text.ToString() + "', Year_of_Study='" + cboYoS.Text.ToString() + "', Postal_Address='" + txtPostalAddress.Text + "',Postal_Code='" + txtPostalCode.Text + "',Town='" + txtTown.Text + "',Residence='" + txtResidence.Text + "',Email_Address='" + txtEmail.Text + "',Phone_No='" + txtPhoneNo.Text + "',Status=" + statuss + " WHERE Reg_No='" + studentCode + "'";
                    msg = "Records have been Successfully updated!";
                    op = 1;
                }

                //open connection
                connection connect = new connection();
                connect.CloseConnection();
                if (connect.OpenConnection() == true)
                {
                    if (op == 0)
                    {
                        optn = "Are you sure you want to Insert these details?";
                        //optn = MessageBox.Show("Are you sure you want to Insert these details?", "SFMS Message", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    }
                    else
                    {
                        optn = optn = "Are you sure you want to update these details?";
                        //optn = MessageBox.Show("Are you sure you want to update these details?", "SFMS Message", MessageBoxButtons.YesNoMessageBoxIcon.Question);
                    }
                    if (MessageBox.Show(optn, "SFMS Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmdd = new MySqlCommand(query, connect.con);

                        //Execute command
                        cmdd.ExecuteNonQuery();

                        //close connection


                        clean();
                        MessageBox.Show(msg, "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        clean();
                        MessageBox.Show("Changes not Effected!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    connect.CloseConnection();
                    GetStudent();



                }

            }
        }

        

        private void GetPrograms()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                query = "SELECT * FROM program ORDER BY Program_Code ASC";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.cboProgCode1.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Program_Code"].ToString().Replace(" ", "") != "")
                    {
                        this.cboProgCode1.Items.Add(dataReader["Program_Code"].ToString());
                    }
                }
                connect.CloseConnection();



            }
        }


       private void GetStudent()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                query = "SELECT * FROM student ORDER BY Reg_No ASC";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.cboRegNo.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Reg_No"].ToString().Replace(" ", "") != "")
                    {
                        this.cboRegNo.Items.Add(dataReader["Reg_No"].ToString());
                    }
                }
                connect.CloseConnection();



            }
        }

       private void btnStuNew_Click(object sender, EventArgs e)
       {
           cboRegNo.Visible = false;
           studentCode = "";
           clean();
       }

       private void btnStuEdit_Click(object sender, EventArgs e)
       {
           cboRegNo.Visible = true;
           studentCode = "";
       }
        
       
        private void btnStuDelete_Click(object sender, EventArgs e)
        {
             if (cboRegNo.Visible == false)
            {
                MessageBox.Show("Select Records to Delete", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboRegNo.Visible = true;
                cboRegNo.Focus();
            }
            else if (cboRegNo.Text.Replace(" ", "") == "" && cboRegNo.Visible == true)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRegNo.Focus();
            }
            else if (txtStudentName1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStudentName1.Focus();
            }
            else if (cboProgCode1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboProgCode1.Focus();
            }
            else if (cboSemester.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSemester.Focus();
            }
            else if (cboYoS.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboYoS.Focus();
            }
            else if (txtStuPostalAddress1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPostalAddress1.Focus();
            }
            else if (txtStuPostalCode1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPostalCode1.Focus();
            }
            else if (txtStuTown1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuTown1.Focus();
            }
            else if (txtStuResidence.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuResidence.Focus();
            }
            else if (txtStuEmailAddress1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuEmailAddress1.Focus();
            }
            else if (txtStuPhoneNo1.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPhoneNo1.Focus();
            }
            else if (txtStuPhoneNo1.Text.Length.ToString() != "13" || (txtPhoneNo.Text.Replace("+", "")).ToString() == "")
            {
                MessageBox.Show("Invalid Phone Number!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStuPhoneNo1.Focus();
            }
            else
            {
                connection connect = new connection();
                connect.CloseConnection();
                if (connect.OpenConnection() == true)
                {
                    if (MessageBox.Show("Are you sure you want to delete these details?", "SFMS Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        query = "DELETE FROM student WHERE Reg_No='" + cboRegNo.Text + "'";
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query, connect.con);

                        //Execute command
                        cmd.ExecuteNonQuery();

                        //close connection

                        clean();

                        MessageBox.Show("Records Successfully Deleted!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        clean();
                        MessageBox.Show("Records Not Deleted!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    connect.CloseConnection();
                    GetStudent();

                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboRegNo.Visible = true;
            cboRegNo.Text = this.dataGridView2.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            studentCode = this.dataGridView2.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
        }

       private void cboRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboRegNo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {

                query = "SELECT * FROM student WHERE Reg_No='" + cboRegNo.Text.ToString() + "'";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.Read())
                {
                    txtStudentName1.Text = dataReader["Student_Name"].ToString();
                    cboProgCode1.Text = dataReader["Program_Code"].ToString();
                    cboSemester.Text = dataReader["Semester"].ToString();
                    cboYoS.Text = dataReader["Year_of_Study"].ToString();
                    txtStuPostalAddress1.Text = dataReader["Postal_Address"].ToString();
                    txtStuPostalCode1.Text = dataReader["Postal_Code"].ToString();
                    txtStuTown1.Text = dataReader["Town"].ToString();
                    txtResidence.Text = dataReader["Residence"].ToString();
                    txtStuEmailAddress1.Text = dataReader["Email_Address"].ToString();
                    txtStuPhoneNo1.Text = dataReader["Phone_No"].ToString();
                    if (dataReader["Status"].ToString() == "1")
                    {
                        chkStatus.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        chkStatus.CheckState = CheckState.Unchecked;
                    }
                    studentCode = cboRegNo.Text;
                }
                connect.CloseConnection();
            }
        }

        private void cboStuSeachCritea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStuSeachCritea.Text == "All")
            {
                txtStuSearchValue.Text = "All";
            }
            else
            {
                txtStuSearchValue.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void btnAllTransaction_Click(object sender, EventArgs e)
        {
            groupBoxFeeTransaction.Visible = true;
            groupBoxStudent.Visible = false;
            gbAccountant.Visible = false;
            groupBoxDashboard.Visible = false;
            groupBoxFeeStatement.Visible = false;
            groupBoxFeeStructure.Visible = false;
            label2.Text = "Manage Fee Transaction Wizzard";
        }
        private void ShowBalance()
        {
            int total = Convert.ToInt32(txtTotalAmout.Text);
            double totalpaid = Convert.ToDouble(txtAmountPaid.Text);
            // int totalpaid = Convert.ToString(txtAmountPaid.Text);

            double balance = 0;
            balance = total - totalpaid;
            txtBalance.Text = ("" + balance);

            //int subVal = Integer.parseInt(txtpaid.getText());
            //float t = Float.parseFloat(txttotalmoney.getText());
            // float perVal = 0;
            //perVal = subVal - t;
            //txtBalence.setText("" + perVal);
        }
        private void ShowTotalFee()
        {
            // string query = "select * from fee where Yos = '"++"' and Semister ='"++"' and program ='"++"'";
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                query = "select * from fee where Yos = '" + txtYos_Stu.Text + "' and Semister ='" + txtSemester_Stu.Text + "' and program ='" + txtProgram_Stu.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    txtTotalAmout.Text = dataReader["Amount"].ToString();

                }
                connect.CloseConnection();



            }
        }
        private void showStudentFee()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                query = "SELECT * FROM student";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.dataGridView3.Rows.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Reg_No"].ToString().Replace(" ", "") != "")
                    {
                        string[] row = new string[] { dataReader["Reg_No"].ToString(), dataReader["Student_Name"].ToString(), dataReader["Program_Code"].ToString(), dataReader["Semester"].ToString(), dataReader["Year_of_Study"].ToString(), dataReader["Email_Address"].ToString(), dataReader["Phone_No"].ToString() };
                        dataGridView3.Rows.Add(row);
                    }

                }


                connect.CloseConnection();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (cboSearchStu.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboSearchStu.Focus();
            }
            else if (txtSearchStuValue.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearchStuValue.Focus();
            }
            else
            {
                connection connect = new connection();
                if (connect.OpenConnection() == true)
                {
                    string searchval = "%" + txtSearchStuValue.Text + "%";
                    if (cboSearchStu.Text == "All")
                    {
                        query = "SELECT * FROM student ORDER BY Reg_No ASC";

                    }
                    else if (cboSearchStu.Text == "Registration No")
                    {
                        query = "SELECT * FROM student WHERE Reg_No LIKE '" + searchval + "' ORDER BY Reg_No ASC";

                    }
                    else if (cboSearchStu.Text == "Student Name")
                    {
                        query = "SELECT * FROM student WHERE Student_Name LIKE '" + searchval + "' ORDER BY Reg_No ASC";

                    }
                    else if (cboSearchStu.Text == "Program")
                    {
                        query = "SELECT * FROM student WHERE Program_Code LIKE '" + searchval + "' ORDER BY Reg_No ASC";

                    }
                    else if (cboSearchStu.Text == "Semester")
                    {
                        query = "SELECT * FROM student WHERE Semester LIKE '" + searchval + "' ORDER BY Reg_No ASC";

                    }
                    else if (cboSearchStu.Text == "Year of Study")
                    {
                        query = "SELECT * FROM student WHERE Year_of_Study LIKE '" + searchval + "' ORDER BY Reg_No ASC";

                    }
                    else
                    {
                        MessageBox.Show("Invalid Criteria", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboSearchStu.Focus();
                    }


                    MySqlCommand cmd = new MySqlCommand(query, connect.con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    this.dataGridView3.Rows.Clear();
                    while (dataReader.Read())
                    {
                        if (dataReader["Reg_No"].ToString().Replace(" ", "") != "")
                        {
                            string[] row = new string[] { dataReader["Reg_No"].ToString(), dataReader["Student_Name"].ToString(), dataReader["Program_Code"].ToString(), dataReader["Semester"].ToString(), dataReader["Year_of_Study"].ToString(), dataReader["Email_Address"].ToString(), dataReader["Phone_No"].ToString() };
                            dataGridView3.Rows.Add(row);
                        }

                    }


                    connect.CloseConnection();

                }

            }
        }

        private void btnshowFee_Click(object sender, EventArgs e)
        {
            ShowTotalFee();
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            ShowBalance();
        }

        private void btnPayFee_Click(object sender, EventArgs e)
        {
            long amountpaid;
            string ap = txtAmountPaid.Text.Trim().ToString();
            if (txtAmountPaid.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAmountPaid.Focus();
            }

            else if (!long.TryParse(ap, out amountpaid))
            {
                MessageBox.Show("Invalid Entry only number are allowed!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAmountPaid.Focus();
            }

            else
            {
                //open connection
                connection connect = new connection();
                connect.CloseConnection();
                if (connect.OpenConnection() == true)
                {
                    int op = 0;
                    query = "INSERT INTO transaction (Stu_Reg,Stu_Name,Total_Paid,Balance)VALUES ('" + cboStu_Reg.Text.ToString() + "','" + txtStu_Name.Text.ToString() + "','" + txtAmountPaid.Text.ToString() + "','" + txtBalance.Text.ToString() + "')";
                    MySqlCommand cmdd = new MySqlCommand(query, connect.con);

                    //Execute command
                    cmdd.ExecuteNonQuery();


                    MessageBox.Show("Records have been successfully saved", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                }



            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboStu_Reg.Visible = true;
            txtYos_Stu.Text = this.dataGridView3.Rows[this.dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString();
            txtSemester_Stu.Text = this.dataGridView3.Rows[this.dataGridView3.CurrentCell.RowIndex].Cells[3].Value.ToString();
            txtProgram_Stu.Text = this.dataGridView3.Rows[this.dataGridView3.CurrentCell.RowIndex].Cells[2].Value.ToString();
            txtStu_Name.Text = this.dataGridView3.Rows[this.dataGridView3.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cboStu_Reg.Text = this.dataGridView3.Rows[this.dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString();

            studentCode = this.dataGridView3.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCustomerReceipts_Click(object sender, EventArgs e)
        {
            groupBoxFeeTransaction.Visible = false;
            groupBoxStudent.Visible = false;
            gbAccountant.Visible = false;
            groupBoxDashboard.Visible = false;
            groupBoxFeeStatement.Visible = true;
            groupBoxFeeStructure.Visible = false;
            label2.Text = "Manage Fee Statement Wizzard";
        }

        private void btnFeeSearchStatement_Click(object sender, EventArgs e)
        {
            if (cboFeeSTransaction.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboFeeSTransaction.Focus();
            }
            else if (txtSearchFeeeTransaction.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearchFeeeTransaction.Focus();
            }
            else
            {
                connection connect = new connection();
                if (connect.OpenConnection() == true)
                {
                    string searchval = "%" + txtSearchFeeeTransaction.Text + "%";
                    if (cboFeeSTransaction.Text == "All")
                    {
                        query = "SELECT * FROM transaction ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Registration No")
                    {
                        query = "SELECT * FROM transaction WHERE Stu_Reg LIKE '" + searchval + "' ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Student Name")
                    {
                        query = "SELECT * FROM transaction WHERE Stu_Name LIKE '" + searchval + "' ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Total Paid")
                    {
                        query = "SELECT * FROM transaction WHERE Total_PaidLIKE '" + searchval + "' ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Balance")
                    {
                        query = "SELECT * FROM transaction WHERE Balance='" + searchval + "' ORDER BY Transaction_Id ASC";

                    }
                    else
                    {
                        MessageBox.Show("Invalid Criteria", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboFeeSTransaction.Focus();
                    }


                    MySqlCommand cmd = new MySqlCommand(query, connect.con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    this.dgvFeeStructure.Rows.Clear();
                    while (dataReader.Read())
                    {
                        if (dataReader["Transaction_Id"].ToString().Replace(" ", "") != "")
                        {
                            string[] row = new string[] { dataReader["Transaction_Id"].ToString(), dataReader["Transaction_Date"].ToString(), dataReader["Stu_Reg"].ToString(), dataReader["Stu_Name"].ToString(), dataReader["Total_Paid"].ToString(), dataReader["Balance"].ToString() };
                            dgvFeeStructure.Rows.Add(row);
                        }

                    }


                    connect.CloseConnection();

                }

            }
        }

        private void btnFeePrint_Click(object sender, EventArgs e)
        {
            if (cboFeeSTransaction.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboFeeSTransaction.Focus();
            }
            else if (txtSearchFeeeTransaction.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearchFeeeTransaction.Focus();
            }
            else
            {
                connection connect = new connection();
                if (connect.OpenConnection() == true)
                {
                    string searchval = "%" + txtSearchFeeeTransaction.Text + "%";
                    string search = txtSearchFeeeTransaction.Text;
                    if (cboFeeSTransaction.Text == "All")
                    {
                        query = "SELECT * FROM transaction ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Registration No")
                    {
                        query = "SELECT * FROM transaction WHERE Stu_Reg LIKE '" + searchval + "' ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Student Name")
                    {
                        query = "SELECT * FROM transaction WHERE Stu_Name LIKE '" + searchval + "' ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Total Paid")
                    {
                        query = "SELECT * FROM transaction WHERE Total_Paid LIKE '" + searchval + "' ORDER BY Transaction_Id ASC";

                    }
                    else if (cboFeeSTransaction.Text == "Balance")
                    {
                        query = "SELECT * from transaction where Balance='" + txtSearchFeeeTransaction.Text + "'";

                    }
                    else
                    {
                        MessageBox.Show("Invalid Criteria", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboFeeSTransaction.Focus();
                    }


                    MySqlCommand cmd = new MySqlCommand(query, connect.con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    this.dgvFeeStructure.Rows.Clear();
                    while (dataReader.Read())
                    {
                        if (dataReader["Transaction_Id"].ToString().Replace(" ", "") != "")
                        {
                            string[] row = new string[] { dataReader["Transaction_Id"].ToString(), dataReader["Transaction_Date"].ToString(), dataReader["Stu_Reg"].ToString(), dataReader["Stu_Name"].ToString(), dataReader["Total_Paid"].ToString(), dataReader["Balance"].ToString() };
                            dgvFeeStructure.Rows.Add(row);
                        }

                    }

                    connect.CloseConnection();
                    PrintDialog printDial = new PrintDialog();
                    printDial.Document = printFeeStatementDocument2;
                    printDial.UseEXDialog = true;
                    if (DialogResult.OK == printDial.ShowDialog())
                    {
                        printFeeStatementDocument2.DocumentName = "Fee Statement Details";
                        printFeeStatementDocument2.Print();
                    }
                }

            }
        }

        private void printFeeStatementDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            foreach (DataGridViewColumn column in dgvFeeStructure.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.Width = dgvFeeStructure.Width + 100;

            int totalRowHeight = dgvFeeStructure.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dgvFeeStructure.Rows)
                totalRowHeight += row.Height;

            dgvFeeStructure.Height = totalRowHeight;
            this.Height = dgvFeeStructure.Height + 100;
            this.dgvFeeStructure.ScrollBars = ScrollBars.None;
            dgvFeeStructure.BorderStyle = BorderStyle.None;
            dgvFeeStructure.BackgroundColor = Color.White;

            Bitmap bm = new Bitmap(this.dgvFeeStructure.Width, this.dgvFeeStructure.Height);
            dgvFeeStructure.DrawToBitmap(bm, new Rectangle(0, 0, this.dgvFeeStructure.Width, this.dgvFeeStructure.Height));
            e.Graphics.DrawImage(bm, 0, 0);

            this.dgvFeeStructure.Height = 437;
            this.dgvFeeStructure.Width = 894; 
            this.dgvFeeStructure.ScrollBars = ScrollBars.Both;
            dgvFeeStructure.BorderStyle = BorderStyle.FixedSingle;
            dgvFeeStructure.BackgroundColor = Color.White;
        }

        private void dgvFeeStructure_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ntnStatement_Click(object sender, EventArgs e)
        {
            cboFeeSTransaction.Text = "All";
            txtSearchFeeeTransaction.Text = "All";

        }

        private void btnFeeSave_Click(object sender, EventArgs e)
        {
            long fees;
            string tution = txtTutionFeeFee.Text.Trim().ToString();
            string examination = txtExaminationFee.Text.Trim().ToString();
            string activity = txtActivityFee.Text.Trim().ToString();
            string medical = txtMedicalFee.Text.Trim().ToString();
            string amenity = txtAmenityFee.Text.Trim().ToString();
            string total = txtTotalAmountFee.Text.Trim().ToString();

            if (txtYos.Text.Replace(" ", "") == "" && cboYosFee.Visible == false)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYos.Focus();
            }
            else if (cboYosFee.Text.Replace(" ", "") == "" && cboYosFee.Visible == true)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYos.Focus();
            }
            if (txtAccYearFee.Text.Replace(" ", "") == "" && cboaccYearFee.Visible == false)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAccYearFee.Focus();
            }
            else if (cboaccYearFee.Text.Replace(" ", "") == "" && cboaccYearFee.Visible == true)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAccYearFee.Focus();
            }
            if (txtSemesterFee.Text.Replace(" ", "") == "" && cboSemesterFee.Visible == false)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSemesterFee.Focus();
            }
            else if (cboSemesterFee.Text.Replace(" ", "") == "" && cboSemesterFee.Visible == true)
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSemesterFee.Focus();
            }
            else if (txtTutionFeeFee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTutionFeeFee.Focus();
            }
            else if (!long.TryParse(tution, out fees))
            {
                MessageBox.Show("Only Numbers are Allowed", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTutionFeeFee.Focus();
            }

            else if (txtExaminationFee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtExaminationFee.Focus();
            }
            else if (!long.TryParse(examination, out fees))
            {
                MessageBox.Show("Only Numbers are Allowed", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtExaminationFee.Focus();
            }

            else if (txtActivityFee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtActivityFee.Focus();
            }
            else if (!long.TryParse(activity, out fees))
            {
                MessageBox.Show("Only Numbers are Allowed", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtActivityFee.Focus();
            }

            else if (txtMedicalFee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMedicalFee.Focus();
            }
            else if (!long.TryParse(medical, out fees))
            {
                MessageBox.Show("Only Numbers are Allowed", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMedicalFee.Focus();
            }

            else if (txtAmenityFee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAmenityFee.Focus();
            }
            else if (!long.TryParse(amenity, out fees))
            {
                MessageBox.Show("Only Numbers are Allowed", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAmenityFee.Focus();
            }

            else if (txtTotalAmountFee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTotalAmountFee.Focus();
            }
            else if (!long.TryParse(total, out fees))
            {
                MessageBox.Show("Only Numbers are Allowed", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTotalAmountFee.Focus();
            }

            else
            {

                string msg = "";
                int op;

                if (feeCode == "")
                {
                    if (FindFeeRecord(txtYos.Text.ToString()) == false)
                    {


                        query = "INSERT INTO fee(Yos,Semister,program,Examination,Tution,Medical,Activity,Amenity,Amount,Accademic_Year) VALUES('" + txtYos.Text.ToString() + "', '" + cboSemesterFee.Text.ToString() + "','" + cboProgramFee.Text + "','" + txtExaminationFee.Text + "','" + txtTutionFeeFee.Text.ToString() + "','" + txtMedicalFee.Text + "','" + txtActivityFee.Text + "','" + txtAmenityFee.Text + "','" + txtTotalAmountFee.Text + "','" + cboaccYearFee.Text + "')";
                        msg = "Records have been successfully saved";
                        op = 0;
                    }
                    else
                    {


                        query = "UPDATE fee SET Yos='" + cboYosFee.Text + "',Semister='" + cboSemesterFee.Text.ToString() + "', program= '" + cboProgramFee.Text.ToString() + "',Examination='" + txtExaminationFee.Text.ToString() + "',Tution='" + txtTutionFeeFee.Text.ToString() + "', Medical='" + txtMedicalFee.Text + "',Activity='" + txtActivityFee.Text + "',	amenity='" + txtAmenityFee.Text + "',Amount='" + txtTotalAmountFee.Text + "',Accademic_Year='" + txtAccYearFee.Text + "' WHERE Fee_Id='" + txtFee_Id.Text.ToString() + "'";
                        msg = "Records have been Successfully updated!";
                        op = 1;
                    }
                }
                else
                {
                    query = "UPDATE fee SET Yos='" + cboYosFee.Text + "',Semister='" + cboSemesterFee.Text.ToString() + "', program= '" + cboProgramFee.Text.ToString() + "', 	Examination='" + txtExaminationFee.Text.ToString() + "',Tution='" + txtTutionFeeFee.Text.ToString() + "', Medical='" + txtMedicalFee.Text + "',Activity='" + txtActivityFee.Text + "',	amenity='" + txtAmenityFee.Text + "',Amount='" + txtTotalAmountFee.Text + "',Accademic_Year='" + txtAccYearFee.Text + "' WHERE Fee_Id='" + feeCode + "'";
                    msg = "Records have been Successfully updated!";
                    op = 1;
                }

                //open connection
                connection connect = new connection();
                connect.CloseConnection();
                if (connect.OpenConnection() == true)
                {
                    if (op == 0)
                    {
                        optn = "Are you sure you want to Insert these details?";
                        //optn = MessageBox.Show("Are you sure you want to Insert these details?", "SFMS Message", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    }
                    else
                    {
                        optn = optn = "Are you sure you want to update these details?";
                        //optn = MessageBox.Show("Are you sure you want to update these details?", "SFMS Message", MessageBoxButtons.YesNoMessageBoxIcon.Question);
                    }
                    if (MessageBox.Show(optn, "SFMS Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmdd = new MySqlCommand(query, connect.con);

                        //Execute command
                        cmdd.ExecuteNonQuery();

                        //close connection


                        clean();
                        MessageBox.Show(msg, "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        clean();
                        MessageBox.Show("Changes not Effected!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    connect.CloseConnection();
                    FeeStructure();
                }

            }
        }

        private void dataGridViewFeeStructure_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboYosFee.Visible = true;
            cboaccYearFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[9].Value.ToString();
            txtYos.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[8].Value.ToString();
            cboProgramFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[7].Value.ToString();
            cboSemesterFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[6].Value.ToString();
            txtTotalAmountFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[5].Value.ToString();
            txtTutionFeeFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[4].Value.ToString();
            txtExaminationFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[3].Value.ToString();
            txtActivityFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[2].Value.ToString();
            txtMedicalFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[1].Value.ToString();
            txtAmenityFee.Text = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[0].Value.ToString();

            studentCode = this.dataGridViewFeeStructure.Rows[this.dataGridViewFeeStructure.CurrentCell.RowIndex].Cells[0].Value.ToString();
        }

        private void btnFeeSearch_Click(object sender, EventArgs e)
        {
            if (cboFeeStructuresSTU.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboFeeStructuresSTU.Focus();
            }
            else if (txtSearchFeee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearchFeee.Focus();
            }
            else
            {
                connection connect = new connection();
                if (connect.OpenConnection() == true)
                {
                    string searchval = "%" + txtSearchFeee.Text + "%";
                    if (cboFeeStructuresSTU.Text == "All")
                    {
                        query = "SELECT * FROM fee ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Year of study")
                    {
                        query = "SELECT * FROM fee WHERE Yos LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Semister")
                    {
                        query = "SELECT * FROM fee WHERE Semister LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Program")
                    {
                        query = "SELECT * FROM fee WHERE program LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Accademic Year")
                    {
                        query = "SELECT * FROM fee WHERE Accademic_Year LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else
                    {
                        MessageBox.Show("Invalid Criteria", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboFeeStructuresSTU.Focus();
                    }


                    MySqlCommand cmd = new MySqlCommand(query, connect.con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    this.dataGridViewFeeStructure.Rows.Clear();
                    while (dataReader.Read())
                    {
                        if (dataReader["Fee_Id"].ToString().Replace(" ", "") != "")
                        {
                            string[] row = new string[] { dataReader["Yos"].ToString(), dataReader["Semister"].ToString(), dataReader["program"].ToString(), dataReader["Examination"].ToString(), dataReader["Tution"].ToString(), dataReader["Medical"].ToString(), dataReader["Activity"].ToString(), dataReader["amenity"].ToString(), dataReader["Amount"].ToString(), dataReader["Accademic_Year"].ToString() };
                            dataGridViewFeeStructure.Rows.Add(row);
                        }

                    }


                    connect.CloseConnection();

                }

            }
        }

        private void btnFeeClear_Click(object sender, EventArgs e)
        {
            cboFeeStructuresSTU.Text = "";
            txtSearchFeee.Text = "";
        }

        private void btnFeeNew_Click(object sender, EventArgs e)
        {
            cboYosFee.Visible = false;
            feeCode = "";
            clean();
            CleanFeeStructure();
        }

        private void btnFeeEdit_Click(object sender, EventArgs e)
        {
            cboYosFee.Visible = true;
            feeCode = "";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (cboFeeStructuresSTU.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboFeeStructuresSTU.Focus();
            }
            else if (txtSearchFeee.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Ensure all fields are filled!", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSearchFeee.Focus();
            }
            else
            {
                connection connect = new connection();
                if (connect.OpenConnection() == true)
                {
                    string searchval = "%" + txtSearchFeee.Text + "%";
                    if (cboFeeStructuresSTU.Text == "All")
                    {
                        query = "SELECT * FROM fee ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Year of study")
                    {
                        query = "SELECT * FROM fee WHERE Yos LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Semister")
                    {
                        query = "SELECT * FROM fee WHERE Semister LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Program")
                    {
                        query = "SELECT * FROM fee WHERE program LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else if (cboFeeStructuresSTU.Text == "Accademic Year")
                    {
                        query = "SELECT * FROM fee WHERE Accademic_Year LIKE '" + searchval + "' ORDER BY Fee_Id ASC";

                    }
                    else
                    {
                        MessageBox.Show("Invalid Criteria", "SFMS Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboFeeStructuresSTU.Focus();
                    }


                    MySqlCommand cmd = new MySqlCommand(query, connect.con);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    this.dgvFeeStructure.Rows.Clear();
                    while (dataReader.Read())
                    {
                        if (dataReader["Fee_Id"].ToString().Replace(" ", "") != "")
                        {
                            string[] row = new string[] { dataReader["Yos"].ToString(), dataReader["Semister"].ToString(), dataReader["program"].ToString(), dataReader["Examination"].ToString(), dataReader["Tution"].ToString(), dataReader["Medical"].ToString(), dataReader["Activity"].ToString(), dataReader["amenity"].ToString(), dataReader["Amount"].ToString(), dataReader["Accademic_Year"].ToString() };
                            dgvFeeStructure.Rows.Add(row);
                        }

                    }


                    connect.CloseConnection();
                    PrintDialog printDial = new PrintDialog();
                    printDial.Document = printFeeStructureDocument2;
                    printDial.UseEXDialog = true;
                    if (DialogResult.OK == printDial.ShowDialog())
                    {
                        printFeeStructureDocument2.DocumentName = "Fee Structure Details";
                        printFeeStructureDocument2.Print();
                    }
                }

            }
        }

        private void printFeeStructureDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            foreach (DataGridViewColumn column in dgvFeeStructure.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.Width = dgvFeeStructure.Width + 100;

            int totalRowHeight = dataGridViewFeeStructure.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dataGridViewFeeStructure.Rows)
                totalRowHeight += row.Height;

            dataGridViewFeeStructure.Height = totalRowHeight;
            this.Height = dataGridViewFeeStructure.Height + 100;
            this.dataGridViewFeeStructure.ScrollBars = ScrollBars.None;
            dataGridViewFeeStructure.BorderStyle = BorderStyle.None;
            dataGridViewFeeStructure.BackgroundColor = Color.White;

            Bitmap bm = new Bitmap(this.dataGridViewFeeStructure.Width, this.dataGridViewFeeStructure.Height);
            dataGridViewFeeStructure.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridViewFeeStructure.Width, this.dataGridViewFeeStructure.Height));
            e.Graphics.DrawImage(bm, 0, 0);

            this.dataGridViewFeeStructure.Height = 362;
            this.dataGridViewFeeStructure.Width = 1091; 
            this.dataGridViewFeeStructure.ScrollBars = ScrollBars.Both;
            dataGridViewFeeStructure.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewFeeStructure.BackgroundColor = Color.White;
        }

        private bool FindFeeRecord(string scode)
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {

                query = "SELECT * FROM fee WHERE Fee_Id='" + scode + "'";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.Read())
                {
                    connect.CloseConnection();
                    return true;
                }
                else
                {
                    connect.CloseConnection();
                    return false;
                }
            }
            else
            {
                return true;
            }



        }

        private void GetStudentProgram()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                query = "SELECT * FROM program ORDER BY Program_Code ASC";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.cboProgramFee.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Program_Code"].ToString().Replace(" ", "") != "")
                    {
                        this.cboProgramFee.Items.Add(dataReader["Program_Code"].ToString());
                    }
                }
                connect.CloseConnection();



            }
        }

        private void cboYosFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {

                query = "SELECT * FROM fee WHERE Yos='" + cboYosFee.Text.ToString() + "'";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                if (dataReader.Read())
                {
                    txtFee_Id.Text = dataReader["Fee_Id"].ToString();
                    cboProgramFee.Text = dataReader["program"].ToString();
                    cboSemesterFee.Text = dataReader["Semister"].ToString();
                    txtExaminationFee.Text = dataReader["Examination"].ToString();
                    txtTutionFeeFee.Text = dataReader["Tution"].ToString();
                    txtMedicalFee.Text = dataReader["Medical"].ToString();
                    txtActivityFee.Text = dataReader["Activity"].ToString();
                    txtAmenityFee.Text = dataReader["amenity"].ToString();
                    txtTotalAmountFee.Text = dataReader["Amount"].ToString();
                    cboaccYearFee.Text = dataReader["Accademic_Year"].ToString();
                    feeCode = cboYosFee.Text;
                }
                connect.CloseConnection();
            }
        }

        private void btnInventoryPrice_Click(object sender, EventArgs e)
        {
            groupBoxStudent.Visible = false;
            gbAccountant.Visible = false;
            groupBoxDashboard.Visible = false;
            groupBoxFeeTransaction.Visible = false;
            groupBoxFeeStatement.Visible = false;
            groupBoxFeeStructure.Visible = true;
            label2.Text = "Manage Fees Structure Wizzard";
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            //groupBoxAdmin.Visible = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
       

    }
}
