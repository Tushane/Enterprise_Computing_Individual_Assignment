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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = " ";
            Decimal unit_price = 0;
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
                unit_price = Decimal.Parse(prod_price.Text);
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
                    file_location = Server.MapPath("~/Pictures/") + prod_image.FileName;
                    file_location1 = "../Pictures/" + prod_image.FileName;
                    prod_image.SaveAs(file_location);


                }
                catch
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
    }
}