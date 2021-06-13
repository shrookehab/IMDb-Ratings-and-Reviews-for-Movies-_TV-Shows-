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
    public partial class Formupdatemovie : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;


        string user_id = "";
        string type = "";
        public Formupdatemovie(string id, string typ)
        {
            InitializeComponent();
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
            string dirctor_id = "", category_id = "", location_id = "";

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select director_id from Directors where Dfirst_name = :direct_name3";
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.Add("direct_name3", director.SelectedItem.ToString());
            OracleDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                dirctor_id = dr["director_id"].ToString();
            }
            dr.Close();

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select category_id from Categories where category_name = :categ_name3";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("categ_name3", categories.SelectedItem.ToString());
            OracleDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                category_id = dr2["category_id"].ToString();
            }
            dr2.Close();

            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select movie_id from Movies where movie_name = :nameofmovie";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("nameofmovie", comboBox1.SelectedItem.ToString());
            OracleDataReader dr3 = c.ExecuteReader();
            if (dr3.Read())
            {
                location_id = dr3["movie_id"].ToString();
            }
            dr3.Close();


            OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "UpdateMovieLocation";
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("MLID", location_id);
            cmd3.Parameters.Add("MLocation", location.Text);
            cmd3.ExecuteNonQuery();


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UpdateMovie";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("MName", name.Text);
            cmd.Parameters.Add("DirID", dirctor_id);
            cmd.Parameters.Add("DateRelease", dateTimePicker1.Value.Date);
            cmd.Parameters.Add("CatID", category_id);
            cmd.Parameters.Add("InitialRating", rating.Text);
            cmd.Parameters.Add("MDuration", duration.Text);
            cmd.Parameters.Add("MTrailerLink", trailer.Text);
            cmd.Parameters.Add("choice", comboBox1.SelectedItem.ToString());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Movie Updated successfully");


            rating.Text = "";
            name.Text = "";
            trailer.Text = "";
            location.Text = "";
            duration.Text = "";
            comboBox1.SelectedItem = null;
            categories.SelectedItem = null;
            director.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now.Date;

        }

        private void Formupdatemovie_Load(object sender, EventArgs e)
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
                director.Items.Add(dr[0]);
            }
            dr.Close();

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select category_name from Categories";
            cmd1.CommandType = CommandType.Text;

            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                categories.Items.Add(dr1[0]);
            }
            dr1.Close();

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select movie_name from Movies";
            cmd2.CommandType = CommandType.Text;

            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox1.Items.Add(dr2[0]);
            }
            dr2.Close();


        }

        private void Formupdatemovie_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select movie_name, Date_of_release, initial_rating, movie_duration, movie_trailer_link, Dfirst_name, category_name, movie_id from Movies, Directors, Categories where director_id = direct_id and category_id = cat_id and movie_name =:name";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("name", comboBox1.SelectedItem.ToString());
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                name.Text = dr[0].ToString();
                dateTimePicker1.Text = dr[1].ToString();
                rating.Text = dr[2].ToString();
                duration.Text = dr[3].ToString();
                trailer.Text = dr[4].ToString();
                director.SelectedItem = dr[5].ToString();
                categories.SelectedItem = dr[6].ToString();
            }
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select movie_location from Movie_Location where Location_movie_id = :id2";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("id2", dr[7].ToString());
            OracleDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.Read())
            {
                location.Text = dr2[0].ToString();
            }
            dr.Close();
            dr2.Close();


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string location_id = "";
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select movie_id from Movies where movie_name = :movieName";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("movieName", comboBox1.SelectedItem.ToString());
            OracleDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                location_id = dr[0].ToString();
            }
            dr.Close();

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "DeleteMovieLocation";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("MLID", location_id);
            cmd1.ExecuteNonQuery();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DeleteMovie";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("MName", comboBox1.SelectedItem.ToString());
            cmd.ExecuteNonQuery();



            MessageBox.Show("Movie Successfully Deleted");
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);

            rating.Text = "";
            name.Text = "";
            trailer.Text = "";
            location.Text = "";
            duration.Text = "";
            comboBox1.SelectedItem = null;
            categories.SelectedItem = null;
            director.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now.Date;


        }
    }
}
