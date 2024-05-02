using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kyrsovaia
{

    public partial class admin : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";
        private string currentUsername;
        private string currentPassword;
        public admin()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            FillComboBox3();
        }
        private void admin_Load(object sender, EventArgs e)
        {
            LoadAdminData();
            LoadPersonalData();
            dataGridView1.Columns["FIO"].HeaderText = "ФИО";
            dataGridView1.Columns["Username"].HeaderText = "Имя пользователя";
            dataGridView1.Columns["Password"].HeaderText = "Пароль";

            dataGridView1.ReadOnly = true;

            dataGridView1.Columns["Username"].Visible = false;
            dataGridView1.Columns["Password"].Visible = false;
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
                    dataTable = adminTable;
                    FillComboBox3();
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
                    string updateQuery = "UPDATE Admin_Auth SET Username = @Username, Password = @Password WHERE ID_employee = @ID_employee";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Username", KodName);
                    updateCommand.Parameters.AddWithValue("@Password", password);
                    updateCommand.Parameters.AddWithValue("@ID_employee", ID_employee);
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.");
                        button3.Text = "Добавить";
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
                    string checkQuery = "SELECT COUNT(*) FROM Admin_Auth WHERE ID_employee = @ID_employee";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@ID_employee", ID_employee);
                    int existingRecords = (int)checkCommand.ExecuteScalar();
                    if (existingRecords > 0)
                    {
                        MessageBox.Show("Запись с таким администратором уже существует.");
                        return;
                    }
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
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string FIO = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                currentUsername = dataGridView1.Rows[selectedIndex].Cells["Username"].Value.ToString();
                currentPassword = dataGridView1.Rows[selectedIndex].Cells["Password"].Value.ToString();
                comboBox2.Text = FIO;
                textBox5.Text = "";
                textBox1.Text = "";
                button3.Text = "Сохранить";
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования данных.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string username = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить " + username + "?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteAdminData(username);
                    LoadAdminData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления данных.");
            }
        }

        private void DeleteAdminData(string username)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Admin_Auth WHERE Username = @Username";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@Username", username);
                deleteCommand.ExecuteNonQuery();

                LoadAdminData();
            }
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            string FIO = textBox4.Text;
           

            string query = "SELECT p.FIO, a.Username, a.Password FROM Admin_Auth a INNER JOIN Personal p ON a.ID_employee = p.ID_employee WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(FIO))
                query += " AND p.FIO LIKE @FIO";
           
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                sqlDataAdapter = new SqlDataAdapter(query, connection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FIO", "%" + FIO + "%");
                
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e){ }
        private void FillComboBox3()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("FIO");
            comboBox3.SelectedIndex = 0;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "FIO")
            {
                string columnName = comboBox3.SelectedItem.ToString();
                DataView dataView = dataTable.DefaultView;
                dataView.Sort = columnName;
                if (checkBox1.Checked)
                {
                    dataView.Sort += " ASC";
                }
                else
                {
                    dataView.Sort += " DESC";
                }
                dataGridView1.DataSource = dataView;
            }
        }

       
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string columnName = comboBox3.SelectedItem.ToString();
                DataView dataView = dataTable.DefaultView;
                dataView.Sort = columnName + (checkBox1.Checked ? " ASC" : " DESC");
                dataGridView1.DataSource = dataView;
            }
        }
        private void label8_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e){ }
        private void button5_Click(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

    }
}
