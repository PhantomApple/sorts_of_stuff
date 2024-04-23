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
        private string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens1;Integrated Security=True";
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

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string FIO = textBox1.Text;
            string Post = comboBox1.Text;
            string Kontakts = textBox3.Text;
            string Experience = textBox2.Text;

            if (button3.Text == "Сохранить")
            {
                // Получаем индекс выбранной строки в DataGridView
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                // Получаем значение первичного ключа для выбранной строки
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

                        // После успешной вставки вызываем метод для обновления данных в DataGridView
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

                        // После успешной вставки вызываем метод для обновления данных в DataGridView
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
            // Получить выделенную строку в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                // Получаем данные из выбранной строки
                string FIO = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();
                string Post = dataGridView1.Rows[selectedIndex].Cells["Post"].Value.ToString();
                string Kontakts = dataGridView1.Rows[selectedIndex].Cells["Kontakts"].Value.ToString();
                string Experience = dataGridView1.Rows[selectedIndex].Cells["Experience"].Value.ToString();

                // Заполняем текстовые поля данными из выбранной строки
                textBox1.Text = FIO;
                comboBox1.Text = Post;
                textBox2.Text = Kontakts;
                textBox3.Text = Experience;

                button3.Text = "Сохранить";
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования данных.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Получить ID выделенной строки в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string personalFIO = dataGridView1.Rows[selectedIndex].Cells["FIO"].Value.ToString();

                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();

                    // Обновляем записи в таблице Sad, чтобы они ссылались на другую группу или на NULL
                    UpdateSadTable(personalFIO);

                    // Перед удалением персонала обновляем ID_direktora в таблице Kindergartens на NULL
                    UpdatePersonalSadLink(personalFIO);

                    // Удаляем запись из таблицы Personal и обновляем столбец Older_group в таблице Groups на NULL
                    DeletePersonalData(personalFIO);

                    // После удаления обновляем данные в DataGridView
                    LoadPersonalsData();
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

                // Обновляем поле Older_group в таблице Groups на NULL
                string updateGroupsQuery = "UPDATE Groups SET Older_group = NULL WHERE Older_group IN (SELECT ID_employee FROM Personal WHERE FIO = @fio)";
                SqlCommand updateGroupsCommand = new SqlCommand(updateGroupsQuery, connection);
                updateGroupsCommand.Parameters.AddWithValue("@fio", fio);
                updateGroupsCommand.ExecuteNonQuery();

                // Удаляем запись из таблицы Personal
                string deleteQuery = "DELETE FROM Personal WHERE FIO = @fio";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@fio", fio);
                deleteCommand.ExecuteNonQuery();
            }
        }
    }
}
