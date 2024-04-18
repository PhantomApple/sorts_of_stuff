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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kyrsovaia
{
    public partial class Parents : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens1;Integrated Security=True";
        public Parents()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            LoadParentsData();
        }
        private void LoadParentsData()
        {
            string query = "SELECT  FIO, Kontakts, Information FROM Parents";

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

        private void Parents_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FIO = textBox1.Text;
            string Kontakts = textBox2.Text;
            string Information = textBox3.Text;
           
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                string insertChildQuery = "INSERT INTO Parents ( FIO, Kontakts, Information) " + "VALUES (@FIO, @Kontakts, @Information)";
                SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                insertChildCommand.Parameters.AddWithValue("@FIO", FIO);
                insertChildCommand.Parameters.AddWithValue("@Kontakts", Kontakts);
                insertChildCommand.Parameters.AddWithValue("@Information", Information);

                int rowsAffected = insertChildCommand.ExecuteNonQuery();

                // Остальной код обработки результатов вставки


                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные успешно добавлены.");

                    // После успешной вставки вызываем метод для обновления данных в DataGridView
                    LoadParentsData();
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
