using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace FoodOrderApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

  

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            database db = new database();
            SQLiteCommand cmd = new SQLiteCommand("Select * from Users where name='" + textUser.Text + "' and password='" + textPW.Text + "'", db.GetConnection());
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string cmbitemvolue = ChooseTheTitle.SelectedItem.ToString();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["felhasznalo"].ToString() == cmbitemvolue)
                    {
                        MessageBox.Show("Beléptél " + dt.Rows[i]["felhasznalo"] + "-ként!");
                        if (ChooseTheTitle.SelectedIndex == 0)
                        {
                            Users f2 = new Users();
                            this.Hide();
                            f2.Show();
                        }
                        else
                        {
                            Courier f3 = new Courier();
                            this.Hide();
                            f3.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rossz szerepkört választottál");
                    }
                }
            }

            else
            {
                MessageBox.Show("Rossz felh. név vagy jelszó!");
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Valóban kiszeretnél lépni?", "Igen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reg f4 = new reg();
            this.Hide();
            f4.Show();
        }
    }
}
