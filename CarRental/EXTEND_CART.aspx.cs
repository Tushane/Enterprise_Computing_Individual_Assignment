using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class EXTEND_CART : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CartManager con = new CartManager();
            List<cart_info> _cart = con.retrieved_cart_data(get_user_id());

            decimal grand_total = 0;
            string cur = "";

            if (_cart != null)
            {
                proPageGen _gen = new proPageGen();

                foreach (cart_info ci in _cart)
                {

                    TextBox box = new TextBox();
                    Calendar start_cal = new Calendar();
                    Button delete = new Button();

                    box.TextChanged += new EventHandler(TextBox_OnChanged);
                    start_cal.SelectionChanged += new EventHandler(Calendar1_SelectionChanged);
                    delete.Click += new EventHandler(but_delete_item);

                    box.ID = "bax_" + ci.get_prod_id();
                    start_cal.ID = "startcal_" + ci.get_prod_id();

                    if (_gen != null)
                    {
                        _gen.set_start_cal(start_cal);
                        _gen.set_end_cal(box);
                        _gen.set_delete(delete);

                        grand_total += decimal.Parse(ci.get_price());

                        cur = ci.get_currency();

                        main.Controls.Add(_gen.generate_break_down_page(ci, "CART_VIEW"));

                    }

                }

                System.Web.UI.HtmlControls.HtmlGenericControl br = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                br.InnerHtml = "<br/> <br/>";


                main.Controls.Add(br);

                System.Web.UI.HtmlControls.HtmlGenericControl newdiv1 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                newdiv1.InnerHtml = "__________________________________________________________________________________________";
                newdiv1.Attributes.Add("Style", "color:black;");
                main.Controls.Add(newdiv1);
                main.Controls.Add(_gen.Cart_generate("GRAND Total = " + cur + "$" + grand_total.ToString(), ""));

                Button clear = new Button();
                clear.Text = "CLEAR CART";
                clear.CssClass = "btn btn-default";
                clear.ID = get_user_id();
                clear.Click += new EventHandler(but_clear_cart);
                main.Controls.Add(clear);

                Button complete = new Button();
                complete.Text = "COMPLETE ORDER";
                complete.CssClass = "btn btn-default";
                complete.ID = "com_" + get_user_id();
                complete.Click += new EventHandler(complete_order);

                main.Controls.Add(complete);

            }

        }

        private string get_user()
        {
            HttpCookie cookie = Request.Cookies["user_info"];

            if (cookie != null)
            {
                return cookie["user_type"];
            }

            return "user_not_found";
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


        protected void TextBox_OnChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box != null)
            {
                if (get_user() == "ADMIN" || get_user() == "NON-ADMIN")
                {
                    CartManager cdm = new CartManager();

                    string id = box.ID;
                    string[] sub_id = id.Split('_');

                    string response = cdm.update_cart_sql(sub_id[1], get_user_id(), box.Text, "NUMBER_OF_DAYS");

                    if (response != "SUCCESSFULLY_UPDATE")
                    {
                        HttpCookie temp_days = new HttpCookie("temp_days" + sub_id[1]);

                        temp_days.Value = box.Text;
                        temp_days.Expires = DateTime.Now.AddDays(1); ;
                        temp_days.Path = Request.ApplicationPath;
                        Response.Cookies.Add(temp_days);
                    }
                }

                Response.Redirect(Request.Url.AbsolutePath);

            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Calendar temp = (Calendar)sender;

                if (get_user() == "ADMIN" || get_user() == "NON-ADMIN")
                {
                    CartManager cdm = new CartManager();

                    string id = temp.ID;
                    string[] sub_id = id.Split('_');

                    string response = cdm.update_cart_sql(sub_id[1], get_user_id(), temp.SelectedDate.Date.ToString(), "PICK_UP_DATE");

                    Response.Redirect(Request.Url.AbsolutePath);
                }

            }
            catch (HttpException ecd)
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }

        protected void but_delete_item(Object sender, EventArgs e)
        {
            HttpCookie tempcookie = Request.Cookies["user_info"];
            string response = " ";

            if (tempcookie != null)
            {
                if (tempcookie["user_type"] != "default")
                {
                    CartManager cm = new CartManager();

                    string[] sub_id = ((Button)sender).ID.Split('_');

                    response = cm.delete_from_cart_sql(sub_id[1], tempcookie["user_id"]);

                }

                if (response == "CART_EMPTY")
                {
                    HttpCookie main_load = Request.Cookies["main_load"];

                    if (main_load != null)
                    {
                        main_load.Value = "yes";
                        main_load.Path = Request.ApplicationPath;
                        main_load.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(main_load);
                    }
                    else
                    {
                        main_load = new HttpCookie("main_load");
                        main_load.Value = "yes";
                        main_load.Path = Request.ApplicationPath;
                        main_load.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(main_load);
                    }
                }

                Response.Redirect(Request.Url.AbsolutePath);
            }

        }

        protected void but_clear_cart(object sender, EventArgs e)
        {

            if (get_user() == "ADMIN" || get_user() == "NON-ADMIN")
            {
                CartManager cm = new CartManager();

                string response = cm.clear_cart_sql(((Button)sender).ID);

                if (response == "CART_EMPTY")
                {
                    HttpCookie main_load = Request.Cookies["main_load"];

                    if (main_load != null)
                    {
                        main_load.Value = "yes";
                        main_load.Path = Request.ApplicationPath;
                        main_load.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(main_load);
                    }
                    else
                    {
                        main_load = new HttpCookie("main_load");
                        main_load.Value = "yes";
                        main_load.Path = Request.ApplicationPath;
                        main_load.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(main_load);
                    }
                }
            }

            Response.Redirect(Request.Url.AbsolutePath);

        }

        protected void complete_order(object sender, EventArgs e)
        {
            string[] sub_id = ((Button)sender).ID.Split('_');

            if (get_user() == "ADMIN" || get_user() == "NON-ADMIN")
            {
                CartManager cm = new CartManager();

                string response = cm.complete_order(sub_id[1]);

                if (response == "ORDER_COMPLETED")
                {
                    HttpCookie main_load = Request.Cookies["main_load"];

                    if (main_load != null)
                    {
                        main_load.Value = "yes";
                        main_load.Path = Request.ApplicationPath;
                        main_load.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(main_load);
                    }
                    else
                    {
                        main_load = new HttpCookie("main_load");
                        main_load.Value = "yes";
                        main_load.Path = Request.ApplicationPath;
                        main_load.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(main_load);
                    }
                }
            }

            Response.Redirect("~/CART_HIST");

        }
    }
}