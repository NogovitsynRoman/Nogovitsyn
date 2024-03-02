using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WindowsFormsApp1;
using System.Net.Http.Headers;
using System.Data.Common;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public int Bytes_received_From;
        public int Bytes_received_UpTo;
        public int Bytes_sent_From;
        public int Bytes_sent_UpTo;

        public int TimeInterval_From;
        public int TimeInterval_UpTo;
        public int TimeInterval_Real;

        public string Interface_name;

        public static int access_level;

        DataBase DB = new DataBase();
        DataTable datatable;
        MySqlDataAdapter dataadapter;

        MySqlCommand command1;

        public Form1()
        {
            InitializeComponent();
            DB.GetConnection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //уровни доступа
            string access_level =  File.ReadAllText("C:\\Users\\romka228huligan41k\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\access_level.txt").Split()[1];
            if (access_level == "1")
            {
                button1.Visible = true;
                button3.Visible = false;
                button4.Visible = true;
                button5.Visible = false; 
            }
            if (access_level == "2")
            {
                button1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
            }
            
            try
            {
                DB.GetConnection().Open();
                MySqlCommand command = DB.GetConnection().CreateCommand();
                command.CommandText = "SELECT Interface_name, Bytes_received, Bytes_sent, TimeInterval " +
                    "FROM interfaceinfo INNER JOIN timeintervaltable ON interfaceinfo.TimeInterval_code=timeintervaltable.TimeInterval_code";
                MySqlDataAdapter adp = new MySqlDataAdapter(command);
                command.ExecuteNonQuery();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView2.DataSource = dt;
                adp.Dispose();
                DB.GetConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "form1_load!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                DB.GetConnection().Close();
                DB.GetConnection().Open();
                command1 = new MySqlCommand($"SELECT * FROM {comboBox3.Text}", DB.GetConnection());
                command1.ExecuteNonQuery();
                datatable = new DataTable();
                dataadapter = new MySqlDataAdapter(command1);
                dataadapter.Fill(datatable);
                dataGridView2.DataSource = datatable.DefaultView;
                DB.GetConnection().Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "LoadData!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Interface_name = Convert.ToString(comboBox3.Text);

            TimeInterval_From = Convert.ToInt32(numericUpDown7.Value);
            TimeInterval_UpTo = Convert.ToInt32(numericUpDown8.Value);

            Bytes_received_From = Convert.ToInt32(numericUpDown9.Value);
            Bytes_received_UpTo = Convert.ToInt32(numericUpDown10.Value);

            Bytes_sent_From = Convert.ToInt32(numericUpDown11.Value);
            Bytes_sent_UpTo = Convert.ToInt32(numericUpDown12.Value);


            if ((TimeInterval_UpTo < TimeInterval_From) || (Bytes_received_UpTo < Bytes_received_From) || (Bytes_sent_UpTo < Bytes_sent_From)) throw new Exception("Ошибка введёных данных!\n\n" +
            "Для корректного вывода необходимо выполнить запрос от меньшего числа к большему числу.");
            DB.GetConnection().Close();
            DB.GetConnection().Open();
            MySqlCommand command = DB.GetConnection().CreateCommand();
            command.CommandText = "SELECT Interface_name, Bytes_received, Bytes_sent, TimeInterval " +
                "FROM interfaceinfo INNER JOIN timeintervaltable ON interfaceinfo.TimeInterval_code=timeintervaltable.TimeInterval_code " +
                "WHERE timeintervaltable.TimeInterval BETWEEN @TimeInterval_From AND @TimeInterval_UpTo AND " +
                "Interface_name LIKE @Interface_name AND " +
                "Bytes_received BETWEEN @Bytes_received_From AND @Bytes_received_UpTo AND " +
                "Bytes_sent BETWEEN @Bytes_sent_From AND @Bytes_sent_UpTo";
            MySqlDataAdapter adp = new MySqlDataAdapter(command);
            command.Parameters.AddWithValue("@Interface_name", "%" + Interface_name + "%");
            command.Parameters.AddWithValue("@Bytes_received_UpTo", Bytes_received_UpTo);
            command.Parameters.AddWithValue("@Bytes_received_From", Bytes_received_From);
            command.Parameters.AddWithValue("@Bytes_sent_UpTo", Bytes_sent_UpTo);
            command.Parameters.AddWithValue("@Bytes_sent_From", Bytes_sent_From);
            command.Parameters.AddWithValue("@TimeInterval_UpTo", TimeInterval_UpTo);
            command.Parameters.AddWithValue("@TimeInterval_From", TimeInterval_From);
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView2.DataSource = dt;
            adp.Dispose();
            DB.GetConnection().Close();
            dataGridView2.AutoResizeColumns();



            dataGridView2.AutoResizeColumns();

            /*for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                string str_view = dataGridView2.Columns[i].HeaderText;
                switch (str_view)
                {
                    case "Interface_name": str_view = "Название интерфейса"; break;
                    case "Bytes_received": str_view = "Отправлено Байт"; break;
                    case "Bytes_sent": str_view = "Получено Байт"; break;
                    case "TimeInterval": str_view = "Временной интервал"; break;
                }
                dataGridView2.Columns[i].HeaderText = str_view;
            }*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DB.GetConnection().Open();
                MySqlCommand command = DB.GetConnection().CreateCommand();
                command.CommandText = "INSERT INTO interfaceinfo() VALUES()";
                MySqlDataAdapter adp = new MySqlDataAdapter(command);
                command.ExecuteNonQuery();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView2.DataSource = dt;
                adp.Dispose();
                command1 = new MySqlCommand($"SELECT * FROM interfaceinfo", DB.GetConnection());
                datatable = new DataTable();
                dataadapter = new MySqlDataAdapter(command1);
                command.ExecuteNonQuery();
                dataadapter.Fill(datatable);
                dataGridView2.DataSource = datatable;
                DB.GetConnection().Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "button3_click!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DB.GetConnection().Open();
                MySqlCommand command = DB.GetConnection().CreateCommand();
                command.CommandText = "SELECT * FROM interfaceinfo";
                MySqlDataAdapter adp = new MySqlDataAdapter(command);
                command.ExecuteNonQuery();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView2.DataSource = dt;
                adp.Dispose();
                DB.GetConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DB.openConnection();
                string quote = "";
                if (dataGridView2.CurrentCell.ValueType.ToString() != "System.Int32")
                {
                    quote = "'";
                }
                DB.command1 = new MySqlCommand($"UPDATE interfaceinfo SET {dataGridView2.Columns[dataGridView2.CurrentCell.ColumnIndex].Name} = {quote}{dataGridView2.CurrentCell.Value}{quote} WHERE Interface_code = {Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString())}", DB.GetConnection());
                //MySqlDataAdapter adp = new MySqlDataAdapter(command1);
                command1.ExecuteNonQuery();
                //DataTable dt = new DataTable();
                //adp.Fill(dt);
                //dataGridView2.DataSource = dt;
                //adp.Dispose();
                //DB.GetConnection().Close();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "cell_value_changed - неверный тип данных!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dataGridView2.CurrentRow.Index;
                DB.GetConnection().Open();
                DB.command1 = new MySqlCommand($"DELETE FROM interfaceinfo WHERE interface_code = {Int32.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString())}", DB.GetConnection());
                DB.command1.ExecuteNonQuery();
                DB.GetConnection().Close();

                dataGridView2.Rows.RemoveAt(rowIndex); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "button5_click!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            //уровни доступа
            string access_level = File.ReadAllText("C:\\Users\\romka228huligan41k\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\access_level.txt").Split()[1];
            if (access_level == "1")
            {
                button1.Visible = true;
                button3.Visible = false;
                button4.Visible = true;
                button5.Visible = false;
            }
            if (access_level == "2")
            {
                button1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
            }

            try
            {
                DB.GetConnection().Open();
                MySqlCommand command = DB.GetConnection().CreateCommand();
                command.CommandText = "SELECT Interface_name, Bytes_received, Bytes_sent, TimeInterval " +
                    "FROM interfaceinfo INNER JOIN timeintervaltable ON interfaceinfo.TimeInterval_code=timeintervaltable.TimeInterval_code";
                MySqlDataAdapter adp = new MySqlDataAdapter(command);
                command.ExecuteNonQuery();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView2.DataSource = dt;
                adp.Dispose();
                DB.GetConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "form1_load!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}