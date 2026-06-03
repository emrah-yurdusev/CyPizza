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
using DevExpress.Utils.Gesture;
using MySql.Data.MySqlClient;

namespace emrah
{
    public partial class Form2 : Form
    {
        private Form1 _form1;
        private MySqlConnection connection;
        string connectionString = "Server=localhost;port=3308;Database=cypizza;Uid=emrah;Pwd=;";
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;

        private int i = 0;

        void list()
        {
            try
            {
                dt = new DataTable();
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT * FROM orders where situation = 'waiting'", connection);
                dataGridView1.DataSource = dt;
                adapter.Fill(dt);
                connection.Close();
            }
            catch
            {

            }
        }

        void chef()
        {
            try
            {
                dt = new DataTable();
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT o_id, pr_name FROM orders where situation = 'getting ready'", connection);
                dataGridView2.DataSource = dt;
                adapter.Fill(dt);
                connection.Close();
            }
            catch
            {

            }
        }

        void finished()
        {
            try
            {
                dt = new DataTable();
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT o_id, pr_name FROM orders WHERE situation = 'finished' " +
                                               "ORDER BY o_id DESC LIMIT 10;", connection);
                dataGridView3.DataSource = dt;
                adapter.Fill(dt);
                connection.Close();
            }
            catch
            {

            }
        }

