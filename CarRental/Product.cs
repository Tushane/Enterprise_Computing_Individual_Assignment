using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class Product
    {

        private string id;
        private string image_location;
        private string description;
        private decimal price;


        //setters
        public void setId(string _id)
        {
            this.id = _id;
        }

        public void setIL(string location)
        {
            this.image_location = location;
        }

        public void setDescription(string des)
        {
            this.description = des;
        }

        public void setPrice(decimal amount)
        {
            this.price = amount;
        }


        //getters
        public string getId()
        {
            return this.id;
        }

        public string getIL()
        {
            return this.image_location;
        }

        public string getDescription()
        {
            return this.description;
        }

        public decimal getPrice()
        {
            return this.price;
        }

    }
}