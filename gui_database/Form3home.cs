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
    public partial class Form3home : Form
    {
        string user_id = "";
        string type = "";
        public Form3home(string id, string typ)
        {
            InitializeComponent();
            user_id = id;
            type = typ;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2login flog = new Form2login();
            flog.Show();
            this.Hide();
        }

        private void Form3home_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3allmovies moAll = new Form3allmovies(user_id,type);
            moAll.Show();
            this.Hide();
        }

        private void Form3home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCategory fc = new FormCategory(user_id,type);
            fc.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormTopRate t = new FormTopRate(user_id,type);
            t.Show();
            this.Hide();


        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Form2login fg = new Form2login();
            fg.Show();
            this.Hide();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2AddFavourite ad = new Form2AddFavourite(user_id, type);
            ad.Show();
            this.Hide();
        }
    }
}
