using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class USER_DATA
    {
        string first_name;
        string middle_name;
        string last_name;
        string phone_number;
        string email_address;
        string username;
        string userpassword;
        string user_type;

        public USER_DATA(string first_name, string middle_name, string last_name, string phone_number, string email_address, string username, string userpassword, string user_type)
        {
            this.first_name = first_name;
            this.middle_name = middle_name;
            this.last_name = last_name;
            this.phone_number = phone_number;
            this.email_address = email_address;
            this.username = username;
            this.userpassword = userpassword;
            this.user_type = user_type;
        }

        public SqlCommand getSqlCommand()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "USER_CREATION";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FIRST_NAME", this.first_name);
            cmd.Parameters.AddWithValue("@MIDDLE_NAME", this.middle_name);
            cmd.Parameters.AddWithValue("@LAST_NAME", this.last_name);
            cmd.Parameters.AddWithValue("@PHONE_NUMBER", this.phone_number);
            cmd.Parameters.AddWithValue("@EMAIL_ADDRESS1", this.email_address);
            cmd.Parameters.AddWithValue("@USERNAME", this.username);
            cmd.Parameters.AddWithValue("@USERPASSWORD", this.userpassword);
            cmd.Parameters.AddWithValue("@USER_TYPE", this.user_type);
            return cmd;
        }
    }
}