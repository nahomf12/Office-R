using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using OFFICE_R.Models;

namespace OFFICE_R
{
    class Database
    {
        public static NpgsqlConnection conn;
        public static NpgsqlDataReader dataReader;
        public Database()
        {
            try
            {
                string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};preload reader=true;POOLING=True;MINPOOLSIZE=10;",
                    "localhost", "5432", "officeruser",
                    "officerpassword", "officerdb");
                conn = new NpgsqlConnection(connstring);
                conn.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateTables()
        {
            Employee.CreateTable();
            Task.CreateTable();
            Models.Comment.CreateTable();
            Announcement.CreateTable();
            Attendance.CreateTable();
        }

        public NpgsqlDataReader ExecuteQuery(string query)
        {
            if (dataReader != null)
            {
                dataReader.Close();
            }
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, conn);
            dataReader = npgsqlCommand.ExecuteReader();
            return dataReader;
        }

        public void ExecuteUpdate(String query)
        {
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, conn);
            npgsqlCommand.ExecuteNonQuery();
        }

        public NpgsqlCommand PrepareStatement(String query)
        {
            return new NpgsqlCommand(query, conn);
        }

        public DataTable GetDataSet(String query)
        {
            DataSet dataSet = new DataSet();
            dataSet.Reset();
            new NpgsqlDataAdapter(query, conn).Fill(dataSet);
            return dataSet.Tables[0];
        }
    }
}
