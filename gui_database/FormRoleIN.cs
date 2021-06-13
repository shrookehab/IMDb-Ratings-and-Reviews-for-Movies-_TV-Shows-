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
    public partial class FormRoleIN : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string movieid = "";
        List<string> actornames = new List<string>();

        string user_id = "";
        string type = "";

        public FormRoleIN(string id, string typ)
        {
            InitializeComponent();
            user_id = id;
            type = typ;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            checkedListBox1.Items.Clear();
            List<string> actors = new List<string>();
            for( int x = 0; x < actornames.Count; x++)
            {
                actors.Add(actornames[x]);
            }
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select movie_id from Movies where movie_name = :namemovie2";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("namemovie2", comboBox1.SelectedItem.ToString());
            OracleDataReader dr4 = cmd2.ExecuteReader();
            if (dr4.Read())
            {
                movieid = dr4[0].ToString();
            }
            dr4.Close();

            for (int i = 0; i < actornames.Count; i++)
            {
                OracleCommand cmd3 = new OracleCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "select actor_id from Actors where Afirst_aname = :nameactor2";
                cmd3.CommandType = CommandType.Text;
                cmd3.Parameters.Add("nameactor2", actornames[i]);
                OracleDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {
                    OracleCommand cmd4 = new OracleCommand();
                    cmd4.Connection = conn;
                    cmd4.CommandText = "select * from Has_Role_In";
                    cmd4.CommandType = CommandType.Text;
                    OracleDataReader dr5 = cmd4.ExecuteReader();
                    while (dr5.Read())
                    {
                        if (dr5["Role_movie_id"].ToString() == movieid && dr5["Role_actor_id"].ToString() == dr3[0].ToString())
                            actors.Remove(actornames[i]);
                    }
                    dr5.Close();


                }
                dr3.Close();


            }
            for (int k = 0; k < actors.Count; k++)
                checkedListBox1.Items.Add(actors[k]);

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormRoleIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
             Form2HomeAdmin fu = new Form2HomeAdmin(user_id,type);
            fu.Show();
            this.Hide();
        }

        private void FormRoleIN_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GetMoviesNames";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("MovieName", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();

            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "GetActorsNames";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("ActorName", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                actornames.Add(dr1[0].ToString());
                //checkedListBox1.Items.Add(dr1[0]);
            }
            dr1.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string actorid = "", movieid = "";


            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select movie_id from Movies where movie_name = :namemovie";
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.Add("namemovie", comboBox1.SelectedItem.ToString());

            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                movieid = dr2[0].ToString();
            }
            dr2.Close();

            for(int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                OracleCommand cmd1 = new OracleCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "select actor_id from Actors where Afirst_aname = :nameactor";
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.Add("nameactor", checkedListBox1.Items[i].ToString());

                OracleDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    actorid = dr1[0].ToString();
                }
                dr1.Close();



                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into Has_Role_In values(:Actorid, :movieid)";
                cmd.Parameters.Add("Actorid", actorid);
                cmd.Parameters.Add("movieid", movieid);
                cmd.ExecuteNonQuery();

                if (checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.Items.Remove(checkedListBox1.Items[i]);
                    checkedListBox1.Refresh();
                }

            }
            MessageBox.Show("Actors are Successfuly Assigned to the movie");


        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form2AddFavourite f = new Form2AddFavourite(user_id,type);
            f.Show();
            this.Hide();
        }
    }
}
