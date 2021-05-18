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
    public partial class etlap : Form
    {
        DB db = new DB();
        SQLiteDataAdapter sda;
        DataTable dt;
        SQLiteCommand cmd;
        int pos = 0;
        int leallasifeltetel;
        public etlap(string user)
        {
            InitializeComponent();
            label13.Text = user;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void etlap_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Etterem et = new Etterem(label13.Text);
            this.Hide();
            et.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "" && textBox9.Text != "")
            {

                DB db = new DB();
                db.openconnection();
                SQLiteCommand cmd = new SQLiteCommand("insert into etlap (etelnev, leiras, ar, allergenek, idoszak, osszetevok, elkeszites, user) values ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox6.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '" + label13.Text + "')", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
              
                textBox6.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
            }
            else
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt! \n(Az ár és időszak adatok igény esetén elhagyhatóak! ) ");
            }
        }

        private void megjelenit()
        {


            cmd = new SQLiteCommand("select * from etlap where user='"+label13.Text+"'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            db.closeconnection();
            leallasifeltetel = dt.Rows.Count;

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
                dataGridView1.Columns[0].HeaderText = "Művelet";
                dataGridView1.Columns[1].HeaderText = "Ételnév";
                dataGridView1.Columns[2].HeaderText = "Leírás";
                dataGridView1.Columns[3].HeaderText = "Ár";
                dataGridView1.Columns[4].HeaderText = "Allergének";
                //dataGridView1.Columns[5].HeaderText = "Étterem";
                dataGridView1.Columns[5].HeaderText = "Időszak";
                dataGridView1.Columns[6].HeaderText = "Összetevők";
                dataGridView1.Columns[7].HeaderText = "Akció (%)";
                dataGridView1.Columns[8].HeaderText = "Elkészítés (perc)";
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
            else {
                MessageBox.Show("Még nincs rögzített étel! ");
            }

        }


        public void showData(int index) //Textboxban megjelenítani az adatot
        {

            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[index][0].ToString();
                textBox2.Text = dt.Rows[index][1].ToString();
                textBox3.Text = dt.Rows[index][2].ToString();
                textBox4.Text = dt.Rows[index][3].ToString();
              //  textBox5.Text = dt.Rows[index][4].ToString();
                textBox6.Text = dt.Rows[index][4].ToString();
                textBox8.Text = dt.Rows[index][5].ToString();
                textBox9.Text = dt.Rows[index][7].ToString();

                dataGridView1.DataSource = dt;
            }
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            megjelenit();
            pos = 0;
            showData(pos);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                pos = 0;
                showData(pos);
            }
            else
            {
                MessageBox.Show("Nincs adat!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pos--;
            if (pos >= 0 && textBox1.Text!="")
            {
                showData(pos);
            }
            else
            {
                MessageBox.Show("Nincs adat!");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pos++;
            if (leallasifeltetel!=0 && pos < leallasifeltetel)
            {
                showData(pos);
            }
            else
            {
                MessageBox.Show("Nincs adat!");
                pos = leallasifeltetel - 1;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                pos = dt.Rows.Count - 1;
                showData(pos);
            }
            else
            {
                MessageBox.Show("Nincs adat!");
               
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                int index1 = e.RowIndex;// lekérjük a sor indexét
                DataGridViewRow selectedRow1 = dataGridView1.Rows[index1];
                textBox1.Text = selectedRow1.Cells[1].Value.ToString();
                textBox2.Text = selectedRow1.Cells[2].Value.ToString();
                textBox3.Text = selectedRow1.Cells[3].Value.ToString();
                textBox4.Text = selectedRow1.Cells[4].Value.ToString();
                //textBox5.Text = selectedRow1.Cells[5].Value.ToString();
                textBox6.Text = selectedRow1.Cells[6].Value.ToString();
                textBox8.Text = selectedRow1.Cells[7].Value.ToString();
                textBox9.Text = selectedRow1.Cells[8].Value.ToString();
            }
            if (e.ColumnIndex == 0)
            {
                int index = e.RowIndex;// lekérjük a sor indexét
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                textBox3.Text = selectedRow.Cells[3].Value.ToString();
                textBox4.Text = selectedRow.Cells[4].Value.ToString();
              //  textBox5.Text = selectedRow.Cells[5].Value.ToString();
                textBox6.Text = selectedRow.Cells[6].Value.ToString();
                textBox8.Text = selectedRow.Cells[6].Value.ToString();
                textBox9.Text = selectedRow.Cells[7].Value.ToString();
                DialogResult result = MessageBox.Show("Törölni szeretné az adatokat?", "Figyelem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    db.openconnection();
                    cmd = new SQLiteCommand("delete from etlap where etelnev='" + textBox1.Text + "' ", db.GetConnection());
                    cmd.ExecuteNonQuery();
                    db.closeconnection();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                 //   textBox5.Text = "";
                    textBox6.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";

                    megjelenit();
                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                 //   textBox5.Text = "";
                    textBox6.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

                db.openconnection();
                cmd = new SQLiteCommand("select * from etlap where etelnev='" + textBox1.Text + "' and user='" + label13.Text + "'", db.GetConnection());
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                sda = new SQLiteDataAdapter(cmd);
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                db.closeconnection();
                textBox6.Text = "";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                db.openconnection();
                cmd = new SQLiteCommand("update etlap set ar='" + textBox3.Text + "' where etelnev='" + textBox1.Text + "' and user='" + label13.Text + "'", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                megjelenit();
            }
            else {
                MessageBox.Show("Töltsd be az étel adatait! ");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
           // textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && comboBox1.Text!="")
            {
                db.openconnection();
                cmd = new SQLiteCommand("update etlap set akcio= '" + textBox7.Text + "' where leiras='" + comboBox1.SelectedItem + "' and user='" + label13.Text + "' ", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                megjelenit();
               

                double akcios = (100 - Double.Parse(textBox7.Text)) / 100;
                double ertek = Int32.Parse(textBox3.Text) * akcios;
                //textBox3.Text = ertek.ToString();

                db.openconnection();
                cmd = new SQLiteCommand("update etlap set ar= '" + ertek.ToString() + "' where leiras='" + comboBox1.SelectedItem + "' and user='" + label13.Text + "' ", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                megjelenit();

            }

            else {
                MessageBox.Show("Kérlek adj meg kedvezmény értéket és válaszd ki az ételtípust! ");
            }
            

        }

        private void etlap_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
