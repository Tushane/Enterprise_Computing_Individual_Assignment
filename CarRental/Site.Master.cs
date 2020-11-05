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
        //private static List<HttpCookie> cart_info = new List<HttpCookie>();

        private static bool clear = false; 

        protected void Page_Load(object sender, EventArgs e)
        {

            HttpCookie main_load = Request.Cookies["main_load"];

            if (main_load == null)
            {
                main_load = new HttpCookie("main_load");
                main_load.Value = "yes";
                main_load.Path = Request.ApplicationPath;
                main_load.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(main_load);
            }

            // clear = false;
            if (main_load.Value == "yes")
            {
                proPageGen webgen = new proPageGen();
                items.Controls.Add(webgen.Cart_generate("Cart Empty", "remove"));
                remove_cookie();
            }
            else 
            {
                proPageGen webgen = new proPageGen();
                items.Controls.Add(webgen.Cart_generate("Cart Empty", "remove"));
                Load_Cart();
             };

        }


        protected void but_clear_cart(object sender, EventArgs e)
        { 

            //if (FindControl("remove").Visible == false)
            //{
            //    FindControl("remove").Visible = true;
            //}

            HttpCookie main_load = Request.Cookies["main_load"];

            if (main_load != null)
            {
                // main_load = new HttpCookie("main_load");
                main_load.Value = "yes";
                Response.Cookies.Remove("main_load");
                main_load.Path = Request.ApplicationPath;
                main_load.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(main_load);
            }


            remove_cookie();
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
                    HttpCookie bybye = Request.Cookies["cart_info" + i.ToString()];
                    //Request.Cookies["temp_start_date" + i.ToString()].Expires = DateTime.Now.AddDays(-1);
                    //Request.Cookies["temp_end_date" + i.ToString()].Expires = DateTime.Now.AddDays(-1);
                    if (bybye != null)
                    {
                        bybye.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(bybye);
                    }
                }

                temp.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(temp);
            }

          
        }

        protected void Load_Cart()
        {
            if (FindControl("remove").Visible != false)
            {
                FindControl("remove").Visible = false;
            }


            int stop = 0;

            proPageGen webgen = new proPageGen();


            HttpCookie _cart_size = Request.Cookies["cart_size"];
            if (_cart_size != null)
            {

                string amt = _cart_size["amount"];
                stop = int.Parse(amt);

            }


            if (stop > 0)
            {



                decimal _total = 0;

                for (int i = 1; i <= stop; i++)
                {
                    HttpCookie _cart = Request.Cookies["cart_info" + i.ToString()];

                    if (_cart != null)
                    {
                        
                        string prod_name = "<p>Name: " + _cart["prod_name"] + "<p>";
                        string prod_cost = "<p>Unit Cost: USD$" + _cart["prod_cost"] + "<p>";
                        string prod_id = _cart["prod_id"];
                        string prod_pick_up_date = "<p>Pick Up Date: " + _cart["start_date"] + "<p>";
                        string prod_return_date = "<p>Return Date: " + _cart["end_date"] + "<p>";
                        int amt_days = int.Parse((DateTime.Parse(_cart["end_date"]) - DateTime.Parse(_cart["start_date"])).TotalDays.ToString());
                        decimal prod_sub_tot = (Decimal.Parse(_cart["prod_cost"]) * amt_days);
                        string prod_sub = "<p>Sub-Total: " + prod_sub_tot.ToString() + "<p>";
                        string data = prod_name + prod_cost + prod_pick_up_date + prod_return_date + prod_sub;

                        _total += prod_sub_tot;

                       items.Controls.Add(webgen.Cart_generate(data, prod_id));

                        Button button1 = new Button();
                        button1.Text = "DELETE";
                        button1.CssClass = "btn btn-default";
                        button1.ID = "cart_info" + i.ToString();
                        button1.Attributes.Add("Style", "text-align:center;");
                        button1.Click += new EventHandler(but_delete_item);
                        items.Controls.Add(button1);

                        System.Web.UI.HtmlControls.HtmlGenericControl newdiv1 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                        newdiv1.InnerHtml = "____________________________";
                        newdiv1.Attributes.Add("Style", "color:black;");
                        items.Controls.Add(newdiv1);
                    }
                }
               items.Controls.Add(webgen.Cart_generate("Total = $" + _total.ToString(), ""));

                System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                newdiv.Attributes.Add("class", "btn btn - default");
                newdiv.Attributes.Add("href", "");
                newdiv.InnerText = "CHECK OUT";
                items.Controls.Add(newdiv);

                // newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                Button button = new Button();
                button.Text = "CLEAR CART";
                button.CssClass = "btn btn-default";
                button.Attributes.Add("Style", "text-align:center;");
                button.Click += new EventHandler(but_clear_cart);
               items.Controls.Add(button);
            }
        }

        protected void but_delete_item(Object sender, EventArgs e)
        {
            Response.Cookies[((Button)sender).ID].Expires = DateTime.Now.AddDays(-1);
           
            HttpCookie temp = Request.Cookies["cart_size"];

            if (temp != null) {
              
                int cart_size = int.Parse(temp["amount"]);
                int amt_null = 0;

                for (int i = 1; i <= cart_size; i++)
                {
                    HttpCookie temp1 = Request.Cookies["cart_info" + i.ToString()];


                    if ( temp1 == null)
                    {
                        amt_null++;

                       // Response.Cookies["main_load"].Value = amt_null.ToString();

                    }
                }

                if (amt_null.ToString() == cart_size.ToString())
                {

                    temp.Expires = DateTime.Now.AddDays(-1);
                    temp.Path = Request.ApplicationPath;
                    Response.Cookies["main_load"].Value = "yes";
                    Response.Cookies.Set(temp);

                }

            }

            Response.Redirect(Request.Url.AbsolutePath);

        }

        //void Application_Error(Object sender, EventArgs e)
        //{
        //    Exception exc = Server.GetLastError();

        //    if (exc is HttpUnhandledException)
        //    {
        //        Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
        //    }

        //}

    }


}