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
        TextBox num_of_days = new TextBox();
        private Label title = new Label();
        Button delete = new Button();

        public System.Web.UI.HtmlControls.HtmlGenericControl generate(Product data, string user_type)
        {


            System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            newdiv.Attributes.Add("Style", "border:1px; border-color:blue; padding-bottom:2%");
            newdiv.Attributes.Add("class", "col-md-4");
            newdiv.ID = "cart_info_" + data.getId();
            string prod_image = "<img style='width:90%; height=45%;' src='" + data.getIL() + "'/>";
            string price = "<p>" + "Unit Cost: " + data.getProdCur() + "$" + data.getPrice().ToString("0.00") + "</p>";
            string prod_name = "<p> Vechile Name: " + data.getProdName() + "<p>";
            string description = "<p> Vechile Description: " + data.getDescription() + "</p>";


            dut.Text = "ADD TO CART >>";
            dut.CssClass = "btn btn-default";
            if (user_type == "default")
            {
                dut.ID = data.getId() + "|" + data.getPrice() + "|" + data.getProdName() + "|" + data.getId();
            }
            else
            {
                dut.ID = "cart_" + data.getId();
            }

            newdiv.InnerHtml = prod_image + prod_name + price + description + "<br/> Select Pickup Date: <br/>";
            newdiv.Controls.Add(start_cal);



            title.Text = "<br> Enter the Amount of Days: ";
            num_of_days.Attributes.Add("style", "width:20%");
            num_of_days.AutoPostBack = true;

            num_of_days.Text = "0";
            newdiv.Controls.Add(title);
            newdiv.Controls.Add(num_of_days);
            newdiv.Controls.Add(dut);

            return newdiv;
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Cart_generate(string data, string id)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

            newdiv.ID = id;
            newdiv.Attributes.Add("class", "cart_content");

            if (id == "remove")
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

        public TextBox get_end_cal()
        {
            return this.num_of_days;
        }

        public void set_end_cal(TextBox temp)
        {
            this.num_of_days = temp;
        }

        public void set_delete(Button del)
        {
            this.delete = del;
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl generate_break_down_page(cart_info ci, string page_name)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl temp = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl image_holder = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl details_holder = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");


            if (page_name == "CART_HISTORY")
            {
                temp.Attributes.Add("style", "border:3px; width:100%; height:350px");
            }
            else
            {
                temp.Attributes.Add("style", "border:3px; width:100%; height:100%");
            }

            Label but_title = new Label();

            temp.ID = "order_" + ci.get_id();
            temp.Attributes.Add("class", "casing");

            image_holder.InnerHtml = "<img style='width:100%; height:100%;' src='" + ci.get_prod_image() + "'/>";
            image_holder.Attributes.Add("style", "height:100%; width:40%; float:left; background-color:blue;");
            details_holder.Attributes.Add("style", "height:100%; width:60%; float:left;");

            string title = "<p> ORDER ID#: " + ci.get_id() + "</p>";
            string prod_name = "<p> VECHILE NAME: " + ci.get_prod_name() + "</p>";
            string prod_description = "<p> VEHICLE DESCRIPTION: " + ci.get_prod_desc() + "</p>";
            string end_day = "<p> VEHICLE RETURN DATE: " + ci.get_returned_date() + "</p>";
            string unit_cost = "<p> UNIT COST: " + ci.get_currency() + "$" + ci.get_unit_cost() + "</p>";

            if (page_name == "CART_HISTORY")
            {
                string start_day = "<p> VEHICLE PICK UP DATE: " + ci.get_pick_up_date() + "</p>";
                string purchased_date = "<p> NUMBER OF DAYS USED: " + ci.get_purchased_date() + "</p>";
                string days = "<p> NUMBER OF DAYS USED: " + ci.get_num_days() + "</p>";
                string total = "<p> GRAND_TOTAL: " + ci.get_currency() + "$" + ci.get_price() + "</p>";
                details_holder.InnerHtml = title + prod_name + prod_description + unit_cost + days + start_day + end_day + total + purchased_date;
            }
            else
            {

                System.Web.UI.HtmlControls.HtmlGenericControl sub = new System.Web.UI.HtmlControls.HtmlGenericControl("P");

                sub.InnerText = "SUB_TOTAL: " + ci.get_price();

                details_holder.InnerHtml = title + prod_name + prod_description;

                but_title.Text = "<br> Enter the Amount of Days: ";
                details_holder.Controls.Add(but_title);
                num_of_days.Attributes.Add("style", "width:20%");
                num_of_days.AutoPostBack = true;
                num_of_days.Text = ci.get_num_days();
                details_holder.Controls.Add(num_of_days);

                start_cal.SelectedDate = ci.get_pickup_date_con();
                details_holder.Controls.Add(start_cal);

                details_holder.Controls.Add(sub);

                //dut.Text = "UPDATE ORDER >>";
                //dut.CssClass = "btn btn-default";
                //dut.ID = ci.get_id();

                //details_holder.Controls.Add(dut);

                delete.Text = "DELETE ORDER";
                delete.CssClass = "btn btn-default";
                delete.ID = "del_" + ci.get_prod_id();

                details_holder.Controls.Add(delete);
            }


            temp.Controls.Add(image_holder);
            temp.Controls.Add(details_holder);

            return temp;
        }
    }
}
