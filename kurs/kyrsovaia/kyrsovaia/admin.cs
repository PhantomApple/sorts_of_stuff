using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace kyrsovaia
{

    public partial class admin : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";

        public admin()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
        }
        private void admin_Load(object sender, EventArgs e)
        {
            LoadAdminData();
            LoadPersonalData();
        }

        private void LoadAdminData()
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                string query = "SELECT p.FIO, a.Username, a.Password FROM Admin_Auth a INNER JOIN Personal p ON a.ID_employee = p.ID_employee";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable adminTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(adminTable);
                    dataGridView1.DataSource = adminTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных из таблицы Admin_Auth: " + ex.Message);
                }
            }
        }


        private void LoadPersonalData()
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                string query = "SELECT ID_employee, FIO FROM Personal WHERE Post = 'Администратор'";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable personalTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(personalTable);
                    if (personalTable.Rows.Count > 0)
                    {
                        comboBox2.DataSource = personalTable;
                        comboBox2.DisplayMember = "FIO";
                        comboBox2.ValueMember = "ID_employee";
                    }
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о персонале: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Hide(); 
            form1.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            int ID_employee = 0;
            string KodName = textBox5.Text;
            string password = textBox1.Text;
            if (comboBox2.SelectedValue != null)
            {
                ID_employee = Convert.ToInt32(comboBox2.SelectedValue);
            }


            if (button3.Text == "Сохранить")
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string primaryKeyValue = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();

                    // Создаем SQL-команду для обновления данных в базе данных
                    string updateQuery = "UPDATE Admin_Auth SET Username = @Username, Password = @Password WHERE ID_employee = @ID_employee";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Username", KodName);
                    updateCommand.Parameters.AddWithValue("@Password", password);
                    updateCommand.Parameters.AddWithValue("@ID_employee", ID_employee);


                    //updateCommand.Parameters.AddWithValue("@primaryKeyValue", primaryKeyValue);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.");
                        LoadAdminData();

                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных.");
                    }
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();

                    // Проверяем, не существует ли уже записи с таким ID_employee
                    string checkQuery = "SELECT COUNT(*) FROM Admin_Auth WHERE ID_employee = @ID_employee";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@ID_employee", ID_employee);

                    int existingRecords = (int)checkCommand.ExecuteScalar();

                    if (existingRecords > 0)
                    {
                        MessageBox.Show("Запись с таким администратором уже существует.");
                        return;
                    }

                    // Создаем SQL-команду для вставки новой записи в таблицу Admin_Auth
                    string insertQuery = "INSERT INTO Admin_Auth (ID_employee, Username, Password) VALUES (@ID_employee, @Username, @Password)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@ID_employee", ID_employee);
                    insertCommand.Parameters.AddWithValue("@Username", KodName);
                    insertCommand.Parameters.AddWithValue("@Password", password);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены.");
                        LoadAdminData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении данных.");
                    }
                }
            }
            textBox1.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получить выделенную строку в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                // Получаем данные из выбранной строки
                string FIO = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                string KodName = dataGridView1.Rows[selectedIndex].Cells["Username"].Value.ToString();
                string password = dataGridView1.Rows[selectedIndex].Cells["Password"].Value.ToString();

                // Заполняем текстовые поля данными из выбранной строки
                comboBox2.Text = FIO;
                textBox5.Text = KodName;
                textBox1.Text = password;



                button3.Text = "Сохранить";
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования данных.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранная строка в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем значение Username выбранной строки
                string username = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();

                // Вызываем метод для удаления данных из базы данных
                DeleteAdminData(username);

                // Перезагружаем данные в DataGridView
                LoadAdminData();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления данных.");
            }
        }

        private void DeleteAdminData(string username)
        {
            // Создаем соединение с базой данных
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                // Создаем SQL-команду для удаления данных из таблицы Admin_Auth по Username
                string deleteQuery = "DELETE FROM Admin_Auth WHERE Username = @Username";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@Username", username);

                // Выполняем SQL-команду
                deleteCommand.ExecuteNonQuery();

                LoadAdminData();
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string FIO = textBox4.Text;
            string Username = textBox3.Text;
            string Password = textBox2.Text;

            string query = "SELECT p.FIO, a.Username, a.Password FROM Admin_Auth a INNER JOIN Personal p ON a.ID_employee = p.ID_employee WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(FIO))
                query += " AND p.FIO LIKE @FIO";
            if (!string.IsNullOrWhiteSpace(Username))
                query += " AND a.Username LIKE @Username";
            if (!string.IsNullOrWhiteSpace(Password))
                query += " AND a.Password LIKE @Password";

            using (SqlConnection connection = new SqlConnection(Connect))
            {
                sqlDataAdapter = new SqlDataAdapter(query, connection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FIO", "%" + FIO + "%");
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Username", "%" + Username + "%");
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Password", "%" + Password + "%");

                dataTable = new DataTable();

                try
                {
                    connection.Open();
                    sqlDataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении поиска: " + ex.Message);
                }
            }
        }
    }
}
