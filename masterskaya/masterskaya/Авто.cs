using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace masterskaya
{
    public partial class Авто : Form
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=Auto_workshop;Integrated Security=True";
        private DataTable avtoTable;
        public Авто()
        {
            InitializeComponent();
            LoadAvto();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.Columns["ID_car"].Visible = false;

            dataGridView1.Columns["Number"].HeaderText = "Номер";
            dataGridView1.Columns["Brand"].HeaderText = "Бренд";
            dataGridView1.Columns["Year_release"].HeaderText = "Год_выпуска";
            dataGridView1.Columns["FIO_owner"].HeaderText = "ФИО владельца";
        }

        private void LoadAvto()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Car";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                avtoTable = new DataTable();

                try
                {
                    adapter.Fill(avtoTable);
                    dataGridView1.DataSource = avtoTable; 
                   // dataGridView1.Columns["Id_master"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки списка авто: " + ex.Message);
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

        private void Авто_Load(object sender, EventArgs e)
        {

        }

        private void carBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();

        }
    }
}
