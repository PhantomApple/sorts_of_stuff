using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace masterskaya
{
    public partial class Заказы : Form
    {
        private string connectionString = @"Data Source=localhost;Initial Catalog=Auto_workshop;Integrated Security=True";

        private DataTable zakazTable;
        public Заказы()
        {
            InitializeComponent();
            LoadZakaz();
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

          
        }

        private void LoadZakaz()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Orders.ID_order, Orders.Price AS Цена, Masters.FIO_master AS ФИО_мастера, Orders.date_of_work AS дата_работы, " +
                               "Orders.type_of_work AS Тип_работы, Orders.plannerd_end_date AS планируемая_дата_окончания, Orders.real_end_date AS реальная_дата_окончания, " +
                               "Car.FIO_owner AS ФИО_владельца, Car.Brand AS Бренд " +
                               "FROM Orders " +
                               "JOIN Masters ON Orders.ID_master = Masters.ID_master " +
                               "JOIN Car ON Orders.ID_car = Car.ID_car";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                zakazTable = new DataTable();

                try
                {
                    adapter.Fill(zakazTable);

                    if (zakazTable != null && zakazTable.Rows.Count > 0)
                    {
                        if (dataGridView1.Columns.Contains("ID_order"))
                        {
                            dataGridView1.Columns["ID_order"].Visible = false;
                        }


                        dataGridView1.DataSource = zakazTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки списка заказов: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Заказы_Load(object sender, EventArgs e)
        {

        }
    }
}
