using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptiSafeAI.App
{
    public partial class frmRaporlarPage : UserControl
    {
        IDatabaseConnection databaseConnection = new MySqlDatabaseConnection();
        
        public frmRaporlarPage()
        {
            InitializeComponent();
        }

        #region SaveReport
        private void SaveReport(string reportText, DateTime reportDate)
        {
            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();

            string query = @"
            INSERT INTO reports (ReportText, ReportDate) VALUES (@ReportText,@ReportDate)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ReportText", reportText);
            cmd.Parameters.AddWithValue("@ReportDate", reportDate);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Raport Oluştruldu");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region Save Button

        private void button2_Click(object sender, EventArgs e)
        {
            string reportText = richTextBox1.Text;
            DateTime dateTime = DateTime.Now;
            SaveReport(reportText, dateTime);
        }
        #endregion

        #region Load Reports

        private void LoadReports()
        {
            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();
            string query = "SELECT Id, ReportDate, FROM reports order by ReportDate ASC";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            foreach (DataRow row in dt.Rows)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Id = Convert.ToInt32(row["id"]),
                    ReportDate = Convert.ToDateTime(row["ReportDate"]),
                    FilePath = row["FilePath"].ToString()
                };
                lstBoxRapor.Items.Add(item);
            }
        }
        #endregion

        #region button1 Click

        private void button1_Click(object sender, EventArgs e)
        {
            LoadReports();
        }
        #endregion

    }
}
