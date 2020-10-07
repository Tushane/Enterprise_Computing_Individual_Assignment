using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            ProductListGenerator gen = new ProductListGenerator();
            List<Product> all_data = gen.getProducts();
            int i = 0;

            foreach (Product data in all_data)
            {
                i++;
                if (i < 4)
                {
                    proPageGen webgen = new proPageGen();

                    if (data != null)
                    {
                        Button dut = webgen.get_but();
                        dut.Click += new EventHandler(but_Click);
                        webgen.set_but(dut);
                        Calendar tempcal = webgen.get_start_cal();
                        tempcal.ID = "startcal_" + (i).ToString();
                        tempcal.SelectionChanged += new EventHandler(Calendar1_SelectionChanged);
                        webgen.set_start_cal(tempcal);

                        tempcal = webgen.get_end_cal();
                        tempcal.ID = "endcal_" + (i).ToString();
                        tempcal.SelectionChanged += new EventHandler(Calendar1_SelectionChanged);
                        webgen.set_end_cal(tempcal);

                        maindivs.Controls.Add(webgen.generate(data));
                    }
                }
            }
        }

        protected void but_Click(object sender, EventArgs e)
        {

            HttpCookie main_load = Request.Cookies["main_load"];

            if (main_load != null)
            {
               // main_load = new HttpCookie("main_load");
                main_load.Value = "no";
                main_load.Path = Request.ApplicationPath;
                main_load.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(main_load);
            }

            string path = Request.ApplicationPath;
            CartManager manageCart = new CartManager();
            HttpCookie _cart_size = manageCart.updateCartSize(Request.Cookies["cart_size"], path);
            Response.Cookies.Add(_cart_size);

            HttpCookie new_date = Request.Cookies["temp_start_date" + _cart_size["amount"]];

            DateTime selected_pickup_date = new DateTime();
            DateTime selected_return_date = new DateTime();

            if (new_date != null)
            {
                selected_pickup_date = DateTime.Parse(new_date.Value);
                new_date.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(new_date);
               
            }
          

            new_date = Request.Cookies["temp_end_date" + _cart_size["amount"]];

            if (new_date != null)
            {
                selected_return_date = DateTime.Parse(new_date.Value);
                new_date.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(new_date);
            }

            string data = (((Button)sender).ID + "|" + selected_pickup_date.ToString() + "|" + selected_return_date.ToString());

            HttpCookie cart_info = manageCart.AddProdToCart(data, path, _cart_size["amount"]);
            Response.Cookies.Add(cart_info);

            //Load_Cart();

            Response.Redirect("Default.aspx");

        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Calendar temp = (Calendar)sender;
            string id = temp.ID;
            string[] sub_id = id.Split('_');

            if (id.Contains("startcal"))
            {
               //string[] sub_id = id.Split('_');
                HttpCookie temp_date = new HttpCookie("temp_start_date" + sub_id[1]);
                temp_date.Value = temp.SelectedDate.Date.ToString();
                temp_date.Path = Request.ApplicationPath;
                temp_date.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(temp_date);
               

            }
            else
            {
               // string[] sub_id = id.Split('_');
                HttpCookie temp_date = new HttpCookie("temp_end_date" + sub_id[1]);
                temp_date.Value = temp.SelectedDate.Date.ToString();
                temp_date.Path = Request.ApplicationPath;
                temp_date.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(temp_date);
               
            }

            Response.Redirect("Default.aspx");

            //if (Request.Cookies["cart_info" + sub_id[1]] != null)
            //{
            //    Load_Cart();
            //}
        }

        protected void but_clear_cart(object sender, EventArgs e)
        {

            if (Master.FindControl("remove").Visible == false)
            {
                Master.FindControl("remove").Visible = true;
            }

            HttpCookie temp = Request.Cookies["cart_size"];

            if (temp != null)
            {
                int size = int.Parse(temp["amount"]);

                for (int i = 1; i <= size; i++)
                {
                    HttpCookie bybye = Request.Cookies["cart_info" + i.ToString()];
                    bybye.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(bybye);
                }

            }

            temp.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(temp);


        }

        protected void Load_Cart()
        {
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
                        //System.Web.UI.HtmlControls.HtmlGenericControl data = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
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

                        Master.FindControl("items").Controls.Add(webgen.Cart_generate(data, prod_id));
                    }
                }

                System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("HR");
                //newdiv.InnerHtml = "<hr/>";
                newdiv.Attributes.Add("Style", "color:black;");

                //newdiv.ID = "line";
                Master.FindControl("items").Controls.Add(newdiv);
                Master.FindControl("items").Controls.Add(webgen.Cart_generate("Total = $" + _total.ToString(), ""));

                newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                newdiv.Attributes.Add("class", "btn btn - default");
                newdiv.Attributes.Add("href", "");
                newdiv.InnerText = "CHECK OUT";
                Master.FindControl("items").Controls.Add(newdiv);

                // newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                Button button = new Button();
                button.Text = "CLEAR CART";
                button.CssClass = "btn btn-default";
                button.Click += new EventHandler(but_clear_cart);
                Master.FindControl("items").Controls.Add(button);
            }
        }

    }
}