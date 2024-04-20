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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kyrsovaia
{
    public partial class Form1 : Form
    {
        // private const string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens;Integrated Security=True";
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        public Form1()
        {
            InitializeComponent();
       string Connect = @"Data Source=DESKTOP-LNMOTHC;Initial Catalog=Kindergartens;Integrated Security=True";
            sqlConnection = new SqlConnection(Connect);

            // Создаем запрос для выборки данных
            string query = "SELECT FIO_children, date_of_birth FROM Childrens";

            // Создаем адаптер данных и заполняем таблицу
            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            dataTable = new DataTable();

            sqlConnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();

            // Отображаем данные в DataGridView
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Childrens childrens = new Childrens();
            Hide();
            childrens.Show();
            //using (SqlConnection connection = new SqlConnection(Connect))
            //{
            //    try
            //    {
            //        connection.Open();
            //        MessageBox.Show("imba");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Ошибка регистрации 😕 : " + ex.Message);
            //    }
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parents parents = new Parents();
            Hide();
            parents.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sad sad = new Sad();
            Hide();
            sad.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Personal personal = new Personal();
            Hide();
            personal.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Groups group = new Groups();
            Hide();
            group.Show();
        }
    }
}
