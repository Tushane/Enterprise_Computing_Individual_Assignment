using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{ 
    public class proPageGen
    {

        private Button dut = new Button();

        public System.Web.UI.HtmlControls.HtmlGenericControl generate(Product data)
        {
            

                System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                newdiv.Attributes.Add("class", "col-md-4");
                string prod_image = "<img style='width:90%; height=45%;' src='" + data.getIL() + "'/>";
                string price = "<p>" + "USD$" + data.getPrice().ToString("0.00") + "</p>";
                string description = "<p>" + data.getDescription() + "</p>";

            System.Web.UI.HtmlControls.HtmlGenericControl newdiv1 = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
           
            dut.Text = "ADD TO CART >>";
            dut.CssClass = "btn btn-default";
            dut.ID = data.getId() + "|" + data.getPrice();
 
            newdiv1.Controls.Add(dut);
     
            newdiv.InnerHtml = prod_image + price + description;
            newdiv.Controls.Add(dut);

            return newdiv;
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Cart_generate(string data)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
            if(data == "Cart Empty")
            {
                newdiv.ID = "remove";
              
            }
            
                newdiv.InnerHtml = data;
            
            return newdiv;
        }

        public Button get_but() { return this.dut; }

        public void set_but(Button temp) { this.dut = temp; }
}
}
