using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderApp
{
    public partial class Etterem : Form
    {
        public Etterem(string user)
        {
            InitializeComponent();
            label2.Text = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Etterem_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            etteremadat et = new etteremadat(label2.Text);
            this.Hide();
            et.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            etlap lap = new etlap(label2.Text);
            this.Hide();
            lap.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            megrendeles meg = new megrendeles(label2.Text);
            this.Hide();
            meg.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
