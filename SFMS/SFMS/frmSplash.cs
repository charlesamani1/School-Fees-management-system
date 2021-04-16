using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFMS
{
    public partial class frmSplash : Form
    {
        int counter;
        public frmSplash()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Splash_Load(object sender, EventArgs e)
        {
            lsplash.Visible = false;
            rsplash.Visible = false;

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (counter <= 100)
                {
                    progressBar1.Value = counter;
                    lblDisplay.Text = "loading..." + counter + "%";
                }
                else
                {
                    // MDIParent1 mdi = new MDIParent1();
                    // mdi.Visible = true;
                    // Login log = new Login();
                    //log.Visible = true;
                    //ResetPassword p = new ResetPassword();
                    LoginForm LF = new LoginForm();
                    LF.Visible = true;
                    this.Show();
                   // LoginOption option = new LoginOption();
                    //option.Visible = true;
                    //this.Show();

                    this.Hide();
                    timer1.Enabled = false;
                }
                counter += 20;
            }
        }
    }

