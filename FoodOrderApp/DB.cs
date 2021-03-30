﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;

namespace FoodOrderApp
{
    class DB
    {
        private SQLiteConnection con = new SQLiteConnection("Data source=FOAdatabase.db");

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
