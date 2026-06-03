using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CyPizza
{
    internal static class Program
    {

        public static void connect(Label label)
        {
            string connectionString = "Server=localhost;port=3308;Database=cypizza;Uid=emrah;Pwd=;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    label.Text= "CONNECTED";
                    label.ForeColor = System.Drawing.Color.Green;
                    

                }
                catch
                {
                    label.Text = "CONNECTED FAİLED";
                    label.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        public static decimal totalprice;

        public static string currentRole = "";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
