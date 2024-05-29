using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace masterskaya
{
    public partial class Админ : Form
    {
        public Админ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Авто авто = new Авто();
            авто.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Механики механики = new Механики();
            механики.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Заказы заказы = new Заказы();
            заказы.Show();
        }

        private void Админ_Load(object sender, EventArgs e)
        {

        }
    }
}
