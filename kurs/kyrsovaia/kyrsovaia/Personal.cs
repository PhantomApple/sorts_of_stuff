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
    public partial class Personal : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";
        public Personal()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            LoadPersonalsData();
            comboBox1.Items.Add("Директор");
            comboBox1.Items.Add("Уборщица");
            comboBox1.Items.Add("Педагог");
            comboBox1.Items.Add("Воспитатель");
            comboBox1.Items.Add("Медсестра");
            comboBox1.Items.Add("Бухгалтер");
            comboBox1.Items.Add("Помощник воспитателя");
            comboBox1.Items.Add("Повар");
            comboBox1.Items.Add("Администратор");
            comboBox1.Items.Add("Педиатр");

            comboBox2.Items.Add("Директор");
            comboBox2.Items.Add("Уборщица");
            comboBox2.Items.Add("Педагог");
            comboBox2.Items.Add("Воспитатель");
            comboBox1.Items.Add("Медсестра");
            comboBox2.Items.Add("Бухгалтер");
            comboBox2.Items.Add("Помощник воспитателя");
            comboBox2.Items.Add("Повар");
            comboBox2.Items.Add("Администратор");
            comboBox2.Items.Add("Педиатр");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; // чтобы пользователь не мог вводить свои значения
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
           
            dataGridView1.Columns["FIO"].HeaderText = "ФИО";
            dataGridView1.Columns["Post"].HeaderText = "Должность";
            dataGridView1.Columns["Kontakts"].HeaderText = "Контакты";
            dataGridView1.Columns["Experience"].HeaderText = "Стаж";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();

            switch (selectedValue)
            {
                case "Директор":
                    break;
                case "Уборщица":
                    break;
                case "Педагог":
                    break;
                 case "Воспитатель":
                    break;
                 case "Медсестра":
                    break;
                 case "Бухгалтер":
                    break;
                 case "Помощник воспитателя":
                    break;
                case "Повар":
                    break;
                case "Администратор":
                    break;
                case "Педиатр":
                    break;
                default:
                    break;
            }
        }
        private void LoadPersonalsData()
        {
            string query = "SELECT  FIO, Post, Kontakts, Experience FROM Personal";

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
            FillComboBox3();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string FIO = textBox1.Text;
            string Post = comboBox1.Text;
            string Kontakts = textBox3.Text;
            string Experience = textBox2.Text;

            if (button3.Text == "Сохранить")
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string primaryKeyValue = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();
                    string insertPersonalQuery = "UPDATE Personal SET FIO = @FIO, Post = @Post, Kontakts = @Kontakts, Experience = @Experience WHERE FIO = @primaryKeyValue";
                    SqlCommand updatePersonalCommand = new SqlCommand(insertPersonalQuery, connection);
                    updatePersonalCommand.Parameters.AddWithValue("@FIO", FIO);
                    updatePersonalCommand.Parameters.AddWithValue("@Post", Post);
                    updatePersonalCommand.Parameters.AddWithValue("@Kontakts", Kontakts);
                    updatePersonalCommand.Parameters.AddWithValue("@Experience", Experience);
                    updatePersonalCommand.Parameters.AddWithValue("@primaryKeyValue", primaryKeyValue);
                    int rowsAffected = updatePersonalCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены.");
                        button3.Text = "Добавить";
                        LoadPersonalsData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении данных.");
                    }
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();
                    string insertChildQuery = "INSERT INTO Personal ( FIO, Post, Kontakts, Experience) " + "VALUES (@FIO, @Post, @Kontakts, @Experience)";
                    SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                    insertChildCommand.Parameters.AddWithValue("@FIO", FIO);
                    insertChildCommand.Parameters.AddWithValue("@Post", Post);
                    insertChildCommand.Parameters.AddWithValue("@Kontakts", Kontakts);
                    insertChildCommand.Parameters.AddWithValue("@Experience", Experience);
                    int rowsAffected = insertChildCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены.");
                        LoadPersonalsData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении данных.");
                    }
                }

                textBox1.Clear();
                textBox3.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Hide();
            form1.ShowDialog();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string FIO = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                string Post = dataGridView1.Rows[selectedIndex].Cells["Post"].Value.ToString();
                string Kontakts = dataGridView1.Rows[selectedIndex].Cells["Kontakts"].Value.ToString();
                string Experience = dataGridView1.Rows[selectedIndex].Cells["Experience"].Value.ToString();
                textBox1.Text = FIO;
                comboBox1.Text = Post;
                textBox3.Text = Kontakts;
                textBox2.Text = Experience;
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
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string personalFIO = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(Connect))
                    {
                        connection.Open();
                        UpdateSadTable(personalFIO);
                        UpdatePersonalSadLink(personalFIO);
                        DeletePersonalData(personalFIO);
                        LoadPersonalsData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления данных.");
            }
        }


        private void UpdateSadTable(string personalFIO)
        {
            string updateSadQuery = "UPDATE Groups SET Older_group = NULL WHERE Older_group IN (SELECT ID_sad FROM Kindergartens WHERE ID_direktora = (SELECT ID_employee FROM Personal WHERE FIO = @personalFIO))";

            using (SqlConnection connection = new SqlConnection(Connect))
            {
                SqlCommand updateSadCommand = new SqlCommand(updateSadQuery, connection);
                updateSadCommand.Parameters.AddWithValue("@personalFIO", personalFIO);

                try
                {
                    connection.Open();
                    updateSadCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении таблицы Groups: " + ex.Message);
                }
            }
        }

        private void UpdatePersonalSadLink(string personalFIO)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                // Создаем SQL-команду для обновления связи детей данного персонала на NULL в таблице Kindergartens
                string updateChildrenQuery = "UPDATE Kindergartens SET ID_direktora = NULL WHERE ID_direktora IN (SELECT ID_employee FROM Personal WHERE FIO = @personalFIO)";
                SqlCommand updateChildrenCommand = new SqlCommand(updateChildrenQuery, connection);
                updateChildrenCommand.Parameters.AddWithValue("@personalFIO", personalFIO);

                // Выполняем обновление связи детей
                updateChildrenCommand.ExecuteNonQuery();
            }
        }


        private void DeletePersonalData(string fio)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();
                string checkAdminUsageQuery = "SELECT COUNT(*) FROM Admin_Auth WHERE ID_employee = (SELECT ID_employee FROM Personal WHERE FIO = @fio)";
                SqlCommand checkAdminUsageCommand = new SqlCommand(checkAdminUsageQuery, connection);
                checkAdminUsageCommand.Parameters.AddWithValue("@fio", fio);
                int adminUsageCount = (int)checkAdminUsageCommand.ExecuteScalar();
                if (adminUsageCount > 0)
                {
                    MessageBox.Show("Администратор используется в форме администратора. Удаление невозможно.");
                    return;
                }

                string updateGroupsQuery = "UPDATE Groups SET Older_group = NULL WHERE Older_group IN (SELECT ID_employee FROM Personal WHERE FIO = @fio)";
                SqlCommand updateGroupsCommand = new SqlCommand(updateGroupsQuery, connection);
                updateGroupsCommand.Parameters.AddWithValue("@fio", fio);
                updateGroupsCommand.ExecuteNonQuery();              
                string deleteQuery = "DELETE FROM Personal WHERE FIO = @fio";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@fio", fio);
                deleteCommand.ExecuteNonQuery();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string FIO = textBox4.Text;
            string Kontakts = maskedTextBox1.Text;
            string Post = comboBox2.Text; 
            string Experience = maskedTextBox2.Text;

            string query = "SELECT FIO, Post, Kontakts, Experience FROM Personal WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(FIO))
                query += " AND FIO LIKE @FIO";
            if (!string.IsNullOrWhiteSpace(Kontakts))
                query += " AND Kontakts LIKE @Kontakts";
            if (!string.IsNullOrWhiteSpace(Post))
                query += " AND Post LIKE @Post";
            if (!string.IsNullOrWhiteSpace(Experience))
                query += " AND Experience LIKE @Experience";

            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FIO", "%" + FIO + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Kontakts", "%" + Kontakts + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Post", "%" + Post + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Experience", "%" + Experience + "%");

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
            comboBox3.Items.Add("Post");
            comboBox3.Items.Add("Experience");
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
            if (comboBox3.SelectedItem.ToString() == "Kontakts" || comboBox3.SelectedItem.ToString() == "Post" || comboBox3.SelectedItem.ToString() == "Experience")
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

    }
}
