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

namespace kyrsovaia
{
    public partial class Groups : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens1;Integrated Security=True";
        public Groups()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
            LoadGroupData();
            LoadOlderGroupData();
            LoadSadData();
            comboBox3.Items.Add("Младшая группа");
            comboBox3.Items.Add("Средняя группа");
            comboBox3.Items.Add("Старшая группа");
           

            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList; // чтобы пользователь не мог вводить свои значения
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
        }
        private void LoadGroupData()
        {
            string query = "SELECT g.ID_sad, g.Name AS GroupName, g.Older_group, g.Level_group, p.FIO AS EmployeeName, k.Name AS KindergartenName " +
                  "FROM Groups g " +
                  "JOIN Personal p ON g.Older_group = p.ID_employee " +
                  "JOIN Kindergartens k ON g.ID_sad = k.ID_Sad";

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

            // Удаляем столбец "Older_group" из таблицы данных перед привязкой к DataGridView
            dataTable.Columns.Remove("Older_group");
           // dataTable.Columns.Remove("ID_sad");

            dataGridView1.DataSource = dataTable;
        }

        private void LoadOlderGroupData()
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                string query = "SELECT ID_employee, FIO FROM Personal";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable parentsTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(parentsTable);

                    comboBox2.DataSource = parentsTable;
                    comboBox2.DisplayMember = "FIO"; // Отображаемое поле
                    comboBox2.ValueMember = "ID_employee"; // Значение, которое будет использоваться при выборе
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о родителях: " + ex.Message);
                }
            }
        }
        private void LoadSadData()
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                string query = "SELECT ID_Sad, Name FROM Kindergartens";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable parentsTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(parentsTable);

                    comboBox1.DataSource = parentsTable;
                    comboBox1.DisplayMember = "Name"; // Отображаемое поле
                    comboBox1.ValueMember = "ID_Sad"; // Значение, которое будет использоваться при выборе
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных о родителях: " + ex.Message);
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Groups_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Hide();
            form.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox3.SelectedItem.ToString();

            switch (selectedValue)
            {
                case "Младшая группа":
                    break;
                case "Средняя группа":
                    break;
                case "Старшая группа":
                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string NameGroup = textBox1.Text;
            int NameSad = Convert.ToInt32(comboBox1.SelectedValue); // Преобразуем выбранный ID сада в int
            int dadGroup = Convert.ToInt32(comboBox2.SelectedValue); // Преобразуем выбранного родителя в int
            string level = comboBox3.Text;

            if (button3.Text == "Сохранить")
            {
                // Получить выделенную строку в DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    // Получаем данные из выбранной строки
                    string oldNameGroup = dataGridView1.Rows[selectedIndex].Cells["GroupName"].Value.ToString();

                    using (SqlConnection connection = new SqlConnection(Connect))
                    {
                        connection.Open();

                        // Создаем SQL-команду для обновления данных в базе данных
                        string updateQuery = "UPDATE Groups SET ID_sad = @NameSad, Name = @NameGroup, Older_group = @dadGroup, Level_group = @level WHERE Name = @oldNameGroup";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@NameSad", NameSad);
                        updateCommand.Parameters.AddWithValue("@NameGroup", NameGroup);
                        updateCommand.Parameters.AddWithValue("@dadGroup", dadGroup);
                        updateCommand.Parameters.AddWithValue("@level", level);
                        updateCommand.Parameters.AddWithValue("@oldNameGroup", oldNameGroup);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены.");

                            // Обновляем данные в DataGridView
                            LoadGroupData();
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
                // Добавление новой группы
                using (SqlConnection connection = new SqlConnection(Connect))
                {
                    connection.Open();

                    string insertChildQuery = "INSERT INTO Groups (ID_sad, Name, Older_group, Level_group) " + "VALUES (@NameSad, @NameGroup, @dadGroup, @level)";
                    SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                    insertChildCommand.Parameters.AddWithValue("@NameSad", NameSad);
                    insertChildCommand.Parameters.AddWithValue("@NameGroup", NameGroup);
                    insertChildCommand.Parameters.AddWithValue("@dadGroup", dadGroup);
                    insertChildCommand.Parameters.AddWithValue("@level", level);

                    int rowsAffected = insertChildCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены.");

                        // После успешной вставки вызываем метод для обновления данных в DataGridView
                        LoadGroupData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении данных.");
                    }
                }
            }
            textBox1.Clear();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получить выделенную строку в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                // Получаем данные из выбранной строки
                string nameGroup = dataGridView1.Rows[selectedIndex].Cells["GroupName"].Value.ToString();
                string idSad = dataGridView1.Rows[selectedIndex].Cells["ID_sad"].Value.ToString();
                string olderGroup = dataGridView1.Rows[selectedIndex].Cells["EmployeeName"].Value.ToString();
                string levelGroup = dataGridView1.Rows[selectedIndex].Cells["Level_group"].Value.ToString();

                // Заполняем текстовые поля данными из выбранной строки
                textBox1.Text = nameGroup;
                comboBox1.Text = idSad;
                comboBox2.Text = olderGroup;
                comboBox3.Text = levelGroup;

                button3.Text = "Сохранить";
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования данных.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Получить выделенную строку в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                // Получаем данные из выбранной строки
                string nameGroup = dataGridView1.Rows[selectedIndex].Cells["GroupName"].Value.ToString();

                // Удаляем группу из таблицы Groups
                DeleteGroup(nameGroup);

                // Обновляем данные в таблице Groups
                LoadGroupData();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления данных.");
            }
        }

        private void DeleteGroup(string nameGroup)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                // Устанавливаем столбец Older_group в NULL для всех записей, где он соответствует удаляемой группе
                string updateNullQuery = "UPDATE Groups SET Older_group = NULL WHERE Older_group = (SELECT ID_employee FROM Personal WHERE FIO = @nameGroup)";
                SqlCommand updateNullCommand = new SqlCommand(updateNullQuery, connection);
                updateNullCommand.Parameters.AddWithValue("@nameGroup", nameGroup);
                updateNullCommand.ExecuteNonQuery();

                // Удаляем запись из таблицы Groups
                string deleteQuery = "DELETE FROM Groups WHERE Name = @nameGroup";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@nameGroup", nameGroup);
                deleteCommand.ExecuteNonQuery();

                // После удаления обновляем данные в datagridview1
                LoadGroupData();
            }
        }
    }
}
