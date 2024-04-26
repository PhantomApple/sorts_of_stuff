using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kyrsovaia
{
    public partial class Childrens : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";

        public Childrens()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);

            // Заполнение DataGridView данными о детях при загрузке формы
            LoadChildrenData();
        }
        private void Childrens_Load(object sender, EventArgs e)
        {
            LoadParentsData();
            LoadGroupsData();
        }

        private void LoadChildrenData()
        {
            string query = "SELECT c.FIO AS FIO_children, c.date_of_birth, c.Information, c.Entrance, p.FIO AS FIO_parent, g.Name AS Group_Name " +
                   "FROM Childrens c " +
                   "LEFT JOIN Parents p ON c.ID_parent = p.id_parent " +
                   "LEFT JOIN Groups g ON c.ID_group = g.ID_group";

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
            FillYearsComboBoxes();
        }

        private void LoadParentsData()
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                string query = "SELECT id_parent, FIO FROM Parents";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable parentsTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(parentsTable);

                    comboBox1.DataSource = parentsTable;
                    comboBox1.DisplayMember = "FIO";
                    comboBox1.ValueMember = "id_parent";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о родителях: " + ex.Message);
                }
            }
        }
        private void LoadGroupsData()
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                string query = "SELECT ID_group, Name FROM Groups";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable groupsTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(groupsTable);

                    comboBox2.DataSource = groupsTable;
                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "ID_group";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о родителях: " + ex.Message);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            string FIO = textBox1.Text;
            DateTime dateOfBirth;
            if (!DateTime.TryParse(maskedTextBox1.Text, out dateOfBirth))
            {
                MessageBox.Show("Некорректный формат даты рождения.");
                return;
            }

            string Information = textBox3.Text;
            DateTime Entrance;
            if (!DateTime.TryParse(maskedTextBox2.Text, out Entrance))
            {
                MessageBox.Show("Некорректный формат даты поступления.");
                return;
            }

            int idParents = 0;
            if (comboBox1.SelectedValue != null)
            {
                idParents = Convert.ToInt32(comboBox1.SelectedValue);
            }

            int iDgroup = 0;
            if (comboBox2.SelectedValue != null)
            {
                iDgroup = Convert.ToInt32(comboBox2.SelectedValue);
            }

            if (button3.Text == "Сохранить")
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string primaryKeyValue = dataGridView1.Rows[selectedIndex].Cells["FIO_children"].Value.ToString();
                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();

                    // Создаем SQL-команду для обновления данных в базе данных
                    string updateChildQuery = "UPDATE Childrens SET FIO = @FIO, date_of_birth = @dateOfBirth, Information = @Information, Entrance = @Entrance, ID_parent = @idParents, ID_group = @iDgroup WHERE FIO = @primaryKeyValue";
                    SqlCommand updateChildCommand = new SqlCommand(updateChildQuery, connection);
                    updateChildCommand.Parameters.AddWithValue("@FIO", FIO);
                    updateChildCommand.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    updateChildCommand.Parameters.AddWithValue("@Information", Information);
                    updateChildCommand.Parameters.AddWithValue("@Entrance", Entrance);
                    updateChildCommand.Parameters.AddWithValue("@idParents", idParents);
                    updateChildCommand.Parameters.AddWithValue("@iDgroup", iDgroup);
                    updateChildCommand.Parameters.AddWithValue("@primaryKeyValue", primaryKeyValue);

                    int rowsAffected = updateChildCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.");
                        LoadChildrenData();
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
                    string insertChildQuery = "INSERT INTO Childrens ( FIO, date_of_birth, Information, Entrance, ID_parent, ID_group) " + "VALUES ( @FIO, @dateOfBirth, @Information, @Entrance, @idParents, @iDgroup)";
                    SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                    insertChildCommand.Parameters.AddWithValue("@FIO", FIO);
                    insertChildCommand.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    insertChildCommand.Parameters.AddWithValue("@Information", Information);
                    insertChildCommand.Parameters.AddWithValue("@Entrance", Entrance);
                    insertChildCommand.Parameters.AddWithValue("@idParents", idParents);
                    insertChildCommand.Parameters.AddWithValue("@iDgroup", iDgroup);
                    int rowsAffected = insertChildCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены.");
                        LoadChildrenData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении данных.");
                    }
                }
            }
            textBox1.Clear();
            maskedTextBox1.Clear();
            textBox3.Clear();
            maskedTextBox2.Clear();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Parents parents = new Parents();
            Hide();
            parents.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Hide();
            form1.ShowDialog();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string fio = dataGridView1.Rows[selectedIndex].Cells["FIO_children"].Value.ToString();
                DeleteChildData(fio);
                LoadChildrenData();
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
                string fio = dataGridView1.Rows[selectedIndex].Cells["FIO_children"].Value.ToString();
                string dateOfBirth = dataGridView1.Rows[selectedIndex].Cells["date_of_birth"].Value.ToString();
                string information = dataGridView1.Rows[selectedIndex].Cells["Information"].Value.ToString();
                string entrance = dataGridView1.Rows[selectedIndex].Cells["Entrance"].Value.ToString();
                textBox1.Text = fio;
                maskedTextBox1.Text = dateOfBirth;
                textBox3.Text = information;
                maskedTextBox2.Text = entrance;
                button3.Text = "Сохранить";
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования данных.");
            }
        }
        private void DeleteChildData(string fio)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Childrens WHERE FIO = @fio";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@fio", fio);
                deleteCommand.ExecuteNonQuery();
            }
        }
        private void FillYearsComboBoxes()
        {
            // Заполнение comboBox4 и comboBox5 только годами
            for (int year = DateTime.Now.Year; year >= 2010; year--)
            {
                comboBox4.Items.Add(year.ToString());
                comboBox5.Items.Add(year.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = "SELECT c.FIO AS FIO_children, c.date_of_birth, c.Information, c.Entrance, p.FIO AS FIO_parent, g.Name AS Group_Name " +
                           "FROM Childrens c " +
                           "LEFT JOIN Parents p ON c.ID_parent = p.id_parent " +
                           "LEFT JOIN Groups g ON c.ID_group = g.ID_group " +
                           "WHERE 1=1";

            if (!string.IsNullOrEmpty(textBox5.Text))
                query += $" AND c.FIO LIKE '%{textBox5.Text}%'";
            if (comboBox4.SelectedIndex != -1 && comboBox4.SelectedItem.ToString() != "Все")
            {
                string selectedYear = comboBox4.SelectedItem.ToString();
                query += $" AND YEAR(c.date_of_birth) = {selectedYear}";
            }
            if (!string.IsNullOrEmpty(textBox7.Text))
                query += $" AND c.Information LIKE '%{textBox7.Text}%'";
            if (!string.IsNullOrEmpty(textBox9.Text))
                query += $" AND p.FIO LIKE '%{textBox9.Text}%'";
            if (!string.IsNullOrEmpty(textBox10.Text))
                query += $" AND g.Name LIKE '%{textBox10.Text}%'";
            if (comboBox5.SelectedIndex != -1 && comboBox5.SelectedItem.ToString() != "Все")
            {
                string selectedYear = comboBox5.SelectedItem.ToString();
                query += $" AND YEAR(c.Entrance) = {selectedYear}";
            }

            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            dataTable = new DataTable();

            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            dataGridView1.DataSource = dataTable;
        }
    }
}
