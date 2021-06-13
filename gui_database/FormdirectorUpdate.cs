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
    public partial class FormdirectorUpdate : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string user_id = "";
        string type = "";

        public FormdirectorUpdate(string id, string typ)
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
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "update Directors set Dfirst_name=:firstname, Dsec_name=:SName, Dlast_name=:LName, De_mail=:Email, director_password=:Password, Dgender=:gender, Dcountry=:Country, Dbirthdate=:Birthdate where Dfirst_name=:name";
            cmd.Parameters.Add("firstname", textBox8.Text);
            cmd.Parameters.Add("SName", textBox1.Text);
            cmd.Parameters.Add("LName", textBox9.Text);
            cmd.Parameters.Add("Email", textBox3.Text);
            cmd.Parameters.Add("Password", textBox2.Text);

            if (radioButton1.Checked == true)
                cmd.Parameters.Add("gender", "Male");
            else
                cmd.Parameters.Add("gender", "Female");

            cmd.Parameters.Add("Country", textBox5.Text);
            cmd.Parameters.Add("Birthdate", dateTimePicker1.Value.Date);
            cmd.Parameters.Add("name", comboBox1.SelectedItem.ToString());
            int r = cmd.ExecuteNonQuery();

            if (r != -1)
            {
                MessageBox.Show("Director is updated successfully");
            }
            textBox8.Text = "";
            textBox1.Text = "";
            textBox9.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now.Date;

        }

        private void FormdirectorUpdate_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select Dfirst_name from Directors";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select Dfirst_name,Dsec_name,Dlast_name,De_mail,director_password,Dgender,Dcountry,Dbirthdate from Directors where Dfirst_name=:name";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("name", comboBox1.SelectedItem.ToString());
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                textBox8.Text = dr[0].ToString();
                textBox1.Text = dr[1].ToString();
                textBox9.Text = dr[2].ToString();
                textBox3.Text = dr[3].ToString();
                textBox2.Text = dr[4].ToString();
                if (dr[5].ToString() == "male" || dr[5].ToString() == "Male")
                {
                    radioButton1.Checked = true;
                }
                else
                    radioButton2.Checked = true;
                textBox5.Text = dr[6].ToString();
                dateTimePicker1.Text = dr[7].ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Delete from Directors where Dfirst_name =:name";
            cmd.Parameters.Add("name", comboBox1.SelectedItem.ToString());
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Director Successfully Deleted");
                comboBox1.Items.RemoveAt(comboBox1. SelectedIndex);
            }
            textBox8.Text = "";
            textBox1.Text = "";
            textBox9.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now.Date;

        }
    }
}
