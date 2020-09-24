using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class ProductListGenerator
    {

        private List<Product> data= new List<Product>();
        Product temp = new Product();

        public ProductListGenerator()
        {


            temp.setId("0");
            temp.setPrice(800);
            temp.setDescription("Mustang GT500, A Fast, Realiable, Luxury out on the town Experience.");
            temp.setIL("../images/banner.jpg");
            data.Add(temp);

            temp = null;
            temp = new Product();

            temp.setId("1");
            temp.setPrice(500);
            temp.setDescription("Mustang GT500, A Fast, Realiable, Luxury out on the town Experience.");
            temp.setIL("../images/car1.jpg");
            data.Add(temp);

            temp = null;
            temp = new Product();

            temp.setId("2");
            temp.setPrice(700);
            temp.setDescription("Mustang GT500, A Fast, Realiable, Luxury out on the town Experience.");
            temp.setIL("../images/car2.jpg");
            data.Add(temp);

            temp = null;
            temp = new Product();

            temp.setId("3");
            temp.setPrice(900);
            temp.setDescription("Mustang GT500, A Fast, Realiable, Luxury out on the town Experience.");
            temp.setIL("../images/car3.jpg");
            data.Add(temp);


            temp = null;


        }


        public List<Product> getProducts()
        {
            return data;
        }

    }
}