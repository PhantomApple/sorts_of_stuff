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
    public partial class Sad : Form
    {

        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens1;Integrated Security=True";
        public Sad()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            LoadSadData();
        }
        private void LoadSadData()
        {
            string query = "SELECT K.Name, K.Adres, K.Kontakts, P.FIO " +
               "FROM Kindergartens AS K " +
               "INNER JOIN Personal AS P ON K.ID_direktora = P.ID_employee";

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
        private void LoadPersonalData()
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                string query = "SELECT ID_employee, FIO FROM Personal WHERE Post = 'Директор'";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable directorsTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(directorsTable);

                    if (directorsTable.Rows.Count > 0)
                    {
                        comboBox1.DataSource = directorsTable;
                        comboBox1.DisplayMember = "FIO"; // Отображаемое поле
                        comboBox1.ValueMember = "ID_employee"; // Значение, которое будет использоваться при выборе
                    }
                    else
                    {
                        MessageBox.Show("Нет доступных директоров для выбора.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о директорах: " + ex.Message);
                }
            }
        }
        private void sad_Load(object sender, EventArgs e)
        {
            LoadPersonalData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string Kontakts = textBox2.Text;
            string Adres = textBox3.Text;

            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                string insertChildQuery = "INSERT INTO Kindergartens (Name, Adres, Kontakts, ID_direktora) " +
                          "VALUES (@Name, @Adres, @Kontakts, @ID_employee)";
                SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                insertChildCommand.Parameters.AddWithValue("@Name", Name);
                insertChildCommand.Parameters.AddWithValue("@Adres", Adres);
                insertChildCommand.Parameters.AddWithValue("@Kontakts", Kontakts);
                insertChildCommand.Parameters.AddWithValue("@ID_employee", comboBox1.SelectedValue); 

                int rowsAffected = insertChildCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные успешно добавлены.");

                    // После успешной вставки вызываем метод для обновления данных в DataGridView
                    LoadSadData();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
