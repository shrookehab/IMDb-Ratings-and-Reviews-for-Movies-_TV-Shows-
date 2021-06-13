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
    public partial class FormTopRate : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;

        string user_id = "";
        string type = "";
        public FormTopRate(string id, string typ)
        {
            InitializeComponent();
            user_id = id;
            type = typ;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormTopRate_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (type == "user")
            {
                Form3home user = new Form3home(user_id,type);
                user.Show();
                this.Hide();

            }
            else
            {
                Form2HomeAdmin adm = new Form2HomeAdmin(user_id, type);
                adm.Show();
                this.Hide();
            }
        }

        private void FormTopRate_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            string comand = "SELECT movie_name, rate_value FROM Rate_Movie, Movies where Rate_movie_id = movie_id ORDER BY rate_value";
            adapter = new OracleDataAdapter(comand, ordb);
            ds = new DataSet();
            adapter.Fill(ds);
            dgmovies.DataSource = ds.Tables[0];


        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}
