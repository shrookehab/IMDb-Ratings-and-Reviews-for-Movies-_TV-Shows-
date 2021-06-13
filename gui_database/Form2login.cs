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
    public partial class Form2login : Form
    {
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        public Form2login()
        {
            InitializeComponent();
            password_text1.PasswordChar = '*';
            password_text1.MaxLength = 10;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2login_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

        

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2signup fs = new Form2signup("","");
            fs.Show();
            this.Hide();
        }

        private void password_text1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isCorrect = false;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select user_id, Ue_mail , user_password, member_type from Users";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["Ue_mail"].ToString() == textBox1.Text.ToString() && dr["user_password"].ToString() == password_text1.Text.ToString() && dr["member_type"].ToString() == "user")
                {
                    Form3home fH = new Form3home(dr["user_id"].ToString(), dr["member_type"].ToString());
                    fH.Show();
                    this.Hide();
                    isCorrect = true;
                    break;
                }
                else if (dr["Ue_mail"].ToString() == textBox1.Text.ToString() && dr["user_password"].ToString() == password_text1.Text.ToString() && dr["member_type"].ToString() == "admin")
                {
                    Form2HomeAdmin fA = new Form2HomeAdmin(dr["user_id"].ToString(), dr["member_type"].ToString());
                    fA.Show();
                    this.Hide();
                    isCorrect = true;
                    break;
                }
            }
            if (isCorrect == false)
                 MessageBox.Show("INVALID USERNAME OR PASSWORD");
            dr.Close();
            textBox1.Text = "";
            password_text1.Text = "";
            
        }

        private void Form2login_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
