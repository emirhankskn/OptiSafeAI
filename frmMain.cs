using MySql.Data.MySqlClient;
using OptiSafeAI.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiSafeAI
{
    public partial class frmMain : Form
    {
        IDatabaseConnection dbConnection = new MySqlDatabaseConnection();
        private bool UserChanger;
        private frmPersoneller frmPersoneller;
        private frmMainPage frmMainPage;
        private frmRaporlarPage frmRaporlarPage;
        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            ShowForm("frmMain");
        }

        private void btnPersoneller_MouseEnter(object sender, EventArgs e)
        {
            btnPersoneller.Dock = DockStyle.Fill;
        }

        private void btnPersoneller_MouseLeave(object sender, EventArgs e)
        {
            btnPersoneller.Dock = DockStyle.None;
        }

        //private void UserChange(bool userChanger)
        //{
        //    if (userChanger)
        //    {
        //        panel13.Visible = true;
        //    }
        //    else
        //    {
        //        panel13.Visible = false;
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            UserChanger = !UserChanger;
            //UserChange(UserChanger);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowForm(string panelName)
        {
            this.pnlMain.Controls.Clear();

            switch (panelName)
            {
                case "personel":
                    if(frmPersoneller == null)
                    {
                        this.frmPersoneller = new frmPersoneller();
                        this.frmPersoneller.Dock = DockStyle.Fill;
                        this.frmPersoneller.Location = new Point(0,0);
                        this.frmPersoneller.TabIndex = 1;
                    }
                    this.pnlMain.Controls.Add(frmPersoneller);
                    break;
                case "frmMain":
                    if(frmMainPage == null)
                    {
                        this.frmMainPage = new frmMainPage();
                        this.frmMainPage.Dock = DockStyle.Fill;
                        this.frmMainPage.Location = new Point(0,0);
                        this.frmMainPage.TabIndex = 1;
                    }
                    this.pnlMain.Controls.Add(frmMainPage);
                    break;
                case "raporlar":
                    if (frmRaporlarPage == null)
                    {
                        this.frmRaporlarPage = new frmRaporlarPage();
                        this.frmRaporlarPage.Dock = DockStyle.Fill;
                        this.frmRaporlarPage.Location = new Point(0,0);
                        this.frmRaporlarPage.TabIndex = 1;
                    }
                    this.pnlMain.Controls.Add(frmRaporlarPage);
                    break;
                default:
                    break;
            }

        }

        private void btnPersoneller_Click(object sender, EventArgs e)
        {
            ShowForm("personel");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowForm("frmMain");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowForm("raporlar");
        }
    }
}
