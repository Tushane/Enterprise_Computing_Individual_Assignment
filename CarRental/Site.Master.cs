using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class SiteMaster : MasterPage
    {
        static string def_user = "default";
        protected void Page_Load(object sender, EventArgs e)
        {
            user_login_check();
            update_nav_iu();
            

            HttpCookie main_load = Request.Cookies["main_load"];

            if (main_load == null)
            {
                main_load = new HttpCookie("main_load");
                main_load.Value = "yes";
                main_load.Path = Request.ApplicationPath;
                main_load.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(main_load);
            }


            if (main_load.Value == "yes")
            {
                proPageGen webgen = new proPageGen();
                items.Controls.Add(webgen.Cart_generate("Cart Empty", "remove"));
                remove_cookie();
                myDropdown.Attributes.CssStyle.Add("height", "100%");
            }
            else
            {
                proPageGen webgen = new proPageGen();
                Load_Cart();
                myDropdown.Attributes.CssStyle.Add("height", "520%");
            };

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
            else
            {
                remove_cookie();

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

        private void remove_cookie()
        {
            HttpCookie temp = Request.Cookies["cart_size"];

            if (temp != null)
            {
                int size = int.Parse(temp["amount"]);

                for (int i = 1; i <= size; i++)
                {
                    HttpCookie key = Request.Cookies["cart_key" + i.ToString()];

                    if (key != null)
                    {
                        HttpCookie bybye = Request.Cookies["cart_info" + key["id"]];

                        if (bybye != null)
                        {
                            bybye.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(bybye);
                        }
                    }
                }

                temp.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(temp);
            }


        }

        protected void Load_Cart()
        {

                proPageGen _gen = new proPageGen();

                if (get_user() == def_user)
                {

                    default_user_gen(_gen);

                }
                else
                {

                    CartManager cdm = new CartManager();

                    List<cart_info> cart = cdm.retrieved_cart_data(get_user_id());

                    custom_user_gen(_gen, cart);

                }



                System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                newdiv.Attributes.Add("class", "btn btn - default");
                newdiv.Attributes.Add("style", "background-color:white; border: 1px;");

                if (get_user() == def_user)
                {
                    newdiv.Attributes.Add("href", "LOGIN.aspx");
                    newdiv.InnerText = "LOGIN TO CHECKOUT";
                }
                else
                {
                    newdiv.Attributes.Add("href", "EXTEND_CART.aspx");
                    newdiv.InnerText = "VIEW ALL ITEMS";
                }
                items.Controls.Add(newdiv);


                Button button = new Button();
                button.Text = "CLEAR CART";
                button.ID = get_user_id();
                button.CssClass = "btn btn-default";
                button.Attributes.Add("Style", "text-align:center;");
                button.Click += new EventHandler(but_clear_cart);
                items.Controls.Add(button);

        }

        protected void but_delete_item(Object sender, EventArgs e)
        {
            HttpCookie tempcookie = Request.Cookies["user_info"];
            string response = " ";

            if (tempcookie != null)
            {
                if (tempcookie["user_type"] == def_user)
                {
                    Response.Cookies[((Button)sender).ID].Expires = DateTime.Now.AddDays(-1);

                }
                else
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

        protected void user_login_check()
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
                Response.Cookies.Add(tempcookie);

            }
    
        }


        private void update_nav_iu()
        {
            HttpCookie tempcookie = Request.Cookies["user_info"];

            if (tempcookie != null)
            {
                if (tempcookie["user_type"] == "ADMIN")
                {

                    main.InnerHtml = "PRODUCT CONTROLLER";
                    main.HRef = "~/PRODUCT_CONTROLLER";
                    prod.InnerHtml = "VIEW PRODUCT";
                    apply.InnerHtml = "LOGOUT";

                    info.Visible = false;
                    con.Visible = false;

                    apply.HRef = "~/LOGOUT";

                    text1.InnerHtml = "Hi " + tempcookie["username"];
                    text1.HRef = "";

                    c_his.Visible = true;

                }
                else if (tempcookie["user_type"] == "NON-ADMIN")
                {

                    main.InnerHtml = "Home";
                    main.HRef = "~/Default";
                    prod.InnerHtml = "Rentals";
                    apply.InnerHtml = "LOGOUT";

                    info.Visible = true;
                    con.Visible = true;

                    apply.HRef = "~/LOGOUT";

                    text1.InnerHtml = "Hi " + tempcookie["username"];
                    text1.HRef = "";

                    c_his.Visible = true;

                }
                else
                {
                    main.InnerHtml = "Home";
                    main.HRef = "~/Default";
                    prod.InnerHtml = "Rentals";
                    apply.InnerHtml = "APPLY NOW!";

                    info.Visible = true;
                    con.Visible = true;

                    apply.HRef = "~/apply";

                    text1.InnerHtml = "LOGIN";
                    text1.HRef = "~/LOGIN";

                    c_his.Visible = false;
                }
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

        //default user cart load
        protected void default_user_gen(proPageGen _gen)
        {
            int stop = 2;
            int cart_size = 0;


            HttpCookie _cart_size = Request.Cookies["cart_size"];
            if (_cart_size != null)
            {

                string amt = _cart_size["amount"];
                cart_size = int.Parse(amt);

            }


            if (cart_size > 0)
            {

                decimal _total = 0;

                for (int i = 1; i <= stop; i++)
                {
                    HttpCookie key = Request.Cookies["cart_key" + i];
                    string cart_code = "";

                    if (key != null)
                    {
                        cart_code = key["id"];
                    }

                    HttpCookie _cart = Request.Cookies["cart_info" + cart_code];

                    if (_cart != null)
                    {

                        string prod_name = "<p>Name: " + _cart["prod_name"] + "<p>";
                        string prod_cost = "<p>Unit Cost: USD$" + _cart["prod_cost"] + "<p>";
                        string prod_id = _cart["prod_id"];
                        string prod_pick_up_date = "<p>Pick Up Date: " + _cart["start_date"] + "<p>";
                        string prod_return_date = "<p>Return Date: " + _cart["end_date"] + "<p>";
                        int amt_days = int.Parse(_cart["amt_days"]);
                        decimal prod_sub_tot = (Decimal.Parse(_cart["prod_cost"]) * amt_days);
                        string prod_sub = "<p>Sub-Total: USD$" + prod_sub_tot.ToString() + "<p>";
                        string data = prod_name + prod_cost + prod_pick_up_date + prod_return_date + prod_sub;

                        _total += prod_sub_tot;

                        items.Controls.Add(_gen.Cart_generate(data, prod_id));

                        Button button1 = new Button();
                        button1.Text = "DELETE";
                        button1.CssClass = "btn btn-default";
                        button1.ID = "cart_info" + prod_id;
                        button1.Attributes.Add("Style", "text-align:center;");
                        button1.Click += new EventHandler(but_delete_item);
                        items.Controls.Add(button1);

                        System.Web.UI.HtmlControls.HtmlGenericControl newdiv1 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                        newdiv1.InnerHtml = "____________________________";
                        newdiv1.Attributes.Add("Style", "color:black;");
                        items.Controls.Add(newdiv1);
                    }
                }

                items.Controls.Add(_gen.Cart_generate("Total = USD$" + _total.ToString(), ""));
            }

        }

        //custom user cart load 
        protected void custom_user_gen(proPageGen _gen, List<cart_info> cart)
        {
            int stop = 2;
            int count = 0;
            int cart_size = cart.Count();
            string cur = " ";

            if (cart_size > 0)
            {

                decimal _total = 0;

                foreach (cart_info _cart in cart)
                {
                    count++;
                    if (count <= stop)
                    {
                        if (_cart != null)
                        {
                            cur = _cart.get_currency();

                            string prod_name = "<p>Name: " + _cart.get_prod_name() + "<p>";
                            string prod_pick_up_date = "<p>Pick Up Date: " + DateTime.Parse(_cart.get_pick_up_date()).ToString("d") + "<p>";
                            string prod_return_date = "<p>Return Date: " + DateTime.Parse(_cart.get_returned_date()).ToString("d") + "<p>";
                            string prod_sub = "<p>Sub-Total: " + _cart.get_currency() + "$" + _cart.get_price() + "<p>";
                            string data = prod_name + prod_pick_up_date + prod_return_date + prod_sub;

                            _total += Decimal.Parse(_cart.get_price());

                            items.Controls.Add(_gen.Cart_generate(data, _cart.get_id()));

                            Button button1 = new Button();
                            button1.Text = "DELETE";
                            button1.CssClass = "btn btn-default";
                            button1.ID = "cartinfo_" + _cart.get_id();
                            button1.Attributes.Add("Style", "text-align:center;");
                            button1.Click += new EventHandler(but_delete_item);
                            items.Controls.Add(button1);

                            System.Web.UI.HtmlControls.HtmlGenericControl newdiv1 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            newdiv1.InnerHtml = "____________________________";
                            newdiv1.Attributes.Add("Style", "color:black;");
                            items.Controls.Add(newdiv1);
                        }
                    }
                }

                items.Controls.Add(_gen.Cart_generate("Total = " + cur + "$" + _total.ToString(), ""));
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