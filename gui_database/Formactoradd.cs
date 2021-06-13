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
    public partial class Formactoradd : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;


        string user_id = "";
        string type = "";

        public Formactoradd(string id, string typ)
        {
            InitializeComponent();
            password_text.PasswordChar = '*';
            password_text.MaxLength = 10;

            user_id = id;
            type = typ;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2HomeAdmin fh = new Form2HomeAdmin(user_id,type);
            fh.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxID, newID;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "GetActorID";
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
            cmd.CommandText = "insert into Actors values(:actid,:fname,:sname,:lname,:email,:password,:gender,:country,:birthdate)";
            cmd.Parameters.Add("actid", newID);
            cmd.Parameters.Add("fname", textBox8.Text);
            cmd.Parameters.Add("sname", textBox1.Text);
            cmd.Parameters.Add("lname", textBox9.Text);
            cmd.Parameters.Add("email", textBox3.Text);
            cmd.Parameters.Add("password", password_text.Text);
            if (radioButton1.Checked == true)
            {
                cmd.Parameters.Add("gender", "Male");

            }
            else
            {
                cmd.Parameters.Add("gender", "Female");

            }

            cmd.Parameters.Add("country", textBox5.Text);
            cmd.Parameters.Add("birthdate", dateTimePicker1.Value.Date);

            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("New Actor Successfuly Added");
            }
            textBox8.Text = "";
            textBox1.Text = "";
            textBox9.Text = "";
            textBox3.Text = "";
            password_text.Text = "";
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Now.Date;
        }

        private void Formactoradd_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();


        }

        private void Formactoradd_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();

        }
    }
}
