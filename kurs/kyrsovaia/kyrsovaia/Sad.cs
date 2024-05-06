using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kyrsovaia
{
    public partial class Sad : Form
    {

        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";
        public Sad()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            LoadSadData();

            dataGridView1.Columns["Name"].HeaderText = "Название";
            dataGridView1.Columns["Adres"].HeaderText = "Адрес";
            dataGridView1.Columns["Kontakts"].HeaderText = "Контакты";
            dataGridView1.Columns["FIO"].HeaderText = "ФИО директора";
        }
        private void LoadSadData()
        {
            string query = "SELECT K.Name, K.Adres, K.Kontakts, P.FIO " +
     "FROM Kindergartens AS K " +
     "LEFT JOIN Personal AS P ON K.ID_direktora = P.ID_employee";


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
                        comboBox1.DisplayMember = "FIO"; 
                        comboBox1.ValueMember = "ID_employee"; 
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
            FillComboBox3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string Kontakts = textBox2.Text;
            string Adres = textBox3.Text;
            if (button3.Text == "Сохранить")
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string primaryKeyValue = dataGridView1.Rows[selectedIndex].Cells["Name"].Value.ToString();

                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();

                    // Создаем SQL-команду для обновления данных в базе данных
                    string updateChildQuery = "UPDATE Kindergartens SET Name = @Name, Adres = @Adres, Kontakts = @Kontakts, ID_direktora = @ID_direktora WHERE Name = @primaryKeyValue";
                    SqlCommand updateSadCommand = new SqlCommand(updateChildQuery, connection);
                    updateSadCommand.Parameters.AddWithValue("@Name", Name);
                    updateSadCommand.Parameters.AddWithValue("@Adres", Adres);
                    updateSadCommand.Parameters.AddWithValue("@Kontakts", Kontakts);
                    updateSadCommand.Parameters.AddWithValue("@ID_direktora", comboBox1.SelectedValue);
                    updateSadCommand.Parameters.AddWithValue("@primaryKeyValue", primaryKeyValue);

                    int rowsAffected = updateSadCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.");

                        button3.Text = "Добавить";
                        LoadSadData();
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

                }
            }
           
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string kindergartensName = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить " + kindergartensName + "?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteSad(kindergartensName);
                    LoadSadData();

                }
               
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления данных.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string name = dataGridView1.Rows[selectedIndex].Cells["Name"].Value.ToString();
                string adres = dataGridView1.Rows[selectedIndex].Cells["Adres"].Value.ToString();
                string kontakts = dataGridView1.Rows[selectedIndex].Cells["Kontakts"].Value.ToString();
                string fio = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                textBox1.Text = name;
                textBox2.Text = adres;
                textBox3.Text = kontakts;
                comboBox1.Text = fio;

                button3.Text = "Сохранить";
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования данных.");
            }
        }

        private void DeleteSad(string name)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();
                string deleteGroupsQuery = "DELETE FROM Groups WHERE ID_sad IN (SELECT ID_Sad FROM Kindergartens WHERE Name = @name)";
                SqlCommand deleteGroupsCommand = new SqlCommand(deleteGroupsQuery, connection);
                deleteGroupsCommand.Parameters.AddWithValue("@name", name);
                deleteGroupsCommand.ExecuteNonQuery();
                string deleteKindergartensQuery = "DELETE FROM Kindergartens WHERE Name = @name";
                SqlCommand deleteKindergartensCommand = new SqlCommand(deleteKindergartensQuery, connection);
                deleteKindergartensCommand.Parameters.AddWithValue("@name", name);
                deleteKindergartensCommand.ExecuteNonQuery();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Name = textBox4.Text;
            string Adres = textBox6.Text;
            string Kontakts = maskedTextBox1.Text;
            string FIO = textBox7.Text;

            string query = "SELECT K.Name, K.Adres, K.Kontakts, P.FIO " +
                           "FROM Kindergartens AS K " +
                           "LEFT JOIN Personal AS P ON K.ID_direktora = P.ID_employee " +
                           "WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(Name))
                query += " AND K.Name LIKE @Name";
            if (!string.IsNullOrWhiteSpace(Adres))
                query += " AND K.Adres LIKE @Adres";
            if (!string.IsNullOrWhiteSpace(Kontakts))
                query += " AND K.Kontakts LIKE @Kontakts";
            if (!string.IsNullOrWhiteSpace(FIO))
                query += " AND P.FIO LIKE @FIO";

            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + Name + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Adres", "%" + Adres + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Kontakts", "%" + Kontakts + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FIO", "%" + FIO + "%");

            dataTable = new DataTable();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении поиска: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            dataGridView1.DataSource = dataTable;
        }

        private void FillComboBox3()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Kontakts");
            comboBox3.SelectedIndex = 0;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string columnName = comboBox3.SelectedItem.ToString();
                DataView dataView = dataTable.DefaultView;
                dataView.Sort = columnName + " DESC";
                dataGridView1.DataSource = dataView;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "Kontakts")
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

    }
}
