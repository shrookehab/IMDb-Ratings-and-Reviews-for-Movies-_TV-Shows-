using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace gui_database
{
    public partial class Form2signup : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string user_id = "";
        string type = "";
        public Form2signup(string id, string typ)
        {
            InitializeComponent();
            textBox2.PasswordChar= '*';
            textBox2.MaxLength = 10;
            //textBox7.Text = "yyyy/MM/dd 00:00:00";
            user_id = id;
            type = typ;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void password_text2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2signup_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int maxID, newID;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "GetUserID";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            c.ExecuteNonQuery();
            try
            {
                maxID = Convert.ToInt32(c.Parameters["id"].Value.ToString());
                newID = maxID + 1;
            }
            catch
            {
                newID = 1;
            }
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Users values(:userid,:fname,:sname,:lname,:email,:password,:membertype,:gender,:country,:birthdate)";
            cmd.Parameters.Add("userid", newID);
            cmd.Parameters.Add("fname", textBox5.Text);
            cmd.Parameters.Add("sname", textBox6.Text);
            cmd.Parameters.Add("lname", textBox3.Text);
            cmd.Parameters.Add("email", textBox1.Text);
            cmd.Parameters.Add("password", textBox2.Text);
            cmd.Parameters.Add("membertype", "user");
            if (radioButton1.Checked == true)
            {
                cmd.Parameters.Add("gender", "Male");

            }
            else
            {
                cmd.Parameters.Add("gender", "Female");

            }

            cmd.Parameters.Add("country", textBox4.Text);
            cmd.Parameters.Add("birthdate", dateTimePicker1.Value.Date);

            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("New User Successfuly Added");
            }
            Form2login fl = new Form2login();
            fl.Show();
            this.Hide();
        }

        private void Form2signup_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
