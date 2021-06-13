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
    public partial class FormCategory : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        string ordb = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;

        string user_id = "";
        string type = "";

        public FormCategory(string id, string typ)
        {
            InitializeComponent();
            textBox1.Text = "Category Name";
            user_id = id;
            type = typ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (type == "user")
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

        private void FormCategory_Load(object sender, EventArgs e)
        {
            if (type == "user")
                panel2.Hide();
            string command = "select * from Categories ";

            adapter = new OracleDataAdapter(command, ordb);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            conn = new OracleConnection(ordb);
            conn.Open();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string command = "select * from Movies mov, Categories categ where mov.cat_id = categ.category_id and category_name = :name";

            adapter = new OracleDataAdapter(command, ordb);
            adapter.SelectCommand.Parameters.Add("name", textBox1.Text);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int maxID, newID;
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "GetCategoryID";
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            c.ExecuteNonQuery();
            try
            {
                maxID = Convert.ToInt32(c.Parameters["id"].Value.ToString());
                newID = maxID + 1;
            }
            catch
            {
                newID = 1;
            }

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into Categories values(:catID, :catName)";
            cmd.Parameters.Add("catID", newID);
            cmd.Parameters.Add("catName", textBox1.Text);
            int r = cmd.ExecuteNonQuery();

            if (r != -1)
            {
                MessageBox.Show("New Category is added");
            }

            textBox1.Text = "";
            string command = "select * from Categories ";

            adapter = new OracleDataAdapter(command, ordb);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }

        private void button3_Click(object sender, EventArgs e)
        {

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Delete from Categories where category_name = :catname";
            cmd.Parameters.Add("catname", textBox1.Text);
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Category Successfully Deleted");
            }
            textBox1.Text = "";
            string command = "select * from Categories ";

            adapter = new OracleDataAdapter(command, ordb);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void FormCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";

        }
    }
}
