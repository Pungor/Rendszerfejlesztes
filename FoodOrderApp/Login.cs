using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace FoodOrderApp
{
    public partial class Login : Form
    {
        // SQLiteConnection con = new SQLiteConnection("data source=FOA_database.db");
        public Login()
        {
         InitializeComponent();
         //   this.Width = 530;
         // megjelenit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ChooseTheTitle.SelectedIndex = 0;
        }

        /*
         private void megjelenit()
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from tabla", con);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        */



        private void LoginButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            SQLiteCommand cmd = new SQLiteCommand("Select * from Users where name='"+textUser.Text+"' and password='"+textPW.Text+"'",db.GetConnection());
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string cmbitemvolue = ChooseTheTitle.SelectedItem.ToString();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["permission"].ToString() == cmbitemvolue)
                        {
                            MessageBox.Show("Beléptél " + dt.Rows[i]["permission"] + "-ként!");
                            if (ChooseTheTitle.SelectedIndex == 0)
                                {
                                    Users user = new Users();
                                    this.Hide();
                                    user.Show();
                                }
                            if (ChooseTheTitle.SelectedIndex == 1)
                                { 
                                    Courier co = new Courier();
                                    this.Hide();
                                    co.Show();
                                }
                            if (ChooseTheTitle.SelectedIndex == 2)
                                {
                                    Admin ad = new Admin();
                                    this.Hide();
                                    ad.Show();
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
