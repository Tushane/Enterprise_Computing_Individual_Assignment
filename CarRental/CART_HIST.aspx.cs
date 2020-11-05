using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class CART_HIST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<cart_info> _cart_hist = new List<cart_info>();

            string query = "SELECT * FROM DBO.VIEW_CART_HISTORY('" + get_user_id() + "')";
            DatabaseConnection con = new DatabaseConnection();
            SqlDataReader reader = con.readSqlData(query);

            while (reader.Read())
            {
                try
                {
                    cart_info ci = new cart_info(reader[0].ToString(), reader[1].ToString(), reader[5].ToString(), reader[4].ToString(), reader[6].ToString(), DateTime.Parse(reader[2].ToString()), DateTime.Parse(reader[3].ToString()), DateTime.Parse(reader[7].ToString()), reader[8].ToString(), reader[9].ToString(), reader[10].ToString());

                    _cart_hist.Add(ci);

                    temp_cart_data.Visible = false;

                }
                catch (Exception ex)
                {
                    temp_cart_data.InnerHtml = "UNABLE TO LOAD CART HISTORY PLEASE RELOAD THE PAGE";
                }

            }

            if (_cart_hist != null)
            {

                foreach (cart_info ci in _cart_hist)
                {

                    proPageGen _gen = new proPageGen();

                    main.Controls.Add(_gen.generate_break_down_page(ci, "CART_HISTORY"));

                    System.Web.UI.HtmlControls.HtmlGenericControl br = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    br.Attributes.Add("style", "height:50px; width:100%;");
                    br.InnerHtml = "<br/> <br/>";


                    main.Controls.Add(br);

                }

            }
            else
            {
                temp_cart_data.InnerHtml = "<h1>YOUR CURRENTLY HAVE NO PURCHASE COMPLETED!</h1>";
            }

        }


        private string get_user_id()
        {
            HttpCookie cookie = Request.Cookies["user_info"];

            if (cookie != null)
            {
                return cookie["user_id"];
            }

            return "user_not_found";
        }
    }
}