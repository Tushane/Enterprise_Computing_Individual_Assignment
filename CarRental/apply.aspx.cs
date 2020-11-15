using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class apply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (pass1.Text == pass2.Text)
            {
                USER_DATA user_info = new USER_DATA(first_name.Text, mid_name.Text, last_name.Text, phone_num.Text, email.Text, username.Text, pass1.Text, "NON-ADMIN");

                DatabaseConnection con = new DatabaseConnection();

                string response = con.insertData(user_info.getSqlCommand());

                if (response == "User Creation Sucessful")
                {
                    Response.Redirect("LOGIN.aspx");
                }
                else
                {
                    error.Text = response;
                }
            }
            else
            {
                error.Text = "Password Doesn't Match";
            }
        }

        protected void clear_form(object sender, EventArgs e)
        {
            first_name.Text = "";
            mid_name.Text = "";
            last_name.Text = "";
            phone_num.Text = "";
            email.Text = "";
            username.Text = "";
            pass1.Text = "";
            pass2.Text = "";
        }
    }
}