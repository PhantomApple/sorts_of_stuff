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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace kyrsovaia
{
    public partial class Parents : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";

        public Parents()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            LoadParentsData();

            dataGridView1.Columns["FIO"].HeaderText = "ФИО родителя";
            dataGridView1.Columns["Kontakts"].HeaderText = "Контакты";
            dataGridView1.Columns["Informations"].HeaderText = "Информация";
          

        }
        private void LoadParentsData()
        {
            string query = "SELECT  FIO, Kontakts, Informations FROM Parents";

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
            FillComboBox3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FIO = textBox1.Text;
            string Kontakts = maskedTextBox1.Text;
            string Information = textBox3.Text;
            if (button3.Text == "Сохранить")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    string primaryKeyValue = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                    using (SqlConnection connection = new SqlConnection(Connect))
                    {
                        connection.Open();
                        string updateChildQuery = "UPDATE Parents SET FIO = @FIO, Kontakts = @Kontakts, Informations = @Informations WHERE FIO = @primaryKeyValue";
                        SqlCommand insertParentsCommand = new SqlCommand(updateChildQuery, connection);
                        insertParentsCommand.Parameters.AddWithValue("@FIO", FIO);
                        insertParentsCommand.Parameters.Add("@Kontakts", SqlDbType.NVarChar, -1).Value = Kontakts;
                        insertParentsCommand.Parameters.AddWithValue("@Informations", Information);
                        insertParentsCommand.Parameters.AddWithValue("@primaryKeyValue", primaryKeyValue);
                        int rowsAffected = insertParentsCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены.");
                            button3.Text = "Добавить";
                            LoadParentsData();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении данных.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку для редактирования данных.");
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();
                    string insertChildQuery = "INSERT INTO Parents ( FIO, Kontakts, Informations) " + "VALUES (@FIO, @Kontakts, @Informations)";
                    SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                    insertChildCommand.Parameters.AddWithValue("@FIO", FIO);
                    insertChildCommand.Parameters.AddWithValue("@Kontakts", Kontakts);
                    insertChildCommand.Parameters.AddWithValue("@Informations", Information);
                    int rowsAffected = insertChildCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены.");
                        LoadParentsData();
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Hide();
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получить выделенную строку в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                // Получаем данные из выбранной строки
                string fio = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                string dateOfBirth = dataGridView1.Rows[selectedIndex].Cells["Kontakts"].Value.ToString();
                string information = dataGridView1.Rows[selectedIndex].Cells["Informations"].Value.ToString();
                // Заполняем текстовые поля данными из выбранной строки
                textBox1.Text = fio;
                maskedTextBox1.Text = dateOfBirth;
                textBox3.Text = information;

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
                string parentFIO = dataGridView1.SelectedRows[0].Cells["FIO"].Value.ToString();
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить " + parentFIO + "?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    UpdateChildrenParentLink(parentFIO);
                    DeleteParent(parentFIO);
                    LoadParentsData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления данных.");
            }
        }

        private void UpdateChildrenParentLink(string parentFIO)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                // Создаем SQL-команду для обновления связи детей данного родителя на NULL
                string updateChildrenQuery = "UPDATE Childrens SET ID_parent = NULL WHERE ID_parent IN (SELECT id_parent FROM Parents WHERE FIO = @parentFIO)";
                SqlCommand updateChildrenCommand = new SqlCommand(updateChildrenQuery, connection);
                updateChildrenCommand.Parameters.AddWithValue("@parentFIO", parentFIO);

                // Выполняем обновление связи детей
                updateChildrenCommand.ExecuteNonQuery();
            }
        }

        private void DeleteParent(string parentFIO)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                // Создаем SQL-команду для удаления родителя
                string deleteParentQuery = "DELETE FROM Parents WHERE FIO = @parentFIO";
                SqlCommand deleteParentCommand = new SqlCommand(deleteParentQuery, connection);
                deleteParentCommand.Parameters.AddWithValue("@parentFIO", parentFIO);

                // Выполняем удаление родителя
                deleteParentCommand.ExecuteNonQuery();
            }
        }

    private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string fio = textBox4.Text;
            string kontakts = maskedTextBox2.Text;
            string informations = textBox6.Text;

            // Вызываем метод поиска данных с заданными критериями
            SearchParentsData(fio, kontakts, informations);
        }
        private void SearchParentsData(string fio, string kontakts, string informations)
        {
            // Формируем запрос с учетом введенных критериев поиска
            string query = "SELECT FIO, Kontakts, Informations FROM Parents WHERE 1=1";
            if (!string.IsNullOrWhiteSpace(fio))
                query += $" AND FIO LIKE '%{fio}%'";
            if (!string.IsNullOrWhiteSpace(kontakts))
                query += $" AND Kontakts LIKE '%{kontakts}%'";
            if (!string.IsNullOrWhiteSpace(informations))
                query += $" AND Informations LIKE '%{informations}%'";

            // Создаем новый адаптер данных и заполняем таблицу
            using (sqlDataAdapter = new SqlDataAdapter(query, sqlConnection))
            {
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
            }

            // Обновляем источник данных для DataGridView
            dataGridView1.DataSource = dataTable;
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void FillComboBox3()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("FIO");
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
            if (comboBox3.SelectedItem.ToString() == "FIO" || comboBox3.SelectedItem.ToString() == "Kontakts")
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
