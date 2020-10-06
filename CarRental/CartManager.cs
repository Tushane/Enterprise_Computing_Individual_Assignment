using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CarRental
{
    public class CartManager
    {

       public HttpCookie AddProdToCart(string cart_content, string path, string temp_amt)
        {
            int cur_amount = int.Parse(temp_amt);
            string[] prod_elements = cart_content.Split('|');

           

            HttpCookie _cart = new HttpCookie("cart_info" + cur_amount.ToString());

            _cart["prod_name"] = prod_elements[0];
            _cart["prod_cost"] = prod_elements[1];

            _cart.Path = path;

            _cart.Expires = DateTime.Now.AddMinutes(10);


          return _cart;
        }


        public HttpCookie updateCartSize(HttpCookie _cart_size, string path)
        {
            int cur_amount = 1;


            if (_cart_size != null)
            {
                cur_amount = int.Parse(_cart_size["amount"]) + 1;
                _cart_size["amount"] = cur_amount.ToString();
                _cart_size.Expires = DateTime.Now.AddMinutes(10);

                return _cart_size;

            }
            else
            {
                _cart_size = new HttpCookie("cart_size");

                _cart_size.Values.Add("amount", cur_amount.ToString());

                _cart_size.Path = path;

                _cart_size.Expires = DateTime.Now.AddDays(1);

                return _cart_size;
            }
        }

       

    }
}