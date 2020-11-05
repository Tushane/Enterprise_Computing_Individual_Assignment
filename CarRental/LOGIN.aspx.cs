using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class LOGIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            DatabaseConnection con = new DatabaseConnection();

            cmd.CommandText = "USER_LOGIN";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_name", username.Text);
            cmd.Parameters.AddWithValue("@password", user_password.Text);

            string responses = con.insertData(cmd);

            string[] response = responses.Split(':');

            if ((response[0] == "ADMIN") || (response[0] == "NON-ADMIN"))
            {
                HttpCookie tempcookie = Request.Cookies["user_info"];

                if (tempcookie == null)
                {
                    tempcookie = new HttpCookie("user_info");

                    tempcookie["user_type"] = response[0];
                    tempcookie["username"] = response[2];
                    tempcookie["user_id"] = response[1];
                    tempcookie.Expires = DateTime.Now.AddDays(1);
                    tempcookie.Path = Request.ApplicationPath;

                }
                else
                {
                    tempcookie["user_type"] = response[0];
                    tempcookie["username"] = response[2];
                    tempcookie["user_id"] = response[1];
                    tempcookie.Expires = DateTime.Now.AddDays(1);
                    tempcookie.Path = Request.ApplicationPath;
                }


                Response.Cookies.Add(tempcookie);
                Response.Redirect("~/ADD_PRODUCT");
            }
            else
            {
                error.Text = response[1];
            }
        }
    }
}