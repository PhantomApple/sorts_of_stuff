using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrsovaia
{
    public partial class Personal : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens1;Integrated Security=True";
        public Personal()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            LoadPersonalsData();
            comboBox1.Items.Add("Директор");
            comboBox1.Items.Add("Уборщик");
            comboBox1.Items.Add("Учитель");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; // чтобы пользователь не мог вводить свои значения
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();

            switch (selectedValue)
            {
                case "Директор":
                    break;
                case "Уборщик":
                    break;
                case "Учитель":
                    break;
                default:
                    break;
            }
        }
        private void LoadPersonalsData()
        {
            string query = "SELECT  FIO, Post, Kontakts FROM Personal";

            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            dataTable = new DataTable();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            dataGridView1.DataSource = dataTable;
        }
       
        private void Personal_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            string FIO = textBox1.Text;
            string Post = comboBox1.Text;
            string Kontakts = textBox3.Text;

            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                string insertChildQuery = "INSERT INTO Personal ( FIO, Post, Kontakts) " + "VALUES (@FIO, @Post, @Kontakts)";
                SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                insertChildCommand.Parameters.AddWithValue("@FIO", FIO);
                insertChildCommand.Parameters.AddWithValue("@Post", Post);
                insertChildCommand.Parameters.AddWithValue("@Kontakts", Kontakts);

                int rowsAffected = insertChildCommand.ExecuteNonQuery();              

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные успешно добавлены.");

                    // После успешной вставки вызываем метод для обновления данных в DataGridView
                    LoadPersonalsData();
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении данных.");
                }
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Hide();
            form1.ShowDialog();
        }
    }
}
