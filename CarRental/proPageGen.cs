using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CarRental
{ 
    public class proPageGen
    {

        public System.Web.UI.HtmlControls.HtmlGenericControl generate(Product data)
        {
            

                System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                newdiv.Attributes.Add("class", "col-md-4");
                string prod_image = "<img style='width:90%; height=45%;' src='" + data.getIL() + "'/>";
                string price = "<p>" + "USD$" + data.getPrice().ToString("0.00") + "</p>";
                string description = "<p>" + data.getDescription() + "</p>";
                newdiv.InnerHtml = prod_image + price + description + "<p><a class='btn btn-default' runat=\"server\" onclick='press()'>ADD TO CART &raquo;</a> </p>";

                  return newdiv;
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Cart_generate(CustCart data)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");

            newdiv.Attributes.Add("class", "#");
            newdiv.InnerHtml = data.Get_custid();
            return newdiv;
        }
}
}
