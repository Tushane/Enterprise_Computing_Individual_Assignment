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


            foreach (Product data in all_data)
            {
                    proPageGen webgen = new proPageGen();

                    if (data != null)
                    {
                        Button dut = webgen.get_but();
                        dut.Click += new EventHandler(but_Click);
                        webgen.set_but(dut);
                        Calendar tempcal = webgen.get_start_cal();
                        tempcal.ID = "startcal_" + data.getId();
                        tempcal.SelectionChanged += new EventHandler(Calendar1_SelectionChanged);
                        webgen.set_start_cal(tempcal);


                        TextBox text_box = webgen.get_end_cal();

                        text_box.ID = "endcal_" + data.getId();
                        text_box.TextChanged += new EventHandler(TextBox_OnChanged);

                        webgen.set_end_cal(text_box);

                        maindiv.Controls.Add(webgen.generate(data, get_user()));
                    }
            }
        }

        protected void but_Click(object sender, EventArgs e)
        {

            HttpCookie main_load = Request.Cookies["main_load"];

            string prod_id = ((Button)sender).ID;

            if (main_load != null)
            {

                main_load.Value = "no";
                main_load.Path = Request.ApplicationPath;
                main_load.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Set(main_load);
            }

            if (get_user() == "ADMIN" || get_user() == "NON-ADMIN")
            {
                string[] sub_id = prod_id.Split('_');
                if (Request.Cookies["temp_days" + sub_id[1]] != null && Request.Cookies["temp_start_date" + sub_id[1]] != null)
                {

                    cart_info ci = new cart_info(sub_id[1], Request.Cookies["temp_days" + sub_id[1]].Value.ToString(), Request.Cookies["temp_start_date" + sub_id[1]].Value.ToString());

                    CartManager cdm = new CartManager();

                    string response = cdm.AddProdToCartSQL(ci, get_user_id());

                    if (response == "SUCCESSFULLY_ADDED_TO_CART")
                    {
                        Response.Cookies["temp_days" + sub_id[1]].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["start_cal" + sub_id[1]].Expires = DateTime.Now.AddDays(-1);
                    }


                }
            }
            else
            {
                default_user_button_click(sender);
            }

            Response.Redirect(Request.Url.AbsolutePath);

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

                    if (response != "SUCCESSFULLY_UPDATE")
                    {
                        HttpCookie temp_date = new HttpCookie("temp_start_date" + sub_id[1]);
                        temp_date.Value = temp.SelectedDate.Date.ToString("d");
                        temp_date.Path = Request.ApplicationPath;
                        temp_date.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(temp_date);
                    }

                    Response.Redirect(Request.Url.AbsolutePath);
                }
                else
                {
                    default_user_calendar_onchanged(temp);
                }

            }
            catch (HttpException ecd)
            {
                Response.Redirect("ErrorPage.aspx");
            }
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
                else
                {
                    default_user_textbox_onchanged(box);
                }

                Response.Redirect(Request.Url.AbsolutePath);

            }
        }

        protected void but_clear_cart(object sender, EventArgs e)
        {
            if (get_user() == "ADMIN" || get_user() == "NON-ADMIN")
            {
                CartManager cm = new CartManager();

                cm.clear_cart_sql(((Button)sender).ID);
            }
            else
            {
                default_clear_cart();
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

        //keeping defualt user functionality
        private void default_clear_cart()
        {
            Request.Cookies.Clear();

            if (Master.FindControl("remove").Visible == false)
            {
                Master.FindControl("remove").Visible = true;
            }
        }
        private void default_user_textbox_onchanged(TextBox box)
        {

            if (box != null)
            {
                string id = box.ID;
                string[] sub_id = id.Split('_');

                HttpCookie changePoint = Request.Cookies["cart_info" + sub_id[1]];


                if (changePoint != null)
                {
                    changePoint["end_date"] = DateTime.Parse(changePoint["start_date"]).AddDays(double.Parse(box.Text)).ToString("d");
                    changePoint["amt_days"] = box.Text;
                    changePoint.Expires = DateTime.Now.AddYears(1);
                    changePoint.Path = Request.ApplicationPath;
                    Response.Cookies.Add(changePoint);

                }
                else
                {
                    try
                    {
                        HttpCookie temp_date1 = new HttpCookie("temp_end_date" + sub_id[1]);
                        double amt = double.Parse(box.Text);
                        if (Request.Cookies["temp_start_date" + sub_id[1]] != null)
                        {
                            DateTime temp = DateTime.Parse(Request.Cookies["temp_start_date" + sub_id[1]].Value);
                            temp_date1.Value = temp.Date.AddDays(amt).ToString("d");
                            temp_date1.Path = Request.ApplicationPath;
                            temp_date1.Expires = DateTime.Now.AddDays(1);
                        }
                        else
                        {
                            Response.Redirect("ErrorPage.aspx");
                        }


                        HttpCookie temp_days = new HttpCookie("temp_days" + sub_id[1]);

                        temp_days.Value = box.Text;
                        temp_days.Expires = DateTime.Now.AddDays(1); ;
                        temp_days.Path = Request.ApplicationPath;
                        Response.Cookies.Add(temp_days);
                        Response.Cookies.Add(temp_date1);
                    }
                    catch (HttpException ecd)
                    {
                        Response.Redirect("ErrorPage.aspx");
                    }
                }
            }
        }

        private void default_user_calendar_onchanged(Calendar temp)
        {
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

            }
            else
            {
                if (id.Contains("startcal"))
                {

                    changePoint["start_date"] = temp.SelectedDate.Date.ToString("d");
                    changePoint["end_date"] = DateTime.Parse(temp.SelectedDate.Date.ToString("d")).AddDays(double.Parse(Request.Cookies["temp_days" + sub_id[1]].Value)).Date.ToString("d");
                    changePoint.Expires = DateTime.Now.AddYears(1);
                    changePoint.Path = Request.ApplicationPath;
                    Response.Cookies.Add(changePoint);
                }

                Response.Redirect(Request.Url.AbsolutePath);
            }
        }

        private void default_user_button_click(object sender)
        {
            string path = Request.ApplicationPath;
            CartManager manageCart = new CartManager();
            HttpCookie _cart_size = manageCart.updateCartSize(Request.Cookies["cart_size"], path);
            Response.Cookies.Add(_cart_size);

            string[] sub_id = ((Button)sender).ID.Split('|');

            HttpCookie new_date = Request.Cookies["temp_start_date" + sub_id[3]];

            DateTime selected_pickup_date = new DateTime();
            DateTime selected_return_date = new DateTime();

            if (new_date != null)
            {
                selected_pickup_date = DateTime.Parse(new_date.Value);
                new_date.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(new_date);

            }


            new_date = Request.Cookies["temp_end_date" + sub_id[3]];

            HttpCookie days = Request.Cookies["temp_days" + sub_id[3]];

            string days_ = "";

            if (days != null)
            {
                days_ = days.Value;
            }

            if (new_date != null)
            {
                selected_return_date = DateTime.Parse(new_date.Value);
                new_date.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(new_date);
            }

            string[] data_fix = ((Button)sender).ID.Split('|');

            string data = (data_fix[0] + "|" + data_fix[1] + "|" + data_fix[2] + "|" + selected_pickup_date.ToString("d") + "|" + selected_return_date.ToString("d") + "|" + days_);

            HttpCookie cart_info = manageCart.AddProdToCart(data, path, _cart_size["amount"]);

            HttpCookie key = new HttpCookie("cart_key" + _cart_size["amount"]);
            key["cart_placement"] = _cart_size["amount"];
            key["id"] = sub_id[3];
            key.Path = Request.ApplicationPath;
            key.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Add(key);
            Response.Cookies.Add(cart_info);
        }
    }
}