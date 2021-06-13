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
    public partial class Formaddmovie : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string user_id = "";
        string type = "";

        public Formaddmovie(string id, string typ)
        {
            InitializeComponent();
            user_id = id;
            type = typ;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Formaddmovie_Load(object sender, EventArgs e)
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
                comboBox2.Items.Add(dr[0]);
            }
            dr.Close();

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select category_name from Categories";
            cmd1.CommandType = CommandType.Text;

            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox1.Items.Add(dr1[0]);
            }
            dr1.Close();



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Formaddmovie_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxID, newID;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "GetMovieID";
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
            cmd.CommandText = "AddNewMovie";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("MID", newID);
            cmd.Parameters.Add("MName", textBox1.Text);

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select director_id from Directors where Dfirst_name = :direct_name2";
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.Add("direct_name2", comboBox2.SelectedItem.ToString());
            OracleDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                cmd.Parameters.Add("DirID", dr["director_id"].ToString());

            }

            cmd.Parameters.Add("DateRelease",dateTimePicker1.Value.Date);

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select category_id from Categories where category_name = :categ_name2";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("categ_name2", comboBox1.SelectedItem.ToString());
            OracleDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {

                cmd.Parameters.Add("CatID", dr2["category_id"].ToString());
            }
            cmd.Parameters.Add("InitialRating", textBox7.Text);
            cmd.Parameters.Add("MDuration", textBox2.Text);
            cmd.Parameters.Add("MTrailerLink", textBox6.Text);
            cmd.ExecuteNonQuery();

            OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "AddMovieLocation";
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("MLID", newID);
            cmd3.Parameters.Add("MLocation", textBox4.Text);
            cmd3.ExecuteNonQuery();
            MessageBox.Show("Movie Added Successfully");

            textBox7.Text = "";
            textBox1.Text = "";
            textBox6.Text = "";
            textBox4.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now.Date;



        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2HomeAdmin fd = new Form2HomeAdmin(user_id,type);
            fd.Show();
            this.Hide();
        }
    }
}
