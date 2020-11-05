using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class cart_info
    {
        private string id;
        private string prod_id;
        private string prod_name;
        private string price;
        private string num_of_days;
        private string currency;
        private string pickup_date;
        private string returned_date;
        private string purchased_date;
        private string image_src; 
        private string prod_desc;
        private string unit_cost;

        public cart_info(string id, string prod_id, string prod_name, string price, string num_of_days, string currency, string pickup_date, string returned_date, string unit_cost, string img, string prod_desc)
        {
            this.id = id;
            this.prod_id = prod_id;
            this.prod_name = prod_name;
            this.price = price;
            this.num_of_days = num_of_days;
            this.currency = currency;
            this.pickup_date = pickup_date;
            this.returned_date = returned_date;
            this.unit_cost = unit_cost;
            this.image_src = img;
            this.prod_desc = prod_desc;
        }

        public cart_info(string prod_id, string num_of_days, string pickup_date)
        {
            this.prod_id = prod_id;
            this.num_of_days = num_of_days;
            this.pickup_date = pickup_date;
        }

        public cart_info(string id, string prod_name, string price, string num_of_days, string currency, DateTime pickup_date, DateTime returned_date, DateTime purchased_date, string image, string prod_desc, string unit_cost)
        {
            this.id = id;
            this.prod_name = prod_name;
            this.price = price;
            this.num_of_days = num_of_days;
            this.currency = currency;
            this.pickup_date = pickup_date.ToString("d");
            this.returned_date = returned_date.ToString("d");
            this.purchased_date = purchased_date.ToString("d");
            this.image_src = image;
            this.prod_desc = prod_desc;
            this.unit_cost = unit_cost;
        }

        public string get_id()
        {
            return this.id;
        }

        public string get_prod_id()
        {
            return this.prod_id;
        }

        public string get_prod_name()
        {
            return this.prod_name;
        }

        public string get_currency()
        {
            return this.currency;
        }

        public string get_pick_up_date()
        {
            return this.pickup_date;
        }

        public string get_returned_date()
        {
            return this.returned_date;
        }

        public string get_unit_cost()
        {
            return this.unit_cost;
        }

        public string get_prod_desc()
        {
            return this.prod_desc;
        }

        public string get_prod_image()
        {
            return this.image_src;
        }

        public string get_price()
        {
            return this.price;
        }

        public string get_num_days()
        {
            return this.num_of_days;
        }

        public int get_num_of_days()
        {
            return int.Parse(this.num_of_days);
        }

        public DateTime get_returned_date_con()
        {
            return DateTime.Parse(this.returned_date);
        }

        public DateTime get_pickup_date_con()
        {
            return DateTime.Parse(this.pickup_date);
        }

        public string get_purchased_date()
        {
            return this.purchased_date;
        }
    }
}