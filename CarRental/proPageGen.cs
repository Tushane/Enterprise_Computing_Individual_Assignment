using Microsoft.Ajax.Utilities;
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
        private Calendar start_cal = new Calendar();
        private Calendar end_cal = new Calendar();
        private Label title = new Label();
        private Label error = new Label();

        public System.Web.UI.HtmlControls.HtmlGenericControl generate(Product data)
        {
            

                System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                newdiv.Attributes.Add("Style", "border:1px; border-color:blue; padding-bottom:2%");
                newdiv.Attributes.Add("class", "col-md-4");
                newdiv.ID = data.getId();
                string prod_image = "<img style='width:90%; height=45%;' src='" + data.getIL() + "'/>";
                string price = "<p>" + "Unit Cost: USD$" + data.getPrice().ToString("0.00") + "</p>";
                string prod_name = "<p> Vechile Name: " + data.getProdName() + "<p>"; 
                string description = "<p> Vechile Description: " + data.getDescription() + "</p>";

            //System.Web.UI.HtmlControls.HtmlGenericControl newdiv1 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
           
            

            dut.Text = "ADD TO CART >>";
            dut.CssClass = "btn btn-default";
            dut.ID = data.getId() + "|" + data.getPrice() + "|" + data.getProdName();
 
            //newdiv1.Controls.Add(dut);
     
            newdiv.InnerHtml = prod_image + prod_name + price + description + "<br/> Select Pickup Date: <br/>";
            //newdiv.Controls.Add(newdiv1);
            newdiv.Controls.Add(start_cal);

            title.Text = "Select Return Date: ";
            newdiv.Controls.Add(title);
            newdiv.Controls.Add(end_cal);
            newdiv.Controls.Add(dut);

            return newdiv;
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Cart_generate(string data, string id)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
           
            newdiv.ID = id;
            newdiv.Attributes.Add("class", "cart_content");

            if(id == "remove")
            {
                newdiv.Attributes.CssStyle.Add("text-align", "center");
            }
              
            
           newdiv.InnerHtml = data;
            
            return newdiv;
        }

        public Button get_but() { return this.dut; }

        public void set_but(Button temp) { this.dut = temp; }

        public Calendar get_start_cal()
        {
            return this.start_cal;
        }

        public void set_start_cal(Calendar temp)
        {
            this.start_cal = temp;
        }

        public Calendar get_end_cal()
        {
            return this.end_cal;
        }

        public void set_end_cal(Calendar temp)
        {
            this.end_cal = temp;
        }


        public void setError(Label temp)
        {
            this.error = temp;
        }

        public Label getError()
        {
            return this.error;
        }
    }
}
