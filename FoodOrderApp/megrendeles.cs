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
    public partial class megrendeles : Form
    {
        DB db = new DB();
        SQLiteDataAdapter sda;
        DataTable dt;
        SQLiteCommand cmd;
        public megrendeles(string user)
        {
            InitializeComponent();
            label9.Text = user;
        }

  

 

        private void megrendeles_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Etterem et = new Etterem(label9.Text);
            this.Hide();
            et.Show();
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                int index1 = e.RowIndex;// lekérjük a sor indexét
                DataGridViewRow selectedRow1 = dataGridView1.Rows[index1];
                textBox1.Text = selectedRow1.Cells[2].Value.ToString();
                textBox2.Text = selectedRow1.Cells[10].Value.ToString();
                textBox3.Text = selectedRow1.Cells[7].Value.ToString();
                textBox5.Text = selectedRow1.Cells[3].Value.ToString();
                textBox7.Text = selectedRow1.Cells[5].Value.ToString();
                textBox4.Text = selectedRow1.Cells[9].Value.ToString();
                textBox9.Text = selectedRow1.Cells[1].Value.ToString();


            }
            if (e.ColumnIndex == 0)
            {
                int index = e.RowIndex;// lekérjük a sor indexét
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                textBox1.Text = selectedRow.Cells[2].Value.ToString();
                textBox2.Text = selectedRow.Cells[10].Value.ToString();
                textBox3.Text = selectedRow.Cells[7].Value.ToString();
                textBox5.Text = selectedRow.Cells[3].Value.ToString();
                textBox7.Text = selectedRow.Cells[5].Value.ToString();
                textBox4.Text = selectedRow.Cells[9].Value.ToString();
                textBox9.Text = selectedRow.Cells[1].Value.ToString();



                DialogResult result = MessageBox.Show("Megtörtént az étel átvétele?/Kiszállítása? ", "Figyelem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (textBox1.Text != ""&& textBox3.Text != "" && textBox6.Text != "" && textBox8.Text != "")
                    {
                        DB db = new DB();
                        db.openconnection();
                         cmd = new SQLiteCommand("insert into szamlazott (Bevetel, futar, berktg) values ('" + textBox3.Text + "', '" + textBox6.Text + "', '" + textBox8.Text + "') ", db.GetConnection());
                       
                        cmd.ExecuteNonQuery();
                        db.closeconnection();

                        MessageBox.Show("Az ételt átvette a fogyasztó/futár! ");
                        DB db2 = new DB();
                        db2.openconnection();
                         cmd = new SQLiteCommand("delete from megrendeles where sorszam ='" + textBox9.Text + "'  ", db2.GetConnection());

                        db2.closeconnection();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";


                    } else if (textBox3.Text != "" && textBox4.Text != "") {
                        DB db = new DB();
                        db.openconnection();
                        cmd = new SQLiteCommand("insert into szamlazott (Bevetel, futar, berktg) values ('" + textBox3.Text + "', '" + textBox6.Text + "', '" + textBox8.Text + "') ", db.GetConnection());
                        cmd.ExecuteNonQuery();
                        db.closeconnection();
                        DB db2 = new DB();
                        db2.openconnection();
                        cmd = new SQLiteCommand("delete from megrendeles where sorszam ='" + textBox9.Text + "' or tel='"+textBox4.Text+"'", db2.GetConnection());
                        cmd.ExecuteNonQuery();
                        db2.closeconnection();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Jelenítsd meg az adatokat! ");
                    }

                  
                }
                megjelenit();

            }
        }
        private void megjelenit()
        {


            cmd = new SQLiteCommand("select * from megrendeles ", db.GetConnection());
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
                dataGridView1.Columns[0].HeaderText = "Művelet";
                dataGridView1.Columns[2].HeaderText = "Sorszám";
                dataGridView1.Columns[2].HeaderText = "Vásárló";
                dataGridView1.Columns[3].HeaderText = "Fogyasztási hely";
                dataGridView1.Columns[4].HeaderText = "E-mail";
                dataGridView1.Columns[5].HeaderText = "Megjegyzés ";
                dataGridView1.Columns[5].HeaderText = "Szállítási idő";
                dataGridView1.Columns[7].HeaderText = "Ár";
                dataGridView1.Columns[8].HeaderText = "Ügyfélazonosító";
                dataGridView1.Columns[9].HeaderText = "Tel";
                dataGridView1.Columns[10].HeaderText = "Étteremazonosító ";
                dataGridView1.Columns[11].HeaderText = "Átvétel ";
                dataGridView1.Columns[12].HeaderText = "Státusz ";


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
        private void megjelenit2()
        {


            cmd = new SQLiteCommand("select * from futar ", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
            db.closeconnection();


            if (dt.Rows.Count > 0)
            {

                //set autosize mode
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }

                // Oszlopok fejlécének középre igazítása
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                //adatok sorok háttérszíne minden 2. sornál
                for (int i = 0; i < dataGridView2.RowCount; i += 2)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                //Fejlécek elnevezése
                dataGridView2.Columns[0].HeaderText = "Név";
                dataGridView2.Columns[1].HeaderText = "Azonosító";
                dataGridView2.Columns[2].HeaderText = "Szállítási terület";
                dataGridView2.Columns[3].HeaderText = "Munkaidő";
                dataGridView2.Columns[4].HeaderText = "Óradíj ";
  


                //Fejléc formázása
                dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 6, FontStyle.Bold);
                //Fejléc háttérszíne
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView2.EnableHeadersVisualStyles = false;
                //adatok középre igazítása
                for (int i = 1; i < dataGridView2.Columns.Count; i++)
                {
                    this.dataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                MessageBox.Show("Még nem szerepel futár az adatbázisban! ");
            }

        }




        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            /*

            if (e.ColumnIndex > 0)
            {

                int index1 = e.RowIndex;// lekérjük a sor indexét
                DataGridViewRow selectedRow2 = dataGridView2.Rows[index1];
                textBox6.Text = selectedRow2.Cells[2].Value.ToString();
                textBox8.Text = selectedRow2.Cells[5].Value.ToString();

            }
            

            if (e.ColumnIndex == 0)
            {
                int index = e.RowIndex;// lekérjük a sor indexét
                DataGridViewRow selectedRow3 = dataGridView2.Rows[index];
                textBox6.Text = selectedRow3.Cells[2].Value.ToString();
                textBox8.Text = selectedRow3.Cells[5].Value.ToString();


            }
            */

        }
        private void button4_Click(object sender, EventArgs e)
        {
            megjelenit();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            megjelenit2();


        }

        private void button6_Click(object sender, EventArgs e)
        {

                cmd = new SQLiteCommand("select * from futar where azonosito='" + textBox6.Text + "' ", db.GetConnection());
                sda = new SQLiteDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                db.closeconnection();
                for (int i = 0; i< dt.Rows.Count; i++) {
                if (textBox6.Text != "" && textBox6.Text == dt.Rows[i]["azonosito"].ToString())
                {
                    textBox6.Text = dt.Rows[i]["azonosito"].ToString();
                    textBox8.Text = dt.Rows[i]["oradij"].ToString();
                }/*
                else if (textBox6.Text == "" || textBox6.Text != dt.Rows[i]["azonosito"].ToString())
                {
                    MessageBox.Show("Kérlek pontosan add meg a futár azonosítóját! ");
                }*/
                else {
                    MessageBox.Show("Kérlek pontosan add meg a futár azonosítóját! ");
                }
                }
               


               
            }
        

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox6.Text != "" && textBox8.Text != "" && textBox5.Text != "" )
            {

                DB db = new DB();
                db.openconnection();
                SQLiteCommand cmd = new SQLiteCommand("insert into kiszallitas (ügyfel, fogyhely, etterem, ar, megjegyzes, telefon, futar, futarar) values ('" + textBox1.Text + "', '" + textBox5.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox7.Text + "', '" + textBox4.Text + "', '" + textBox6.Text + "', '" + textBox8.Text + "')", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();

            }
            else
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt! \n(A futár adatok is kellenek! ) ");
            }
        }
    }
}
