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
    public partial class Kosar : Form
    {
        DB db = new DB();
        SQLiteDataAdapter sda;
        DataTable dt;
        SQLiteCommand cmd;

        public Kosar(string buyer)
        {
            InitializeComponent();
            label1.Text = buyer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rendel rend = new Rendel(label1.Text);
            this.Hide();
            rend.Show();

        }
        private void megjelenit()
        {

            db.openconnection();
            cmd = new SQLiteCommand("select * from kosar where username='" + label1.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            db.closeconnection();

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
            dataGridView1.Columns[0].HeaderText = "Étel";
            dataGridView1.Columns[1].HeaderText = "Ár";
            dataGridView1.Columns[2].HeaderText = "Étterem";
            dataGridView1.Columns[3].HeaderText = "Időszak";
            dataGridView1.Columns[4].HeaderText = "Akció";
            dataGridView1.Columns[5].HeaderText = "Elkészítés (perc)";
            dataGridView1.Columns[6].HeaderText = "Felhasználó";
            //dataGridView1.Columns[7].HeaderText = "Étterem azonosító ";


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


        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Kosar_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            megjelenit();
            db.openconnection();
            cmd = new SQLiteCommand("select * from kosar where username='" + label1.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();

            if (dt.Rows.Count != 0)
            {
                int szumma = 0;
                int ar = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (szumma < Int32.Parse(dt.Rows[i]["elkeszites"].ToString()))
                    {
                        szumma = Int32.Parse(dt.Rows[i]["elkeszites"].ToString());
                    }
                    ar += Int32.Parse(dt.Rows[i]["kosarar"].ToString());
                }
                szumma += 20; //20 perces kiszállítási idővel számolva
                textBox8.Text = ar.ToString();
                textBox2.Text = szumma.ToString();
                textBox7.Text = dt.Rows[0]["kosaretterem"].ToString();
               



            }
            else
            {
                MessageBox.Show("Még nincs megrendelésed! ");
            }
            db.openconnection();
            cmd = new SQLiteCommand("select * from belepes where felhnev='" + label1.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();
            if (dt.Rows.Count > 0)
                textBox1.Text = label1.Text;
            else
                textBox1.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string atvetel = "személyes";
            string atvetel2 = "kiszállítás";

            db.openconnection();
            SQLiteCommand cmd = new SQLiteCommand("select * from megrendeles ", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            db.closeconnection();
            int sorszam = dt.Rows.Count + 1;
            db.openconnection();


                if (textBox2.Text != "" && textBox1.Text != "" && textBox3.Text != "" && textBox8.Text != "")
                 {
                if (checkBox1.Checked)
                {

                    SQLiteCommand cmd5 = new SQLiteCommand("insert into megrendeles (sorszam, vasarlo, fhely, email, megjegyzes, kiszallido, ar, azonosito, tel, etteremazonosito, atvetel) values ('" + sorszam.ToString() + "','" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox2.Text + "', '" + textBox8.Text + "', '" + label1.Text + "', '" + textBox4.Text + "', '" + textBox7.Text + "','" + atvetel2 + "')", db.GetConnection());
                    sda = new SQLiteDataAdapter(cmd5);
                    dt = new DataTable();
                    sda.Fill(dt);
                    db.closeconnection();
                    MessageBox.Show("Hamarosan szállítjuk! A várható kiszállítási idő:" + textBox2.Text + " perc. ");
                    db.openconnection();
                    SQLiteCommand cmd2 = new SQLiteCommand("delete from kosar where username='" + label1.Text + "' ", db.GetConnection());
                    sda = new SQLiteDataAdapter(cmd2);
                    dt = new DataTable();
                    sda.Fill(dt);
                    db.closeconnection();


                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    checkBox1.Checked = false;
                }
                else
                {
                    MessageBox.Show("Kérlek add meg az adataidat, és átvételi módot! ");
                }

            }
            else if (checkBox2.Checked && textBox2.Text != "" && textBox8.Text != "")
            {
                db.openconnection();
                SQLiteCommand cmd6 = new SQLiteCommand("select * from belepes where felhnev='" + label1.Text + "'", db.GetConnection());
                sda = new SQLiteDataAdapter(cmd6);
                dt = new DataTable();
                sda.Fill(dt);
                db.closeconnection();
                if (dt.Rows.Count > 0)
                {
                    db.openconnection();
                    SQLiteCommand cmd3 = new SQLiteCommand("insert into megrendeles (sorszam, vasarlo, fhely, email, megjegyzes, kiszallido, ar, azonosito, tel, etteremazonosito, atvetel) values ('" + sorszam.ToString() + "','" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox2.Text + "', '" + textBox8.Text + "', '" + label1.Text + "', '" + textBox4.Text + "', '" + textBox7.Text + "','" + atvetel + "')", db.GetConnection());
                    sda = new SQLiteDataAdapter(cmd3);
                    dt = new DataTable();
                    sda.Fill(dt);
                    db.closeconnection();
                    db.openconnection();
                    SQLiteCommand cmd4 = new SQLiteCommand("delete from kosar where username='" + label1.Text + "' ", db.GetConnection());
                    sda = new SQLiteDataAdapter(cmd4);
                    dt = new DataTable();
                    sda.Fill(dt);
                    db.closeconnection();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    checkBox2.Checked = false;
                    MessageBox.Show("Sikeres megrendelés, várunk, hogy átvedd az ételt! ");

                }

                else {
                    if (textBox4.Text != "")
                    {

                        db.openconnection();
                        SQLiteCommand cmd7 = new SQLiteCommand("insert into megrendeles (sorszam, vasarlo, fhely, email, megjegyzes, kiszallido, ar, azonosito, tel, etteremazonosito, atvetel) values ('" + sorszam.ToString() + "','" + textBox1.Text + "', '" + textBox3.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox2.Text + "', '" + textBox8.Text + "', '" + label1.Text + "', '" + textBox4.Text + "', '" + textBox7.Text + "','" + atvetel + "')", db.GetConnection());
                            sda = new SQLiteDataAdapter(cmd7);
                        dt = new DataTable();
                        sda.Fill(dt);
                        db.closeconnection();
                        db.openconnection();
                        SQLiteCommand cmd8 = new SQLiteCommand("delete from kosar where username='" + label1.Text + "' ", db.GetConnection());
                        sda = new SQLiteDataAdapter(cmd8);
                        dt = new DataTable();
                        sda.Fill(dt);
                        db.closeconnection();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        checkBox2.Checked = false;
                        MessageBox.Show("Sikeres megrendelés, várunk, hogy átvedd az ételt! ");
                    }
                    else
                    {
                        MessageBox.Show("Kérlek adj meg telefonszámot! ");
                    }

                }


               
            }

            else
            {
                MessageBox.Show("Kérlek add meg az adataidat, és átvételi módot! ");
            }
        }
    }
}
