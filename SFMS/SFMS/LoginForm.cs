using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using LoginForm;

namespace SFMS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {

            InitializeComponent();
            txtPasssword.PasswordChar = '*';
            txtPasssword.MaxLength = 30;
        }

        private void btnBackLog_Click(object sender, EventArgs e)
        {
            groupBoxAdmin.Visible = false;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPassword RP = new ResetPassword();
            RP.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int A = rand.Next(0, 255);
            int R = rand.Next(0, 255);
            int G = rand.Next(0, 255);
            int B = rand.Next(0, 255);
            lblwelcome.ForeColor = Color.FromArgb(A, R, G, B);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            txtUser.Focus();
            groupBoxAdmin.Visible = false;
            llpanel.Visible = false;
            rlpanel.Visible = false;
            y.Visible = true;
            x.Visible = true;
            GetStaff();
        }
        private void GetForm1()
        {
            connection conn = new connection();
            if (conn.OpenConnection() == true)
            {
                string query = "select * FROM staff";
                MySqlCommand cmd = new MySqlCommand(query, conn.con);
                MySqlDataReader DataReader = cmd.ExecuteReader();
                conn.CloseConnection();
            }
        }

        private void btnLgoin_Click(object sender, EventArgs e)
        {
            connection conn = new connection();
            if (conn.OpenConnection())
            {

                string querry = "SELECT * FROM staff WHERE Login_Name='" + this.txtUser.Text + "' and Passsword ='" + this.txtPasssword.Text + "'and Priviledges ='" + this.cboRole.Text + "'  ";
                MySqlCommand cmd = new MySqlCommand(querry, conn.con);
                MySqlDataReader datareader = cmd.ExecuteReader();
                int count = 0;
                while (datareader.Read())
                {
                    count = count + 1;

                }
                if (count == 1)
                {

                    MessageBox.Show("Login successfull\n" + "You Are Welcome " + txtUser.Text + " Just click OK to have access of the system.", "CMS MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // MDIParent1 mdi = new MDIParent1("WELCOME" + txtUser_Name.Text);
                    //mdi.Visible = true;
                    //this.Hide();
                    string x = txtUser.Text;
                    string y = cboRole.Text;
                    frmHome H = new frmHome(x,y);
                    H.Visible = true;
                    this.Hide();
                    // SchoolManagementSystem cat = new SchoolManagementSystem();
                    // cat.Visible = true;
                    // this.Hide();
                }


                else
                {
                    MessageBox.Show("Login  unsuccessfull", "FOMS MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtUser_Click(object sender, EventArgs e)
        {
            leftloginmiddlepanel.Visible = true;
            leftbottomloginpanel.Visible = false;
            y.Visible = false;
            x.Visible = false;
        }

        private void txtPasssword_Click(object sender, EventArgs e)
        {
            leftloginmiddlepanel.Visible = false;
            leftbottomloginpanel.Visible = true;
            y.Visible = false;
            x.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
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

        private void GetStaff()
        {
            connection connect = new connection();
            if (connect.OpenConnection() == true)
            {
                String query = "SELECT * FROM role ORDER BY Role_id ASC";
                MySqlCommand cmd = new MySqlCommand(query, connect.con);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                this.cboRole.Items.Clear();
                while (dataReader.Read())
                {
                    if (dataReader["Role_Name"].ToString().Replace(" ", "") != "")
                    {
                        this.cboRole.Items.Add(dataReader["Role_Name"].ToString());
                    }
                }
                connect.CloseConnection();



            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            groupBoxAdmin.Visible = true;
        }

        private void loginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        

        
    }
}
