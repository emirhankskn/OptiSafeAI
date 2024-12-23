using MySql.Data.MySqlClient;
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
    public partial class frmLogin : Form
    {
        #region Global Değişkenler
        private bool def = true;
        private UserManager userManager;
        #endregion

        #region Form Ctor

        public frmLogin()
        {
            InitializeComponent();
            IDatabaseConnection dbConnection = new MySqlDatabaseConnection();
            IUserRepository userRepository = new UserRepository(dbConnection);
            userManager = new UserManager(userRepository);
        }
        #endregion

        #region Form Load

        private void Form1_Load(object sender, EventArgs e)
        {
            pnlMain.Location = new Point(this.ClientSize.Width /2 -pnlMain.Size.Width /2, this.ClientSize.Height / 2 - pnlMain.Size.Height /2 );
            pnlMessage.Size = new Size(pnlMain.Width, pnlMain.Height / 5);
        }
        #endregion

        #region Menü Butonları (Kapatma Aşşağıya Alma)

        private void btnDown_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Login Butonu

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new User
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
            };

            if (userManager.ValidateUser(user))
            {
                frmMain frmMain = new frmMain();
                frmMain.Show();
                this.Hide();
            }
            else
            {
                lblMessage.Text = "Geçersiz kullanıcı adı veya şifre.";
                lblMessage.Visible = true;
                pnlMessage.Visible = true;
                timer1.Start();
            }
        }
        #endregion

        #region Şifrenin Gizli veya Görünür Olmasını Sağlayan Fonksiyon

        private void PwChar(bool def)
        {
            txtPassword.UseSystemPasswordChar = def;
            pctPW.Image = def ? Properties.Resources.hidden : Properties.Resources.view;
        }
        #endregion

        #region Şifrenin Yanındaki Göz Butonu

        private void pctPW_Click(object sender, EventArgs e)
        {
            def = !def;
            PwChar(def);
        }
        #endregion

        #region Timer Tick

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            pnlMessage.Visible = false;
            lblMessage.Visible = false;
            timer1.Stop();
        }
        #endregion

    }
}
