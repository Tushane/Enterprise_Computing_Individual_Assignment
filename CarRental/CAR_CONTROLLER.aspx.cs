using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRental
{
    public partial class ADD_CARS : System.Web.UI.Page
    {
        private static List<Product> prod_list;
        private static bool visual_check = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (visual_check == true)
            {
                load_drop_down();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string name = " ";
            decimal unit_price = 0;
            string pro_desc = " ";

            string file_location = " ";
            string file_location1 = " ";
            string prodcurrency = prod_cur.Text;

            if (prod_name.Text != null)
            {
                name = prod_name.Text;
            }
            else
            {
                error.Text = "Please Enter an Name for the Vehicle Your Adding. ";
            }

            if (prod_price.Text != null)
            {
                unit_price = decimal.Parse(prod_price.Text);
            }
            else
            {
                if (error.Text != null)
                {
                    error.Text += "Also, You need to Enter an Unit Cost for the Vehicle";
                }
                else
                {
                    error.Text = "Please Enter an Unit Cost for the Vechicle";
                }
            }

            if (prod_desc.Text != null)
            {
                pro_desc = prod_desc.Text;
            }
            else
            {
                if (error.Text != null)
                {
                    error.Text += "Also, You need to Enter an Description for the Vehicle";
                }
                else
                {
                    error.Text = "Please Enter an Description for the Vehicle";
                }
            }

            if (prod_image.HasFile)
            {
                try
                {
               
                    file_location = Server.MapPath("~/images/") + prod_image.FileName;

                    file_location1 = "../images/" + prod_image.FileName;
                    prod_image.SaveAs(file_location);


                }
                catch(HttpException esx)
                {
                    if (error.Text != null)
                    {
                        error.Text += "Also, Unable to Upload File";
                        file_location = null;
                    }
                    else
                    {
                        error.Text = "Unable to Upload File";
                        file_location = null;
                    }
                }
            }
            else
            {
                if (error.Text != null)
                {
                    error.Text += "Also, You need to Upload an Image for the Vehicle";
                }
                else
                {
                    error.Text = "Please Upload an Image for the Vehicle";
                }
                file_location = null;
            }

            if (name != null && prod_cur != null && unit_price != 0 && file_location1 != null && pro_desc != null)
            {
                //error.Text = file_location;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "ADD_PRODUCTS_TO_WEBSITE";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRODUCT_NAME", name);
                cmd.Parameters.AddWithValue("@PRODUCT_CURRENCY", prodcurrency);
                cmd.Parameters.AddWithValue("@PRODUCT_DESC", pro_desc);
                cmd.Parameters.AddWithValue("@PRODUCT_COST", unit_price);
                cmd.Parameters.AddWithValue("@IMAGE_LOCATION", file_location1);

                DatabaseConnection con = new DatabaseConnection();
                string response = " ";

                try
                {

                    response = con.insertData(cmd);

                }
                catch (Exception ex)
                {
                    response = "Failed to Add Product to Website, Logout and Login then try again. However if the issue persist contact Tech Support for a assistance.";
                }

                error.Text = response;
            }
        }

        protected void add_check(object sender, EventArgs e)
        {
            DropDownList1.Visible = false;

  
           Button2.Visible = false;
           Button3.Visible = true;
           DropDownList1.Visible = false;
           image.Visible = false;
            Button1.Visible = true;
            Button4.Visible = false;
            visual_check = false;
            image.ImageUrl = "";
            prod_name.Text = "";
            prod_price.Text = "";
            prod_desc.Text = "";
            Label5.Visible = true;
        }

        protected void delete_check(object sender, EventArgs e)
        {
            Button5.Visible = true;
            Label5.Visible = false;
            Button4.Visible = true;
            Button2.Visible = true;
            Button3.Visible = false;
            DropDownList1.Visible = true;
            image.Visible = true;
            visual_check = true;
            prod_image.Visible = false;
            load_drop_down();
            Button1.Visible = false;
        }

        protected void load_drop_down()
        {
            if (DropDownList1.Items.Count <= 0)
            {
                ProductListGenerator gen = new ProductListGenerator();

                prod_list = gen.getProducts();

                if (prod_list != null)
                {

                    foreach (Product prod in prod_list)
                    {
                        ListItem item = new ListItem();
                        item.Text = prod.getId();
                        DropDownList1.Items.Add(item);
                    }


                }
            }

            DropDownList1.SelectedIndexChanged += selected_click;
        }

        public void selected_click(object sender, EventArgs e)
        {
            string response = ((DropDownList)sender).SelectedValue;

            foreach (Product prod in prod_list)
            {
                if (prod.getId() == response)
                {
                    image.ImageUrl = prod.getIL();
                    prod_name.Text = prod.getProdName();
                    prod_price.Text = prod.getPrice().ToString();
                    prod_desc.Text = prod.getDescription();
                }
            }

        }

        public void Update_Click(object sender, EventArgs e)
        {
            decimal unit_price = decimal.Parse(prod_price.Text);

            if (DropDownList1.SelectedValue != null && prod_cur != null && unit_price != 0 && prod_desc.Text != null && prod_name.Text != null)
            {
                //error.Text = file_location;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update_product";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRODUCT_ID", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@PROD_NAME", prod_name.Text);
                cmd.Parameters.AddWithValue("@PROD_DESC", prod_desc.Text);
                cmd.Parameters.AddWithValue("@PROD_PRICE", unit_price);

                DatabaseConnection con = new DatabaseConnection();
                string response = " ";

                try
                {

                    response = con.insertData(cmd);

                }
                catch (Exception ex)
                {
                    response = "Failed to Update Product to Website, Logout and Login then try again. However if the issue persist contact Tech Support for a assistance.";
                }

                error.Text = response;
            }
        }

        protected void delete_click(object sender, EventArgs e)
        {
            if (get_user() == "ADMIN")
            {
                DatabaseConnection con = new DatabaseConnection();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete_product";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRODUCT_ID", DropDownList1.SelectedValue);

                string response = con.insertData(cmd);

                if (response == "completed")
                {
                    error.Text = "Car Has Been Deleted";
                }
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
    }
}