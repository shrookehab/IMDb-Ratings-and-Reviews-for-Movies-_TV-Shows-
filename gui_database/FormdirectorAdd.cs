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
    public partial class FormdirectorAdd : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string user_id = "";
        string type = "";

        public FormdirectorAdd(string id, string typ)
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
           textBox2.MaxLength = 10;

            user_id = id;
            type = typ;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2HomeAdmin fh = new Form2HomeAdmin(user_id, type);
            fh.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxID, newID;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "GetDirectorID";
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
            cmd.CommandText = "insert into Directors values (:directorid,:firstname,:SName,:LName,:Email,:Password,:gender,:Country,:Birthdate)";
            cmd.Parameters.Add("directorid", newID);
            cmd.Parameters.Add("firstname", textBox8.Text);
            cmd.Parameters.Add("SName", textBox1.Text);
            cmd.Parameters.Add("LName", textBox9.Text);
            cmd.Parameters.Add("Email", textBox3.Text);
            cmd.Parameters.Add("Password", textBox2.Text);

            if (radioButton1.Checked)
                cmd.Parameters.Add("gender", radioButton1.Text);
            else
                cmd.Parameters.Add("gender", radioButton2.Text);

            cmd.Parameters.Add("Country", textBox5.Text);

            cmd.Parameters.Add("Birthdate", dateTimePicker1.Value.Date);
            int r = cmd.ExecuteNonQuery();

            if (r != -1)
            {
                MessageBox.Show("New Director is added");
            }
            textBox8.Text = "";
            textBox1.Text = "";
            textBox9.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Now.Date;
        }

        private void FormdirectorAdd_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void FormdirectorAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }
    }
}
