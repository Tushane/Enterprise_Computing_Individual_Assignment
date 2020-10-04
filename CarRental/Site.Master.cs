using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string id = "0";
            //string cust_id = "0939843";
            //SelectedCars temp = new SelectedCars();

            //System.Web.UI.HtmlControls.HtmlGenericControl newdiv = null;


            //temp.set_prod_ids(id);

            //CustCart _cart = new CustCart();

            //_cart.Add_car(temp);
            //_cart.set_cust_id(cust_id);

            //proPageGen webgen = new proPageGen();

            //if (_cart != null)
            //{
            //    items.Controls.Add(webgen.Cart_generate(_cart));
            //}
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }

       
        protected void Add_to_cart_Click(object sender, ImageClickEventArgs e)
        {
            string id = "0";
            string cust_id = "0939843";
            SelectedCars temp = new SelectedCars();

           temp.set_prod_ids(id);

            CustCart _cart = new CustCart();

            if (_cart.Get_custid() == null)
            {
                _cart.set_cust_id(cust_id);
            }
            _cart.Add_car(temp);
           

            proPageGen webgen = new proPageGen();

            if (_cart != null)
            {
                items.Controls.Add(webgen.Cart_generate(_cart));
            }
        }
    }
}