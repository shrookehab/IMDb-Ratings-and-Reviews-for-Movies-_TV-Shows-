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
    public partial class FormActorupdate : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string user_id = "";
        string type = "";

        public FormActorupdate(string id, string typ)
        {
            InitializeComponent();
            password.PasswordChar = '*';
            password.MaxLength = 10;

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
            cmd.CommandText = "update Actors set Afirst_aname = :fname, Asec_name = :sname, Alast_name = :lname, Ae_mail = :email, actor_password= :pass, Agender = :gender, Acountry = :country, Abirthdate = :abirthdate where Afirst_aname = :nameofactor";
            cmd.Parameters.Add("fname", firstname.Text);
            cmd.Parameters.Add("sname", secondname.Text);
            cmd.Parameters.Add("lname", lastname.Text);
            cmd.Parameters.Add("email", email.Text);
            cmd.Parameters.Add("pass", password.Text);
            if (male.Checked == true)
            {
                cmd.Parameters.Add("gender", "Male");

            }
            else
            {
                cmd.Parameters.Add("gender", "Female");

            }
            cmd.Parameters.Add("country", country.Text);
            cmd.Parameters.Add("abirthdate", dateTimePicker1.Value.Date);
            cmd.Parameters.Add("nameofactor", comboBox1.SelectedItem.ToString());
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Actor Successfully Updated :) ");
            }
            firstname.Text = "";
            secondname.Text = "";
            lastname.Text = "";
            email.Text = "";
            password.Text = "";
            country.Text = "";
            male.Checked = false;
            female.Checked = false;
            comboBox1.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now.Date;


        }

        private void FormActorupdate_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select Afirst_aname from Actors";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void FormActorupdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select Afirst_aname, Asec_name, Alast_name, Ae_mail, actor_password, Agender, Acountry, Abirthdate from Actors where Afirst_aname =:name";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("name", comboBox1.SelectedItem.ToString());
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                firstname.Text = dr[0].ToString();
                secondname.Text = dr[1].ToString();
                lastname.Text = dr[2].ToString();
                email.Text = dr[3].ToString();
                password.Text = dr[4].ToString();
                if (dr[5].ToString() == "male" || dr[5].ToString() == "Male")
                {
                    male.Checked = true;
                }
                else
                    female.Checked = true;
                country.Text = dr[6].ToString();
                dateTimePicker1.Text = dr[7].ToString();

            }
            dr.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Delete from Actors where Afirst_aname =:name";
            cmd.Parameters.Add("name", comboBox1.SelectedItem.ToString());
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Actor Successfully Deleted");
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            }
            firstname.Text = "";
            secondname.Text = "";
            lastname.Text = "";
            email.Text = "";
            password.Text = "";
            country.Text = "";
            male.Checked = false;
            female.Checked = false;
            comboBox1.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now.Date;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

