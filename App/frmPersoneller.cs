using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OptiSafeAI.App
{
    public partial class frmPersoneller : UserControl
    {
        #region Global Değişkenler

        IDatabaseConnection databaseConnection = new MySqlDatabaseConnection();
        int a;
        public event EventHandler PersonelEklendi;

        #endregion

        #region Eklenicek Event! (Yapılmadı)

        protected virtual void OnPersonelEklendi(EventArgs e)
        {
            PersonelEklendi?.Invoke(this, e);
        }
        #endregion

        #region frmPersonel Ctor

        public frmPersoneller()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load 

        private void frmPersoneller_Load(object sender, EventArgs e)
        {
            #region ComboBox Load
            LoadComboBoxData("Department");
            LoadComboBoxData("positions");
            #endregion
            #region DataGridView Data Load
            LoadPersonnelData();
            //using (MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection())
            //{
            //    conn.Open();
            //    string query = "SELECT * From Employees";
            //    DataTable dt = new DataTable();
            //    using (MySqlCommand cmd = new MySqlCommand(query, conn))
            //    using (MySqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        dt.Load(reader);
            //    }
            //    dataGridView1.DataSource = dt;

            //}
            #endregion
            dataGridView1.Font = new Font("Times New Roman", 12);
            LoadChart();
        }
        #endregion

        #region LoadPersonel Function

        private void LoadPersonnelData()
        {
            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();
            string query = @" SELECT e.Id, e.FullName, p.Name as PositionsName, d.Name as DepartmentName, e.BreachCount, e.EmploymentDate FROM employees e 
            JOIN positions p ON e.PositionId = p.Id
            JOIN department d ON e.DepartmentId = d.Id 
            ORDER BY Id 
";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                conn.Open(); adapter.Fill(dt); dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı hatası: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region DataGrid Double Click

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var cellValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value;

            if (cellValue != null)
            {
                string personelName = cellValue.ToString();
                ViewPersonelDirectory(personelName);
            }
        }
        #endregion

        #region Çift Tıklandığında Dosya Açma Metodu

        private void ViewPersonelDirectory(string personelName)
        {
            string directoryPath = Path.Combine(@"C:\PersonelData\", personelName);
            if (Directory.Exists(directoryPath))
            {
                System.Diagnostics.Process.Start("explorer.exe", directoryPath);
            }
            else
            {
                MessageBox.Show("Personel Dosyası bulunamadı");
                Directory.CreateDirectory(directoryPath);
                MessageBox.Show("Yeni Personel Dosyası Oluşturuldu");
            }
        }
        #endregion

        #region ComboBox Load Data

        private void LoadComboBoxData(string cmbName)
        {
            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();

            switch (cmbName)
            {
                case "Department":
                    conn.Open();
                    string query = "SELECT Id,Name From department ORDER BY Name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter VeriAdaptoru = new MySqlDataAdapter(cmd);
                    DataTable Tablo = new DataTable();
                    VeriAdaptoru.Fill(Tablo);
                    cmbPosition.DataSource = Tablo;
                    cmbPosition.DisplayMember = "Name";
                    cmbPosition.ValueMember = "Id";
                    VeriAdaptoru.Dispose();
                    conn.Close();
                    break;
                case "positions":
                    conn.Open();
                    string query2 = "SELECT Id,Name From positions ORDER BY Name";
                    MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
                    DataTable Tablo2 = new DataTable();
                    adapter2.Fill(Tablo2);
                    cmbDepartment.DataSource = Tablo2;
                    cmbDepartment.DisplayMember = "Name";
                    cmbDepartment.ValueMember = "Id";
                    adapter2.Dispose();
                    conn.Close();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Refresh Button

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPersonnelData();
        }
        #endregion

        #region Delete Button
        private void btnDelete_Click(object sender, EventArgs e)
        {


            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();
            conn.Open();
            string query = "DELETE FROM employees WHERE Id=@Id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", a);
            int count = cmd.ExecuteNonQuery();
            if (count < 0)
            {
                MessageBox.Show("Lütfen Tablodan Bir Kişi Seçiniz");
            }
            else
                MessageBox.Show("Seçilen Kişi Başarılı ile silindi");
            DataLoad();
            conn.Close();
        }
        #endregion

        #region DataGrid Cell Click

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                a = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            }
            catch { }
        }
        #endregion

        #region Deneme Load Silincek

        private void DataLoad()
        {
            using (MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection())
            {
                conn.Open();
                string query = @" SELECT e.Id, e.FullName, p.Name, e.DepartmentId, e.BreachCount, e.EmploymentDate FROM employees e 
                JOIN positions p ON e.PositionId = p.Id
                ";
                DataTable dt = new DataTable();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    dt.Load(reader);
                    reader.Close();
                }
                conn.Close();
                dataGridView1.DataSource = dt;

            }
        }
        #endregion

        #region Search Button

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;
            bool isDepartmentChecked = checkBox1.Checked;
            bool isPositionChecked = checkBox2.Checked;
            SearchPersonel(searchTerm, isDepartmentChecked, isPositionChecked);
        }
        #endregion

        #region Search Metodu

        private void SearchPersonel(string searchTerm, bool byDepartment, bool byPosition)
        {
            string query = @" SELECT e.Id, e.FullName, p.Name as PositionName, d.Name as DepartmentName, e.BreachCount, e.EmploymentDate FROM employees e 
            JOIN positions p ON e.PositionId = p.Id 
            JOIN department d ON e.DepartmentId = d.Id WHERE ";
            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();

            if (byDepartment)
            {
                query += "d.Name LIKE @SearchTerm";
            }
            else if (byPosition)
            {
                query += "p.Name LIKE @SearchTerm";
            }
            else
            {
                query += "e.FullName LIKE @SearchTerm";
            }

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

            MySqlDataReader reader = null;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata : {ex.Message}");
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

        }
        #endregion

        #region Personel Add Button

        private void btnPersonelAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cmbPosition.SelectedValue.ToString());
            MessageBox.Show(cmbPosition.Text);

            MessageBox.Show(cmbDepartment.SelectedValue.ToString());
            MessageBox.Show(cmbDepartment.Text);

            string fullName = txtName.Text;
            int positionId = Convert.ToInt32(cmbPosition.SelectedValue);
            int departmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
            DateTime employmentDate = DateTime.Now;
            AddPersonel(fullName, positionId, departmentId, employmentDate);
            //OnPersonelEklendi(EventArgs.Empty);
        }
        #endregion

        #region Personel Add Metot

        private void AddPersonel(string fullName, int positionId, int departmentId, DateTime employmentDate)
        {
            string query = @" INSERT INTO employees (FullName, PositionId, DepartmentId, BreachCount, EmploymentDate) VALUES (@FullName, @PositionId, @DepartmentId, @BreachCount, @EmploymentDate)";
            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@PositionId", positionId);
            cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
            cmd.Parameters.AddWithValue("@BreachCount", 0);
            cmd.Parameters.AddWithValue("@EmploymentDate", employmentDate);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Yeni Personel Eklendi");
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

        #region LoadChart
        private void LoadChart()
        {
            MySqlConnection conn = (MySqlConnection)databaseConnection.GetConnection();
            try
            {
                conn.Open();
                string query = @"
                SELECT bt.Name,COUNT(e.BreachCount) as BreachFrequency 
                FROM employees e
                JOIN breachtypes bt ON e.BreachCount = bt.Id
                GROUP BY bt.Name
                ORDER BY BreachFrequency DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                chartBreachCount.Series.Clear();
                Series series = new Series 
                {
                    Name = "BreachFrequency",
                    Color = System.Drawing.Color.Blue, 
                    ChartType = SeriesChartType.RangeColumn 
                };
                chartBreachCount.Series.Add(series);

                foreach (DataRow row in dataTable.Rows)
                {
                    series.Points.AddXY(row["Name"].ToString(), Convert.ToInt32(row["BreachFrequency"]));
                }
                chartBreachCount.Invalidate();

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
    }
}
