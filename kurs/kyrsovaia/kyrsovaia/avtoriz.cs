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

namespace kyrsovaia
{
    public partial class avtoriz : Form
    {
        private SqlConnection sqlConnection;
        private string Connect = @"Data Source=localhost;Initial Catalog=Kindergartens1;Integrated Security=True";

        public avtoriz()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Connect);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (AuthenticateUser(email, password))
            {
                Form1 form1 = new Form1();
                Hide();
                form1.Show();
            }
            else
            {
                MessageBox.Show("Ошибка: неверная почта или пароль.");
            }
        }
        private bool AuthenticateUser(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(Connect))
            {
                try
                {
                    string insertUserQuery = "SELECT COUNT(*) FROM Admin_Auth WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(insertUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", email); 
                        command.Parameters.AddWithValue("@Password", password);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        connection.Close();
                        if (count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при аутентификации пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


        private void avtoriz_Load(object sender, EventArgs e)
        {

        }
    }
}
