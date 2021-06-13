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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel3.Width += 3;
            if (panel3.Width >= 800)
            {
                timer1.Stop();
                Form2login f2 = new Form2login();
                f2.Show();
                this.Hide();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
