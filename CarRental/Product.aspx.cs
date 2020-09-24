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
                    maindiv.Controls.Add(webgen.generate(data));
                }
            }
        }
    }
}