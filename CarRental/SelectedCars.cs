using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class SelectedCars
    {

        private string prod_ids;
        private DateTime pickup_date;
        private DateTime return_date;


        public string getSelectedProd()
        {
            return this.prod_ids;
        }


        public void set_prod_ids(string temp)
        {
            this.prod_ids = temp;
        }

        public void set_pickup_date(DateTime temp)
        {
            this.pickup_date = temp;
        }

        public void set_return_date(DateTime temp)
        {
            this.return_date = temp;
        }


    }
}