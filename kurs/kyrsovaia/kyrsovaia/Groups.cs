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
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";
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

            comboBox4.Items.Add("Младшая группа");
            comboBox4.Items.Add("Средняя группа");
            comboBox4.Items.Add("Старшая группа");

            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList; 

            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            dataGridView1.Columns["GroupName"].HeaderText = "Название группы";
            dataGridView1.Columns["Level_group"].HeaderText = "Уровень группы";
            dataGridView1.Columns["EmployeeName"].HeaderText = "ФИО сотрудника";
            dataGridView1.Columns["KindergartenName"].HeaderText = "Название сада";

            if (dataGridView1.Columns.Contains("ID_Sad"))
            {
                dataGridView1.Columns["ID_Sad"].Visible = false;
            }
        }
        private void LoadGroupData()
        {
            string query = "SELECT g.ID_Sad, g.Name AS GroupName, g.Older_group, g.Level_group, p.FIO AS EmployeeName, k.Name AS KindergartenName " +
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
            dataTable.Columns.Remove("Older_group");
            if (dataGridView1.Columns.Contains("ID_Sad"))
            {
                dataGridView1.Columns["ID_Sad"].Visible = false;
            }

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
                    comboBox2.DisplayMember = "FIO"; 
                    comboBox2.ValueMember = "ID_employee"; 
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
                    comboBox1.DisplayMember = "Name"; 
                    comboBox1.ValueMember = "ID_Sad"; 
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
            this.groupsTableAdapter.Fill(this.kindergartens1DataSet.Groups);
            FillComboBox3();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Hide();
            form.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string NameGroup = textBox1.Text;
            int NameSad = Convert.ToInt32(comboBox1.SelectedValue); 
            int dadGroup = Convert.ToInt32(comboBox2.SelectedValue); 
            string level = comboBox3.Text;

            if (button3.Text == "Сохранить")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    string oldNameGroup = dataGridView1.Rows[selectedIndex].Cells["GroupName"].Value?.ToString();
                    if (oldNameGroup != null)
                    {
                        using (SqlConnection connection = new SqlConnection(Connect))
                        {
                            connection.Open();
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
                                button3.Text = "Добавить";
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
            }
            else
            {
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                string nameGroup = dataGridView1.Rows[selectedIndex].Cells["GroupName"].Value.ToString();
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить " + nameGroup + "?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteGroup(nameGroup);
                    LoadGroupData();

                }
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

                // Очищаем элементы управления ввода
                textBox1.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string NameGroup = textBox2.Text;
            string NameSad = textBox3.Text;
            string DadGroup = textBox4.Text;
            string LevelGroup = comboBox4.Text;

            string query = "SELECT g.ID_sad, g.Name AS GroupName, g.Older_group, g.Level_group, p.FIO AS EmployeeName, k.Name AS KindergartenName " +
                  "FROM Groups g " +
                  "JOIN Personal p ON g.Older_group = p.ID_employee " +
                  "JOIN Kindergartens k ON g.ID_sad = k.ID_Sad " +
                  "WHERE 1 = 1";

            if (!string.IsNullOrWhiteSpace(NameGroup))
                query += " AND g.Name LIKE @NameGroup";
            if (!string.IsNullOrWhiteSpace(NameSad))
                query += " AND k.Name LIKE @NameSad";
            if (!string.IsNullOrWhiteSpace(DadGroup))
                query += " AND p.FIO LIKE @DadGroup";
            if (!string.IsNullOrWhiteSpace(LevelGroup))
                query += " AND g.Level_group LIKE @LevelGroup";

            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@NameGroup", "%" + NameGroup + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@NameSad", "%" + NameSad + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@DadGroup", "%" + DadGroup + "%");
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@LevelGroup", "%" + LevelGroup + "%");

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
            comboBox5.Items.Clear();
           // comboBox5.Items.Add("Older_group");
            comboBox5.Items.Add("Level_group");
            comboBox5.SelectedIndex = 0;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string columnName = comboBox5.SelectedItem.ToString();
                DataView dataView = dataTable.DefaultView;
                dataView.Sort = columnName + " DESC";
                dataGridView1.DataSource = dataView;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem != null) 
            {
                string columnName = comboBox5.SelectedItem.ToString(); 
                DataView dataView = dataTable.DefaultView;

                if ( columnName == "Level_group") 
                {
                    dataView.Sort = columnName; 
                    if (checkBox1.Checked)
                    {
                        dataView.Sort += " ASC"; 
                    }
                    else
                    {
                        dataView.Sort += " DESC"; 
                    }
                }

                dataGridView1.DataSource = dataView; 
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

    }
}
