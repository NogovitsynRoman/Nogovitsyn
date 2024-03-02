using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        DataBase DataBs = new DataBase();
        DataSet dtset = new DataSet();

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> usrdt = new List<string>();
                DataBs.GetConnection();
                DataBs.GetConnection().Open();
                DataBs.command1 = new MySqlCommand($"SELECT * FROM accounts WHERE Login = '{textBox1.Text}'", DataBs.GetConnection());
                var adap = new MySqlDataAdapter { SelectCommand = DataBs.command1 };
                adap.Fill(dtset);
                foreach (object cell in dtset.Tables[0].Rows[0].ItemArray)
                {
                    usrdt.Add(cell.ToString());
                }
                if (usrdt[1] == textBox1.Text & usrdt[2] == textBox2.Text)
                {
                    using (StreamWriter writer = new StreamWriter("C:\\Users\\romka228huligan41k\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\access_level.txt"))
                    {
                        writer.WriteLine($"{usrdt[1]} {usrdt[3]}");
                    }
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();
                }
                DataBs.GetConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Такого пользователя нет в системе!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /*string Login = Convert.ToString(textBox1.Text);
            string Password = Convert.ToString(textBox2.Text);
            string Access_level;

            DB.GetConnection().Open();
            MySqlCommand command = DB.GetConnection().CreateCommand();
            command.CommandText = "SELECT Login, Password, access_level FROM accounts WHERE Login=@Login AND Password=@Password";
            
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            //command.Parameters.AddWithValue("@access_level", Access_level);
            MySqlDataReader adp = command.ExecuteReader();

            //MySqlCommand command1 = new MySqlCommand($"SELECT Login, Password, access_level)

            //запись уровней доступа в текстовый файл в корневой папке проекта
            using (StreamWriter writer = new StreamWriter("C:\\Users\\romka228huligan41k\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\access_level.txt"))
            {
                writer.WriteLine($"");
            }    
            string textToWrite = "SELECT access_level";

            //File.WriteAllText(filePath, textToWrite );

            //Console.WriteLine()


            if (adp.Read())
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Такого пользователя нет в системе!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            adp.Close();
            DB.GetConnection().Close();
            */
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {

        }

       
    }
}
