using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAssignment.Client.Checkout
{
    public class Address
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }

        public Address(string firstName, string lastName, string phoneNumber,
                       string addressLine1, string addressLine2, string country,
                       string state, string city, string zipcode)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.addressLine1 = addressLine1;
            this.addressLine2 = addressLine2;
            this.country = country;
            this.state = state;
            this.city = city;
            this.zipcode = zipcode;
        }

        public Address()
        {

        }
    }
}