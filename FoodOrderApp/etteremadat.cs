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
    public partial class etteremadat : Form
    {
        DB db = new DB();
        SQLiteDataAdapter sda;
        DataTable dt;
        SQLiteCommand cmd;
        public etteremadat(string user)
        {
            InitializeComponent();
            label7.Text = user;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void etteremadat_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Etterem et = new Etterem(label7.Text);
            this.Hide();
            et.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                
                DB db = new DB();
                db.openconnection();
                SQLiteCommand cmd = new SQLiteCommand("insert into etterem (nev, stilus, helyszin, nyitvatartas, user) values ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + label7.Text + "')", db.GetConnection());
                cmd.ExecuteNonQuery();
                db.closeconnection();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";


            }
            else
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt! ");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void megjelenit()
        {
            DB db = new DB();
            db.openconnection();
            cmd = new SQLiteCommand("select * from szamlazott ", db.GetConnection());
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
                dataGridView1.Columns[0].HeaderText = "Összbevétel (Ft)";
                dataGridView1.Columns[1].HeaderText = "Futár megnevezése";
                dataGridView1.Columns[2].HeaderText = "futár bére";


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

        private void button5_Click(object sender, EventArgs e)
        {
            megjelenit();
        }
    }
}
