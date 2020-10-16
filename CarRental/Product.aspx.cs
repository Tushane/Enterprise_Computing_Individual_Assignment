using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class Product1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductListGenerator gen = new ProductListGenerator();
            List<Product> all_data = gen.getProducts();
            int i = 0;

            foreach (Product data in all_data)
            {
                i++;
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

                        maindiv.Controls.Add(webgen.generate(data));
                }
            }
        }

        protected void but_Click(object sender, EventArgs e)
        {
            //try
            //{
            HttpCookie main_load = Request.Cookies["main_load"];

            if (main_load != null)
            {

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

            string data = (((Button)sender).ID + "|" + selected_pickup_date.ToString("d") + "|" + selected_return_date.ToString("d"));

            HttpCookie cart_info = manageCart.AddProdToCart(data, path, _cart_size["amount"]);
            Response.Cookies.Add(cart_info);

            Response.Redirect(Request.Url.AbsolutePath);
            //}
            //catch (Exception ecd)
            //{
            //    Response.Redirect("ErrorPage.aspx");
            //}

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Calendar temp = (Calendar)sender;
                string id = temp.ID;
                string[] sub_id = id.Split('_');

                HttpCookie changePoint = Request.Cookies["cart_info" + sub_id[1]];

                if (changePoint == null)
                {
                    if (id.Contains("startcal"))
                    {

                        HttpCookie temp_date = new HttpCookie("temp_start_date" + sub_id[1]);
                        temp_date.Value = temp.SelectedDate.Date.ToString("d");
                        temp_date.Path = Request.ApplicationPath;
                        temp_date.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(temp_date);


                    }
                    else
                    {

                        HttpCookie temp_date = new HttpCookie("temp_end_date" + sub_id[1]);
                        temp_date.Value = temp.SelectedDate.Date.ToString("d");
                        temp_date.Path = Request.ApplicationPath;
                        temp_date.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(temp_date);

                    }
                }
                else
                {
                    if (id.Contains("startcal"))
                    {

                        changePoint["start_date"] = temp.SelectedDate.Date.ToString("d");
                        changePoint.Expires = DateTime.Now.AddYears(1);
                        changePoint.Path = Request.ApplicationPath;
                        Response.Cookies.Add(changePoint);

                    }
                    else
                    {

                        changePoint["end_date"] = temp.SelectedDate.Date.ToString("d");
                        changePoint.Expires = DateTime.Now.AddYears(1);
                        changePoint.Path = Request.ApplicationPath;
                        Response.Cookies.Add(changePoint);

                    }

                    Response.Redirect(Request.Url.AbsolutePath);
                }
            }
            catch (HttpException ecd)
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }

        protected void but_clear_cart(object sender, EventArgs e)
        {
            Request.Cookies.Clear();
            //Response.Redirect("Defaut.aspx");

            if (Master.FindControl("remove").Visible == false)
            {
                Master.FindControl("remove").Visible = true;
            }
        }

        //protected void Load_Cart()
        //{
        //    int stop = 0;

        //    proPageGen webgen = new proPageGen();


        //    HttpCookie _cart_size = Request.Cookies["cart_size"];
        //    if (_cart_size != null)
        //    {

        //        string amt = _cart_size["amount"];
        //        stop = int.Parse(amt);

        //    }


        //    if (stop > 0)
        //    {



        //        decimal _total = 0;

        //        for (int i = 1; i <= stop; i++)
        //        {
        //            HttpCookie _cart = Request.Cookies["cart_info" + i.ToString()];

        //            if (_cart != null)
        //            {
        //                string data = _cart["prod_name"] + "|" + _cart["prod_cost"];

        //                _total += Decimal.Parse(_cart["prod_cost"]);

        //                Master.FindControl("items").Controls.Add(webgen.Cart_generate(data, prod_id));
        //            }
        //        }

        //        System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //        newdiv.InnerHtml = "__________________";
        //        newdiv.Attributes.Add("Style", "color:black;");


        //        Master.FindControl("items").Controls.Add(newdiv);
        //        Master.FindControl("items").Controls.Add(webgen.Cart_generate("Sub_Total = $" + _total.ToString()));

        //        newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
        //        newdiv.Attributes.Add("class", "btn btn - default");
        //        newdiv.Attributes.Add("href", "");
        //        newdiv.InnerText = "CHECK OUT";
        //        Master.FindControl("items").Controls.Add(newdiv);

        //        newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
        //        Button button = new Button();
        //        button.Text = "CLEAR CART";
        //        button.CssClass = "btn btn-default";
        //        button.Click += new EventHandler(but_clear_cart);
        //        Master.FindControl("items").Controls.Add(button);
        //    }
        //}
    }
}
