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
    public partial class Form2HomeAdmin : Form
    {
        string user_id = "";
        string type = "";

        public Form2HomeAdmin(string id, string typ)
        {
            InitializeComponent();
            user_id = id;
            type = typ;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Formaddmovie addm = new Formaddmovie(user_id, type);
            addm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2HomeAdmin_Load(object sender, EventArgs e)
        {

        }

        private void Form2HomeAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3allmovies fall = new Form3allmovies(user_id, type);
            fall.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCategory fc = new FormCategory(user_id, type);
            fc.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form2login fl = new Form2login();
            fl.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormdirectorAdd fd = new FormdirectorAdd(user_id, type);
            fd.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Formactoradd fac = new Formactoradd(user_id,type);
            fac.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Formupdatemovie fum = new Formupdatemovie(user_id, type);
            fum.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormActorupdate fau = new FormActorupdate(user_id,type);
            fau.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FormdirectorUpdate fdu = new FormdirectorUpdate(user_id,type);
            fdu.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2AddFavourite ad = new Form2AddFavourite(user_id, type);
            ad.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FormRoleIN r = new FormRoleIN(user_id,type);
            r.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormTopRate top = new FormTopRate(user_id,type);
            top.Show();
            this.Hide();
        }
    }
}
