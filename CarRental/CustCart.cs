using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class CustCart
    {
        private string cust_id;
        private List<SelectedCars> all_cars = new List<SelectedCars>();
       

        //getters
        public string Get_custid()
        {
            return this.cust_id;
        }

        public List<SelectedCars> Get_all_card()
        {
            return this.all_cars;
        }

      

        //setters
        public void set_cust_id(string temp)
        {
            this.cust_id = temp;
        }

        public void Add_car(SelectedCars temp)
        {
            all_cars.Add(temp);
        }

      

    }
}