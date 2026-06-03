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
using MySql.Data.MySqlClient;

namespace emrah
{
    public partial class Form3 : Form
    {
        private Form1 _form1;
        private MySqlConnection connection;
        string connectionString = "Server=localhost;port=3308;Database=cypizza;Uid=emrah;Pwd=;";
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;

        void chef()
        {
            try
            {
                dt = new DataTable();
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT * FROM orders where situation = 'getting ready'", connection);
                dataGridView1.DataSource = dt;
                adapter.Fill(dt);
                connection.Close();
            }
            catch
            {

            }
        }
        public Form3(Form1 form1)
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
            _form1 = form1;
        }

        

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            chef();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (Program.currentRole == "admin") { }


            else
                _form1.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Program.connect(label13);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a row.");
                return;
            }
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            
            string situation = "finished";
            string query = "UPDATE orders set situation= @situation " +"where o_id= @id;";
            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@situation", situation);
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            if (cmd.ExecuteNonQuery() > 0) { }


            else
            {
                MessageBox.Show("Failed");
            }
            connection.Close();
            chef();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chef();
        }
    }
}
