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
                        maindivs.Controls.Add(webgen.generate(data));
                    }
                }
            }
        }
    }
}