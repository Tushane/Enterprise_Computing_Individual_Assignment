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

        private static bool pass_correct = false;
        private static string password = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (pass1.Text == pass2.Text && pass_correct == true)
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

        private bool pass1_TextChanged(object sender, EventArgs e)
        {
             password = ((TextBox)sender).Text;

            if (pass_upper(password))
            {
                if (pass_digit(password))
                {
                    if (pass_special(password))
                    {
                       return true;
                    }
                    else
                    {
                        error.Text = "You need have either one of the Special Characters (@,# or &) which your password";
                    }
                }
                else
                {
                    error.Text = "You need to Enter an Digit into Your Password";
                }
            }
            else
            {
                error.Text = "There is no upper Cased letter in Your Password. Please Add One.";
            }

            return false;
        }

        private bool pass_upper(string pass)
        {
            bool upper = false;
            char[] password = pass.ToCharArray();

            foreach(char p in password)
            {
                if (char.IsUpper(p))
                {
                    upper = true;
                }
            }

            return upper;
        }


        private bool pass_digit(string pass)
        {
            bool upper = false;
            char[] password = pass.ToCharArray();

            foreach (char p in password)
            {
                if (char.IsNumber(p))
                {
                    upper = true;
                }
            }

            return upper;
        }

        private bool pass_special(string pass)
        {
            bool upper = false;
            char[] password = pass.ToCharArray();

            foreach (char p in password)
            {
                if (p == '@' || p == '#' || p == '&')
                {
                    upper = true;
                }
            }

            return upper;
        }
    }
}