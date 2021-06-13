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
    public partial class Form3allmovies : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;
        string user_id = "";
        string type = "";
        public Form3allmovies(string id, string typ)
        {
            InitializeComponent();
            textBox1.Text = "Movie Name";
            user_id = id;
            type = typ;
        }

        private void Form3allmovies_Load(object sender, EventArgs e)
        {
            if (type == "user")
                panel2.Hide();

            string command = "select * from Movies ";

            adapter = new OracleDataAdapter(command, ordb);
            ds = new DataSet();
            adapter.Fill(ds);
            dgmovies.DataSource = ds.Tables[0];

            conn = new OracleConnection(ordb);
            conn.Open();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if( type == "user")
            {
                Form3home fH = new Form3home(user_id, type);
                fH.Show();
                this.Hide();

            }
            else
            {
                Form2HomeAdmin fH = new Form2HomeAdmin(user_id, type);
                fH.Show();
                this.Hide();

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2AddFavourite ad = new Form2AddFavourite(user_id, type);
            ad.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3allmovies_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Formaddmovie fd = new Formaddmovie(user_id,type);
            fd.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Formupdatemovie fu = new Formupdatemovie(user_id,type);
            fu.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string command = "select * from Movies where movie_name = :name";

            adapter = new OracleDataAdapter(command, ordb);
            adapter.SelectCommand.Parameters.Add("name", textBox1.Text);
            ds = new DataSet();
            adapter.Fill(ds);
            dgmovies.DataSource = ds.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select movie_id from Movies where movie_name = :movie_name2";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("movie_name2", textBox1.Text);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Movie msh = new Movie(textBox1.Text, dr["movie_id"].ToString(), user_id, type);
                msh.Show();
                this.Hide();

            }
            dr.Close(); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgmovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";

        }
    }
}
