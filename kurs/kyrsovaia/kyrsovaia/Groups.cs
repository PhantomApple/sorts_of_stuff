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
            string query = "SELECT /*g.ID_sad,*/ g.Name AS GroupName, /*g.Older_group,*/ g.Level_group, p.FIO AS EmployeeName, k.Name AS KindergartenName " +
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
            string NameSad = comboBox1.Text;
            string dadGroup = comboBox2.Text;
            string level = comboBox3.Text;


            using (SqlConnection connection = new SqlConnection(Connect))
            {
                connection.Open();

                string insertChildQuery = "INSERT INTO Groups ( ID_sad, Name, Older_group, Level_group) " + "VALUES (@NameSad, @NameGroup, @dadGroup, @level)";
                SqlCommand insertChildCommand = new SqlCommand(insertChildQuery, connection);
                insertChildCommand.Parameters.AddWithValue("@ID_sad", NameSad);
                insertChildCommand.Parameters.AddWithValue("@Name", NameGroup);
                insertChildCommand.Parameters.AddWithValue("@Older_group", dadGroup);
                insertChildCommand.Parameters.AddWithValue("@Level_group", level);

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
                textBox1.Clear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
