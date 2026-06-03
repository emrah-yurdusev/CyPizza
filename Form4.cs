using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CyPizza;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace emrah
{
    public partial class Form4 : Form
    {
        private MySqlConnection connection;
        string connectionString = "Server=localhost;port=3308;Database=cypizza;Uid=emrah;Pwd=;";
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;

        void stock()
        {
            try
            {
                dt = new DataTable();
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT * FROM products", connection);
                dataGridView1.DataSource = dt;
                adapter.Fill(dt);
                connection.Close();
            }
            catch
            {

            }

        }
        public Form4()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);

            try
            {

                connection.Open();
                label13.Text = "CONNECTED";
                label13.ForeColor = System.Drawing.Color.Green;
                connection.Close();
            }
            catch
            {
                label13.Text = "CONNECTED FAİLED";
                label13.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            stock();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                numericUpDown1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                numericUpDown2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();


            }
            catch
            {

                throw;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (Program.currentRole == "admin")
            { }

            else
            {
                Form1 form1 = new Form1();
                form1.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text) ||
                string.IsNullOrEmpty(comboBox1.Text) ||
                string.IsNullOrEmpty(numericUpDown1.Text) ||
                string.IsNullOrEmpty(numericUpDown2.Text))
               
            {
                MessageBox.Show("Make sure all fields are filled out!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "INSERT INTO products(pr_name, type, unit, price ) VALUES(@pr_name, @type, @unit, @price)";
            cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@pr_name", comboBox2.Text);
            cmd.Parameters.AddWithValue("@type", comboBox1.Text);
            cmd.Parameters.AddWithValue("@unit", numericUpDown1.Text);
            cmd.Parameters.AddWithValue("@price", numericUpDown2.Text);
            connection.Open();
            if (cmd.ExecuteNonQuery() > 0)
            { }
              
            else
            {
                MessageBox.Show("Failed");
            }
            connection.Close();
            stock();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int pr_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string dlt = "DELETE FROM products WHERE pr_id = @pr_id";
            MySqlCommand cmd = new MySqlCommand(dlt, connection);
            cmd.Parameters.AddWithValue("@pr_id", pr_id);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            stock();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox2.Text) ||
                string.IsNullOrEmpty(comboBox1.Text) ||
                string.IsNullOrEmpty(numericUpDown1.Text)||
                string.IsNullOrEmpty(numericUpDown2.Text))
            {
                MessageBox.Show("Make sure all fields are filled out!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int pr_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string sql = "update products set pr_name=@pr_name, type=@type, unit=@unit, price=@price " + "where pr_id=@pr_id";
            cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@pr_id", pr_id);
            cmd.Parameters.AddWithValue("@pr_name", comboBox2.Text);
            cmd.Parameters.AddWithValue("@type", comboBox1.Text);
            cmd.Parameters.AddWithValue("@unit", numericUpDown1.Text);
            cmd.Parameters.AddWithValue("@price", numericUpDown2.Text);
            connection.Open();
            if (cmd.ExecuteNonQuery() > 0)
                { }

            else
            {
                MessageBox.Show("Failed");
            }
            connection.Close();
            stock();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "drink")
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                comboBox2.Items.Add("cola");
                comboBox2.Items.Add("fanta");
                comboBox2.Items.Add("sprite");
                comboBox2.Items.Add("ayran");
                comboBox2.Items.Add("soda");
                comboBox2.Items.Add("water");
            }
            else if (comboBox1.Text == "menu")
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                comboBox2.Items.Add("margherita");
                comboBox2.Items.Add("pepperoni");
                comboBox2.Items.Add("sausage");
                comboBox2.Items.Add("olive");
                comboBox2.Items.Add("chicken");
                comboBox2.Items.Add("salad");
            }
            else if (comboBox1.Text == "extra")
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                comboBox2.Items.Add("ketchup");
                comboBox2.Items.Add("mayonnaise");
                comboBox2.Items.Add("mustard");
                comboBox2.Items.Add("barbecue");
                comboBox2.Items.Add("garlic");
                comboBox2.Items.Add("hot sauce");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Program.connect(label13);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stock();
        }
    }
}

