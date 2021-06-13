using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace gui_database
{
   
    public partial class ReportShow : Form
    {
        CrystalReport1 R;
        public ReportShow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            R.SetParameterValue(0, comboBox1.Text);
            R.SetParameterValue(1, textBox1.Text);
           R.SetParameterValue(2, textBox2.Text);
            //Convert.ToDateTime(textBox2.Text);
            R.SetParameterValue(3, textBox3.Text);
            crystalReportViewer1.ReportSource = R;
        }

        private void ReportShow_Load(object sender, EventArgs e)
        {
            R = new CrystalReport1();
            foreach (ParameterDiscreteValue v in R.Parameter_Choos_The_Category.DefaultValues)
                comboBox1.Items.Add(v.Value);
        }
    }
}
