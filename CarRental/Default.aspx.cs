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
            //try
            //{
                HttpCookie main_load = Request.Cookies["main_load"];

                    if (main_load != null)
                    {
                       
                        main_load.Value = "no";
                        main_load.Path = Request.ApplicationPath;
                        main_load.Expires = DateTime.Now.AddYears(1);
                        Request.Cookies.Remove("main_load");
                        Response.Cookies.Set(main_load);
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

                Response.Redirect("Default.aspx");
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

                if (changePoint == null) {
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

                //Response.Redirect("Default.aspx");

                //if (Request.Cookies["cart_info" + sub_id[1]] != null)
                //{
                //    Load_Cart();
                //}

            }
            catch (HttpException ecd)
            {
                Response.Redirect("ErrorPage.aspx");
            }
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


    }
}