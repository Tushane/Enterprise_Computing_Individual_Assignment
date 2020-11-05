using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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



            HttpCookie _cart = new HttpCookie("cart_info" + prod_elements[0]);

            _cart["prod_name"] = prod_elements[2];
            _cart["prod_id"] = prod_elements[0];
            _cart["prod_cost"] = prod_elements[1];
            _cart["start_date"] = prod_elements[3];
            _cart["end_date"] = prod_elements[4];
            _cart["amt_days"] = prod_elements[5];

            _cart.Path = path;

            _cart.Expires = DateTime.Now.AddYears(1);


            return _cart;
        }


        public HttpCookie updateCartSize(HttpCookie _cart_size, string path)
        {
            int cur_amount = 1;


            if (_cart_size != null)
            {
                cur_amount = int.Parse(_cart_size["amount"]) + 1;
                _cart_size["amount"] = cur_amount.ToString();
                _cart_size.Path = path;
                _cart_size.Expires = DateTime.Now.AddYears(1);

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


        public string AddProdToCartSQL(cart_info ci, string user_id)
        {
            DatabaseConnection con = new DatabaseConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "ADD_PRODUCTS_TO_CART";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NUM_OF_DAYS", ci.get_num_of_days());
            cmd.Parameters.AddWithValue("@PRODUCT_ID", ci.get_prod_id());
            cmd.Parameters.AddWithValue("@USERID", user_id);
            cmd.Parameters.AddWithValue("@PICKUP_DATE", ci.get_pickup_date_con());

            string response = con.insertData(cmd);

            con.closeSqlData();

            return response;
        }

        public List<cart_info> retrieved_cart_data(string user_id)
        {
            List<cart_info> response = new List<cart_info>();

            string query = "SELECT * FROM DBO.VIEW_CART('" + user_id + "')";

            DatabaseConnection con = new DatabaseConnection();

            SqlDataReader reader = con.readSqlData(query);

            while (reader.Read())
            {
                cart_info temp_data = new cart_info(reader[0].ToString(), reader[1].ToString(), reader[6].ToString(),
                                                    reader[5].ToString(), reader[4].ToString(), reader[7].ToString(), reader[2].ToString(),
                                                    reader[3].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString());

                response.Add(temp_data);

                temp_data = null;
            }


            return response;
        }

        public string update_cart_sql(string order_id, string user_id, string update_value, string update_type)
        {

            DatabaseConnection con = new DatabaseConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE_CART";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CART_ID", order_id);
            cmd.Parameters.AddWithValue("@USER_ID", user_id);
            cmd.Parameters.AddWithValue("@INFO", update_value);
            cmd.Parameters.AddWithValue("@INFO_TYPE", update_type);

            string response = con.insertData(cmd);

            con.closeSqlData();

            return response;

        }

        public string delete_from_cart_sql(string order_id, string user_id)
        {

            DatabaseConnection con = new DatabaseConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "DELETE_FROM_CART";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CART_ID", order_id);
            cmd.Parameters.AddWithValue("@USER_ID", user_id);

            string response = con.insertData(cmd);

            con.closeSqlData();

            return response;

        }

        public string clear_cart_sql(string user_id)
        {

            DatabaseConnection con = new DatabaseConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "CLEAR_CART";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@USER_ID", user_id);

            string response = con.insertData(cmd);

            con.closeSqlData();

            return response;

        }


        public string complete_order(string user_id)
        {

            DatabaseConnection con = new DatabaseConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "COMPLETE_ORDER";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@USERID", user_id);

            string response = con.insertData(cmd);

            con.closeSqlData();

            return response;

        }

    }
}