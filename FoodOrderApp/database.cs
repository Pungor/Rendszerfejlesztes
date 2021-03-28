using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace FoodOrderApp
{
    class database
    {
        private SQLiteConnection con = new SQLiteConnection("Data source=FOA_database.db");

        public SQLiteConnection GetConnection()
        {
            return con;
        }

        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }
    }
}
