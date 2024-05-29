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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace masterskaya
{
    public partial class Авторизация : Form
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=Auto_workshop;Integrated Security=True";

        private bool isPasswordVisible = false;
        public Авторизация()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void Авторизация_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            textBox2.UseSystemPasswordChar = !isPasswordVisible;

            if (isPasswordVisible)
            {
                checkBox1.Text = "Скрыть";
            }
            else
            {
                checkBox1.Text = "Показать";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Masters WHERE Login = @Login AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();

                    if (count == 1)
                    {
                        //MessageBox.Show("Авторизация успешна!");
                        Админ админ = new Админ();
                        админ.Show();
                        this.Hide(); 
                    }
                    else if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Заполните все поля!");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Неверные логин или пароль. Попробуйте еще раз.");
                    }
                }
            }
        }
    }
}
