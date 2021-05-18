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
    public partial class Form1 : Form
    {

        DataTable dt = new DataTable();
        SQLiteDataAdapter sda;
        SQLiteCommand cmd = new SQLiteCommand();

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Készítette: Nagy Richárd, Pungor Zoltán, Sabján Attila\n 2021.04.26");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Regisztracio Reg = new Regisztracio();
            this.Hide();
            Reg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            cmd = new SQLiteCommand("select * from belepes where felhnev='" + textBox1.Text + "' and jelszo='" + textBox2.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            sda.Fill(dt);

            if (checkBox1.Checked)
            {
                db.openconnection();
                cmd = new SQLiteCommand("select * from kosar ", db.GetConnection());
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                sda = new SQLiteDataAdapter(cmd);
                sda.Fill(dt);
                db.closeconnection();
                int buyer = dt.Rows.Count;

                this.Hide();
                Rendel rendel = new Rendel(buyer.ToString());
                rendel.Show();
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    String cmbitem;
                    cmbitem = comboBox1.Text;
                    if (cmbitem == "vásárló" && dt.Rows[0]["szerepkor"].ToString() == cmbitem )
                    {
                        this.Hide();
                        Rendel rendel = new Rendel(textBox1.Text);
                        rendel.Show();
                    }
                    else if (cmbitem == "étterem" && dt.Rows[0]["szerepkor"].ToString() == cmbitem )
                    {
                        this.Hide();
                        Etterem etterem = new Etterem(textBox1.Text);
                        etterem.Show();

                    }
                    else if (cmbitem == "futár" && dt.Rows[0]["szerepkor"].ToString() == cmbitem )
                    {
                        this.Hide();
                        Futar futar = new Futar(textBox1.Text);
                        futar.Show();
                    } 

                    else
                    {
                        MessageBox.Show("Rossz szerepkört választottál! ");
                    }

                }
                else if (dt.Rows.Count == 0 )
                {
                    MessageBox.Show("Kérem töltse ki a mezőket!");
                }
                else
                    MessageBox.Show("Rossz felhasználói név vagy jelszó!");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                button1_Click(sender, e);
            }
        }
    }
}
