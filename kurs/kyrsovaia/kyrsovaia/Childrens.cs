using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kyrsovaia
{
    public partial class Childrens : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens1;Integrated Security=True";

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
                    comboBox1.DisplayMember = "FIO"; // Отображаемое поле
                    comboBox1.ValueMember = "id_parent"; // Значение, которое будет использоваться при выборе
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
                    comboBox2.DisplayMember = "Name"; // Отображаемое поле
                    comboBox2.ValueMember = "ID_group"; // Значение, которое будет использоваться при выборе
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
            if (!DateTime.TryParse(textBox2.Text, out dateOfBirth))
            {
                MessageBox.Show("Некорректный формат даты рождения.");
                return;
            }

            string Information = textBox3.Text;
            DateTime Entrance;
            if (!DateTime.TryParse(textBox4.Text, out Entrance))
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

                // Остальной код обработки результатов вставки


            if (rowsAffected > 0)
            {
                MessageBox.Show("Данные успешно добавлены.");

                // После успешной вставки вызываем метод для обновления данных в DataGridView
                LoadChildrenData();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении данных.");
            }
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Добавьте вашу логику обработки события здесь
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
    }
}