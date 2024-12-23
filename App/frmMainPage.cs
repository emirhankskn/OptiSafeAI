using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiSafeAI.App
{
    public partial class frmMainPage : UserControl
    {
        IDatabaseConnection dbConnection = new MySqlDatabaseConnection();
        public frmMainPage()
        {
            InitializeComponent();
            frmPersoneller frmPersoneller = new frmPersoneller();
            frmPersoneller.PersonelEklendi += FrmPersoneller_PersonelEklendi;
        }

        private void FrmPersoneller_PersonelEklendi(object sender, EventArgs e)
        {
            Panel pnlInfo = new Panel();
            pnlInfo.Dock = DockStyle.Top;
            Label lblInfo = new Label();
            lblInfo.Text = "Yeni Personel Eklendi";


            pnlInfo.Controls.Add(lblInfo);
            pnlNotification.Controls.Add(pnlInfo);
        }

        private void frmMainPage_Load(object sender, EventArgs e)
        {
            string query = "select COUNT(*) FROM employees";
            using (MySqlConnection connection = (MySqlConnection)dbConnection.GetConnection())
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                int employeesCount = Convert.ToInt32(result);

                lblPersonel.Text = $"Number of Employees : {employeesCount}";
            }
        }
    }
}
