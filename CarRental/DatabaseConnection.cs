using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class DatabaseConnection
    {

        private string serverName = "Data Source=.\\SQLEXPRESS;Initial Catalog=JAKE_DATA_STORAGE;Integrated Security=True";

        SqlConnection con;
        private SqlConnection Connection()
        {
            return new SqlConnection(serverName);

        }

        public string insertData(SqlCommand cmd)
        {

            this.con = Connection();
            this.con.Open();
            cmd.Connection = con;

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@responseMessage";
            outPutParameter.SqlDbType = System.Data.SqlDbType.VarChar;
            outPutParameter.Size = 100;
            outPutParameter.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

            cmd.ExecuteNonQuery();
            string results = outPutParameter.Value.ToString();

            closeSqlData();

            return results;

        }

        public SqlDataReader readSqlData(string data)
        {
            this.con = Connection();

            con.Open();
            SqlCommand cmd = new SqlCommand(data, con);

            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        public void closeSqlData()
        {
            this.con.Close();
        }
    }
}