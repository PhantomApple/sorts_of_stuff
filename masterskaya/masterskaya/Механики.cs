using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace masterskaya
{
    public partial class Механики : Form
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=Auto_workshop;Integrated Security=True";

        private DataTable mehTable;
        public Механики()
        {
            InitializeComponent();
            LoadMeh();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.Columns["FIO_master"].HeaderText = "ФИО_мастер";
            dataGridView1.Columns["Experience"].HeaderText = "Опыт_работы";
            dataGridView1.Columns["Discharge"].HeaderText = "Разряд";
            dataGridView1.Columns["Personal_number"].HeaderText = "Личный_номер";
            dataGridView1.Columns["Login"].HeaderText = "Логин";
            dataGridView1.Columns["Password"].HeaderText = "Пароль";
        }

        private void LoadMeh()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Masters";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                mehTable = new DataTable();

                try
                {
                    adapter.Fill(mehTable);
                    dataGridView1.DataSource = mehTable; // Отображение данных в DataGridView
                    dataGridView1.Columns["ID_master"].Visible = false;
                    dataGridView1.Columns["Login"].Visible = false;
                    dataGridView1.Columns["Password"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки списка механиков: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Механики_Load(object sender, EventArgs e)
        {

        }
    }
}
