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
        protected void Page_Load(object sender, EventArgs e)
        {


            if (FindControl("remove") == null)
            {
                proPageGen webgen = new proPageGen();
                items.Controls.Add(webgen.Cart_generate("Cart Empty"));
            }


            Load_Cart();

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void but_clear_cart(object sender, EventArgs e)
        {
            Request.Cookies.Clear();
            //Response.Redirect("Defaut.aspx");

            if (FindControl("remove").Visible == false)
            {
                FindControl("remove").Visible = true;
            }
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
                FindControl("remove").Visible = false;


                decimal _total = 0;

                for (int i = 1; i <= stop; i++)
                {
                    HttpCookie _cart = Request.Cookies["cart_info" + i.ToString()];

                    if (_cart != null)
                    {
                        string data = _cart["prod_name"] + "|" + _cart["prod_cost"];

                        _total += Decimal.Parse(_cart["prod_cost"]);

                        items.Controls.Add(webgen.Cart_generate(data));
                    }
                }

                System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                newdiv.InnerHtml = "__________________";
                newdiv.Attributes.Add("Style", "color:black;");


                items.Controls.Add(newdiv);
                items.Controls.Add(webgen.Cart_generate("Sub_Total = $" + _total.ToString()));

                newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                newdiv.Attributes.Add("class", "btn btn - default");
                newdiv.Attributes.Add("href", "");
                newdiv.InnerText = "CHECK OUT";
               items.Controls.Add(newdiv);

                newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                Button button = new Button();
                button.Text = "CLEAR CART";
                button.CssClass = "btn btn-default";
                button.Click += new EventHandler(but_clear_cart);
                items.Controls.Add(button);
            }
        }


    }
}