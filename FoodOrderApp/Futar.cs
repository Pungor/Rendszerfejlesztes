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
    public partial class Futar : Form
    {
        DB db = new DB();
        SQLiteDataAdapter sda;
        DataTable dt;
        SQLiteCommand cmd;
        public Futar(string user)
        {
            InitializeComponent();
            textBox6.Text = user;

            DB db = new DB();
            db.openconnection();
            SQLiteCommand cmd = new SQLiteCommand("select *from futar where azonosito='" + textBox6.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();
            if (dt.Rows.Count > 0)
            {
                textBox5.Text = dt.Rows[0]["nev"].ToString();
                textBox6.Text = dt.Rows[0]["azonosito"].ToString();
                textBox1.Text = dt.Rows[0]["terulet"].ToString();
                textBox2.Text = dt.Rows[0]["munkaido"].ToString();
                textBox3.Text = dt.Rows[0]["oradij"].ToString();
            }
            else
            {
                MessageBox.Show("Kérlek töltsd ki az adataidat! ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Futar_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {

                DB db = new DB();

                SQLiteCommand cmd2 = new SQLiteCommand("select *from futar where azonosito='" + textBox6.Text + "'", db.GetConnection());
                sda = new SQLiteDataAdapter(cmd2);
                dt = new DataTable();
                sda.Fill(dt);
                db.closeconnection();
                textBox5.Text = dt.Rows[0]["nev"].ToString();
                textBox6.Text = dt.Rows[0]["azonosito"].ToString();
                textBox1.Text = dt.Rows[0]["terulet"].ToString();
                textBox2.Text = dt.Rows[0]["munkaido"].ToString();
                textBox3.Text = dt.Rows[0]["oradij"].ToString();
                if (dt.Rows.Count == 0)
                {
                    db.openconnection();
                    SQLiteCommand cmd = new SQLiteCommand("insert into futar (nev, azonosito, terulet, munkaido, oradij) values ('" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "')", db.GetConnection());
                    cmd.ExecuteNonQuery();
                    db.closeconnection();
                    db.openconnection();
                }
                else {
                    MessageBox.Show("Már rögzítve van! ");
                }

            }
            else
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt!" );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                db.openconnection();
                cmd = new SQLiteCommand("update futar set oradij='" + textBox3.Text + "' where azonosito='" + textBox6.Text + "'", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                db.openconnection();
                SQLiteCommand cmd2 = new SQLiteCommand("select *from futar where azonosito='" + textBox6.Text + "'", db.GetConnection());
                sda = new SQLiteDataAdapter(cmd2);
                dt = new DataTable();
                sda.Fill(dt);
                db.closeconnection();
                textBox3.Text = dt.Rows[0]["oradij"].ToString();
            }
            else
            {
                MessageBox.Show("Kérlek előbb tölts ki, és ments el minden mezőt!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                db.openconnection();
                cmd = new SQLiteCommand("update futar set munkaido='" + textBox2.Text + "' where azonosito='" + textBox6.Text + "'", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                db.openconnection();
                SQLiteCommand cmd2 = new SQLiteCommand("select *from futar where azonosito='" + textBox6.Text + "'", db.GetConnection());
                sda = new SQLiteDataAdapter(cmd2);
                dt = new DataTable();
                sda.Fill(dt);
                db.closeconnection();
                textBox2.Text = dt.Rows[0]["munkaido"].ToString();
            }
            else
            {
                MessageBox.Show("Kérlek előbb tölts ki és ments el minden mezőt!");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                db.openconnection();
                cmd = new SQLiteCommand("update futar set terulet='" + textBox1.Text + "' where azonosito='" + textBox6.Text + "'", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                db.openconnection();
                SQLiteCommand cmd2 = new SQLiteCommand("select *from futar where azonosito='" + textBox6.Text + "'", db.GetConnection());
                sda = new SQLiteDataAdapter(cmd2);
                dt = new DataTable();
                sda.Fill(dt);
                db.closeconnection();
                textBox1.Text = dt.Rows[0]["terulet"].ToString();
            }
            else
            {
                MessageBox.Show("Kérlek előbb tölts ki és ments el minden mezőt!");
            }

        }
        private void megjelenit()
        {

            db.openconnection();
            cmd = new SQLiteCommand("select * from kiszallitas ", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            db.closeconnection();


            if (dt.Rows.Count > 0)
            {

                //set autosize mode
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }

                // Oszlopok fejlécének középre igazítása
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                //adatok sorok háttérszíne minden 2. sornál
                for (int i = 0; i < dataGridView1.RowCount; i += 2)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                //Fejlécek elnevezése
                dataGridView1.Columns[0].HeaderText = "Ügyfél";
                dataGridView1.Columns[1].HeaderText = "Szállítási cím";
                dataGridView1.Columns[2].HeaderText = "Étterem";
                dataGridView1.Columns[3].HeaderText = "Ár";
                dataGridView1.Columns[4].HeaderText = "Megjegyzés ";
                dataGridView1.Columns[5].HeaderText = "Telefon";
                dataGridView1.Columns[6].HeaderText = "Futár";
                dataGridView1.Columns[7].HeaderText = "Futár díj";
        



                //Fejléc formázása
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 6, FontStyle.Bold);
                //Fejléc háttérszíne
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.EnableHeadersVisualStyles = false;
                //adatok középre igazítása
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    this.dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                MessageBox.Show("Még nincs rögzített megrendelés! ");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            megjelenit();
            cmd = new SQLiteCommand("select * from kiszallitas where futar='" + textBox6.Text + "' ", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();
            int ber = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                ber += Int32.Parse(dt.Rows[i]["futarar"].ToString());
            }
            textBox4.Text = ber.ToString();

        }
    }
}
