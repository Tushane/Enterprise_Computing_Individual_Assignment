using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class LOGOUT : System.Web.UI.Page
    {
        string def_user = "default";
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie tempcookie = Request.Cookies["user_info"];

            if (tempcookie == null)
            {
                tempcookie = new HttpCookie("user_info");

                tempcookie["user_type"] = def_user;
                tempcookie["username"] = "";
                tempcookie["user_id"] = "";
                tempcookie.Expires = DateTime.Now.AddDays(1);
                tempcookie.Path = Request.ApplicationPath;

            }
            else
            {
                tempcookie["user_type"] = def_user;
                tempcookie["username"] = "";
                tempcookie["user_id"] = "";
                tempcookie.Expires = DateTime.Now.AddDays(1);
                tempcookie.Path = Request.ApplicationPath;
            }


            Response.Cookies.Add(tempcookie);


            Response.Redirect("Default.aspx");
        }
    }
}