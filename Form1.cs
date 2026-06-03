using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using emrah;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace CyPizza


{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        string connectionString = "Server=localhost;port=3308;Database=cypizza;Uid=emrah;Pwd=;";
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;

        void register()
        {
            try
            {
                dt = new DataTable();
                connection.Open();
                adapter = new MySqlDataAdapter("SELECT * FROM employers", connection);
                dataGridView1.DataSource = dt;
                adapter.Fill(dt);
                connection.Close();
            }
            catch
            {
                
            }

        }
        public static Form2 form2;

        private int count = 0;

        public Form1()
        {


            InitializeComponent();
            form2 = new Form2(this);

           

            connection = new MySqlConnection(connectionString);
            
            try
            {

                connection.Open();
                label2.Text = "CONNECTED";
                label2.ForeColor = System.Drawing.Color.Green;
                connection.Close();
            }
            catch
            {
                label2.Text = "CONNECTED FAİLED";
                label2.ForeColor = System.Drawing.Color.Red;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            register();

        }
        
        public void whatIsRole(string role)
        {
            Program.currentRole = role;
            switch (role.ToLowerInvariant().Trim())
            {
                case "admin":
                    groupBox1.Visible = true;
                    groupBox2.Visible = false;

                    break;

                case "cashier":
                    Form2 form2 = new Form2(this);
                    form2.Show();
                    break;

                case "chef":
                    Form3 form3 = new Form3(this);
                    form3.Show();
                    break;

                case "stock":
                    Form4 form4 = new Form4();
                    form4.Show();
                    break;

                default:
                    MessageBox.Show("Undefined role! ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

            }

            textBox2.Clear();

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password cannot be empty!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connection.Open();

                string query = "SELECT en.password, e.job, e.e_id " +
                        "FROM employers e " +
                        "INNER JOIN entry en ON e.e_id = en.e_id " +
                        "WHERE e.e_name = @username and e.e_id = en.e_id";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string stredpwd = reader["password"].ToString().Trim();
                            string role = reader["job"].ToString().Trim();
                            int e_id = Convert.ToInt32(reader["e_id"]);

                            if (string.IsNullOrEmpty(stredpwd))
                            {
                                reader.Close();

                                string updatequery = "UPDATE entry SET password = @password WHERE e_id = @e_id";
                                using (MySqlCommand updatecmd = new MySqlCommand(updatequery, connection))
                                {
                                    updatecmd.Parameters.AddWithValue("@password", password);
                                    updatecmd.Parameters.AddWithValue("@e_id", e_id);
                                    updatecmd.ExecuteNonQuery();
                                }
                                MessageBox.Show("Password has been created. Please log in again.");
                            }
                            else if (stredpwd == password)
                            {
                                MessageBox.Show("welcome: " + role);
                                whatIsRole(role);

                                if (role != "admin")
                                {
                                    this.Hide();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect password!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect username or password!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }


        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            for (int i = Application.OpenForms.Count - 1; i >= 0 ; i--)
            {
                Form form = Application.OpenForms[i];
                if ( form != this)
                {
                    form.Close();

                }
            }
                




        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) ||
               string.IsNullOrEmpty(textBox4.Text) ||
               string.IsNullOrEmpty(comboBox1.Text) ||
               string.IsNullOrEmpty(textBox5.Text) ||
               string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Make sure all fields are filled out!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "INSERT INTO employers(e_name, e_surname, job, e_mail, p_number) VALUES(@e_name, @e_surname, @job, @e_mail, @p_number)";
            cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@e_name", textBox3.Text);
            cmd.Parameters.AddWithValue("@e_surname", textBox4.Text);
            cmd.Parameters.AddWithValue("@job", comboBox1.Text);
            cmd.Parameters.AddWithValue("@e_mail", textBox5.Text);
            cmd.Parameters.AddWithValue("@p_number", textBox6.Text);
            connection.Open();
            if (cmd.ExecuteNonQuery() > 0)
            {
                long last_id = cmd.LastInsertedId;
                string sqlq = "INSERT INTO entry(e_id, password) VALUES(@e_id ,@password)";
                MySqlCommand cmdentry = new MySqlCommand(sqlq, connection);
                cmdentry.Parameters.AddWithValue("@e_id", last_id);
                cmdentry.Parameters.AddWithValue("@password", "");

                if (cmdentry.ExecuteNonQuery() > 0)
                {
                    
                }
                else
                {
                    MessageBox.Show("Failed to insert into entry table.");
                }
                
            }
            else
            {
                MessageBox.Show("registration failed");
            }
            connection.Close();
            register();
            count = 0;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
            
        }


        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            

                try
                {
                    textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                }
                catch
                {

                    throw;
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int e_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            string dltEntry = "DELETE FROM entry WHERE e_id = @e_id";
            MySqlCommand dltEntrycmd = new MySqlCommand(dltEntry, connection);
            dltEntrycmd.Parameters.AddWithValue("@e_id", e_id);

            string dltemply = "DELETE FROM employers WHERE e_id = @e_id";
            MySqlCommand dltemplycmd = new MySqlCommand(dltemply, connection);
            dltemplycmd.Parameters.AddWithValue("@e_id", e_id);

            connection.Open();
            dltEntrycmd.ExecuteNonQuery();
            dltemplycmd.ExecuteNonQuery();
            connection.Close();
            register();
            count = 0;

            
        }

       

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) ||
               string.IsNullOrEmpty(textBox4.Text) ||
               string.IsNullOrEmpty(comboBox1.Text) ||
               string.IsNullOrEmpty(textBox5.Text) ||
               string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Make sure all fields are filled out!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int e_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string sql = "update employers set e_name=@e_name, e_surname=@e_surname, job=@job, e_mail=@e_mail, p_number=@p_number " + "where e_id = @e_id";
            cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@e_id", e_id);
            cmd.Parameters.AddWithValue("@e_name", textBox3.Text);
            cmd.Parameters.AddWithValue("@e_surname", textBox4.Text);
            cmd.Parameters.AddWithValue("@job", comboBox1.Text);
            cmd.Parameters.AddWithValue("@e_mail", textBox5.Text);
            cmd.Parameters.AddWithValue("@p_number", textBox6.Text);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            register();
            count = 0;
            MessageBox.Show("Records updated successfully.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Program.connect(label2);
            register();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
           

            
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.Text = "";
            
                
        }
    } 
}

