using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            textBox1.Enter += TextBox_Enter;
            textBox1.Leave += TextBox_Leave;
            textBox2.Enter += TextBox_Enter;
            textBox2.Leave += TextBox_Leave;

            // Устанавливаем подсказку при запуске формы
            SetPlaceholder(textBox1, "Введите имя...");
            SetPlaceholder(textBox2, "Введите пароль...");
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

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetPlaceholder(textBox, textBox.Tag.ToString());
            }
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.Tag = placeholder;
            textBox.ForeColor = Color.Gray; // Цвет подсказки серый
        }
        private void avtoriz_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
