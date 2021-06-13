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
    public partial class Movie : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;
        string movie_id = "";
        string user_id = "";
        string type = "";
        public Movie(string movie_name,string m_id,string u_id, string typ)
        {
            InitializeComponent();
            titleMovie.Text = movie_name;
            movie_id = m_id;
            user_id = u_id;
            type = typ;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Movie_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3allmovies fam = new Form3allmovies(user_id, type);
            fam.Show();
            this.Hide();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuRating1_onValueChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "InsertRateValue";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("RTMID", int.Parse(movie_id));
            cmd.Parameters.Add("RTUID", int.Parse(user_id));
            cmd.Parameters.Add("ValueOfRate", bunifuRating1.Value);
            cmd.ExecuteNonQuery();
            if (bunifuRating1.Value == 1)
                label1.Text = "1";
            if (bunifuRating1.Value == 2)
                label1.Text = "2";
            if (bunifuRating1.Value == 3)
                label1.Text = "3";
            if (bunifuRating1.Value == 4)
                label1.Text = "4";
            if (bunifuRating1.Value == 5)
                label1.Text = "5";


        }
        private void Movie_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();

        }

        private void bunifuRating1_Load(object sender, EventArgs e)
        {
            //int rate_value = 0;
            //OracleCommand cmd = new OracleCommand();
            //cmd.Connection = conn;

            //cmd.CommandText = "GetRateValue";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("RMID", int.Parse(movie_id));
            //cmd.Parameters.Add("RUID", int.Parse(user_id));
            //cmd.Parameters.Add("RateValue", OracleDbType.Int32, ParameterDirection.Output);
            //cmd.ExecuteNonQuery();
            //try
            //{
            //    label1.Text = (cmd.Parameters["RateValue"].Value.ToString());
            //}
            //catch
            //{
            //    rate_value = 0;
            //}
            ////label1.Text = rate_value.ToString();
            //if (label1.Text == "1")
            //    bunifuRating1.Value = 1;
            //if (label1.Text == "2")
            //    bunifuRating1.Value = 2;
            //if (label1.Text == "3")
            //    bunifuRating1.Value = 3;
            //if (label1.Text == "4")
            //    bunifuRating1.Value = 4;
            //if (label1.Text == "5")
            //    bunifuRating1.Value = 5;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "InsertComment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CMID", int.Parse(movie_id));
            cmd.Parameters.Add("CUID", int.Parse(user_id));
            cmd.Parameters.Add("CommentMovie", comment.Text);
            cmd.ExecuteNonQuery();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2AddFavourite ad = new Form2AddFavourite(user_id, type);
            ad.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "AddToFav";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("FMID", int.Parse(movie_id));
            cmd.Parameters.Add("FUID", int.Parse(user_id));
            cmd.ExecuteNonQuery();


        }
    }
}
