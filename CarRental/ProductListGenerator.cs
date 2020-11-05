using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            DatabaseConnection con = new DatabaseConnection();

            SqlDataReader reader = con.readSqlData("SELECT ID, PRODUCT_PRICE, PRODUCT_DESC, PRODUCT_IMAGE, PRODUCT_NAME, CURRENCY FROM DBO.PRODUCT_DISPLAY_INFO");

            while (reader.Read())
            {
                Product temp = new Product();

                temp.setId(reader[0].ToString());
                temp.setPrice(decimal.Parse(reader[1].ToString()));
                temp.setDescription(reader[2].ToString());
                temp.setIL(reader[3].ToString());
                temp.setProdName(reader[4].ToString());
                temp.setProdCurrency(reader[5].ToString());

                data.Add(temp);
            }

            reader.Close();

            con.closeSqlData();
        }


        public List<Product> getProducts()
        {
            return data;
        }

    }
}