        public Form2(Form1 form1)
        {
            InitializeComponent();
            textBox2.TextChanged += TextBox2_TextChanged;

            textBox1.TextChanged += TextBox1_TextChanged;
            _form1 = form1;
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
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            int usd = 35;
            int euro = 36;

            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    int a = Convert.ToInt32(textBox1.Text);


                    if (comboBox5.SelectedItem != null)
                    {
                        var exc = comboBox5.SelectedItem.ToString();

                        if (exc == "USD")
                        {
                            int result = usd * a;
                            label14.Text = result.ToString();
                        }
                        else if (exc == "EURO")
                        {
                            int result = euro * a;
                            label14.Text = result.ToString();
                        }
                        else
                        {
                            label14.Text = "Invalid selection.";
                        }
                    }
                    else
                    {
                        label14.Text = "Please make a selection.";
                    }
                }
                catch (FormatException)
                {
                    label14.Text = "Please enter a valid number.";
                }
            }
            else
            {
                label14.Text = "Please enter a value.";
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {

                e.Handled = true;
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {

                decimal a = Convert.ToInt32(textBox2.Text);
                decimal b = a - Program.totalprice;
                textBox3.Text = b.ToString();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            list();
            finished();
            chef();
            try
            {
                
                string sqll = "SELECT MAX(o_id) FROM orders;";
                MySqlCommand command = new MySqlCommand(sqll, connection);
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();
                if (result != DBNull.Value && result != null)
                {
                    i = 1 + Convert.ToInt32(result);
                }
                else
                {
                    i = 1; 
                }
                
            }
            catch
            {
               
            }
            
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Application.Exit();
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            
            this.Hide();
            if (Program.currentRole == "admin") { }


            else
                _form1.Show();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            var comboBoxNumericMap = new Dictionary<ComboBox, NumericUpDown>
            {
                { comboBox3, numericUpDown1 },
                { comboBox2, numericUpDown2 },
                { comboBox4, numericUpDown3 }
            };

            List<string> insufficientStockItems = new List<string>();

            try
            {
                connection.Open();

                foreach (var pair in comboBoxNumericMap)
                {
                    ComboBox comboBox = pair.Key;
                    NumericUpDown numericUpDown = pair.Value;

                    if (!string.IsNullOrEmpty(comboBox.Text))
                    {
                        string stockQuery = "SELECT unit FROM products WHERE pr_name = @name";
                        MySqlCommand stockCmd = new MySqlCommand(stockQuery, connection);
                        stockCmd.Parameters.AddWithValue("@name", comboBox.Text);
                        int currentStock = Convert.ToInt32(stockCmd.ExecuteScalar());

                        decimal requiredAmount = numericUpDown.Value;

                        
                        if (currentStock < requiredAmount)
                        {
                            insufficientStockItems.Add($"{comboBox.Text} - Kalan Stok: {currentStock}");
                        }
                        else
                        {
                            
                            string updateUnitQuery = "UPDATE products SET unit = unit - @unit WHERE pr_name = @name AND unit >= @unit;";
                            MySqlCommand updateUnitCmd = new MySqlCommand(updateUnitQuery, connection);
                            updateUnitCmd.Parameters.AddWithValue("@unit", numericUpDown.Value);
                            updateUnitCmd.Parameters.AddWithValue("@name", comboBox.Text);

                            int rowsAffected = updateUnitCmd.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                insufficientStockItems.Add($"{comboBox.Text} - Yetersiz stok");
                            }
                        }
                    }
                }

                
                if (insufficientStockItems.Count == 0)
                {
                    string updateOrderQuery = "UPDATE orders SET situation = 'getting ready' WHERE o_id = @i";
                    MySqlCommand updateOrderCmd = new MySqlCommand(updateOrderQuery, connection);
                    updateOrderCmd.Parameters.AddWithValue("@i", i);

                    if (updateOrderCmd.ExecuteNonQuery() > 0)
                    {
                       
                    }
                    else
                    {
                        MessageBox.Show("The order status could not be updated.");
                    }
                }
                else
                {
                    
                    MessageBox.Show($"Products with insufficient units: {string.Join(", ", insufficientStockItems)}");
                }

                if (insufficientStockItems.Count == 0) i++;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            list();
            chef();
            finished();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            Program.connect(label13);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var comboBoxNumericMap = new Dictionary<ComboBox, NumericUpDown>
    {
                    { comboBox3, numericUpDown1 },
                    { comboBox2, numericUpDown2 },
                    { comboBox4, numericUpDown3 }
                 };
            if (!(string.IsNullOrEmpty(comboBox4.Text) && string.IsNullOrEmpty(comboBox2.Text) && string.IsNullOrEmpty(comboBox3.Text)))
            {
                
                
                    

                    foreach (var pair in comboBoxNumericMap)
                    {
                        ComboBox comboBox = pair.Key;
                        NumericUpDown numericUpDown = pair.Value;


                        if (!string.IsNullOrEmpty(comboBox.Text))
                        {

                            string query = "SELECT price FROM products WHERE pr_name = @name";
                            MySqlCommand cmd = new MySqlCommand(query, connection);
                            
                                connection.Open();
                                cmd.Parameters.AddWithValue("@name", comboBox.Text);
                                object result = cmd.ExecuteScalar();
                                connection.Close();
                                int price = Convert.ToInt32(result);

                                decimal unit = numericUpDown.Value;
                                Program.totalprice += price * unit;
                        



                            
                        }
                    }
                


                label15.Text = Program.totalprice.ToString();
                
            }
            


            foreach (var pair in comboBoxNumericMap)
            {
                ComboBox comboBox = pair.Key;
                NumericUpDown numericUpDown = pair.Value;


                if (!string.IsNullOrEmpty(comboBox.Text))
                {

                    string query = "insert into orders values(@id, @name, @type, @unit, @situation );";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    {
                        string Type = "";
                        if (comboBox.Name == "comboBox3")
                        {
                            Type = "menü";
                        }
                        else if (comboBox.Name == "comboBox2")
                        {
                            Type = "extra";
                        }
                        else if (comboBox.Name == "comboBox4")
                        {
                            Type = "drink";
                        }
                        cmd.Parameters.AddWithValue("@id", i);
                        cmd.Parameters.AddWithValue("@name", comboBox.Text);
                        cmd.Parameters.AddWithValue("@type", Type);
                        cmd.Parameters.AddWithValue("@unit", numericUpDown.Value);
                        cmd.Parameters.AddWithValue("@situation", "waiting");
                       connection.Open();
                        if (cmd.ExecuteNonQuery() > 0) { 

                            

                        }
                            

                        else
                        {
                            MessageBox.Show("Failed");
                        }
                        
                        connection.Close();
                        


                    }

                }

            }

            list();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chef();
            finished();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
           { 
                string dlt = "DELETE FROM orders WHERE o_id = @o_id";
                MySqlCommand cmd = new MySqlCommand(dlt, connection);
                if (dataGridView1.CurrentRow == null)
                {
                    cmd.Parameters.AddWithValue("@o_id", i);
                }
                else
                {
                    int o_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@o_id", o_id);
                }
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Failed");
            }
            list();
        }
    }
}
