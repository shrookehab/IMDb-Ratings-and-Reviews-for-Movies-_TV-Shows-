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
    public partial class Form2AddFavourite : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string user_id = "";
        string type = "";
        public Form2AddFavourite(string id, string typ)
        {
            InitializeComponent();
            textBox1.Text = "Movie Name";
            user_id = id;
            type = typ;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (type == "user")
            {
                Form3home us = new Form3home(user_id, type);
                us.Show();
                this.Hide();

            }
            else
            {
                Form2HomeAdmin ad = new Form2HomeAdmin(user_id, type);
                ad.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2AddFavourite_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form2AddFavourite_Load(object sender, EventArgs e)
        {
            string command = "select movie_name, Date_of_release, initial_rating, movie_duration, movie_trailer_link from Movies, Users, Add_To_Fav_Movie where movie_id = Add_To_Fav_movie_id and user_id = Add_To_Fav_user_id and user_id = :id1";

            adapter = new OracleDataAdapter(command, ordb);
            adapter.SelectCommand.Parameters.Add("id1", user_id);

            ds = new DataSet();
            adapter.Fill(ds);
            dgmovies.DataSource = ds.Tables[0];

            conn = new OracleConnection(ordb);
            conn.Open();


        }

        private void button6_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string command = "select movie_name, Date_of_release, initial_rating, movie_duration, movie_trailer_link from Movies, Users, Add_To_Fav_Movie where movie_id = Add_To_Fav_movie_id and user_id = Add_To_Fav_user_id and movie_name = :name2 and user_id = :id";

            adapter = new OracleDataAdapter(command, ordb);
            adapter.SelectCommand.Parameters.Add("name2", textBox1.Text);
            adapter.SelectCommand.Parameters.Add("id", user_id);

            ds = new DataSet();
            adapter.Fill(ds);
            dgmovies.DataSource = ds.Tables[0];

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2AddFavourite f = new Form2AddFavourite(user_id, type);
            f.Show();
            this.Hide();
        }
    }
}
